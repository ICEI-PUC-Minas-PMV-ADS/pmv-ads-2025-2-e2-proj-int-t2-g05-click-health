// ===== util =====
function getCurrentUser(){
  try { return JSON.parse(localStorage.getItem('currentUser') || '{}'); }
  catch { return {}; }
}
function getCurrentPatient(){
  // defina em algum lugar: localStorage.setItem('currentPatient', JSON.stringify({ name:'Maria Clara Andrade' }))
  try { return JSON.parse(localStorage.getItem('currentPatient') || '{}'); }
  catch { return {}; }
}
const user = getCurrentUser();
const patient = getCurrentPatient();

// cabeçalho
document.getElementById('patientName').textContent = patient.name || 'Paciente';

document.getElementById('btnNovo')?.addEventListener('click', ()=>{
  window.location.href = './registroclinico.html';
});

function fmtDateLong(d){
  return d.toLocaleDateString('pt-BR', { day:'2-digit', month:'long', year:'numeric' });
}
function fmtTime(d){
  return d.toLocaleTimeString('pt-BR', { hour:'2-digit', minute:'2-digit' });
}

// ===== fetch (registros clínicos + atividades) =====
async function getJSON(url){
  try {
    const r = await fetch(url);
    if (!r.ok) throw 0;
    return await r.json();
  } catch { return []; }
}

async function loadData(){
  const [clin, acts] = await Promise.all([
    getJSON('/api/registros'),       // nosso endpoint
    getJSON('/api/atividades')       // se não existir, vem []
  ]);

  // unifica
  const items = [];

  // registros clínicos
  (clin || []).forEach(r=>{
    const dt = new Date(r.dataHora || r.DataHora || r.data || r.createdAt);
    const extras = typeof r.extrasJson === 'string' ? safeJSON(r.extrasJson) : (r.extrasJson || {});
    const isImage = (r.tipoRegistro || '').toLowerCase().includes('imagem');
    const isLab = (r.tipoRegistro || '').toLowerCase().includes('laboratorial');

    items.push({
      id: r.id || r.Id,
      source: 'clinico',
      datetime: dt,
      title: r.titulo || r.Titulo || 'Registro Clínico',
      byName: r.createdByName || r.CreatedByName || '',
      byId:   r.createdById   || r.CreatedById   || '',
      description: r.descricao || r.Descricao || '',
      extras,
      badge: isImage ? 'Imagem' : (isLab ? 'Laboratório' : 'Anotação')
    });
  });

  // atividades (se houver)
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

    group.forEach(i=>{
      gEl.appendChild(renderEntry(i));
    });

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
    editBtn.addEventListener('click', ()=>{
      // se sua página de edição aceita ?id=, redireciona
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
  // simples: alterna a descrição (colapsar/expandir)
  const body = card.querySelector('.body');
  if (!body) return;
  if (body.style.display === 'none') body.style.display = '';
  else body.style.display = 'none';
}

function makePill(text){
  const d = document.createElement('div');
  d.className = 'pill';
  d.textContent = text;
  return d;
}

function safeJSON(s){
  try { return JSON.parse(s); } catch { return {}; }
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

// inicialização
loadData();
