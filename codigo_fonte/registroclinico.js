// ========= ZOOM DE LEITURA =========
const zoomIn  = document.getElementById('zoomIn');
const zoomOut = document.getElementById('zoomOut');
const zoomReset = document.getElementById('zoomReset');

let zoom = 100;
const supportsZoomProp = !!document.body && ('zoom' in document.body.style);

function applyZoom() {
  if (zoomReset) zoomReset.textContent = `${zoom}%`;
  if (supportsZoomProp) {
    document.body.style.zoom = (zoom / 100);
  } else {
    // Firefox fallback: requer no CSS:
    // :root{--ui-scale:1}
    // .page-card{transform:scale(var(--ui-scale));transform-origin:top center}
    document.documentElement.style.setProperty('--ui-scale', (zoom / 100));
  }
}
zoomIn?.addEventListener('click',  () => { zoom = Math.min(200, zoom + 10); applyZoom(); });
zoomOut?.addEventListener('click', () => { zoom = Math.max( 70, zoom - 10); applyZoom(); });
zoomReset?.addEventListener('click', () => { zoom = 100; applyZoom(); });

document.addEventListener('keydown', (e)=>{
  if(e.altKey && (e.key==='=' || e.key==='+')) { e.preventDefault(); zoomIn?.click(); }
  if(e.altKey && e.key==='-')                        { e.preventDefault(); zoomOut?.click(); }
  if(e.altKey && e.key==='0')                        { e.preventDefault(); zoomReset?.click(); }
});
applyZoom();

// Alto contraste
const hcToggle = document.getElementById('hcToggle');
hcToggle?.addEventListener('change', ()=> document.documentElement.classList.toggle('hc', hcToggle.checked));

// ==== ELEMENTOS BASE DO FORM ====
const form = document.getElementById('registroForm');
const formMsg = document.getElementById('formMsg');
const inputFiles = document.getElementById('anexos');
const filesList = document.getElementById('filesList');

// Preencher datetime com agora
const dataHora = document.getElementById('dataHora');
if (dataHora) {
  const now = new Date();
  now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
  dataHora.value = now.toISOString().slice(0,16);
}

// ========= DITADO =========
const dictBtn = document.getElementById('dictBtn');
const dictStatus = document.getElementById('dictStatus');
const descricao = document.getElementById('descricao');

let rec; let ditando = false;

if (('webkitSpeechRecognition' in window) || ('SpeechRecognition' in window)) {
  const SR = window.SpeechRecognition || window.webkitSpeechRecognition;
  rec = new SR();
  rec.lang = 'pt-BR';
  rec.interimResults = true;
  rec.continuous = true;

  rec.onresult = (evt) => {
    let interim = '';
    for (let i = evt.resultIndex; i < evt.results.length; i++) {
      const r = evt.results[i];
      const texto = r[0].transcript.trim();
      if (r.isFinal) {
        descricao.value = (descricao.value + ' ' + texto).replace(/\s+/g,' ').trim();
      } else {
        interim += texto + ' ';
      }
    }
    if (dictStatus) dictStatus.textContent = interim ? `… ${interim.trim()}` : '';
  };

  rec.onstart = () => { if (dictStatus) dictStatus.textContent = 'Ditando…'; dictBtn?.setAttribute('aria-pressed','true'); ditando=true; };
  rec.onend   = () => { if (dictStatus) dictStatus.textContent = 'Ditado pausado.'; dictBtn?.setAttribute('aria-pressed','false'); ditando=false; };

  dictBtn?.addEventListener('click', () => { ditando ? rec.stop() : rec.start(); });
  document.addEventListener('keydown', (e)=>{ if(e.altKey && (e.key==='d' || e.key==='D')) { e.preventDefault(); dictBtn?.click(); }});
} else {
  if (dictBtn) dictBtn.disabled = true;
  if (dictStatus) dictStatus.textContent = 'Ditado não suportado neste navegador.';
}

// ===== Unidades dos exames (numérico ou qualitativo) =====
const LAB_TESTS = {
  'Hemoglobina': {unit:'g/dL', kind:'number'},
  'Hematócrito': {unit:'%', kind:'number'},
  'Leucócitos': {unit:'10^3/µL', kind:'number'},
  'Plaquetas': {unit:'10^3/µL', kind:'number'},
  'Glicose': {unit:'mg/dL', kind:'number'},
  'Ureia': {unit:'mg/dL', kind:'number'},
  'Creatinina': {unit:'mg/dL', kind:'number'},
  'Sódio': {unit:'mEq/L', kind:'number'},
  'Potássio': {unit:'mEq/L', kind:'number'},
  'Cloro': {unit:'mEq/L', kind:'number'},
  'Colesterol Total': {unit:'mg/dL', kind:'number'},
  'HDL-C': {unit:'mg/dL', kind:'number'},
  'LDL-C': {unit:'mg/dL', kind:'number'},
  'Triglicerídeos': {unit:'mg/dL', kind:'number'},
  'Hemoglobina A1c': {unit:'%', kind:'number'},
  'TSH': {unit:'µIU/mL', kind:'number'},
  'T4 Livre': {unit:'ng/dL', kind:'number'},
  'PCR (Proteína C Reativa)': {unit:'mg/L', kind:'number'},
  'D-dímero': {unit:'ng/mL', kind:'number'},
  'Ferritina': {unit:'ng/mL', kind:'number'},
  'Vitamina D (25-OH)': {unit:'ng/mL', kind:'number'},
  'Cálcio': {unit:'mg/dL', kind:'number'},
  'Magnésio': {unit:'mg/dL', kind:'number'},
  'Ácido Úrico': {unit:'mg/dL', kind:'number'},
  'TGO (AST)': {unit:'U/L', kind:'number'},
  'TGP (ALT)': {unit:'U/L', kind:'number'},
  'GGT': {unit:'U/L', kind:'number'},
  'Fosfatase Alcalina': {unit:'U/L', kind:'number'},
  'Bilirrubina Total': {unit:'mg/dL', kind:'number'},
  'Albumina': {unit:'g/dL', kind:'number'},
  'Lactato': {unit:'mmol/L', kind:'number'},
  'Troponina': {unit:'ng/L', kind:'number'},
  'BNP/NT-proBNP': {unit:'pg/mL', kind:'number'},
  'PSA Total': {unit:'ng/mL', kind:'number'},
  'Proteinúria (urina)': {unit:'mg/dL', kind:'number'},
  'EAS (urina rotina) – qualitativo': {unit:'(qualitativo)', kind:'text'}
};

// ==== LÓGICA CONDICIONAL (imagem / laboratório) ====
const tipoRegistro = document.getElementById('tipoRegistro');
const imagemBox = document.getElementById('imagemBox');
const labBox = document.getElementById('labBox');

// imagem
const imgTipo = document.getElementById('imgTipo');
const imgTipoOutro = document.getElementById('imgTipoOutro');
const imgRegiao = document.getElementById('imgRegiao');
const imgRegiaoOutra = document.getElementById('imgRegiaoOutra');
const imgData = document.getElementById('imgData');

function toggleRequired(el, on){ if(!el) return; on ? el.setAttribute('required','') : el.removeAttribute('required'); }
function setImagemRequired(on){
  toggleRequired(imgTipo, on);
  toggleRequired(imgRegiao, on);
  toggleRequired(imgData, on);
  document.querySelectorAll('input[name="contraste"]').forEach(r=> r.required = on);
}
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

// “Outros” em imagem
imgTipo?.addEventListener('change', ()=>{
  if (imgTipo.value === 'Outros'){ show(imgTipoOutro); imgTipoOutro.setAttribute('required',''); }
  else { hide(imgTipoOutro); imgTipoOutro.removeAttribute('required'); imgTipoOutro.value=''; }
});
imgRegiao?.addEventListener('change', ()=>{
  if (imgRegiao.value === 'Outros'){ show(imgRegiaoOutra); imgRegiaoOutra.setAttribute('required',''); }
  else { hide(imgRegiaoOutra); imgRegiaoOutra.removeAttribute('required'); imgRegiaoOutra.value=''; }
});

// ======= LAB: múltiplos exames =======
const labList = document.getElementById('labList');
const labAddBtn = document.getElementById('labAdd');
const labRowTpl = document.getElementById('labRowTpl');
const labData = document.getElementById('labData'); // data da coleta (geral)

function setLabRequired(on){
  if (labData) on ? labData.setAttribute('required','') : labData.removeAttribute('required');
}

function createLabRow(prefill = null){
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
      nomeOutro.setAttribute('required','');
      unidadeOutro.setAttribute('required','');
      unidadeBadge.textContent = 'Unidade: (defina acima)';
      valor.type = 'number';
      valor.placeholder = 'Informe o valor';
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
      valor.type = 'number';
      valor.placeholder = 'Informe o valor';
    }
  }

  sel.addEventListener('change', applyMeta);
  del.addEventListener('click', ()=> { node.remove(); });

  if (prefill){
    if (prefill.nome && [...sel.options].some(o=>o.text===prefill.nome)) sel.value = prefill.nome;
    else if (prefill.nome){ sel.value = 'Outros'; applyMeta(); nomeOutro.value = prefill.nome; }
    else applyMeta();

    if (prefill.unidade) { unidadeBadge.textContent = `Unidade: ${prefill.unidade}`; if (sel.value==='Outros') unidadeOutro.value = prefill.unidade; }
    if (prefill.valor!=null) valor.value = prefill.valor;
  } else {
    applyMeta();
  }

  sel.setAttribute('required','');
  valor.setAttribute('required','');

  labList.appendChild(node);
}

function ensureAtLeastOneLabRow(){
  if (!labList.querySelector('.lab-row')) createLabRow();
}

labAddBtn?.addEventListener('click', ()=> createLabRow());

// adaptador global para auto-preenchimento via PDF.js
window.addLabFromPDF = function(nome, valor, unidade){
  createLabRow({ nome, valor, unidade });
};

onTipoRegistroChanged(); // inicializa visibilidade

// ===== Upload com drag & drop + listagem + autofill PDF =====
const drop = document.getElementById('drop');
function renderFiles(){
  filesList.innerHTML = '';
  Array.from(inputFiles.files || []).forEach(async (f)=>{
    const li = document.createElement('li');
    li.textContent = `${f.name} (${Math.round(f.size/1024)} KB)`;
    filesList.appendChild(li);
    if (f.type === 'application/pdf') {
      try { await tryAutofillFromPDF(f); } catch { /* silencioso */ }
    }
  });
}
drop?.addEventListener('click', ()=> inputFiles?.click());
drop?.addEventListener('dragover', (e)=>{ e.preventDefault(); drop.classList.add('drag'); });
drop?.addEventListener('dragleave', ()=> drop.classList.remove('drag'));
drop?.addEventListener('drop', (e)=>{
  e.preventDefault(); drop.classList.remove('drag');
  inputFiles.files = e.dataTransfer.files;
  renderFiles();
});
inputFiles?.addEventListener('change', renderFiles);

// ====== PDF Autofill (imagem e múltiplos labs) ======
async function tryAutofillFromPDF(file){
  if (!file || file.type !== 'application/pdf' || !window.pdfjsLib) return;

  const buf = await file.arrayBuffer();
  const pdf = await pdfjsLib.getDocument({ data: buf }).promise;
  let text = '';
  for (let p=1; p<=pdf.numPages; p++){
    const page = await pdf.getPage(p);
    const content = await page.getTextContent();
    text += ' ' + content.items.map(i => i.str).join(' ');
  }

  const raw = text;
  text = text.replace(/\s+/g,' ').toLowerCase();

  // datas (dd/mm/aaaa ou dd-mm-aaaa)
  const dateRegex = /(\b\d{1,2}[\/\-]\d{1,2}[\/\-]\d{2,4}\b)/g;
  const allDates = [...text.matchAll(dateRegex)].map(m=>m[1]);
  const firstDateISO = allDates[0] ? allDates[0].replace(/-/g,'/').split('/').reverse().join('-') : '';

  // ===== heurísticas de imagem =====
  const imgKeys = [
    {k:'tomografia', v:'Tomografia Computadorizada'},
    {k:'ressonância', v:'Ressonância Magnética'},
    {k:'ressonancia', v:'Ressonância Magnética'},
    {k:'ultrassom', v:'Ultrassom'},
    {k:'ultra-som', v:'Ultrassom'},
    {k:'raio x', v:'Raio-X'},
    {k:'radiografia', v:'Raio-X'},
    {k:'endoscopia', v:'Endoscopia Digestiva Alta'},
    {k:'colonoscopia', v:'Colonoscopia'}
  ];
  const regioes = ['cabeça','crânio','cranio','face','pescoço','pescoco','tórax','torax','abdome','pelve',
                   'cervical','torácica','toracica','lombar','ombro','braço','braco','cotovelo',
                   'punho','mão','mao','quadril','coxa','joelho','perna','tornozelo','pé','pe',
                   'mama','próstata','prostata','útero','utero','rins','vias urinárias','vias urinarias',
                   'fígado','figado','biliar','pâncreas','pancreas','baço','baco','vasos'];

  const imgHit = imgKeys.find(x => text.includes(x.k));
  if (imgHit){
    tipoRegistro.value = 'Resultado de Exame de Imagem'; onTipoRegistroChanged();
    imgTipo.value = imgHit.v; imgTipo.dispatchEvent(new Event('change'));
    const r = regioes.find(rg => text.includes(rg));
    if (r){
      const map = {'torax':'Tórax','pe':'Tornozelo/Pé','pé':'Tornozelo/Pé','cervical':'Coluna Cervical','torácica':'Coluna Torácica','toracica':'Coluna Torácica','lombar':'Coluna Lombar'};
      const pretty = map[r] || r.charAt(0).toUpperCase()+r.slice(1);
      const found = Array.from(imgRegiao.options).some(o => o.text.toLowerCase() === pretty.toLowerCase());
      if (found){ imgRegiao.value = pretty; }
      else { imgRegiao.value = 'Outros'; imgRegiao.dispatchEvent(new Event('change')); imgRegiaoOutra.value = pretty; }
    }
    const contraste = text.includes('com contraste') ? 'Com contraste'
                    : text.includes('sem contraste') ? 'Sem contraste' : '';
    if (contraste){
      const radio = document.querySelector(`input[name="contraste"][value="${contraste}"]`);
      if (radio) radio.checked = true;
    }
    if (firstDateISO) imgData.value = firstDateISO;
  }

  // ===== heurísticas de laboratório (múltiplos) =====
  // Para cada exame conhecido, procura um número próximo ao nome.
  const tests = Object.keys(LAB_TESTS);
  let foundAnyLab = false;

  for (const name of tests) {
    const nameLow = name.toLowerCase().replace(/[()]/g,'').replace(/\s+/g,' ').trim();
    const idx = text.indexOf(nameLow);
    if (idx === -1) continue;

    // captura uma janela de texto depois do nome para achar valor
    const windowText = text.slice(idx, idx + 140);
    const meta = LAB_TESTS[name];

    // número com vírgula/ponto (ou simples)
    const numMatch = windowText.match(/([\d]+[\.,]\d+|\d+)/);
    let valor = null;
    if (meta.kind === 'text') {
      // qualitativo: tenta capturar "positivo/negativo/reagente/não reagente"
      const qualMatch = windowText.match(/\b(positivo|negativo|reagente|não reagente|nao reagente|normal|anormal)\b/);
      valor = qualMatch ? qualMatch[1] : '';
    } else {
      valor = numMatch ? numMatch[1].replace(',','.') : null;
    }

    // adiciona linha
    tipoRegistro.value = 'Resultado de Exame Laboratorial'; onTipoRegistroChanged();
    window.addLabFromPDF(name, valor, meta.unit);
    foundAnyLab = true;
  }

  if (foundAnyLab && firstDateISO) {
    labData.value = firstDateISO;
  }
}

// ====== SUBMIT ======
form?.addEventListener('submit', async (e)=>{
  e.preventDefault();
  if (formMsg) formMsg.textContent = 'Salvando…';

  const tipo = tipoRegistro?.value;
  let extras = {}; let resumo = '';

  if (tipo === 'Resultado de Exame de Imagem') {
    const contraste = (document.querySelector('input[name="contraste"]:checked')?.value) || '';
    const tipoEx = (imgTipo.value === 'Outros' ? imgTipoOutro.value : imgTipo.value);
    const regiao = (imgRegiao.value === 'Outros' ? imgRegiaoOutra.value : imgRegiao.value);

    extras = {
      categoria: 'imagem',
      tipo: tipoEx, regiao, contraste,
      dataRealizacao: imgData.value
    };
    resumo = `Exame de imagem: ${tipoEx} — ${regiao} — ${contraste}. Data: ${imgData.value}.`;
  }

  if (tipo === 'Resultado de Exame Laboratorial') {
    const rows = [...labList.querySelectorAll('.lab-row')];
    if (rows.length === 0) {
      if (formMsg) formMsg.textContent = 'Inclua ao menos um exame laboratorial.'; 
      return;
    }
    const exames = rows.map(row => {
      const sel = row.querySelector('.labNome');
      const valor = row.querySelector('.labValor')?.value ?? '';
      const nomeOutro = row.querySelector('.labNomeOutro')?.value ?? '';
      const unidadeOutro = row.querySelector('.labUnidadeOutro')?.value ?? '';
      const opt = sel.value;
      const meta = LAB_TESTS[opt];

      const nome = (opt === 'Outros') ? nomeOutro : opt;
      const unidade = (opt === 'Outros') ? unidadeOutro : (meta?.unit || '');
      const qualitativo = meta?.kind === 'text' || (opt === 'Outros' && isNaN(Number(valor)));

      return { exame: nome, unidade, valor, qualitativo };
    });

    extras = {
      categoria: 'laboratorial',
      dataColeta: labData?.value || '',
      exames
    };

    const resumoExames = exames.map(x => `${x.exame}: ${x.valor}${x.unidade ? ' ' + x.unidade : ''}`).join('; ');
    resumo = `Exames laboratoriais — Coleta: ${labData?.value || '—'} — ${resumoExames}.`;
  }

  const descCampo = document.getElementById('descricao');
  const descricaoFinal = resumo ? (resumo + '\n\n' + (descCampo?.value || '')) : (descCampo?.value || '');

  const fd = new FormData();
  fd.append('dataHora', document.getElementById('dataHora')?.value || '');
  fd.append('tipoRegistro', tipo || '');
  fd.append('titulo', document.getElementById('titulo')?.value || '');
  fd.append('descricao', descricaoFinal);
  fd.append('extras', JSON.stringify(extras));

  Array.from(inputFiles?.files || []).forEach(f=> fd.append('anexos', f, f.name));

  try{
    const resp = await fetch('/api/registros', { method:'POST', body: fd });
    if(!resp.ok) throw new Error(await resp.text());
    if (formMsg) formMsg.textContent = 'Registro salvo com sucesso.';
    form.reset?.(); if (filesList) filesList.innerHTML='';
    onTipoRegistroChanged(); // reseta blocos condicionais
  }catch(err){
    if (formMsg) formMsg.textContent = 'Erro: ' + (err?.message || err);
  }
});
