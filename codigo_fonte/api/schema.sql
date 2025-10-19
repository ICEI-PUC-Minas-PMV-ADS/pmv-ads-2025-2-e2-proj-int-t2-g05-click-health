CREATE TABLE IF NOT EXISTS registros (
  id            INTEGER PRIMARY KEY AUTOINCREMENT,
  dataHora      TEXT NOT NULL,
  tipoRegistro  TEXT NOT NULL,
  titulo        TEXT NOT NULL,
  descricao     TEXT NOT NULL,
  extras_json   TEXT NOT NULL,
  criado_em     TEXT NOT NULL DEFAULT (datetime('now'))
);

CREATE TABLE IF NOT EXISTS anexos (
  id           INTEGER PRIMARY KEY AUTOINCREMENT,
  registro_id  INTEGER NOT NULL,
  filename     TEXT NOT NULL,
  originalname TEXT NOT NULL,
  mimetype     TEXT NOT NULL,
  size_bytes   INTEGER NOT NULL,
  path_rel     TEXT NOT NULL,
  criado_em    TEXT NOT NULL DEFAULT (datetime('now'))
);

CREATE INDEX IF NOT EXISTS idx_registros_dataHora ON registros (dataHora DESC);
CREATE INDEX IF NOT EXISTS idx_anexos_registro ON anexos (registro_id);

-- ===== Atividades (feed geral) =====
CREATE TABLE IF NOT EXISTS atividades (
  id            INTEGER PRIMARY KEY AUTOINCREMENT,
  dataHora      TEXT NOT NULL,
  titulo        TEXT NOT NULL,
  descricao     TEXT NOT NULL,
  createdById   TEXT,
  createdByName TEXT,
  criado_em     TEXT NOT NULL DEFAULT (datetime('now'))
);

-- (opcional) anexos de atividades — se quiser anexar coisas no registro de atividades
CREATE TABLE IF NOT EXISTS atividades_anexos (
  id           INTEGER PRIMARY KEY AUTOINCREMENT,
  atividade_id INTEGER NOT NULL,
  filename     TEXT NOT NULL,
  originalname TEXT NOT NULL,
  mimetype     TEXT NOT NULL,
  size_bytes   INTEGER NOT NULL,
  path_rel     TEXT NOT NULL,
  criado_em    TEXT NOT NULL DEFAULT (datetime('now')),
  FOREIGN KEY(atividade_id) REFERENCES atividades(id) ON DELETE CASCADE
);
CREATE INDEX IF NOT EXISTS idx_atividades_dataHora ON atividades (dataHora DESC);
CREATE INDEX IF NOT EXISTS idx_atividades_anexos ON atividades_anexos (atividade_id);
-- ===== Registro de Atividades (feed geral) =====
CREATE TABLE IF NOT EXISTS atividades (
  id            INTEGER PRIMARY KEY AUTOINCREMENT,
  dataHora      TEXT NOT NULL,
  titulo        TEXT NOT NULL,
  descricao     TEXT NOT NULL,
  createdById   TEXT,
  createdByName TEXT,
  criado_em     TEXT NOT NULL DEFAULT (datetime('now'))
);

--  anexos de atividades — 
CREATE TABLE IF NOT EXISTS atividades_anexos (
  id           INTEGER PRIMARY KEY AUTOINCREMENT,
  atividade_id INTEGER NOT NULL,
  filename     TEXT NOT NULL,
  originalname TEXT NOT NULL,
  mimetype     TEXT NOT NULL,
  size_bytes   INTEGER NOT NULL,
  path_rel     TEXT NOT NULL,
  criado_em    TEXT NOT NULL DEFAULT (datetime('now')),
  FOREIGN KEY(atividade_id) REFERENCES atividades(id) ON DELETE CASCADE
);
CREATE INDEX IF NOT EXISTS idx_atividades_dataHora ON atividades (dataHora DESC);
CREATE INDEX IF NOT EXISTS idx_atividades_anexos ON atividades_anexos (atividade_id);
