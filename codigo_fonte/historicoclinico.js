// ====== USER/PATIENT ======
function getCurrentUser(){ try { return JSON.parse(localStorage.getItem('currentUser')||'{}'); } catch { return {}; } }
function getCurrentPatient(){ try { return JSON.parse(localStorage.getItem('currentPatient')||'{}'); } catch { return {}; } }
const USER = getCurrentUser();
const PATIENT = getCurrentPatient();

// ====== APPBAR (zoom/contraste) ======
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

// ====== API ======
const API = '/api/clinico';
const lista = document.getElementById('lista');
const estado = document.getElementById('estado');

// ====== UTILS ======
function fmtDateTime(s){ try{ return new Date(s).toLocaleString('pt-BR',{ dateStyle:'short', timeStyle:'short' }); } catch{ return s; } }
function iconFor(mime, name){
  const ext = (name||'').split('.').pop().toLowerCase();
  if (mime?.startsWith('image/')) return '🖼️';
  if (ext === 'pdf' || mime === 'application/pdf') return '📄';
  return '📎';
}

// ====== MODAL PREVIEW ======
const modal = document.getElementById('modal');
const modalClose = document.getElementById('modalClose');
const modalBody = document.getElementById('modalBody');
const modalTitle = document.getElementById('modalTitle');
modalClose.onclick = ()=> modal.classList.remove('open');
modal.addEventListener('click', (e)=>{ if(e.target===modal) modal.classList.remove('open'); });
function openPreview(anexo){
  modalTitle.textContent = anexo.originalname || 'Anexo';
  modalBody.innerHTML = '';
  if ((anexo.mimetype||'').startsWith('image/')){
    const img = new Image(); img.src = anexo.url; img.alt = anexo.originalname || 'Imagem';
    img.style.maxWidth='85vw'; img.style.maxHeight='75vh'; modalBody.appendChild(img);
  } else if (anexo.mimetype==='application/pdf' || (anexo.url||'').endsWith('.pdf')){
    const iframe=document.createElement('iframe'); iframe.className='viewer'; iframe.src=anexo.url; modalBody.appendChild(iframe);
  } else {
    const p=document.createElement('p'); p.style.padding='16px';
    p.innerHTML=`Este tipo de arquivo não tem preview.<br/><a class="btn" href="${anexo.url}" download>Baixar</a>`;
    modalBody.appendChild(p);
  }
  modal.classList.add('open');
}

// ====== RENDER ======
function render(items){
  lista.innerHTML=''; estado.style.display='none';
  if(!items.length){
    estado.style.display='block';
    estado.innerHTML = 'Nenhum registro encontrado. <a class="btn" href="/registroclinico.html">+ Novo Registro</a>';
    return;
  }

  items.forEach(it=>{
    const card=document.createElement('article'); card.className='card';

    const header=document.createElement('header');
    const left=document.createElement('div');
    const right=document.createElement('div'); right.className='row';

    const ttl=document.createElement('div'); ttl.className='title'; ttl.textContent=it.titulo||'(Sem título)';

    const meta=document.createElement('div'); meta.className='meta';
    meta.textContent = `${fmtDateTime(it.datahora)} • ${it.autor_nome||'—'} • `;
    const tag=document.createElement('span'); tag.className='tag'; tag.textContent=it.tipo||'—';
    meta.appendChild(tag);

    left.appendChild(ttl); left.appendChild(meta);

    const btnVer=document.createElement('button'); btnVer.className='btn'; btnVer.textContent='Ver';
    const content=document.createElement('div'); content.className='content';
    btnVer.onclick=()=> content.classList.toggle('open'); right.appendChild(btnVer);

    if (USER?.id && USER.id === it.autor_id){
      const btnEd=document.createElement('a'); btnEd.className='btn'; btnEd.textContent='Editar'; btnEd.href=`/registroclinico.html?id=${it.id}`;
      right.appendChild(btnEd);
    }

    header.appendChild(left); header.appendChild(right); card.appendChild(header);

    // Descrição
    if ((it.descricao||'').trim()){
      const desc=document.createElement('div'); desc.style.whiteSpace='pre-wrap'; desc.textContent=(it.descricao||'').trim();
      content.appendChild(desc);
    }

    // -------- bloco específicos por categoria (EXTRAS) --------
    if (it.extras?.categoria === 'laboratorial'){
      const b=document.createElement('div'); b.style.marginTop='12px';
      const h=document.createElement('div'); h.style.fontWeight='700'; h.textContent='Exames Laboratoriais';
      const info=document.createElement('div'); info.className='meta';
      info.textContent = it.extras.dataColeta ? `Data da coleta: ${new Date(it.extras.dataColeta).toLocaleDateString('pt-BR')}` : '';
      b.appendChild(h); if(info.textContent) b.appendChild(info);

      // tabela simples
      const table=document.createElement('div');
      table.style.display='grid'; table.style.gridTemplateColumns='2fr 1fr 1fr'; table.style.gap='8px';
      table.style.marginTop='8px';
      const head=['Exame','Unidade','Valor'];
      head.forEach(t=>{ const c=document.createElement('div'); c.style.fontWeight='700'; c.textContent=t; table.appendChild(c); });
      (it.extras.exames||[]).forEach(ex=>{
        const c1=document.createElement('div'); c1.textContent=ex.exame||'—';
        const c2=document.createElement('div'); c2.textContent=ex.unidade||'—';
        const c3=document.createElement('div'); c3.textContent=(ex.valor??'').toString();
        table.appendChild(c1); table.appendChild(c2); table.appendChild(c3);
      });
      b.appendChild(table);
      content.appendChild(b);
    }

    if (it.extras?.categoria === 'imagem'){
      const b=document.createElement('div'); b.style.marginTop='12px';
      const h=document.createElement('div'); h.style.fontWeight='700'; h.textContent='Exame de Imagem';
      const list=document.createElement('ul'); list.style.margin='6px 0 0 18px';
      const rows=[
        ['Tipo', it.extras.tipo||'—'],
        ['Região', it.extras.regiao||'—'],
        ['Contraste', it.extras.contraste||'—'],
        ['Data de realização', it.extras.dataRealizacao ? new Date(it.extras.dataRealizacao).toLocaleDateString('pt-BR') : '—']
      ];
      rows.forEach(([k,v])=>{ const li=document.createElement('li'); li.textContent=`${k}: ${v}`; list.appendChild(li); });
      b.appendChild(h); b.appendChild(list);
      content.appendChild(b);
    }

    // anexos
    const anexos=it.anexos||[];
    if (anexos.length){
      const label=document.createElement('div'); label.style.marginTop='10px'; label.style.fontSize='12px'; label.style.color='#374151'; label.textContent='Anexos:';
      const wrap=document.createElement('div'); wrap.className='anexos';
      anexos.forEach(ax=>{
        const th=document.createElement('div'); th.className='thumb'; th.title=ax.originalname;
        if ((ax.mimetype||'').startsWith('image/')){ const im=new Image(); im.src=ax.url; im.alt=ax.originalname||'Imagem'; th.appendChild(im); }
        else if (ax.mimetype==='application/pdf' || (ax.url||'').endsWith('.pdf')){ const d=document.createElement('div'); d.className='pdf'; d.innerHTML=`📄 PDF<br>${(ax.originalname||'').slice(0,16)}`; th.appendChild(d); }
        else { const d=document.createElement('div'); d.className='pdf'; d.innerHTML=`${iconFor(ax.mimetype, ax.originalname)} ${(ax.originalname||'').slice(0,16)}`; th.appendChild(d); }
        th.addEventListener('click', ()=> openPreview(ax));
        wrap.appendChild(th);
      });
      content.appendChild(label); content.appendChild(wrap);
    }

    card.appendChild(content);
    lista.appendChild(card);
  });
}

// ====== LOAD ======
async function load(){
  try{
    const qs = new URLSearchParams();
    if (PATIENT?.id) qs.set('paciente_id', PATIENT.id);
    const resp = await fetch(`${API}?${qs.toString()}`);
    if(!resp.ok) throw new Error(await resp.text());
    const data = await resp.json();
    data.sort((a,b)=> new Date(b.datahora) - new Date(a.datahora) || (b.id - a.id));
    render(data);
  }catch(e){
    console.error(e);
    estado.style.display='block';
    estado.textContent='Erro ao carregar histórico: ' + (e.message||e);
  }
}
load();
