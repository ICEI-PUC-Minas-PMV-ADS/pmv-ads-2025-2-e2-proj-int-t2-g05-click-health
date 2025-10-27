// JavaScript source code
// MOCK DATA
const paciente = { nome: "Maria Clara Andrade", dataNascimento: "15/05/1960" };
const cuidadores = [
    { id: 1, email: "eduardo.singularo@sga.pucminas.br", permissao: "Pode Editar" },
    { id: 2, email: "joao.pedro@exemplo.com", permissao: "Apenas Visualizar" }
];
const feed = [
    { id: 1, tipo: 'concluido', texto: "Tarefa Concluída: 'Losartana'", autor: "Lucas Moura", hora: "Hoje, às 11:30" },
    { id: 2, tipo: 'medicamento', texto: "Medicamento Adicionado: AAS 100mg", autor: "João Pedro", hora: "Hoje, às 09:15" }
];

// FUNÇÃO PARA RENDERIZAR CONTEÚDO
function renderContent(view) {
    const content = document.getElementById('content');
    content.innerHTML = '';

    if (view === 'configuracoes') {
        content.innerHTML = `
      <div class="card">
        <h2>Perfil do Paciente</h2>
        <p><strong>Nome:</strong> ${paciente.nome}</p>
        <p><strong>Data de Nascimento:</strong> ${paciente.dataNascimento}</p>
      </div>

      <div class="card">
        <h2>Gerenciar Cuidadores</h2>
        ${cuidadores.map(c => `
          <div class="cuidador">
            <span>${c.email}</span>
            <span class="${c.permissao === 'Pode Editar' ? 'badge-edit' : 'badge-view'}">${c.permissao}</span>
          </div>
        `).join('')}
      </div>
    `;
    }

    if (view === 'feedAtividades') {
        content.innerHTML = `
      <div class="card">
        <h2>Feed de Atividades</h2>
        ${feed.map(f => `
          <div class="feed-item">
            <div class="icon">✔️</div>
            <div class="content">
              <strong>${f.tipo}</strong><br>
              ${f.texto}<br>
              <small>${f.autor} - ${f.hora}</small>
            </div>
          </div>
        `).join('')}
      </div>
    `;
    }

    if (!['configuracoes', 'feedAtividades'].includes(view)) {
        content.innerHTML = `<div class="card"><h2>${view} - Em Breve...</h2></div>`;
    }
}

// EVENTOS SIDEBAR
document.querySelectorAll('.sidebar a').forEach(link => {
    link.addEventListener('click', function () {
        document.querySelectorAll('.sidebar a').forEach(l => l.classList.remove('active'));
        this.classList.add('active');
        renderContent(this.dataset.view);
    });
});

// Inicial
renderContent('configuracoes');
