// Arquivo: wwwroot/js/cadastro-paciente.js
// Interações para as páginas de cadastro de paciente.

document.addEventListener('DOMContentLoaded', function () {

    console.log("Script carregado."); // Confirma que o script iniciou

    // --- Lógica para Adicionar Foto (Passo 1) ---
    const avatarUpload = document.querySelector('.avatar-upload');
    const hiddenFileInput = document.getElementById('avatar-file-input');

    if (avatarUpload && hiddenFileInput) {
        avatarUpload.addEventListener('click', () => hiddenFileInput.click());

        hiddenFileInput.addEventListener('change', (event) => {
            if (event.target.files && event.target.files.length > 0) {
                avatarUpload.textContent = 'Foto selecionada!';
            }
        });
    }

    // --- Lógica para Gerenciar Cuidadores (Passo 2) ---
    const caregiverList = document.querySelector('.caregiver-list');
    const inviteButton = document.getElementById('btn-convidar-cuidador');
    const emailInput = document.getElementById('cuidador-email-input');
    const errorMsg = document.getElementById('cuidador-error-msg');
    if (caregiverList && inviteButton && emailInput) {

        // Adiciona cuidador à lista ao clicar em Convidar
        inviteButton.addEventListener('click', function() {
            const email = emailInput.value.trim();
            if (email === '') {
                errorMsg.style.display ='block';
                emailInput.focus();
                return;
            } else {
                errorMsg.style.display = 'none';
            }

            // Cria o HTML do novo item
            const novoItemHtml = `
                <li class="caregiver-item" data-email="${email}">
                    <span class="caregiver-email">${email}</span>
                    <div class="btn-toggle-group">
                        <button type="button" class="btn-toggle active js-toggle-permission" data-permission="edit">Pode Editar</button>
                        <button type="button" class="btn-toggle js-toggle-permission" data-permission="view">Apenas Visualizar</button>
                    </div>
                    <a href="#" class="btn-danger-link js-remove-caregiver">Remover</a>
                    <input type="hidden" name="emailParaConvidar" value="${email}">
                </li>`;

            caregiverList.insertAdjacentHTML('beforeend', novoItemHtml);
            emailInput.value = '';
            emailInput.focus();
        });

        // Lida com cliques na lista (Remover ou Alternar Permissão)
        caregiverList.addEventListener('click', function(event) {
            // Remover
            if (event.target.classList.contains('js-remove-caregiver')) {
                event.preventDefault();
                const item = event.target.closest('.caregiver-item');
                {
                    item.remove();
                }
            }
            // Alternar permissão
            else if (event.target.classList.contains('js-toggle-permission')) {
                const button = event.target;
                if (button.classList.contains('active')) return;

                const group = button.closest('.btn-toggle-group');
                group.querySelectorAll('.js-toggle-permission').forEach(btn => btn.classList.remove('active'));
                button.classList.add('active');
            }
        });
    }

});
