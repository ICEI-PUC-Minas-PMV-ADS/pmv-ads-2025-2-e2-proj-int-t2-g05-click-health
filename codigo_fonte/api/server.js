// codigo_fonte/api/server.js
const express  = require('express');
const cors     = require('cors');
const path     = require('path');
const fs       = require('fs');
const multer   = require('multer');
const sqlite3  = require('sqlite3').verbose();

const app = express();
const PORT = process.env.PORT || 3000;

/* ===================== CORS (único) ===================== */
app.use(cors({
  origin: true, // aceita localhost:3000/5500 etc. em dev
  credentials: true,
  methods: ['GET','POST','PUT','DELETE','OPTIONS'],
  allowedHeaders: ['Content-Type','X-User-Id','X-User-Name'],
}));

/* ===================== LOG BÁSICO ===================== */
app.use((req, _res, next) => {
  console.log(`[${new Date().toISOString()}] ${req.method} ${req.url}`);
  next();
});

/* ===================== BODY PARSER ===================== */
app.use(express.json());

/* ===================== BANCO ===================== */
const DB_FILE = path.join(__dirname, '..', 'registroclinico.db');
const db = new sqlite3.Database(DB_FILE);

db.serialize(() => {
  db.run(`CREATE TABLE IF NOT EXISTS registros (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    dataHora TEXT,
    tipoRegistro TEXT,
    titulo TEXT,
    descricao TEXT,
    extras_json TEXT,
    created_at TEXT DEFAULT (datetime('now','localtime')),
    created_by_id TEXT,
    created_by_name TEXT
  )`);

  // tabela padrão (se já existir com outro schema, manteremos e detectaremos as colunas)
  db.run(`CREATE TABLE IF NOT EXISTS anexos (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    registro_id INTEGER NOT NULL,
    file_name TEXT,
    mime_type TEXT,
    size_bytes INTEGER,
    storage_path TEXT,
    created_at TEXT DEFAULT (datetime('now','localtime')),
    FOREIGN KEY (registro_id) REFERENCES registros(id) ON DELETE CASCADE
  )`);

  db.run(`PRAGMA foreign_keys = ON`);

  // Migrações best-effort (ignoram erro se já existir)
  db.run(`ALTER TABLE registros ADD COLUMN created_by_id TEXT`, () => {});
  db.run(`ALTER TABLE registros ADD COLUMN created_by_name TEXT`, () => {});
});

/* ====== DETECTOR DE SCHEMA (Opção A+ robusta) ====== */
let ANEXOS_INFO = []; // rows do PRAGMA table_info(anexos)
function refreshAnexosInfo(cb) {
  db.all(`PRAGMA table_info(anexos)`, (err, rows) => {
    if (err || !rows) {
      console.warn('Não foi possível inspecionar colunas de anexos; usando defaults do CREATE TABLE.');
      ANEXOS_INFO = [];
      return cb && cb();
    }
    ANEXOS_INFO = rows; // [{cid,name,type,notnull,dflt_value,pk}, ...]
    console.log('Colunas de anexos detectadas:', ANEXOS_INFO.map(r => `${r.name}${r.notnull?'(NN)':''}`).join(', '));
    cb && cb();
  });
}
refreshAnexosInfo();

// helper: verifica se a coluna existe
function hasCol(name) {
  return ANEXOS_INFO.some(c => c.name === name);
}
// helper: coluna existe e é NOT NULL
function isNotNull(name) {
  const c = ANEXOS_INFO.find(x => x.name === name);
  return c ? !!c.notnull : false;
}

// monta o insert dinâmico para a tabela "anexos"
function buildAnexoInsert(registroId, f) {
  // valores disponíveis
  const orig   = f.originalname || path.basename(f.path);
  const fname  = path.basename(f.path);
  const mime   = f.mimetype || '';
  const size   = typeof f.size === 'number' ? f.size : null;
  const fpath  = f.path;

  // colher colunas que vamos preencher
  const cols = ['registro_id'];
  const vals = [registroId];

  // originalname (se existir)
  if (hasCol('originalname')) {
    cols.push('originalname'); vals.push(orig);
  }
  // nome de arquivo salvo (file_name ou filename)
  if (hasCol('file_name')) { cols.push('file_name'); vals.push(fname); }
  else if (hasCol('filename')) { cols.push('filename'); vals.push(fname); }

  // mimetype
  if (hasCol('mime_type')) { cols.push('mime_type'); vals.push(mime); }
  else if (hasCol('mimetype')) { cols.push('mimetype'); vals.push(mime); }

  // size
  if (hasCol('size_bytes')) { cols.push('size_bytes'); vals.push(size); }
  else if (hasCol('size')) { cols.push('size'); vals.push(size); }

  // path
  if (hasCol('storage_path')) { cols.push('storage_path'); vals.push(fpath); }
  else if (hasCol('path_rel')) { cols.push('path_rel'); vals.push(fpath); }
  else if (hasCol('path')) { cols.push('path'); vals.push(fpath); }

  // Garantir colunas NOT NULL (além das já incluídas) com melhores valores
  // (não colocamos id/created_at/registro_id)
  for (const col of ANEXOS_INFO) {
    if (!col.notnull) continue;
    if (col.name === 'id' || col.name === 'created_at') continue;
    if (col.name === 'registro_id') continue;
    if (!cols.includes(col.name)) {
      // coluna NOT NULL não preenchida: tentar valor apropriado
      if (col.name === 'originalname') {
        cols.push('originalname'); vals.push(orig);
      } else if (col.name === 'file_name' || col.name === 'filename') {
        cols.push(col.name); vals.push(fname);
      } else if (col.name === 'mime_type' || col.name === 'mimetype') {
        cols.push(col.name); vals.push(mime);
      } else if (col.name === 'size_bytes' || col.name === 'size') {
        cols.push(col.name); vals.push(size);
      } else if (col.name === 'storage_path' || col.name === 'path_rel' || col.name === 'path') {
        cols.push(col.name); vals.push(fpath);
      } else {
        // fallback genérico para NOT NULL desconhecido
        cols.push(col.name); vals.push(String(orig || fname || ''));
      }
    }
  }

  const placeholders = cols.map(() => '?').join(', ');
  const sql = `INSERT INTO anexos (${cols.join(', ')}) VALUES (${placeholders})`;
  return { sql, vals };
}

/* ===================== UPLOADS ===================== */
const UPLOAD_DIR = path.join(__dirname, '..', 'uploads');
if (!fs.existsSync(UPLOAD_DIR)) fs.mkdirSync(UPLOAD_DIR, { recursive: true });

const storage = multer.diskStorage({
  destination: (_req, _file, cb) => cb(null, UPLOAD_DIR),
  filename:   (_req, file, cb) => {
    const safe = file.originalname.replace(/[^\w.\-]+/g, '_');
    cb(null, `${Date.now()}_${Math.round(Math.random()*1e9)}_${safe}`);
  }
});

// (Opcional) limite por arquivo (ex.: 20MB). Ajuste se quiser.
// const upload = multer({ storage, limits: { fileSize: 20 * 1024 * 1024 } });
const upload = multer({ storage });

/* ===================== ROTAS API ===================== */

// Criar registro + anexos
app.post('/api/registros', upload.array('anexos'), (req, res) => {
  try {
    const { dataHora, tipoRegistro, titulo, descricao, extras } = req.body;
    const userId   = req.header('X-User-Id')   || null;
    const userName = req.header('X-User-Name') || null;

    console.log('POST /api/registros payload:', {
      userId, files: (req.files||[]).length, bodyKeys: Object.keys(req.body||{})
    });

    if (!dataHora || !tipoRegistro || !titulo || !descricao) {
      return res.status(400).send('Campos obrigatórios ausentes.');
    }

    const extras_json = extras || '{}';

    db.run(
      `INSERT INTO registros (dataHora, tipoRegistro, titulo, descricao, extras_json, created_by_id, created_by_name)
       VALUES (?, ?, ?, ?, ?, ?, ?)`,
      [dataHora, tipoRegistro, titulo, descricao, extras_json, userId, userName],
      function (err) {
        if (err) {
          console.error('ERRO insert registro:', err);
          return res.status(500).send('Falha ao inserir registro.');
        }

        const registroId = this.lastID;

        if (!req.files || !req.files.length) {
          console.log(`✔ Registro ${registroId} salvo sem anexos`);
          return res.status(201).json({ ok: true, id: registroId, anexos: 0 });
        }

        const stmtList = [];
        try {
          for (const f of req.files) {
            console.log('  ↳ anexo:', f.originalname, '→', f.path, `(${f.size} bytes)`);
            const { sql, vals } = buildAnexoInsert(registroId, f);
            stmtList.push({ sql, vals });
          }
        } catch (buildErr) {
          console.error('ERRO ao montar INSERT de anexo:', buildErr);
          return res.status(500).send('Falha ao preparar anexos.');
        }

        // Executa todos os INSERTs
        let done = 0;
        for (const st of stmtList) {
          db.run(st.sql, st.vals, function (e2) {
            if (e2) {
              console.error('ERRO insert anexos:', e2, 'SQL:', st.sql, 'VALS:', st.vals);
              return res.status(500).send('Falha ao inserir anexos.');
            }
            if (++done === stmtList.length) {
              console.log(`✔ Registro ${registroId} salvo com ${req.files.length} anexo(s)`);
              res.status(201).json({ ok: true, id: registroId, anexos: req.files.length });
            }
          });
        }
      }
    );
  } catch (e) {
    console.error('ERRO inesperado /api/registros:', e);
    res.status(500).send('Erro inesperado no upload.');
  }
});

// Listar registros (com anexos e canEdit)
app.get('/api/registros', (req, res) => {
  const userId = req.header('X-User-Id') || null;

  db.all(
    `SELECT * FROM registros
      ORDER BY datetime(dataHora) DESC, id DESC`,
    (err, regs) => {
      if (err) return res.status(500).send('Erro ao listar registros.');
      const ids = regs.map(r => r.id);
      if (!ids.length) return res.json([]);

      const marks = ids.map(()=>'?').join(',');
      db.all(
        `SELECT * FROM anexos
          WHERE registro_id IN (${marks})
          ORDER BY id ASC`,
        ids,
        (e, anexos) => {
          if (e) return res.status(500).send('Erro ao listar anexos.');
          const by = {};
          for (const a of anexos) (by[a.registro_id] ||= []).push(a);
          const out = regs.map(r => ({
            ...r,
            anexos: by[r.id] || [],
            canEdit: !!(userId && r.created_by_id === userId)
          }));
          res.json(out);
        }
      );
    }
  );
});

// Detalhe
app.get('/api/registros/:id', (req, res) => {
  const id = Number(req.params.id);
  db.get(`SELECT * FROM registros WHERE id=?`, [id], (e, reg) => {
    if (e) return res.status(500).send('Erro ao obter registro.');
    if (!reg) return res.status(404).send('Registro não encontrado.');
    db.all(`SELECT * FROM anexos WHERE registro_id=? ORDER BY id ASC`, [id], (e2, anexos) => {
      if (e2) return res.status(500).send('Erro ao listar anexos.');
      res.json({ ...reg, anexos });
    });
  });
});

// Atualizar (somente autor)
app.put('/api/registros/:id', (req, res) => {
  const userId = req.header('X-User-Id') || null;
  const id = Number(req.params.id);
  const { dataHora, tipoRegistro, titulo, descricao, extras_json } = req.body;

  db.get(`SELECT created_by_id FROM registros WHERE id=?`, [id], (e, row) => {
    if (e) return res.status(500).send('Erro ao buscar registro.');
    if (!row) return res.status(404).send('Registro não encontrado.');
    if (!userId || row.created_by_id !== userId) return res.status(403).send('Sem permissão para editar.');

    db.run(
      `UPDATE registros
         SET dataHora=?, tipoRegistro=?, titulo=?, descricao=?, extras_json=?
       WHERE id=?`,
      [dataHora, tipoRegistro, titulo, descricao, extras_json || '{}', id],
      function (err2) {
        if (err2) return res.status(500).send('Erro ao atualizar registro.');
        res.json({ ok: true, updated: this.changes });
      }
    );
  });
});

// Add anexos (somente autor) — usa inserção dinâmica
app.post('/api/registros/:id/anexos', upload.array('anexos'), (req, res) => {
  const userId = req.header('X-User-Id') || null;
  const id = Number(req.params.id);

  db.get(`SELECT created_by_id FROM registros WHERE id=?`, [id], (e, row) => {
    if (e) return res.status(500).send('Erro ao buscar registro.');
    if (!row) return res.status(404).send('Registro não encontrado.');
    if (!userId || row.created_by_id !== userId) return res.status(403).send('Sem permissão.');

    if (!req.files || !req.files.length) return res.status(400).send('Nenhum arquivo enviado.');

    const inserts = [];
    try {
      for (const f of req.files) {
        const { sql, vals } = buildAnexoInsert(id, f);
        inserts.push({ sql, vals });
      }
    } catch (buildErr) {
      console.error('ERRO ao montar INSERT de anexo (add):', buildErr);
      return res.status(500).send('Falha ao preparar anexos.');
    }

    let done = 0;
    for (const st of inserts) {
      db.run(st.sql, st.vals, function (err2) {
        if (err2) {
          console.error('ERRO insert anexos (add):', err2, 'SQL:', st.sql, 'VALS:', st.vals);
          return res.status(500).send('Erro ao salvar anexos.');
        }
        if (++done === inserts.length) {
          res.json({ ok: true, count: inserts.length });
        }
      });
    }
  });
});

// Remover anexo (somente autor) — tenta remover arquivo físico usando storage_path/path_rel/path
app.delete('/api/anexos/:anexoId', (req, res) => {
  const userId = req.header('X-User-Id') || null;
  const anexoId = Number(req.params.anexoId);

  db.get(
    `SELECT a.*, r.created_by_id
       FROM anexos a
       JOIN registros r ON r.id=a.registro_id
      WHERE a.id=?`,
    [anexoId],
    (e, row) => {
      if (e) return res.status(500).send('Erro ao buscar anexo.');
      if (!row) return res.status(404).send('Anexo não encontrado.');
      if (!userId || row.created_by_id !== userId) return res.status(403).send('Sem permissão.');

      const filePath = row.storage_path || row.path_rel || row.path;
      try { if (filePath) fs.unlinkSync(filePath); } catch {}
      db.run(`DELETE FROM anexos WHERE id=?`, [anexoId], function (err2) {
        if (err2) return res.status(500).send('Erro ao apagar anexo.');
        res.json({ ok: true, deleted: this.changes });
      });
    }
  );
});

/* ===================== ESTÁTICOS ===================== */
const STATIC_DIR = path.join(__dirname, '..');   // codigo_fonte/
app.use(express.static(STATIC_DIR));             // /style.css, /images/..., *.html, *.js
app.use('/uploads', express.static(UPLOAD_DIR)); // anexos

/* ===================== START ===================== */
app.listen(PORT, () => {
  console.log(`API rodando em http://localhost:${PORT}`);
  console.log(`DB: ${DB_FILE}`);
  console.log(`Uploads: ${UPLOAD_DIR}`);
  console.log(`Servindo estáticos de: ${STATIC_DIR}`);
});
