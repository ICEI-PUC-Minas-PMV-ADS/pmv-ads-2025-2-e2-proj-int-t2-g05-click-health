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
| CT-RF07 - Registro de histórico clínico     | RF-007                  | Testar se é possível registrar alterações no histórico clínico do paciente                       | 1. Acessar perfil do paciente<br>2. Ir para aba "Histórico Clínico"<br>3. Adicionar alteração de medicação   | Informações são salvas com data, hora e responsável e exibidas no histórico                    |
| CT-RF08 - Exibição de feed por paciente     | RF-008                  | Verificar se o sistema exibe feed de atividades do paciente em ordem cronológica                 | 1. Realizar ações no sistema (ex: agendar, editar, registrar)<br>2. Acessar feed do paciente                 | Feed mostra ações em ordem cronológica com data, hora e autor                                  |
| CT-RF09 - Trilha de auditoria de ações      | RF-009                  | Garantir que as operações no perfil do paciente sejam registradas com detalhes                   | 1. Editar dados do paciente<br>2. Acessar trilha de auditoria                                                | Sistema exibe quem fez, o que foi feito, quando, e os dados antes/depois                       |
| CT-RF10 - Login seguro e gestão de acessos  | RF-010                  | Validar autenticação, convites e revogação de acessos a perfis de pacientes                      | 1. Fazer login com credenciais válidas<br>2. Convidar novo usuário<br>3. Revogar acesso                      | Login ocorre com segurança; convites e revogações funcionam corretamente                       |
| CT-RF11 - Edição de dados cadastrais        | RF-011                  | Testar se é possível editar dados básicos do paciente conforme permissões atribuídas             | 1. Acessar ficha do paciente<br>2. Clicar em "Editar"<br>3. Alterar dados<br>4. Salvar                       | Dados são atualizados corretamente e respeitam as permissões do usuário                        |
| CT-RF12 - Exportar relatórios do paciente   | RF-012                  | Verificar a funcionalidade de exportação de dados do paciente em formatos PDF, CSV e ZIP         | 1. Acessar perfil do paciente<br>2. Clicar em "Exportar dados"<br>3. Escolher formato e baixar               | Arquivo é gerado corretamente com os dados completos e downloads funcionam                     |




# Plano de Testes de Software – Requisitos Não Funcionais


| **Caso de Teste** | **Requisito Associado** | **Objetivo do Teste** | **Passos** | **Critério de Êxito** |
|-------------------|--------------------------|------------------------|------------|------------------------|
| CT-RNF-001 | RNF-001 – Segurança e Privacidade | Verificar se os dados sensíveis de saúde estão protegidos conforme a LGPD. | 1. Tentar acessar dados sem login.<br>2. Verificar criptografia dos dados em repouso e em trânsito.<br>3. Validar controle de acesso por perfil (admin, cuidador, etc). | O acesso a dados é bloqueado para usuários não autenticados.<br>Dados sensíveis estão criptografados.<br>Perfis têm permissões adequadas. |
| CT-RNF-002 | RNF-002 – Usabilidade e Acessibilidade | Avaliar se a interface é intuitiva, acessível e fácil de usar por cuidadores sem formação técnica. | 1. Navegar sem treinamento pelas funções principais.<br>2. Avaliar contraste de cores e tamanho da fonte.<br>3. Testar uso com leitores de tela (ex: NVDA). | Usuário navega com facilidade.<br>Contraste e legibilidade atendem às diretrizes WCAG.<br>Leitor de tela lê corretamente os elementos da interface. |
| CT-RNF-003 | RNF-003 – Disponibilidade | Verificar se o sistema mantém alta disponibilidade (24/7) e comunica manutenções com antecedência. | 1. Monitorar o uptime por 7 dias.<br>2. Verificar presença de aviso de manutenção programada.<br>3. Testar comportamento do sistema durante e após a manutenção. | O sistema mantém disponibilidade acima de 99,5%.<br>Usuários são informados previamente sobre manutenções.<br>Serviço retorna ao normal sem falhas. |
| CT-RNF-004 | RNF-004 – Desempenho | Garantir que as funções principais tenham tempo de resposta rápido, mesmo com múltiplos usuários. | 1. Medir tempo para carregar perfil, atualizar feed e salvar novo registro.<br>2. Simular múltiplos cuidadores acessando simultaneamente. | Todas as funções críticas respondem em menos de 2 segundos.<br>O sistema não apresenta lentidão perceptível sob carga. |
| CT-RNF-005 | RNF-005 – Escalabilidade | Verificar se o sistema suporta aumento de usuários e dados sem comprometer o desempenho. | 1. Simular crescimento gradual de usuários (ex: 100, 500, 1000).<br>2. Avaliar resposta do sistema e tempo de carregamento.<br>3. Verificar uso eficiente de recursos. | O sistema continua operando com desempenho aceitável em diferentes cargas.<br>Não ocorrem falhas ou lentidão excessiva. |
| CT-RNF-006 | RNF-006 – Portabilidade e Facilidade de Acesso | Validar acesso ao sistema via navegador em diferentes dispositivos, sem necessidade de instalação. | 1. Acessar o sistema em diferentes navegadores (Chrome, Firefox, Safari).<br>2. Testar acesso via desktop, smartphone e tablet.<br>3. Avaliar responsividade do layout. | O sistema funciona corretamente em todos os navegadores e dispositivos testados.<br>Layout se adapta bem a diferentes telas.<br>Sem instalação adicional necessária. |
| CT-RNF-007 | RNF-007 – Manutenibilidade e Evolutividade | Avaliar se o código-fonte é estruturado para fácil manutenção e evolução. | 1. Analisar documentação técnica e comentários no código.<br>2. Verificar modularização e separação de responsabilidades.<br>3. Identificar uso de frameworks e padrões conhecidos. | O código é modular, bem documentado e segue boas práticas.<br>Facilidade de adicionar novas funcionalidades sem afetar o sistema existente. |
| CT-RNF-008 | RNF-008 – Confiabilidade dos Dados | Verificar integridade dos dados, controle de concorrência e confiabilidade dos registros de auditoria. | 1. Simular edição simultânea da mesma informação por dois usuários.<br>2. Verificar consistência após salvamento.<br>3. Auditar logs de alterações. | Sistema lida corretamente com conflitos de edição.<br>Dados não apresentam inconsistências.<br>Registros de auditoria são imutáveis e rastreáveis. |
| CT-RNF-009 | RNF-009 – Backup e Recuperação de Dados | Garantir que os dados sejam salvos periodicamente e possam ser restaurados em caso de falha. | 1. Verificar política de backup (frequência, local de armazenamento).<br>2. Simular perda de dados.<br>3. Realizar restauração a partir de um backup recente. | Backups são realizados automaticamente em intervalos definidos.<br>Restauração é concluída com sucesso.<br>Dados são recuperados sem perdas ou erros. |




 
> **Links Úteis**:
> - [IBM - Criação e Geração de Planos de Teste](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Práticas e Técnicas de Testes Ágeis](http://assiste.serpro.gov.br/serproagil/Apresenta/slides.pdf)
> -  [Teste de Software: Conceitos e tipos de testes](https://blog.onedaytesting.com.br/teste-de-software/)
> - [Criação e Geração de Planos de Teste de Software](https://www.ibm.com/developerworks/br/local/rational/criacao_geracao_planos_testes_software/index.html)
> - [Ferramentas de Test para Java Script](https://geekflare.com/javascript-unit-testing/)
> - [UX Tools](https://uxdesign.cc/ux-user-research-and-user-testing-tools-2d339d379dc7)
