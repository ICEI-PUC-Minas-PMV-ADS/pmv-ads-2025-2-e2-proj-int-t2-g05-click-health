const fs = require('fs');
const path = require('path');
const initSqlJs = require('sql.js');

const DB_PATH = path.join(__dirname, '..', 'registroclinico.db');

let SQL, db, saving = Promise.resolve();

async function open() {
  if (!SQL) {
    SQL = await initSqlJs({
      locateFile: f => path.join(require.resolve('sql.js/dist/sql-wasm.js'), '..', f)
    });
  }
  if (fs.existsSync(DB_PATH)) {
    const buf = fs.readFileSync(DB_PATH);
    db = new SQL.Database(new Uint8Array(buf));
  } else {
    db = new SQL.Database();
  }

  const schema = fs.readFileSync(path.join(__dirname, 'schema.sql'), 'utf8');
  db.run(schema);
}

function flush() {
  saving = saving.then(() => new Promise((resolve, reject) => {
    try {
      const data = db.export();
      fs.writeFile(DB_PATH, Buffer.from(data), err => err ? reject(err) : resolve());
    } catch (e) { reject(e); }
  }));
  return saving;
}

function all(sql, params = []) {
  const stmt = db.prepare(sql);
  stmt.bind(params);
  const rows = [];
  while (stmt.step()) rows.push(stmt.getAsObject());
  stmt.free();
  return rows;
}

function get(sql, params = []) {
  return all(sql, params)[0] || null;
}

function run(sql, params = []) {
  const stmt = db.prepare(sql);
  stmt.run(params);
  const lastId = db.exec('SELECT last_insert_rowid() AS id')[0]?.values?.[0]?.[0] ?? null;
  stmt.free();
  return { lastId };
}

module.exports = { open, flush, all, get, run };
