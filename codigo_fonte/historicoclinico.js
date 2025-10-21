// ========= API BASE & USER =========
const API_BASE = window.location.port === '3000' ? '' : 'http://localhost:3000';

let CURRENT_USER = JSON.parse(localStorage.getItem('currentUser') || 'null');
if (!CURRENT_USER) {
  CURRENT_USER = { id: 'u_' + Math.random().toString(36).slice(2), name: 'Usuário Padrão' };
  localStorage.setItem('currentUser', JSON.stringify(CURRENT_USER));
}
const headersAuth = { 'X-User-Id': CURRENT_USER.id, 'X-User-Name': CURRENT_USER.name };

document.getElementById('who').textContent = `Logado como: ${CURRENT_USER.name} (${CURRENT_USER.id})`;

// ========= LISTAGEM =========
async function carregaHistorico() {
  const resp = await fetch(`${API_BASE}/api/registros`, { headers: headersAuth });
  if (!resp.ok) { alert(await resp.text()); return; }
  const data = await resp.json();

  const tbody = document.querySelector('#grid tbody');
  tbody.innerHTML = '';

  for (const r of data) {
    const tr = document.createElement('tr');

    const anexosHTML = (r.anexos || []).map(a => {
      const base = a.storage_path?.split(/[\\/]/).pop();
      const name = a.file_name || 'arquivo';
      const href = base ? `${API_BASE}/uploads/${base}` : '#';
      return `<li><a href="${href}" target="_blank" rel="noopener">${name}</a></li>`;
    }).join('');

    tr.innerHTML = `
      <td>${r.dataHora ? new Date(r.dataHora).toLocaleString() : ''}</td>
      <td>${r.tipoRegistro || ''}</td>
      <td>${r.titulo || ''}</td>
      <td>${(r.descricao || '').slice(0, 120)}${(r.descricao || '').length > 120 ? '…' : ''}</td>
      <td>${anexosHTML ? `<ul class="file-list">${anexosHTML}</ul>` : '<span class="muted">—</span>'}</td>
      <td>
        ${r.canEdit ? `<button class="btn btn-edit" data-id="${r.id}">Editar</button>` : '<span class="muted">—</span>'}
      </td>
    `;
    tbody.appendChild(tr);
  }
}

document.addEventListener('click', (e) => {
  if (e.target.matches('.btn-edit')) {
    const id = e.target.getAttribute('data-id');
    abreModalEdicao(id);
  }
});

// ========= MODAL EDIÇÃO =========
async function abreModalEdicao(id) {
  const resp = await fetch(`${API_BASE}/api/registros/${id}`, { headers: headersAuth });
  if (!resp.ok) { alert(await resp.text()); return; }
  const r = await resp.json();

  document.getElementById('editId').value = r.id;
  document.getElementById('editDataHora').value = r.dataHora ? r.dataHora.slice(0,16) : '';
  document.getElementById('editTipo').value = r.tipoRegistro || '';
  document.getElementById('editTitulo').value = r.titulo || '';
  document.getElementById('editDescricao').value = r.descricao || '';
  document.getElementById('editExtras').value = (() => {
    try { return JSON.stringify(JSON.parse(r.extras_json || '{}'), null, 2); }
    catch { return r.extras_json || '{}'; }
  })();

  const ul = document.getElementById('editAnexosList');
  ul.innerHTML = '';
  for (const a of (r.anexos || [])) {
    const base = a.storage_path?.split(/[\\/]/).pop();
    const href = base ? `${API_BASE}/uploads/${base}` : '#';
    const nome = a.file_name || 'arquivo';
    const li = document.createElement('li');
    li.innerHTML = `
      <a href="${href}" target="_blank" rel="noopener">${nome}</a>
      <button class="btn btn-del-anexo" data-anexo="${a.id}">Remover</button>
    `;
    ul.appendChild(li);
  }

  document.getElementById('modalEdit').showModal();
}

document.getElementById('editForm').addEventListener('submit', async (e)=>{
  e.preventDefault();
  const id = document.getElementById('editId').value;
  let extras_json = document.getElementById('editExtras').value.trim();
  try { JSON.parse(extras_json || '{}'); } catch { return alert('Extras JSON inválido.'); }

  const body = {
    dataHora: document.getElementById('editDataHora').value,
    tipoRegistro: document.getElementById('editTipo').value,
    titulo: document.getElementById('editTitulo').value,
    descricao: document.getElementById('editDescricao').value,
    extras_json
  };

  // Atualiza campos textuais
  {
    const resp = await fetch(`${API_BASE}/api/registros/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json', ...headersAuth },
      body: JSON.stringify(body)
    });
    if (!resp.ok) return alert(await resp.text());
  }

  // Envia novos anexos se houver
  const novos = document.getElementById('editAnexosNovos').files;
  if (novos && novos.length) {
    const fd = new FormData();
    Array.from(novos).forEach(f => fd.append('anexos', f));
    const up = await fetch(`${API_BASE}/api/registros/${id}/anexos`, {
      method: 'POST',
      headers: headersAuth,
      body: fd
    });
    if (!up.ok) return alert('Registro salvo, mas falhou ao enviar anexos: ' + await up.text());
  }

  document.getElementById('modalEdit').close();
  carregaHistorico();
});

document.addEventListener('click', async (e)=>{
  if (e.target.matches('.btn-del-anexo')) {
    const anexoId = e.target.getAttribute('data-anexo');
    if (!confirm('Remover este anexo?')) return;
    const resp = await fetch(`${API_BASE}/api/anexos/${anexoId}`, {
      method: 'DELETE',
      headers: headersAuth
    });
    if (!resp.ok) return alert(await resp.text());
    e.target.closest('li')?.remove();
  }
});

// boot
carregaHistorico();
