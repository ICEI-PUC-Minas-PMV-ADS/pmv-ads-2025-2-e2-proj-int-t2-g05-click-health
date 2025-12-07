# Registro de Testes de Software

<span style="color:red">Pré-requisitos: <a href="3-Projeto de Interface.md"> Projeto de Interface</a></span>, <a href="8-Plano de Testes de Software.md"> Plano de Testes de Software</a>

Para cada caso de teste definido no Plano de Testes de Software, realize o registro das evidências dos testes feitos na aplicação pela equipe, que comprovem que o critério de êxito foi alcançado (ou não!!!). Para isso, utilize uma ferramenta de captura de tela que mostre cada um dos casos de teste definidos (obs.: cada caso de teste deverá possuir um vídeo do tipo _screencast_ para caracterizar uma evidência do referido caso).

| **Caso de Teste** 	| **CT01 – Gerenciar múltiplos pacientes** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-001 - Validar se o cuidador consegue cadastrar, visualizar, editar e excluir pacientes |
|Registro de evidência | ![testerf001](https://github.com/user-attachments/assets/7d2b22c0-edfc-4763-b6d8-5d4bfa6b740e)|

| **Caso de Teste** 	| **CT002 – Agenda de Medicação/ listagem de documentos agendados e botão para agendar novos medicamentos** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-002 - Validar se o cuidador pode ver a lista de medicamentos agendados e clicar no botão de adicionar medicação |
|Registro de evidência | ![testerf002](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/main/docs/img/CT002%20%E2%80%93%20Agenda%20de%20Medica%C3%A7%C3%A3o,%20listagem%20de%20documentos%20agendados%20e%20bot%C3%A3o%20para%20agendar%20novos%20medicamentos.gif?raw=true) |

| **Caso de Teste** 	| **CT03 – Visualizar registros do histórico clínico** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-003 - Verificar se o usuário consegue visualizar corretamente as informações do histórico clínico do paciente, incluindo data, hora e responsável pelo registro. |
|Registro de evidência | ![testerf002](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/main/docs/img/CT03%20%E2%80%93%20Visualizar%20registros%20do%20hist%C3%B3rico%20cl%C3%ADnico.gif?raw=true) |

| **Caso de Teste** 	| **CT04 – Login seguro** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-004 - Validar autenticação |
|Registro de evidência | ![testerf004](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/main/docs/img/CT04%20%E2%80%93%20Login%20seguro.gif?raw=true) |

| **Caso de Teste** 	| **CT05 – Edição de dados cadastrais** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-005 - Testar se é possível editar dados básicos do paciente |
|Registro de evidência | ![testerf005](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/main/docs/img/CT05%20%E2%80%93%20Edi%C3%A7%C3%A3o%20de%20dados%20cadastrais.gif?raw=true) |

| **Caso de Teste** 	| **CT06 – Gerenciar Alertas e Notificações** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-RF06 - Validar o funcionamento completo do CRUD de alertas e a exibição das notificações no painel do cuidador, garantindo que os dados sejam salvos, atualizados, excluídos e exibidos corretamente nas seções “Novas” e “Anteriores”. |
|Registro de evidência | ![testerf005](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/main/docs/img/CT06%20%E2%80%93%20Gerenciar%20Alertas%20e%20Notifica%C3%A7%C3%B5es.gif?raw=true) |

| **Caso de Teste** 	| **CT07 – Cadastrar Nova conta na Aplicação** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-007 - Validar o funcionamento completo do processo de criação de uma nova conta |
|Registro de evidência | ![testerf005](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/main/docs/img/CT07%20%E2%80%93%20Cadastrar%20Nova%20conta%20na%20Aplica%C3%A7%C3%A3o.gif?raw=true) |


## Relatório de testes de software

1. Introdução

O presente relatório apresenta a análise detalhada dos testes realizados na aplicação, incluindo a discussão dos resultados obtidos, identificação de pontos fortes e fragilidades, além da descrição das falhas observadas durante a execução dos casos de teste. Com base nas evidências coletadas, são propostas estratégias de melhoria para as próximas iterações do projeto, visando elevar a confiabilidade, a usabilidade e a eficácia da solução.

2. Discussão Detalhada dos Resultados
2.1 Pontos Fortes Identificados

Durante os testes, observou-se que algumas funcionalidades essenciais apresentaram bom desempenho e contribuem positivamente para a experiência do usuário:

- Fluxo de cadastro de medicamentos: apesar de limitado na ausência de alertas, a criação de novos registros funciona corretamente e sem falhas aparentes.

- Visualização de registros no Histórico Clínico: o sistema apresenta as informações completas (data, horário e responsável), permitindo que o usuário consulte os detalhes de forma clara.

- Cadastro inicial de novas contas: a criação das contas é realizada corretamente, com validação básica dos dados e navegação adequada após o salvamento.

Contribuição dos aspectos positivos:
Essas funcionalidades demonstram estabilidade e coerência nos fluxos principais, garantindo que o usuário consiga realizar operações básicas de forma intuitiva. A clareza na exibição de informações e a simplicidade no cadastro favorecem a usabilidade e reduzem erros durante a navegação.

2.2 Fragilidades da Solução

Apesar dos pontos positivos, algumas fragilidades foram identificadas. Essas inconsistências impactam diretamente a completude dos requisitos e a qualidade da experiência do usuário.

As principais fragilidades observadas foram:

Casos de teste incompatíveis com funcionalidades não implementadas, como:

- cadastro de alertas na tela de Medicamentos;

- tela de perfis de usuários;

- edição de registros no Histórico Clínico;

- Inconsistências na exibição de informações no Feed, impossibilitando validar corretamente o caso CT-RF04.


Impacto das fragilidades:
Fragilidades como ausência de funcionalidades previstas e comportamentos inesperados afetam diretamente a eficácia da solução, gerando insegurança no usuário e comprometendo o atendimento aos requisitos do projeto. Além disso, prejudicam processos críticos, como monitoramento de ações, gerenciamento de contas e rastreamento clínico.

3. Falhas Detectadas – Exemplos e Evidências
3.1 Falta de suporte a alertas na tela de Medicamentos

Evidência:
Durante a tentativa de execução do CT correspondente, constatou-se que a tela de "Medicamentos" permite apenas cadastrar medicamentos, e não há opção de criar alertas ou lembretes.

Impacto:
O caso de teste precisou ser excluído, pois só não é possível configurar "Alertas" na tela de medicamentos, prejudicando a coerência do sistema.

3.2 Função de edição ausente no Histórico Clínico

Evidência:
Durante a tentativa de execução do CT correspondente, verificou-se que não é possível editar registros, apenas visualizá-los.

Exemplo:
Após selecionar um registro e clicar em “Ver detalhes”, nenhuma opção de edição é disponibilizada.

Impacto:
A ausência de edição reduz a funcionalidade da área clínica e limita a capacidade de correção de dados.

3.3 Inconsistências no Feed dos Pacientes

Evidências observadas:

- Feed não exibe dados por paciente;

- Card “Medicamentos do Feed” não está em ordem cronológica;

- Nome do paciente não aparece nos cards;

- Card “Próximos agendamentos” só exibe informações ao clicar em “Ver todos”;

- Card “Alertas” fica vazio, pois não é possível criar alertas na tela de Medicamentos.

Impacto:
A inconsistência prejudica a visualização de informações importantes e compromete a usabilidade da tela.

4. Estratégias de Correção e Ações Propostas

Com base nas falhas identificadas, o grupo propõe as seguintes ações para as próximas iterações:

4.1 Ajustes futuros no Código

Ajustar o backend para garantir ordenação correta e preenchimento dos cards no Feed.

Implementar edição de registros no Histórico Clínico (caso o requisito seja mantido).

4.2 Melhorias na Interface

Reorganizar telas para que funcionalidades relacionadas (ex.: medicamentos) estejam agrupadas de forma coerente.

Exibir mensagens claras de confirmação e erro, evitando frustração do usuário.

Revisar elementos do Feed para melhorar clareza e navegabilidade.

4.3 Otimizações de Desempenho

Garantir carregamento consistente dos cards na Dashboard.

Validar endpoints com maior tráfego (contas, feed, notificações) para reduzir lentidão e falhas.

4.4 Usabilidade e Acessibilidade

Padronizar botões, ícones e rótulos para reduzir ambiguidades.

Incluir estados visuais para ações indisponíveis (ex.: botões desabilitados ao invés de ocultos).

Melhorar hierarquia visual no histórico clínico e no feed.

5. Melhorias Obtidas e Evolução do Projeto

A partir dos testes realizados, foi possível:

- Revisar e alinhar os casos de teste à realidade atual da aplicação.

- Identificar com clareza quais funcionalidades estão completas, quais precisam de ajustes e quais ainda devem ser desenvolvidas.

- Obter uma visão mais precisa sobre o comportamento do sistema em uso real, permitindo maior maturidade no processo de desenvolvimento.


As melhorias propostas têm potencial para:

- Tornar o sistema mais intuitivo e coerente;

- Aumentar sua confiabilidade e consistência;

- Reduzir erros operacionais;

- Melhorar a experiência do usuário final;

- Aproximar o produto dos objetivos definidos no projeto.

6. Conclusão

Os testes foram fundamentais para evidenciar tanto os avanços já alcançados pela aplicação quanto as fragilidades que ainda precisam ser tratadas. As observações coletadas permitiram revisar casos de teste, identificar problemas relevantes e estabelecer um plano de ação concreto para aprimorar o sistema. A partir dos ajustes sugeridos, espera-se uma evolução significativa da solução nas próximas iterações, garantindo maior estabilidade, usabilidade e alinhamento aos requisitos do projeto.


