// ===== util =====
function getCurrentUser(){
  try { return JSON.parse(localStorage.getItem('currentUser') || '{}'); }
  catch { return {}; }
}
function getCurrentPatient(){
  try { return JSON.parse(localStorage.getItem('currentPatient') || '{}'); }
  catch { return {}; }
}
const user = getCurrentUser();
const patient = getCurrentPatient();

// cabeçalho
document.getElementById('patientName').textContent = patient.name || 'Paciente';
document.getElementById('btnNovo')?.addEventListener('click', ()=> {
  window.location.href = './registroclinico.html';
});

function fmtDateLong(d){
  return d.toLocaleDateString('pt-BR', { day:'2-digit', month:'long', year:'numeric' });
}
function fmtTime(d){
  return d.toLocaleTimeString('pt-BR', { hour:'2-digit', minute:'2-digit' });
}
function safeJSON(s){ try { return JSON.parse(s); } catch { return {}; } }

// ===== fetch (registros clínicos + atividades) =====
async function getJSON(url){
  try {
    const r = await fetch(url);
    if (!r.ok) throw 0;
    return await r.json();
  } catch { return []; }
}

function normAnexo(a){
  // normaliza campos vindos da API
  return {
    name: a.originalname || a.filename || 'arquivo',
    href: a.path_rel ? ('/' + a.path_rel.replace(/^\/?/, '')) : '',
    type: a.mimetype || '',
    size: a.size_bytes || 0
  };
}

async function loadData(){
  const [clin, acts] = await Promise.all([
    getJSON('/api/registros'),   // já vem com anexos
    getJSON('/api/atividades')   // pode estar vazio
  ]);

  const items = [];

  // registros clínicos
  (clin || []).forEach(r=>{
    const dt = new Date(r.dataHora || r.DataHora || r.data || r.createdAt);
    const extras = typeof r.extrasJson === 'string' ? safeJSON(r.extrasJson) : (r.extrasJson || {});
    const isImage = (r.tipoRegistro || '').toLowerCase().includes('imagem');
    const isLab   = (r.tipoRegistro || '').toLowerCase().includes('laboratorial');

    items.push({
      id: r.id || r.Id,
      source: 'clinico',
      datetime: dt,
      title: r.titulo || r.Titulo || 'Registro Clínico',
      byName: r.createdByName || r.CreatedByName || '',
      byId:   r.createdById   || r.CreatedById   || '',
      description: r.descricao || r.Descricao || '',
      extras,
      anexos: (r.anexos || []).map(normAnexo),
      badge: isImage ? 'Imagem' : (isLab ? 'Laboratório' : 'Anotação')
    });
  });

  // atividades (se/ quando usar)
  (acts || []).forEach(a=>{
    const dt = new Date(a.dataHora || a.DataHora || a.data || a.createdAt);
    items.push({
      id: a.id || a.Id,
      source: 'atividade',
      datetime: dt,
      title: a.titulo || a.Titulo || 'Atividade',
      byName: a.createdByName || a.CreatedByName || '',
      byId:   a.createdById   || a.CreatedById   || '',
      description: a.descricao || a.Descricao || '',
      extras: {},
      anexos: (a.anexos || []).map(normAnexo),
      badge: 'Atividade'
    });
  });

  // ordena decrescente
  items.sort((a,b)=> b.datetime - a.datetime);

  renderTimeline(items);
}

// ===== render =====
function renderTimeline(items){
  const el = document.getElementById('timeline');
  el.innerHTML = '';
  document.getElementById('countInfo').textContent = `${items.length} registros`;

  // agrupar por dia
  const groups = {};
  items.forEach(i=>{
    const key = i.datetime.toISOString().slice(0,10);
    (groups[key] ||= []).push(i);
  });

  Object.keys(groups).sort((a,b)=> b.localeCompare(a)).forEach(key=>{
    const group = groups[key];
    const d = new Date(key);
    const gEl = document.createElement('div');
    gEl.className = 'date-group';

    const label = document.createElement('div');
    label.className = 'date-label';
    label.textContent = fmtDateLong(d);
    gEl.appendChild(label);

    group.forEach(i=> gEl.appendChild(renderEntry(i)) );
    el.appendChild(gEl);
  });
}

function renderEntry(i){
  const wrap = document.createElement('div');
  wrap.className = 'entry';

  const dot = document.createElement('div');
  dot.className = 'dot' + (i.source === 'atividade' ? ' green' : '');
  wrap.appendChild(dot);

  const card = document.createElement('div');
  card.className = 'card';

  const titleRow = document.createElement('div');
  titleRow.className = 'title-row';

  const title = document.createElement('div');
  title.className = 'title';
  title.textContent = i.title;
  titleRow.appendChild(title);

  const actions = document.createElement('div');
  actions.className = 'actions';

  const viewBtn = document.createElement('button');
  viewBtn.className = 'ghost';
  viewBtn.textContent = 'Ver';
  viewBtn.addEventListener('click', ()=> toggleDetails(card));
  actions.appendChild(viewBtn);

  if (i.source === 'clinico' && user?.id && i.byId === user.id) {
    const editBtn = document.createElement('button');
    editBtn.className = 'ghost';
    editBtn.textContent = 'Editar';
    editBtn.addEventListener('click', ()=> {
      window.location.href = `./registroclinico.html?id=${i.id}`;
    });
    actions.appendChild(editBtn);
  }
  titleRow.appendChild(actions);
  card.appendChild(titleRow);

  const meta = document.createElement('div');
  meta.className = 'meta';
  meta.textContent = `${i.badge} • ${fmtTime(i.datetime)} • por ${i.byName || '—'}`;
  card.appendChild(meta);

  const body = document.createElement('div');
  body.className = 'body';
  body.textContent = i.description || '';
  card.appendChild(body);

  // anexos (preview + download)
  if (i.anexos && i.anexos.length) {
    const files = document.createElement('div');
    files.className = 'files-grid';
    i.anexos.forEach(ax => files.appendChild(renderAttachment(ax)));
    card.appendChild(files);
  }

  const foot = document.createElement('div');
  foot.className = 'foot';
  foot.appendChild(makePill(i.badge));
  if (i.source === 'clinico') {
    const p = document.createElement('div');
    p.className = 'pill';
    p.textContent = extrasPreview(i.extras);
    foot.appendChild(p);
  }
  card.appendChild(foot);

  wrap.appendChild(card);
  return wrap;
}

function toggleDetails(card){
  const body = card.querySelector('.body');
  if (!body) return;
  body.style.display = (body.style.display === 'none') ? '' : 'none';
}

function makePill(text){
  const d = document.createElement('div');
  d.className = 'pill';
  d.textContent = text;
  return d;
}

function extrasPreview(extras){
  if (!extras || typeof extras !== 'object') return '';
  if (extras.categoria === 'imagem'){
    const p1 = extras.tipo ? extras.tipo : '';
    const p2 = extras.regiao ? ` — ${extras.regiao}` : '';
    const p3 = extras.contraste ? ` — ${extras.contraste}` : '';
    const p4 = extras.dataRealizacao ? ` • ${extras.dataRealizacao}` : '';
    return (p1 + p2 + p3 + p4).trim();
  }
  if (extras.categoria === 'laboratorial'){
    const arr = extras.exames || [];
    const s = arr.slice(0,3).map(x => `${x.exame}: ${x.valor}${x.unidade ? ' ' + x.unidade : ''}`).join(' • ');
    return s || 'Exames';
  }
  return '';
}

// ===== helpers de anexos =====
function renderAttachment(ax){
  const box = document.createElement('div');
  box.className = 'file-card';

  const isImg = ax.type?.startsWith('image/');
  const isPDF = ax.type === 'application/pdf';

  if (isImg) {
    const a = document.createElement('a');
    a.href = ax.href;
    a.target = '_blank';
    a.download = ax.name || '';
    const img = document.createElement('img');
    img.src = ax.href;
    img.alt = ax.name || 'Imagem';
    img.loading = 'lazy';
    a.appendChild(img);
    box.appendChild(a);
  } else if (isPDF) {
    const icon = document.createElement('div');
    icon.className = 'file-icon';
    icon.textContent = 'PDF';
    box.appendChild(icon);

    const name = document.createElement('div');
    name.className = 'file-name';
    name.textContent = ax.name || 'arquivo.pdf';
    box.appendChild(name);

    const row = document.createElement('div');
    row.className = 'file-actions';
    const a1 = document.createElement('a');
    a1.href = ax.href; a1.target = '_blank'; a1.textContent = 'Abrir';
    const a2 = document.createElement('a');
    a2.href = ax.href; a2.download = ax.name || 'arquivo.pdf'; a2.textContent = 'Download';
    row.appendChild(a1); row.appendChild(a2);
    box.appendChild(row);
  } else {
    const icon = document.createElement('div');
    icon.className = 'file-icon';
    icon.textContent = 'ARQ';
    box.appendChild(icon);

    const name = document.createElement('div');
    name.className = 'file-name';
    name.textContent = ax.name || 'arquivo';
    box.appendChild(name);

    const a2 = document.createElement('a');
    a2.href = ax.href; a2.download = ax.name || 'arquivo'; a2.textContent = 'Download';
    a2.className = 'file-download';
    box.appendChild(a2);
  }

  return box;
}

// inicialização
loadData();
