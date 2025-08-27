# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

Definição do problema e ideia de solução a partir da perspectiva do usuário. É composta pela definição do  diagrama de personas, histórias de usuários, requisitos funcionais e não funcionais além das restrições do projeto.

Apresente uma visão geral do que será abordado nesta parte do documento, enumerando as técnicas e/ou ferramentas utilizadas para realizar a especificações do projeto


## Personas

Identifique, em torno de, 5 personas. Para cada persona, lembre-se de descrever suas angústicas, frustrações e expectativas de vida relacionadas ao problema. Além disso, defina uma "aparência" para a persona. Para isso, você poderá utilizar sites como [https://this-person-does-not-exist.com/pt#google_vignette](https://this-person-does-not-exist.com/pt) ou https://thispersondoesnotexist.com/ 

O público-alvo do projeto abrange diversos perfis de usuários dentro do ecossistema de cuidado domiciliar de saúde:

Cuidadores familiares principais: geralmente filhos, cônjuges ou outros parentes próximos que assumem a coordenação do cuidado de um paciente. São usuários que criarão as contas no sistema, cadastrarão os pacientes sob seus cuidados e gerenciarão as informações principais. Em muitos casos, podem ser indivíduos de meia-idade responsáveis por pais idosos, ou mães/pais de crianças com necessidades especiais de saúde. Esse perfil busca uma ferramenta para organizar todas as tarefas de cuidado, compartilhar responsabilidades com outros familiares e acompanhar de perto a situação de saúde do paciente.

Cuidadores familiares secundários ou de apoio: incluem outros parentes, amigos próximos ou vizinhos que auxiliam no cuidado, porém não centralizam as decisões. Também engloba cuidadores informais que ajudam eventualmente (por exemplo, irmãos que revezam turnos de cuidado, netos auxiliando avós, etc.). Esse público necessita acessar informações atualizadas sobre o paciente, cumprir tarefas designadas (como levar a consultas ou administrar medicação em determinados horários) e comunicar aos demais qualquer observação relevante, mesmo que não estejam envolvidos no dia a dia constantemente.

Cuidadores profissionais contratados: profissionais de saúde ou cuidadores formais (por exemplo, técnicos de enfermagem, home care) que são contratados pela família para prover assistência ao paciente em casa. Embora não sejam membros da família, eles precisam integrar-se ao Click Health do paciente. Para esses usuários, o sistema oferece acesso às rotinas e históricos médicos necessários para desempenhar seu trabalho de forma informada, além de permitir registro de ocorrências ou observações durante seus turnos (conforme as permissões concedidas pela família).

Paciente (usuário indireto): o paciente em si, quando tiver condições cognitivas e físicas, também é beneficiário e potencial usuário indireto do sistema. Por exemplo, um idoso que use smartphones poderia consultar sua própria agenda de medicamentos e ser notificado, aumentando sua autonomia. Contudo, na maioria dos cenários o paciente será o sujeito do cuidado e não operará diretamente o sistema; ainda assim, todo o desenho da solução é centrado em melhorar a assistência e, portanto, o paciente é a figura central a ser beneficiada pelo uso coordenado da plataforma pelos cuidadores.

Foram levantadas as seguintes personas:


|APARÊNCIA| NOME |DESCRIÇÃO                 |
|--------------------|------------------------------------|----------------------------------------|
|  |  Maria Clara Andrade          |   Maria Clara é uma mulher da geração Baby Boomer, simpática e muito ligada à família. Vive com o marido e depende do apoio dos filhos para a rotina de consultas e medicação. Apesar de usar o celular, sente dificuldade com tecnologias complexas.  <br>- Idade: 67 anos </br> - Profissão: Aposentada (professora) </br> - Localização: Belo Horizonte, MG</br> - Formação: Pedagogia </br> Objetivo: Ter suporte confiável no gerenciamento de medicamentos e consultas médicas.</br> |
|<b> APARÊNCIA - DESCRIÇÃO VISUAL| <b>DORES | <b> EXPECTATIVA           |
| Mulher de cabelos grisalhos, pele clara, óculos discretos, sorriso gentil, estilo casual com blusa clara e colar simples. |  Medo de esquecer horários de medicamentos ou consultas.<br> Dificuldade de comunicação com todos os cuidadores, que trocam mensagens por canais diferentes.<br> Ansiedade por não saber quem está acompanhando suas necessidades diárias          |     Aplicativo simples e acessível, com lembretes claros. <br> Interface com botões grandes e mensagens fáceis de entender. <br> Tranquilidade em saber que seus filhos e netos estão coordenados em tempo real.         |

|APARÊNCIA| NOME |DESCRIÇÃO                 |
|--------------------|------------------------------------|----------------------------------------|
|  |    Lucas Henrique Moura      |  Lucas é da geração Z, curioso e acostumado com tecnologia. Usa o celular para quase tudo e quer contribuir no cuidado do avô, organizando tarefas simples como acompanhar horários de medicação e alertar os cuidadores mais velhos.  <br>- Idade: 18 anos </br> - Profissão: Estudante  </br> - Localização: Rio de Janeiro, RJ </br> - Formação: Início da graduação em Administração  </br> Objetivo: Ajudar os pais no cuidado do avô com Alzheimer.</br> |
|<b> APARÊNCIA - DESCRIÇÃO VISUAL| <b>DORES | <b> EXPECTATIVA           |
| Rapaz jovem, pele morena, cabelo ondulado curto, camiseta preta, mochila esportiva e fone sem fio. | Falta de clareza sobre o que já foi feito pelos outros cuidadores. <br> Frustração com atrasos e falhas na comunicação em grupo. Ansiedade para contribuir de forma mais efetiva no cuidado familiar. <br>           |    Aplicativo moderno e intuitivo, com interface próxima das redes sociais.  <br>   Alertas e feedback visual sobre as tarefas realizadas.      |


|APARÊNCIA| NOME |DESCRIÇÃO                 |
|--------------------|------------------------------------|----------------------------------------|
|  |     João Pedro Lima     |  João é da geração Millennial. Conectado, prático e multitarefa, trabalha em home office e organiza parte da rotina de saúde da mãe. É responsável pelos agendamentos e comunicação com profissionais de saúde.   <br>- Idade: 38 anos  </br> - Profissão: Engenheiro  </br> - Localização: São Paulo, SP </br> - Formação: Engenharia da Computação </br> Objetivo: Integrar tecnologia e organização para cuidar da mãe idosa.|
|<b> APARÊNCIA - DESCRIÇÃO VISUAL| <b>DORES | <b> EXPECTATIVA           |
| Homem jovem, cabelo castanho curto, pele clara, barba por fazer, camisa polo azul e smartwatch no pulso. | Falta de integração de informações em um único sistema. <br> Perda de tempo conferindo mensagens e e-mails em múltiplos canais.
 <br>  Ansiedade em caso de emergências quando não está fisicamente presente         |    Painel centralizado para organizar compromissos e alertas.  <br> Confiabilidade nas notificações e histórico atualizado em tempo real. <br>   Recursos que permitam monitorar tudo mesmo à distância.     |

 
|APARÊNCIA| NOME |DESCRIÇÃO                 |
|--------------------|------------------------------------|----------------------------------------|
|  |      Gabriela Santos    |  Gabriela é da geração Millennial/Z. Empática, dedicada e organizada, atende até 8 pacientes diariamente. Precisa de ferramentas rápidas que facilitem o registro de informações clínicas durante os atendimentos.   <br>- Idade: 28 anos  </br> - Profissão: Enfermeira home care </br> - Localização: Belo Horizonte, MG </br> - Formação: Enfermagem  </br> Objetivo: Registrar informações de saúde de pacientes de forma prática e padronizada.
</br> |
|<b> APARÊNCIA - DESCRIÇÃO VISUAL| <b>DORES | <b> EXPECTATIVA           |
| Mulher jovem, pele morena clara, cabelo castanho preso em coque, uniforme branco com jaleco limpo e estetoscópio no pescoço. | Falta de padronização na comunicação entre cuidadores da mesma família.  <br> Dificuldade para registrar e atualizar informações rapidamente no celular.
 <br>  Necessidade de histórico confiável para embasar decisões médicas.         |  Interface prática e rápida para registrar doses, sintomas e orientações médicas.    <br>   Histórico organizado e acessível para todos os cuidadores. <br>  Redução de falhas de comunicação entre profissionais e familiares.    |

 
|APARÊNCIA| NOME |DESCRIÇÃO                 |
|--------------------|------------------------------------|----------------------------------------|
|  |  Helena Martins        |  Helena é da geração X, profissional, focada em resultados, mas com sensibilidade para lidar com idosos e famílias. Busca ferramentas seguras para acessar informações confiáveis sem burocracia.   <br>- Idade: 45 anos  </br> - Profissão: Médica </br> - Localização: Porto Alegre, RS </br> - Formação: Medicina, especialização em geriatria  </br> Objetivo: Acompanhar remotamente o histórico de saúde dos pacientes. </br> |
|<b> APARÊNCIA - DESCRIÇÃO VISUAL| <b>DORES | <b> EXPECTATIVA           |
| Mulher de pele clara, cabelo castanho médio, jaleco branco elegante, óculos de grau, postura confiante.  | Dificuldade em consolidar informações enviadas por múltiplos cuidadores. <br> Perda de tempo pedindo dados ou atualizações por canais informais. <br>           |  Plataforma confiável com relatórios completos e exportáveis.    <br>    Interface simples para acesso rápido a históricos e atualizações.     |

 
|APARÊNCIA| NOME |DESCRIÇÃO                 |
|--------------------|------------------------------------|----------------------------------------|
|  |          |     <br>- Idade:  </br> - Profissão:  </br> - Localização: </br> - Formação:  </br> Objetivo: 
</br> |
|<b> APARÊNCIA - DESCRIÇÃO| <b>DORES | <b> EXPECTATIVA           |
|  |  <br> 
 <br>           |      <br>         |

Utilize também como referência o exemplo abaixo:

<img src="https://github.com/ICEI-PUC-Minas-PMV-ADS/IntApplicationProject-Template/blob/main/docs/img/AnaClara1.png" alt="Persona1"/>

Enumere e detalhe as personas da sua solução. Para tanto, baseie-se tanto nos documentos disponibilizados na disciplina e/ou nos seguintes links:

> **Links Úteis**:
> 
> - [Rock Content](https://rockcontent.com/blog/personas/)
> - [Hotmart](https://blog.hotmart.com/pt-br/como-criar-persona-negocio/)
> - [O que é persona?](https://resultadosdigitais.com.br/blog/persona-o-que-e/)
> - [Persona x Público-alvo](https://flammo.com.br/blog/persona-e-publico-alvo-qual-a-diferenca/)
> - [Mapa de Empatia](https://resultadosdigitais.com.br/blog/mapa-da-empatia/)
> - [Mapa de Stalkeholders](https://www.racecomunicacao.com.br/blog/como-fazer-o-mapeamento-de-stakeholders/)
>
Lembre-se que você deve ser enumerar e descrever precisamente e personalizada todos os clientes ideais que sua solução almeja.

## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
| Cuidador familiar principal |     Cadastrar múltiplos pacientes na minha conta para gerenciar em um só lugar os cuidados de todos os meus familiares dependentes.       |      facilitar a organização e acesso às informações.          |
|   Cuidador familiar principal    |       consultar um log de auditoria que mostre todas as edições feitas nos dados do paciente (quem alterou, o quê e quando)           | ter transparência sobre as modificações e poder esclarecer quaisquer divergências de informação que apareçam. |
| Cuidador familiar principal |     compartilhar o perfil de um paciente com outros cuidadores definindo se cada um pode editar ou apenas visualizar os dados       |       dividir responsabilidades com segurança e sem expor dados além do necessário         |
|   Cuidador |           agendar consultas, exames e horários de medicação no sistema, incluindo lembretes automáticos       | esquecer compromissos importantes nem doses de medicamentos, melhorando a adesão ao tratamento do paciente. | 
| Cuidador | receber notificações no meu celular ou e-mail antes dos horários críticos (como lembrete de dar um medicamento ou da aproximação de uma consulta)  | antecipar e preparar o que for necessário, reduzindo o risco de falhas no cuidado.           |     
| Cuidador profissional          | registrar no histórico do paciente qualquer alteração ou observação clínica | que essa informação fique documentada e disponível imediatamente a todos os cuidadores envolvidos.|
|  Cuidador de apoio                | visualizar um feed de atualizações recentes do paciente ao entrar no aplicativo | inteirar rapidamente do que aconteceu nos últimos dias (como novos medicamentos iniciados ou eventos adversos), sem precisar ligar para o cuidador principal para pedir um resumo. |  

Apresente aqui as histórias de usuário que são relevantes para o projeto de sua solução. As Histórias de Usuário consistem em uma ferramenta poderosa para a compreensão e elicitação dos requisitos funcionais e não funcionais da sua aplicação. Se possível, agrupe as histórias de usuário por contexto, para facilitar consultas recorrentes à essa parte do documento.

> **Links Úteis**:
> - [Histórias de usuários com exemplos e template](https://www.atlassian.com/br/agile/project-management/user-stories)
> - [Como escrever boas histórias de usuário (User Stories)](https://medium.com/vertice/como-escrever-boas-users-stories-hist%C3%B3rias-de-usu%C3%A1rios-b29c75043fac)
> - [User Stories: requisitos que humanos entendem](https://www.luiztools.com.br/post/user-stories-descricao-de-requisitos-que-humanos-entendem/)
> - [Histórias de Usuários: mais exemplos](https://www.reqview.com/doc/user-stories-example.html)
> - [9 Common User Story Mistakes](https://airfocus.com/blog/user-story-mistakes/)

## Requisitos

 Com base nos objetivos e nas histórias de usuários, definimos os requisitos do projeto divididos em funcionais e não funcionais. Os requisitos funcionais descrevem as funcionalidades que o sistema Click Health deve oferecer, enquanto os não funcionais dizem respeito a propriedades de qualidade, desempenho e restrições do sistema.

 As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001|Cadastrar, visualizar, atualizar e excluir (CRUD)* múltiplos pacientes por usuário, permitindo que um cuidador gerencie diversos perfis sob sua conta. | Alta | 
|RF-002| Compartilhar o acesso de cada paciente com outros usuários e    | Alta |
|RF-003| Permitir o usuário configurar permissões (somente leitura ou leitura/escrita) por paciente. | Alta  |
|RF-004| Registrar compromissos de saúde (consultas, exames)  | Alta | 
|RF-005|  Programar horários de medicação por paciente, com lembretes e alertas de compra de insumos.  | Alta  |
|RF-006| Enviar notificações/alertas a cuidadores sobre eventos agendados, com lembretes configuráveis. | Alta |
|RF-007| Registrar (CRUD)* histórico clínico por paciente: alterações de medicação (com data e responsável), resultados de exames, internações e anotações clínicas. | Alta | 
|RF-008|  Exibir feed de atualizações por paciente, em ordem cronológica, com registro automático de ações.  | Alta |
|RF-009| Manter trilha de auditoria (CRUD)* de operações em dados do paciente (quem, o que, quando, antes/depois). | Alta |
|RF-010| Autenticar usuários com login seguro e gerenciar convites (CRUD) e revogação de acessos por paciente.| Alta | 
|RF-011| Editar (CRUD)* dados cadastrais do paciente (nome, data de nascimento, contatos, informações médicas básicas), respeitando permissões.   | Média |
|RF-012| Gerar relatórios / exportar dados do paciente (PDF/CSV/ZIP com anexos) para compartilhar com profissionais. | Média |

*CRUD: criar, ler, atualizar ou excluir dados.

### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| Segurança e Privacidade: O sistema deve proteger os dados sensíveis de saúde armazenados em conformidade com a LGPD (Lei Geral de Proteção de Dados).| Alta | 
|RNF-002| Usabilidade e Acessibilidade: A interface do usuário deve ser intuitiva e de fácil navegação, considerando que muitos cuidadores não têm treinamento técnico. Devem ser seguidas diretrizes de design acessível (por exemplo, contraste adequado, fontes legíveis, suporte a leitores de tela) para atender também usuários com eventuais limitações visuais ou motoras. | Alta  | 
|RNF-003| Disponibilidade: O sistema deve ter alta disponibilidade, visando estar acessível 24 horas por dia, 7 dias por semana, já que os cuidados de pacientes ocorrem continuamente. Manutenções programadas devem ser comunicadas antecipadamente e minimizadas para não prejudicar o acompanhamento dos pacientes. | Alta | 
|RNF-004| Desempenho: As principais funções (carregar o perfil do paciente, atualizar o feed, salvar um novo registro) devem ocorrer com rapidez. O sistema deve suportar simultaneamente diversos cuidadores acessando informações de um mesmo paciente sem degradação perceptível de desempenho. | Média  | 
|RNF-005| Escalabilidade: A solução deve ser desenvolvida com arquitetura escalável, capaz de suportar um aumento no número de usuários e de dados ao longo do tempo. Isso implica usar boas práticas de desenvolvimento para que o sistema mantenha desempenho adequado. | Média | 
|RNF-006| Portabilidade e Facilidade de Acesso: O sistema deve ser acessível diretamente via navegador de internet, funcionando em computadores, smartphones e tablets, sem necessidade de instalação complexa, garantindo praticidade para cuidadores e pacientes. | Média  | 
|RNF-007| Manutenibilidade e Evolutividade: O software deve ser desenvolvido seguindo padrões de código e arquitetura que facilitem sua manutenção e evolução. Isso inclui documentação clara, modularização de componentes e uso de frameworks conhecidos.  | Baixa | 
|RNF-008| Confiabilidade dos Dados: Além da segurança, o sistema deve garantir integridade transacional – por exemplo, se dois usuários tentarem editar simultaneamente a mesma informação, deve haver controle de concorrência para evitar inconsistências. Os registros de auditoria não podem ser alterados indevidamente, assegurando confiabilidade em quem fez cada alteração. | Alta  | 
|RNF-009| Backup e Recuperação de Dados: O sistema deve manter cópias de segurança periódicas das informações dos pacientes e possibilitar a restauração em caso de falha ou perda. | Alta |  

Com base nas Histórias de Usuário, enumere os requisitos da sua solução. Classifique esses requisitos em dois grupos:

- [Requisitos Funcionais
 (RF)](https://pt.wikipedia.org/wiki/Requisito_funcional):
 correspondem a uma funcionalidade que deve estar presente na
  plataforma (ex: cadastro de usuário).
- [Requisitos Não Funcionais
  (RNF)](https://pt.wikipedia.org/wiki/Requisito_n%C3%A3o_funcional):
  correspondem a uma característica técnica, seja de usabilidade,
  desempenho, confiabilidade, segurança ou outro (ex: suporte a
  dispositivos iOS e Android).
Lembre-se que cada requisito deve corresponder à uma e somente uma
característica alvo da sua solução. Além disso, certifique-se de que
todos os aspectos capturados nas Histórias de Usuário foram cobertos.

## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrição                                             |
|--|-------------------------------------------------------|
|01| O projeto deverá ser entregue até o final do semestre |
|02| Não será desenvolvido um módulo de backend completo nesta fase; o sistema funcionará com armazenamento local (LocalStorage ou banco em nuvem simples).      |
|03| O escopo inicial não contemplará integrações externas com sistemas de saúde        |
|04| O sistema não deverá funcionar offline .        |
|05| O design da interface deverá seguir padrões minimalistas e de fácil usabilidade.        |
|06| O projeto deverá ser desenvolvido utilizando apenas tecnologias já dominadas pela equipe (HTML, CSS, C#).        |
|07| A segurança será básica, com autenticação simples (e-mail e senha).        |

Enumere as restrições à sua solução. Lembre-se de que as restrições geralmente limitam a solução candidata.

> **Links Úteis**:
> - [O que são Requisitos Funcionais e Requisitos Não Funcionais?](https://codificar.com.br/requisitos-funcionais-nao-funcionais/)
> - [O que são requisitos funcionais e requisitos não funcionais?](https://analisederequisitos.com.br/requisitos-funcionais-e-requisitos-nao-funcionais-o-que-sao/)

## Diagrama de Casos de Uso

O diagrama de casos de uso é o próximo passo após a elicitação de requisitos, que utiliza um modelo gráfico e uma tabela com as descrições sucintas dos casos de uso e dos atores. Ele contempla a fronteira do sistema e o detalhamento dos requisitos funcionais com a indicação dos atores, casos de uso e seus relacionamentos. 

As referências abaixo irão auxiliá-lo na geração do artefato “Diagrama de Casos de Uso”.

> **Links Úteis**:
> - [Criando Casos de Uso](https://www.ibm.com/docs/pt-br/elm/6.0?topic=requirements-creating-use-cases)
> - [Como Criar Diagrama de Caso de Uso: Tutorial Passo a Passo](https://gitmind.com/pt/fazer-diagrama-de-caso-uso.html/)
> - [Lucidchart](https://www.lucidchart.com/)
> - [Astah](https://astah.net/)
> - [Diagrams](https://app.diagrams.net/)
