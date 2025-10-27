# Plano de Testes de Software 

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>

Apresente os cenários de testes utilizados na realização dos testes da sua aplicação. Escolha cenários de testes que demonstrem os requisitos sendo satisfeitos.

Não deixe de enumerar os casos de teste de forma sequencial e de garantir que o(s) requisito(s) associado(s) a cada um deles está(ão) correto(s) - de acordo com o que foi definido na seção "2 - Especificação do Projeto". 

Por exemplo:
 
# Plano de Testes de Software - Requisitos Funcionais

| **Caso de Teste**                           | **Requisito Associado** | **Objetivo do Teste**                                                                            | **Passos**                                                                                                   | **Critério de Êxito**                                                                          |
| ------------------------------------------- | ----------------------- | ------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------------------------------- |
| CT-RF01 - Gerenciar múltiplos pacientes     | RF-001                  | Validar se o cuidador consegue cadastrar, visualizar, editar e excluir diversos pacientes        | 1. Acessar área "Pacientes"<br>2. Cadastrar novo paciente<br>3. Editar paciente<br>4. Excluir paciente       | Pacientes são corretamente manipulados conforme operações CRUD, sem erro                       |
| CT-RF02 - Compartilhar acesso de paciente   | RF-002                  | Verificar se o cuidador consegue compartilhar o perfil de um paciente com outro usuário          | 1. Acessar perfil de paciente<br>2. Selecionar opção "Compartilhar acesso"<br>3. Inserir e-mail do convidado | O novo usuário recebe acesso ao paciente compartilhado e pode vê-lo na sua lista               |
| CT-RF03 - Configurar permissões de acesso   | RF-003                  | Confirmar se é possível atribuir permissões diferentes por paciente (leitura ou leitura/escrita) | 1. Compartilhar paciente com outro usuário<br>2. Selecionar tipo de permissão<br>3. Confirmar                | As permissões são aplicadas corretamente, restringindo ou liberando ações conforme configurado |
| CT-RF04 - Registrar compromissos de saúde   | RF-004                  | Testar se é possível agendar consultas, exames ou outros compromissos por paciente               | 1. Acessar agenda do paciente<br>2. Clicar em "Novo compromisso"<br>3. Preencher dados<br>4. Salvar          | Compromisso aparece na agenda e no histórico do paciente                                       |
| CT-RF05 - Programar medicação e alertas     | RF-005                  | Validar se o cuidador pode configurar horários de medicação e alertas de insumos                 | 1. Acessar aba "Medicação"<br>2. Adicionar nova medicação<br>3. Definir horários e lembretes                 | Alertas são disparados nos horários definidos; insumos com estoque baixo geram aviso           |
| CT-RF06 - Notificações de eventos agendados | RF-006                  | Verificar se o sistema envia lembretes configuráveis de eventos agendados                        | 1. Criar evento com lembrete<br>2. Configurar tempo de antecedência<br>3. Salvar                             | Cuidador recebe notificação no tempo definido antes do evento                                  |
| CT-RF07 - Registro de histórico clínico     | RF-007                  | Testar se é possível registrar alterações no histórico clínico do paciente                       | 1. Acessar perfil do paciente<br>2. Ir para aba "Histórico Clínico"<br>3. Adicionar alteração de um registro já realizado   | Informações são salvas com data, hora e responsável e exibidas no histórico                    |
| CT-RF08 - Exibição de feed por paciente     | RF-008                  | Verificar se o sistema exibe feed de atividades do paciente em ordem cronológica                 | 1. Realizar ações no sistema (ex: agendar, editar, registrar)<br>2. Acessar feed do paciente                 | Feed mostra ações em ordem cronológica com data, hora e autor                                  |
| CT-RF09 - Trilha de auditoria de ações      | RF-009                  | Garantir que as operações no perfil do paciente sejam registradas com detalhes                   | 1. Editar dados do paciente<br>2. Acessar trilha de auditoria                                                | Sistema exibe quem fez, o que foi feito, quando, e os dados antes/depois                       |
| CT-RF10 - Login seguro e gestão de acessos  | RF-010                  | Validar autenticação, convites e revogação de acessos a perfis de pacientes                      | 1. Fazer login com credenciais válidas<br>2. Convidar novo usuário<br>3. Revogar acesso                      | Login ocorre com segurança; convites e revogações funcionam corretamente                       |
| CT-RF11 - Edição de dados cadastrais        | RF-011                  | Testar se é possível editar dados básicos do paciente conforme permissões atribuídas             | 1. Acessar ficha do paciente<br>2. Clicar em "Editar"<br>3. Alterar dados<br>4. Salvar                       | Dados são atualizados corretamente e respeitam as permissões do usuário                        |
| CT-RF12 - Exportar relatórios do paciente   | RF-012                  | Verificar a funcionalidade de exportação de dados do paciente em formatos PDF, CSV e ZIP         | 1. Acessar perfil do paciente<br>2. Clicar em "Exportar dados"<br>3. Escolher formato e baixar               | Arquivo é gerado corretamente com os dados completos e downloads funcionam                     |
| CT-RF13 – Gerenciar Alertas e Notificações | RF-013| Validar o funcionamento completo do CRUD de alertas e a exibição das notificações no painel do cuidador, garantindo que os dados sejam salvos, atualizados, excluídos e exibidos corretamente nas seções “Novas” e “Anteriores”. | 1. Acessar a tela “Alertas”<br>2. Clicar em “+ Novo Alerta” e preencher os campos Paciente, Título, Mensagem, Tipo, Status e Data/Hora<br>3. Clicar em “Salvar” e verificar que o alerta aparece na lista<br>4. Editar o alerta e alterar o status para “Lido”<br>5. Visualizar os detalhes do alerta e confirmar as informações<br>6. Excluir o alerta e confirmar sua remoção<br>7. Acessar a tela “Notificações” e verificar os alertas nas seções “Novas” e “Anteriores” | O alerta é criado, exibido, editado e excluído corretamente; notificações aparecem conforme o status e o banco de dados é atualizado sem erros. |
| CT-RF14 – Cadastrar Nova conta na Aplicação | RF-014| Validar o funcionamento completo do CRUD de processo de criação de uma nova conta e a exibição da lista completa de novas contas criadas, garantindo que os dados sejam salvos, atualizados, excluídos e exibidos corretamente nas seções “Novas” e “Anteriores”. | 1. Acessar a tela “cadastrar”<br>2. Preencher o formulário para nova conta” e preencher os campos: Nome, Email e Senha<br>3. Clicar em “Salvar” e ser direcionado para a lista completa de contas já cadastradas<br>4. Editar a conta cadastrada”<br>5. Visualizar os detalhes da conta e confirmar as informações<br>6. Excluir a conta e confirmar sua remoção<br>7. 




<img width="1366" height="728" alt="Login" src="https://github.com/user-attachments/assets/6983b712-cc01-4ddf-8683-78c20eb07d7a" />

<img width="1366" height="728" alt="cadastro" src="https://github.com/user-attachments/assets/83240ac4-2183-4506-9512-e30a4dd80bf4" />

<img width="1366" height="728" alt="Pacientes" src="https://github.com/user-attachments/assets/dbf13145-aa8d-4f49-b548-7a2d376561cd" />

<img width="1366" height="760" alt="novo paciente" src="https://github.com/user-attachments/assets/e3ba95f8-f94c-4acb-b93d-a12ccf67fb8e" />

<img width="1366" height="728" alt="dados" src="https://github.com/user-attachments/assets/a9811b2c-dfb8-4899-931c-de04ec885b04" />

<img width="1366" height="728" alt="agenda" src="https://github.com/user-attachments/assets/865c297f-8724-4707-94b6-7843445bd344" />

 <img width="1366" height="728" alt="medicação" src="https://github.com/user-attachments/assets/7fb6ca04-19ef-4825-89d6-aa7c638d18b2" />

<img width="1366" height="728" alt="historico" src="https://github.com/user-attachments/assets/74d95fa4-7fe9-4e2f-8800-997f38d6c888" />

<img width="1366" height="728" alt="feed" src="https://github.com/user-attachments/assets/8a479e78-5ea8-4e86-96cb-dd73b0803960" />

<img width="1366" height="728" alt="feedcompartilhamento" src="https://github.com/user-attachments/assets/086a8a2e-e2fd-4dca-866f-6e43f7e10189" />

<img width="1366" height="728" alt="excluir paciente" src="https://github.com/user-attachments/assets/91e00ac1-fbde-428b-98ce-c6532a258e79" />


**Link para visualização**
https://www.figma.com/make/YPo0dePEQeianSUiy62teD/ClickHealth?node-id=0-1&p=f&t=iNQcjheUeWspvHSk-0&fullscreen=1 
 

> **Links Úteis**:
> - [IBM - Criação e Geração de Planos de Teste](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Práticas e Técnicas de Testes Ágeis](http://assiste.serpro.gov.br/serproagil/Apresenta/slides.pdf)
> -  [Teste de Software: Conceitos e tipos de testes](https://blog.onedaytesting.com.br/teste-de-software/)
> - [Criação e Geração de Planos de Teste de Software](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Ferramentas de Test para Java Script](https://geekflare.com/javascript-unit-testing/)
> - [UX Tools](https://uxdesign.cc/ux-user-research-and-user-testing-tools-2d339d379dc7)
