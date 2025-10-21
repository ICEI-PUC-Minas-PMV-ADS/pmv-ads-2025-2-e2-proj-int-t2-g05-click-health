// ========= CONFIG =========
const API = '/api/clinico';
let CURRENT_USER = JSON.parse(localStorage.getItem('currentUser') || 'null');
if (!CURRENT_USER) {
  CURRENT_USER = { id:'u_' + Math.random().toString(36).slice(2), name:'Usuário Padrão' };
  localStorage.setItem('currentUser', JSON.stringify(CURRENT_USER));
}
function getCurrentPatient(){ try { return JSON.parse(localStorage.getItem('currentPatient')||'{}'); } catch { return {}; } }
const PATIENT = getCurrentPatient();

// ========= AppBar (zoom/contraste) =========
let zoom = 100;
const zoomIn  = document.getElementById('zoomIn');
const zoomOut = document.getElementById('zoomOut');
const zoomReset = document.getElementById('zoomReset');
function applyZoom(){ if(zoomReset) zoomReset.textContent = `${zoom}%`; document.body.style.zoom = zoom/100; }
zoomIn?.addEventListener('click', ()=>{ zoom=Math.min(200,zoom+10); applyZoom(); });
zoomOut?.addEventListener('click', ()=>{ zoom=Math.max(70, zoom-10); applyZoom(); });
zoomReset?.addEventListener('click', ()=>{ zoom=100; applyZoom(); });
applyZoom();
document.getElementById('hcToggle')?.addEventListener('change', e=> document.documentElement.classList.toggle('hc', e.target.checked));

// ========= Form refs =========
const form = document.getElementById('registroForm');
const msg  = document.getElementById('formMsg');
const dataHora = document.getElementById('dataHora');
const tipoRegistro = document.getElementById('tipoRegistro');
const titulo = document.getElementById('titulo');
const descricao = document.getElementById('descricao');
const inputFiles = document.getElementById('anexos');
const filesList  = document.getElementById('filesList');
const anexosExistentes = document.getElementById('anexosExistentes');

// inicializa data
if (dataHora) {
  const now = new Date();
  now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
  dataHora.value = now.toISOString().slice(0,16);
}

// ========= Condicionais (imagem/lab) =========
const imagemBox = document.getElementById('imagemBox');
const imgTipo = document.getElementById('imgTipo');
const imgRegiao = document.getElementById('imgRegiao');
const imgData = document.getElementById('imgData');

const labBox = document.getElementById('labBox');
const labList = document.getElementById('labList');
const labAddBtn = document.getElementById('labAdd');
const labRowTpl = document.getElementById('labRowTpl');
const labData = document.getElementById('labData');

function show(el){ el?.classList.remove('hide'); el?.setAttribute('aria-hidden','false'); }
function hide(el){ el?.classList.add('hide'); el?.setAttribute('aria-hidden','true'); }
function onTipoRegistroChanged(){
  const v = tipoRegistro?.value;
  if (v === 'Resultado de Exame de Imagem'){ show(imagemBox); hide(labBox); }
  else if (v === 'Resultado de Exame Laboratorial'){ show(labBox); hide(imagemBox); ensureLabRow(); }
  else { hide(imagemBox); hide(labBox); }
}
tipoRegistro?.addEventListener('change', onTipoRegistroChanged);
function ensureLabRow(){ if(!labList.querySelector('.lab-row')) createLabRow(); }

function createLabRow(){
  const node = labRowTpl.content.firstElementChild.cloneNode(true);
  const sel = node.querySelector('.labNome');
  const badge = node.querySelector('.labUnidadeBadge');
  const outroWrap = node.querySelector('.labOutroWrap');
  const valor = node.querySelector('.labValor');
  const del = node.querySelector('.labDel');
  sel.addEventListener('change', ()=>{
    if (sel.value==='Outros'){ outroWrap.classList.remove('hide'); badge.textContent='Unidade: (defina)'; }
    else { outroWrap.classList.add('hide'); badge.textContent='Unidade: —'; }
  });
  del.addEventListener('click', ()=> node.remove());
  labList.appendChild(node);
}
labAddBtn?.addEventListener('click', ()=> createLabRow());

// upload preview
function renderFiles(){
  filesList.innerHTML='';
  Array.from(inputFiles.files||[]).forEach(f=>{
    const li=document.createElement('li'); li.textContent=`${f.name} (${Math.round(f.size/1024)} KB)`;
    filesList.appendChild(li);
  });
}
const drop = document.getElementById('drop');
drop?.addEventListener('click', ()=> inputFiles.click());
drop?.addEventListener('dragover', e=>{e.preventDefault(); drop.classList.add('drag');});
drop?.addEventListener('dragleave', ()=> drop.classList.remove('drag'));
drop?.addEventListener('drop', e=>{e.preventDefault(); drop.classList.remove('drag'); inputFiles.files=e.dataTransfer.files; renderFiles();});
inputFiles?.addEventListener('change', renderFiles);

// ========= Edição =========
const params = new URLSearchParams(location.search);
const editId = params.get('id');
let anexosAntigos = [];

async function loadRegistro(){
  if(!editId) return;
  const r = await fetch(`${API}/${editId}`);
  if(!r.ok) return alert('Erro ao carregar registro');
  const it = await r.json();
  if (it.autor_id && CURRENT_USER.id !== it.autor_id){
    alert('Você não tem permissão para editar este registro.');
    location.href = '/historicoclinico.html';
    return;
  }

  dataHora.value = new Date(it.datahora).toISOString().slice(0,16);
  tipoRegistro.value = it.tipo;
  titulo.value = it.titulo;
  descricao.value = it.descricao || '';
  onTipoRegistroChanged();

  // popular extras em edição (opcional)
  if (it.extras?.categoria === 'imagem'){
    imgTipo.value = it.extras.tipo || '';
    imgRegiao.value = it.extras.regiao || '';
    if (it.extras.dataRealizacao) imgData.value = it.extras.dataRealizacao;
  }
  if (it.extras?.categoria === 'laboratorial'){
    if (it.extras.dataColeta) labData.value = it.extras.dataColeta;
    (it.extras.exames||[]).forEach(ex=>{
      createLabRow();
      const row = labList.lastElementChild;
      row.querySelector('.labNome').value = 'Outros'; // para garantir campos livres
      row.querySelector('.labOutroWrap').classList.remove('hide');
      row.querySelector('.labNomeOutro').value = ex.exame || '';
      row.querySelector('.labUnidadeOutro').value = ex.unidade || '';
      row.querySelector('.labValor').value = ex.valor ?? '';
      row.querySelector('.labUnidadeBadge').textContent = `Unidade: ${ex.unidade||'—'}`;
    });
  }

  anexosAntigos = it.anexos || [];
  renderAnexosExistentes();
}
function renderAnexosExistentes(){
  anexosExistentes.innerHTML='';
  if(!anexosAntigos.length) return;
  const lbl=document.createElement('div'); lbl.textContent='Anexos já salvos (desmarque para remover):';
  lbl.style.margin='8px 0'; lbl.style.fontSize='12px'; anexosExistentes.appendChild(lbl);
  anexosAntigos.forEach(ax=>{
    const row=document.createElement('label'); row.style.display='flex'; row.style.alignItems='center'; row.style.gap='8px';
    const chk=document.createElement('input'); chk.type='checkbox'; chk.checked=true; chk.dataset.filename=ax.filename;
    const sp=document.createElement('span'); sp.textContent=ax.originalname;
    row.appendChild(chk); row.appendChild(sp); anexosExistentes.appendChild(row);
  });
}

// ========= Salvar =========
form?.addEventListener('submit', async (e)=>{
  e.preventDefault();
  msg.textContent = 'Salvando…';

  // montar extras conforme tipo
  let extras = null;
  const tipo = tipoRegistro.value;

  if (tipo === 'Resultado de Exame de Imagem'){
    const contraste = (document.querySelector('input[name="contraste"]:checked')?.value) || '';
    extras = {
      categoria:'imagem',
      tipo: imgTipo.value || '',
      regiao: imgRegiao.value || '',
      contraste,
      dataRealizacao: imgData.value || ''
    };
  } else if (tipo === 'Resultado de Exame Laboratorial'){
    const rows = [...document.querySelectorAll('#labList .lab-row')];
    const exames = rows.map(r=>{
      const opt = r.querySelector('.labNome').value;
      const outro = r.querySelector('.labNomeOutro')?.value || '';
      const und  = r.querySelector('.labUnidadeOutro')?.value || '';
      const val  = r.querySelector('.labValor')?.value ?? '';
      return { exame: (opt==='Outros'? outro : opt), unidade: (opt==='Outros'? und : '—'), valor: val };
    }).filter(x => x.exame);
    extras = { categoria:'laboratorial', dataColeta: labData?.value || '', exames };
  }

  const fd = new FormData();
  fd.set('datahora', dataHora.value);
  fd.set('tipo', tipo);
  fd.set('titulo', titulo.value);
  fd.set('descricao', descricao.value);
  fd.set('autor_id', CURRENT_USER.id);
  fd.set('autor_nome', CURRENT_USER.name);
  fd.set('paciente_id', PATIENT.id || '');
  fd.set('paciente_nome', PATIENT.name || '');
  if (extras) fd.set('extras', JSON.stringify(extras));
  Array.from(inputFiles.files||[]).forEach(f=> fd.append('anexos', f, f.name));

  if (editId){
    const keep = Array.from(anexosExistentes.querySelectorAll('input[type=checkbox]'))
      .filter(c=>c.checked).map(c=>c.dataset.filename);
    fd.set('keep', JSON.stringify(keep));
  }

  try{
    const resp = await fetch(editId?`${API}/${editId}`:API, { method: editId?'PUT':'POST', body: fd });
    if(!resp.ok) throw new Error(await resp.text());
    msg.textContent = 'Registro salvo com sucesso.';
    setTimeout(()=> location.href='/historicoclinico.html', 700);
  }catch(err){
    console.error(err);
    msg.textContent = 'Erro: ' + err.message;
  }
});

if (editId) loadRegistro();
