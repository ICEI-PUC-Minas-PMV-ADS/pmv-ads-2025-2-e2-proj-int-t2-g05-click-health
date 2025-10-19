const express = require('express');
const cors = require('cors');
const path = require('path');
const fs = require('fs');
const multer = require('multer');
const db = require('./db');

const app = express();
app.use(cors());
app.use(express.json());

// Servir frontend (toda a pasta codigo_fonte)
app.use(express.static(path.join(__dirname, '..')));

// Uploads
const UPLOAD_DIR = path.join(__dirname, '..', 'uploads');
if (!fs.existsSync(UPLOAD_DIR)) fs.mkdirSync(UPLOAD_DIR, { recursive: true });

const storage = multer.diskStorage({
  destination: (_, __, cb) => cb(null, UPLOAD_DIR),
  filename: (_, file, cb) => {
    const ts = Date.now();
    const safe = file.originalname.replace(/[^\w.\-]+/g, '_');
    cb(null, `${ts}-${safe}`);
  }
});
const upload = multer({ storage });

// Logs de erros não tratados (útil em dev)
process.on('unhandledRejection', err => console.error('❌ unhandledRejection:', err));
process.on('uncaughtException',  err => console.error('❌ uncaughtException:',  err));

(async () => {
  await db.open();

  // ---------------- REGISTROS CLÍNICOS ----------------

  // Criar registro (com upload)
  app.post('/api/registros', upload.array('anexos'), async (req, res) => {
    try {
      const { dataHora, tipoRegistro, titulo, descricao, extras } = req.body;
      if (!dataHora || !tipoRegistro || !titulo || !descricao) {
        return res.status(400).send('Campos obrigatórios ausentes.');
      }

      const extras_json = extras || '{}';
      const ins = db.run(
        `INSERT INTO registros (dataHora, tipoRegistro, titulo, descricao, extras_json)
         VALUES (?, ?, ?, ?, ?)`,
        [dataHora, tipoRegistro, titulo, descricao, extras_json]
      );
      const registroId = ins.lastId;

      if (req.files?.length) {
        for (const f of req.files) {
          db.run(
            `INSERT INTO anexos (registro_id, filename, originalname, mimetype, size_bytes, path_rel)
             VALUES (?, ?, ?, ?, ?, ?)`,
            [registroId, f.filename, f.originalname, f.mimetype, f.size,
             path.join('uploads', f.filename).replace(/\\/g, '/')]
          );
        }
      }

      await db.flush();
      res.status(201).json({ id: registroId });
    } catch (e) {
      console.error(e);
      res.status(500).send('Erro ao salvar registro.');
    }
  });

  // Listar registros (já com anexos embutidos)
  app.get('/api/registros', (_, res) => {
    try {
      const regs = db.all(
        `SELECT id, dataHora, tipoRegistro, titulo, descricao, extras_json, criado_em
           FROM registros
          ORDER BY datetime(dataHora) DESC, id DESC
          LIMIT 500`
      );

      const ids = regs.map(r => r.id);
      let anexosByReg = {};
      if (ids.length) {
        const marks = ids.map(()=>'?').join(',');
        const allAnex = db.all(
          `SELECT registro_id, originalname, mimetype, size_bytes, path_rel, id
             FROM anexos
            WHERE registro_id IN (${marks})
            ORDER BY id ASC`,
          ids
        );
        anexosByReg = allAnex.reduce((acc, a) => {
          (acc[a.registro_id] ||= []).push(a);
          return acc;
        }, {});
      }

      const out = regs.map(r => ({
        ...r,
        extrasJson: r.extras_json,       // compat com o front
        anexos: anexosByReg[r.id] || []  // lista de anexos
      }));

      res.json(out);
    } catch (e) {
      console.error(e);
      res.status(500).send('Erro ao listar registros.');
    }
  });

  // Obter um registro (detalhe + anexos)
  app.get('/api/registros/:id', (req, res) => {
    try {
      const id = Number(req.params.id);
      const reg = db.get(
        `SELECT id, dataHora, tipoRegistro, titulo, descricao, extras_json, criado_em
           FROM registros WHERE id=?`,
        [id]
      );
      if (!reg) return res.status(404).send('Registro não encontrado.');

      const anexos = db.all(
        `SELECT id, originalname, mimetype, size_bytes, path_rel, criado_em
           FROM anexos WHERE registro_id=? ORDER BY id ASC`,
        [id]
      );
      res.json({ ...reg, anexos });
    } catch (e) {
      console.error(e);
      res.status(500).send('Erro ao obter registro.');
    }
  });

  // ---------------- ATIVIDADES (feed) ----------------

  // Listar atividades
  app.get('/api/atividades', (_, res) => {
    try {
      const acts = db.all(
        `SELECT id, dataHora, titulo, descricao, createdById, createdByName, criado_em
           FROM atividades
          ORDER BY datetime(dataHora) DESC, id DESC
          LIMIT 500`
      );
      res.json(acts);
    } catch (e) {
      console.error(e);
      res.status(500).send('Erro ao listar atividades.');
    }
  });

  // Criar atividade (opcional, se usar upload em atividades descomente a tabela no schema)
  app.post('/api/atividades', upload.array('anexos'), async (req, res) => {
    try {
      const { dataHora, titulo, descricao, createdById, createdByName } = req.body;
      if (!dataHora || !titulo || !descricao) return res.status(400).send('Campos obrigatórios ausentes.');

      const ins = db.run(
        `INSERT INTO atividades (dataHora, titulo, descricao, createdById, createdByName)
         VALUES (?, ?, ?, ?, ?)`,
        [dataHora, titulo, descricao, createdById || null, createdByName || null]
      );

      // se quiser anexar arquivos em atividades, inclua aqui o insert em atividades_anexos
      await db.flush();
      res.status(201).json({ id: ins.lastId });
    } catch (e) {
      console.error(e);
      res.status(500).send('Erro ao salvar atividade.');
    }
  });

  // arquivos públicos
  app.use('/uploads', express.static(UPLOAD_DIR));

  // start
  const PORT = process.env.PORT || 3000;
  app.listen(PORT, () => console.log(`API rodando em http://localhost:${PORT}`));
})();
