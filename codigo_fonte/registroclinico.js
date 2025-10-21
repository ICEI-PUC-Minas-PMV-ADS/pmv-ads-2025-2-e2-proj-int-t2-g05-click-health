// ========= API BASE & USER =========
const API_BASE = location.port === '3000' ? '' : 'http://localhost:3000';

let CURRENT_USER = JSON.parse(localStorage.getItem('currentUser') || 'null');
if (!CURRENT_USER) {
  CURRENT_USER = { id: 'u_' + Math.random().toString(36).slice(2), name: 'Usuário Padrão' };
  localStorage.setItem('currentUser', JSON.stringify(CURRENT_USER));
}
const headersAuth = { 'X-User-Id': CURRENT_USER.id, 'X-User-Name': CURRENT_USER.name };

// ========= ZOOM & ACESSIBILIDADE =========
const zoomIn  = document.getElementById('zoomIn');
const zoomOut = document.getElementById('zoomOut');
const zoomReset = document.getElementById('zoomReset');
let zoom = 100;
const supportsZoomProp = !!document.body && ('zoom' in document.body.style);
function applyZoom(){ 
  if(zoomReset) zoomReset.textContent = `${zoom}%`; 
  if(supportsZoomProp){document.body.style.zoom=(zoom/100);} 
  else {document.documentElement.style.setProperty('--ui-scale',(zoom/100));}
}
zoomIn?.addEventListener('click',()=>{zoom=Math.min(200,zoom+10);applyZoom();});
zoomOut?.addEventListener('click',()=>{zoom=Math.max(70,zoom-10);applyZoom();});
zoomReset?.addEventListener('click',()=>{zoom=100;applyZoom();});
document.getElementById('hcToggle')?.addEventListener('change', (e)=> document.documentElement.classList.toggle('hc', e.target.checked));
applyZoom();

// ==== ELEMENTOS BASE DO FORM ====
const form = document.getElementById('registroForm');
const formMsg = document.getElementById('formMsg');
const inputFiles = document.getElementById('anexos');
const filesList = document.getElementById('filesList');

const dataHora = document.getElementById('dataHora');
if (dataHora) { 
  const now=new Date(); 
  now.setMinutes(now.getMinutes()-now.getTimezoneOffset()); 
  dataHora.value = now.toISOString().slice(0,16); 
}

// ==== LÓGICA CONDICIONAL ====
const tipoRegistro = document.getElementById('tipoRegistro');
const imagemBox = document.getElementById('imagemBox');
const labBox = document.getElementById('labBox');

const imgTipo = document.getElementById('imgTipo');
const imgTipoOutro = document.getElementById('imgTipoOutro');
const imgRegiao = document.getElementById('imgRegiao');
const imgRegiaoOutra = document.getElementById('imgRegiaoOutra');
const imgData = document.getElementById('imgData');

const labList = document.getElementById('labList');
const labAddBtn = document.getElementById('labAdd');
const labRowTpl = document.getElementById('labRowTpl');
const labData = document.getElementById('labData');

function toggleRequired(el,on){ if(!el) return; on?el.setAttribute('required',''):el.removeAttribute('required'); }
function setImagemRequired(on){ toggleRequired(imgTipo,on); toggleRequired(imgRegiao,on); toggleRequired(imgData,on); document.querySelectorAll('input[name="contraste"]').forEach(r=> r.required=on); }
function setLabRequired(on){ if(labData) on?labData.setAttribute('required',''):labData.removeAttribute('required'); }
function show(el){ el?.classList.remove('hide'); el?.setAttribute('aria-hidden','false'); }
function hide(el){ el?.classList.add('hide'); el?.setAttribute('aria-hidden','true'); }

function onTipoRegistroChanged(){
  const v = tipoRegistro?.value;
  if (v === 'Resultado de Exame de Imagem') {
    show(imagemBox); setImagemRequired(true);
    hide(labBox); setLabRequired(false);
  } else if (v === 'Resultado de Exame Laboratorial') {
    show(labBox); setLabRequired(true);
    hide(imagemBox); setImagemRequired(false);
    ensureAtLeastOneLabRow();
  } else {
    hide(imagemBox); setImagemRequired(false);
    hide(labBox); setLabRequired(false);
  }
}
tipoRegistro?.addEventListener('change', onTipoRegistroChanged);

imgTipo?.addEventListener('change', ()=>{ 
  if(imgTipo.value==='Outros'){ show(imgTipoOutro); imgTipoOutro.setAttribute('required',''); } 
  else { hide(imgTipoOutro); imgTipoOutro.removeAttribute('required'); imgTipoOutro.value=''; }
});
imgRegiao?.addEventListener('change', ()=>{ 
  if(imgRegiao.value==='Outros'){ show(imgRegiaoOutra); imgRegiaoOutra.setAttribute('required',''); } 
  else { hide(imgRegiaoOutra); imgRegiaoOutra.removeAttribute('required'); imgRegiaoOutra.value=''; }
});

// ======= LAB TESTS =======
const LAB_TESTS = {
  'Hemoglobina': {unit:'g/dL', kind:'number'}, 'Hematócrito': {unit:'%', kind:'number'},
  'Leucócitos': {unit:'10^3/µL', kind:'number'}, 'Plaquetas': {unit:'10^3/µL', kind:'number'},
  'Glicose': {unit:'mg/dL', kind:'number'}, 'Ureia': {unit:'mg/dL', kind:'number'},
  'Creatinina': {unit:'mg/dL', kind:'number'}, 'Sódio': {unit:'mEq/L', kind:'number'},
  'Potássio': {unit:'mEq/L', kind:'number'}, 'Cloro': {unit:'mEq/L', kind:'number'},
  'Colesterol Total': {unit:'mg/dL', kind:'number'}, 'HDL-C': {unit:'mg/dL', kind:'number'},
  'LDL-C': {unit:'mg/dL', kind:'number'}, 'Triglicerídeos': {unit:'mg/dL', kind:'number'},
  'Hemoglobina A1c': {unit:'%', kind:'number'}, 'TSH': {unit:'µIU/mL', kind:'number'},
  'T4 Livre': {unit:'ng/dL', kind:'number'}, 'PCR (Proteína C Reativa)': {unit:'mg/L', kind:'number'},
  'D-dímero': {unit:'ng/mL', kind:'number'}, 'Ferritina': {unit:'ng/mL', kind:'number'},
  'Vitamina D (25-OH)': {unit:'ng/mL', kind:'number'}, 'Cálcio': {unit:'mg/dL', kind:'number'},
  'Magnésio': {unit:'mg/dL', kind:'number'}, 'Ácido Úrico': {unit:'mg/dL', kind:'number'},
  'TGO (AST)': {unit:'U/L', kind:'number'}, 'TGP (ALT)': {unit:'U/L', kind:'number'},
  'GGT': {unit:'U/L', kind:'number'}, 'Fosfatase Alcalina': {unit:'U/L', kind:'number'},
  'Bilirrubina Total': {unit:'mg/dL', kind:'number'}, 'Albumina': {unit:'g/dL', kind:'number'},
  'Lactato': {unit:'mmol/L', kind:'number'}, 'Troponina': {unit:'ng/L', kind:'number'},
  'BNP/NT-proBNP': {unit:'pg/mL', kind:'number'}, 'PSA Total': {unit:'ng/mL', kind:'number'},
  'Proteinúria (urina)': {unit:'mg/dL', kind:'number'}, 'EAS (urina rotina) – qualitativo': {unit:'(qualitativo)', kind:'text'}
};

function createLabRow(prefill=null){
  const node = labRowTpl.content.firstElementChild.cloneNode(true);
  const sel = node.querySelector('.labNome');
  const unidadeBadge = node.querySelector('.labUnidadeBadge');
  const outroWrap = node.querySelector('.labOutroWrap');
  const nomeOutro = node.querySelector('.labNomeOutro');
  const unidadeOutro = node.querySelector('.labUnidadeOutro');
  const valor = node.querySelector('.labValor');
  const del = node.querySelector('.labDel');

  function applyMeta(){
    const opt = sel.value;
    if (opt === 'Outros'){
      outroWrap.classList.remove('hide');
      nomeOutro.setAttribute('required',''); unidadeOutro.setAttribute('required','');
      unidadeBadge.textContent = 'Unidade: (defina acima)';
      valor.type='number'; valor.placeholder='Informe o valor';
      return;
    }
    outroWrap.classList.add('hide');
    nomeOutro.removeAttribute('required'); unidadeOutro.removeAttribute('required');
    const meta = LAB_TESTS[opt];
    if (meta){
      unidadeBadge.textContent = `Unidade: ${meta.unit}`;
      valor.type = meta.kind === 'text' ? 'text' : 'number';
      valor.placeholder = meta.kind === 'text' ? 'Digite o resultado qualitativo' : 'Informe o valor';
    } else {
      unidadeBadge.textContent = 'Unidade: —';
      valor.type='number'; valor.placeholder='Informe o valor';
    }
  }

  sel.addEventListener('change', applyMeta);
  del.addEventListener('click', ()=> node.remove());
  applyMeta();

  if (prefill){
    if (prefill.nome && [...sel.options].some(o=>o.text===prefill.nome)) sel.value = prefill.nome;
    if (prefill.unidade) unidadeBadge.textContent = `Unidade: ${prefill.unidade}`;
    if (prefill.valor!=null) valor.value = prefill.valor;
    applyMeta();
  }

  sel.setAttribute('required','');
  valor.setAttribute('required','');
  labList.appendChild(node);
}
function ensureAtLeastOneLabRow(){ if(!labList.querySelector('.lab-row')) createLabRow(); }
labAddBtn?.addEventListener('click', ()=> createLabRow());
onTipoRegistroChanged();

// ===== Upload (drag & drop)
const drop = document.getElementById('drop');
function renderFiles(){
  filesList.innerHTML=''; 
  Array.from(inputFiles.files||[]).forEach((f)=>{
    const li=document.createElement('li');
    li.textContent=`${f.name} (${Math.round(f.size/1024)} KB)`;
    filesList.appendChild(li);
  });
}
drop?.addEventListener('click', ()=> inputFiles?.click());
drop?.addEventListener('dragover', (e)=>{ e.preventDefault(); drop.classList.add('drag'); });
drop?.addEventListener('dragleave', ()=> drop.classList.remove('drag'));
drop?.addEventListener('drop', (e)=>{ e.preventDefault(); drop.classList.remove('drag'); inputFiles.files=e.dataTransfer.files; renderFiles(); });
inputFiles?.addEventListener('change', renderFiles);

// ===== Submit
form?.addEventListener('submit', async (e)=>{
  e.preventDefault();
  formMsg.textContent = 'Salvando…';

  const tipo = tipoRegistro?.value;
  let extras = {};

  if (tipo === 'Resultado de Exame de Imagem') {
    const contraste = (document.querySelector('input[name="contraste"]:checked')?.value) || '';
    const tipoEx = (imgTipo.value === 'Outros' ? imgTipoOutro.value : imgTipo.value);
    const regiao = (imgRegiao.value === 'Outros' ? imgRegiaoOutra.value : imgRegiao.value);
    extras = { categoria:'imagem', tipo:tipoEx, regiao, contraste, dataRealizacao: imgData.value };
  } else if (tipo === 'Resultado de Exame Laboratorial') {
    const rows=[...document.querySelectorAll('#labList .lab-row')];
    if(!rows.length){ formMsg.textContent='Inclua ao menos um exame laboratorial.'; return; }
    const exames = rows.map(row=>{
      const sel=row.querySelector('.labNome');
      const valor=row.querySelector('.labValor')?.value??'';
      const nomeOutro=row.querySelector('.labNomeOutro')?.value??'';
      const unidadeOutro=row.querySelector('.labUnidadeOutro')?.value??'';
      const opt=sel.value; const meta=LAB_TESTS[opt];
      const nome=(opt==='Outros')?nomeOutro:opt;
      const unidade=(opt==='Outros')?unidadeOutro:(meta?.unit||'');
      const qualitativo = meta?.kind === 'text' || (opt==='Outros' && isNaN(Number(valor)));
      return { exame:nome, unidade, valor, qualitativo };
    });
    extras = { categoria:'laboratorial', dataColeta: labData?.value || '', exames };
  }

  // ===== LOGS DE DEPURAÇÃO =====
  console.log('API_BASE:', API_BASE, location.href);
  console.log('Arquivos selecionados:', inputFiles?.files?.length, inputFiles?.files);

  const fd = new FormData();
  fd.append('dataHora', dataHora?.value || '');
  fd.append('tipoRegistro', tipo || '');
  fd.append('titulo', document.getElementById('titulo')?.value || '');
  fd.append('descricao', document.getElementById('descricao')?.value || '');
  fd.append('extras', JSON.stringify(extras));
  Array.from(inputFiles?.files||[]).forEach(f => fd.append('anexos', f, f.name));
  console.log('FormData keys:', [...fd.keys()]);

  // ===== FETCH =====
  try {
    const resp = await fetch(`${API_BASE}/api/registros`, {
      method: 'POST',
      body: fd,
      headers: { 'X-User-Id': CURRENT_USER.id, 'X-User-Name': CURRENT_USER.name },
      credentials: 'include'
    });
    if(!resp.ok) throw new Error(await resp.text());
    formMsg.textContent = 'Registro salvo com sucesso.';
    form.reset(); filesList.innerHTML=''; onTipoRegistroChanged();
  } catch(err) {
    formMsg.textContent = 'Erro: ' + (err?.message || err);
    console.error('Erro ao salvar registro:', err);
  }
});
