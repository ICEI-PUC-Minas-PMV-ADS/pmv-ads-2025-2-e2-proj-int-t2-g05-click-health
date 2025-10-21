// api/server.js
const express = require('express');
const path = require('path');
const fs = require('fs');
const sqlite3 = require('sqlite3').verbose();
const multer = require('multer');
const cors = require('cors');

const app = express();
const PORT = process.env.PORT || 3000;

// paths base
const BASE_DIR = path.resolve(__dirname, '..');
const DB_PATH  = path.join(BASE_DIR, 'registroclinico.db');
const UP_DIR   = path.join(BASE_DIR, 'uploads');
const STATIC_DIR = BASE_DIR;

if (!fs.existsSync(UP_DIR)) fs.mkdirSync(UP_DIR, { recursive: true });

app.use(cors());
app.use(express.json({ limit: '10mb' }));
app.use(express.urlencoded({ extended: true }));
app.use(express.static(STATIC_DIR));
app.use('/uploads', express.static(UP_DIR));

// uploads
const storage = multer.diskStorage({
  destination: (_, __, cb) => cb(null, UP_DIR),
  filename: (_, file, cb) => {
    const safe = file.originalname.normalize('NFD').replace(/[^a-zA-Z0-9.\-_]/g, '_');
    cb(null, `${Date.now()}__${safe}`);
  }
});
const upload = multer({ storage });

// db
const db = new sqlite3.Database(DB_PATH);
db.serialize(() => {
  db.run(`
    CREATE TABLE IF NOT EXISTS registro_clinico (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      created_at   TEXT NOT NULL,
      datahora     TEXT NOT NULL,
      tipo         TEXT NOT NULL,
      titulo       TEXT NOT NULL,
      descricao    TEXT,
      anexos_json  TEXT,
      extras_json  TEXT,            -- << NOVO: imagem/lab
      autor_id     TEXT,
      autor_nome   TEXT,
      paciente_id  TEXT,
      paciente_nome TEXT
    )
  `);
  // migração defensiva (se tabela já existia sem a coluna)
  db.run(`ALTER TABLE registro_clinico ADD COLUMN extras_json TEXT`, ()=>{});
  console.log('DB pronto em', DB_PATH);
});

function nowISO(){ return new Date().toISOString(); }

// criar
app.post('/api/clinico', upload.array('anexos', 10), (req, res) => {
  try{
    const { datahora, tipo, titulo, descricao,
            autor_id, autor_nome, paciente_id, paciente_nome, extras } = req.body;

    const anexos = (req.files||[]).map(f=>({
      filename:f.filename, originalname:f.originalname, mimetype:f.mimetype, size:f.size,
      url:`/uploads/${f.filename}`
    }));

    const stmt = db.prepare(`
      INSERT INTO registro_clinico
        (created_at, datahora, tipo, titulo, descricao, anexos_json, extras_json,
         autor_id, autor_nome, paciente_id, paciente_nome)
      VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
    `);
    stmt.run(
      nowISO(),
      datahora || nowISO(),
      tipo || '',
      titulo || '',
      descricao || '',
      JSON.stringify(anexos),
      extras || null,
      autor_id || null,
      autor_nome || null,
      paciente_id || null,
      paciente_nome || null,
      function(err){
        if (err) return res.status(500).json({ error: err.message });
        res.json({ id:this.lastID });
      }
    );
  }catch(e){ res.status(500).json({ error:e.message }); }
});

// listar (mais recente primeiro)
app.get('/api/clinico', (req, res) => {
  const { paciente_id } = req.query;
  const where = paciente_id ? ` WHERE paciente_id = ?` : '';
  const order = ` ORDER BY datetime(datahora) DESC, id DESC`;

  db.all(`
    SELECT * FROM registro_clinico
    ${where} ${order}
  `, paciente_id ? [paciente_id] : [], (err, rows)=>{
    if (err) return res.status(500).json({ error: err.message });
    const mapped = (rows||[]).map(r=>({
      ...r,
      anexos: r.anexos_json ? JSON.parse(r.anexos_json) : [],
      extras: r.extras_json ? JSON.parse(r.extras_json) : null
    }));
    res.json(mapped);
  });
});

// obter 1
app.get('/api/clinico/:id', (req, res)=>{
  db.get(`SELECT * FROM registro_clinico WHERE id = ?`, [req.params.id], (err, row)=>{
    if (err) return res.status(500).json({ error: err.message });
    if (!row) return res.status(404).json({ error:'Registro não encontrado' });
    row.anexos = row.anexos_json ? JSON.parse(row.anexos_json) : [];
    row.extras = row.extras_json ? JSON.parse(row.extras_json) : null;
    res.json(row);
  });
});

// atualizar
app.put('/api/clinico/:id', upload.array('anexos', 10), (req, res)=>{
  const id = req.params.id;
  db.get(`SELECT * FROM registro_clinico WHERE id = ?`, [id], (err, old)=>{
    if (err) return res.status(500).json({ error: err.message });
    if (!old) return res.status(404).json({ error:'Registro não encontrado' });

    const { datahora, tipo, titulo, descricao, autor_id, autor_nome, extras } = req.body;

    let keep = [];
    try { keep = JSON.parse(req.body.keep || '[]'); } catch {}

    const antigos = old.anexos_json ? JSON.parse(old.anexos_json) : [];
    const mantidos = antigos.filter(a=> keep.includes(a.filename));
    const novos = (req.files||[]).map(f=>({
      filename:f.filename, originalname:f.originalname, mimetype:f.mimetype, size:f.size,
      url:`/uploads/${f.filename}`
    }));
    const anexos = [...mantidos, ...novos];

    const stmt = db.prepare(`
      UPDATE registro_clinico
      SET datahora = ?, tipo = ?, titulo = ?, descricao = ?, anexos_json = ?, extras_json = ?,
          autor_id = ?, autor_nome = ?
      WHERE id = ?
    `);
    stmt.run(
      datahora || old.datahora,
      tipo || old.tipo,
      titulo || old.titulo,
      (descricao ?? old.descricao),
      JSON.stringify(anexos),
      (extras || old.extras_json),
      autor_id || old.autor_id,
      autor_nome || old.autor_nome,
      id,
      function(e){
        if (e) return res.status(500).json({ error:e.message });
        res.json({ ok:true, id });
      }
    );
  });
});

// start
app.listen(PORT, ()=>{
  console.log(`API rodando em http://localhost:${PORT}`);
  console.log('DB:', DB_PATH);
  console.log('Uploads:', UP_DIR);
  console.log('Servindo estáticos de:', STATIC_DIR);
});
