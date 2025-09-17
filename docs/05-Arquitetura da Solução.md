# Arquitetura da Solução

A arquitetura da solução define como o sistema Click Health será organizado em termos de componentes de software, estrutura de dados e ambiente de hospedagem. Essa etapa é fundamental para assegurar que os requisitos funcionais e não funcionais sejam devidamente atendidos, garantindo escalabilidade, usabilidade e segurança.

## Diagrama de Classes

O diagrama de classes representa graficamente a estrutura lógica do software, destacando as classes, seus atributos e métodos, bem como os relacionamentos entre elas. No Click Health, esse artefato foi elaborado a partir das histórias de usuário e requisitos funcionais, servindo como guia para o desenvolvimento orientado a objetos.

As classes modeladas incluem elementos como Usuário, Paciente, Plano de Cuidado, Agenda, Histórico Clínico e Notificações, refletindo as principais entidades do sistema. Os relacionamentos explicitam como essas entidades interagem para viabilizar a experiência de cuidado colaborativo.

<img src= "https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/412da9b65349d2163d268f6f910bc065a6795826/docs/img/diagramadeclasse.png">
<img width="960" height="309" alt="diagrama_monitoramento - clickhealth" src="https://github.com/user-attachments/assets/83cf2ed6-f3ee-4922-9475-d02ae3591958" />


## Modelo ER (Projeto Conceitual)

O Modelo Entidade-Relacionamento (ER) traduz a estrutura conceitual dos dados, representando entidades (Usuário, Paciente, Exame, Medicação etc.) e seus relacionamentos (ex.: “Usuário gerencia Paciente”, “Paciente possui Exames”).

Esse artefato serve de ponte entre a modelagem orientada a objetos e a modelagem de banco de dados, assegurando que os dados necessários às funcionalidades sejam corretamente estruturados.
O Modelo ER representa através de um diagrama como as entidades (coisas, objetos) se relacionam entre si na aplicação interativa.

Sugestão de ferramentas para geração deste artefato: LucidChart e Draw.io.

A referência abaixo irá auxiliá-lo na geração do artefato “Modelo ER”.

> - [Como fazer um diagrama entidade relacionamento | Lucidchart](https://www.lucidchart.com/pages/pt/como-fazer-um-diagrama-entidade-relacionamento)

# Projeto da Base de Dados - Click Health

O Projeto da Base de Dados tem como finalidade converter o Modelo Entidade-Relacionamento (ER) em um Modelo Relacional, mantendo a coerência com o Diagrama de Classes elaborado anteriormente.
Nesse processo, cada entidade identificada no modelo conceitual é descrita em forma de tabela, com a definição de colunas, chaves primárias (PK), chaves estrangeiras (FK) e restrições de integridade. Essa estrutura garante consistência entre os dados, reduz redundâncias e assegura a integridade referencial do sistema.

### Tabela: **Usuario**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_usuario | INT | PK, AUTO_INCREMENT |
| email | VARCHAR(100) | UNIQUE, NOT NULL |
| senha_hash | VARCHAR(255) | NOT NULL |
| estado | VARCHAR(20) | CHECK (estado IN ('ativo','inativo')) |
| created_at | DATETIME | DEFAULT CURRENT_TIMESTAMP |
| updated_at | DATETIME | ON UPDATE CURRENT_TIMESTAMP |


### Tabela: **Paciente**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_paciente | INT | PK, AUTO_INCREMENT |
| id_usuario | INT | FK → Usuario(id_usuario), NOT NULL |
| condicoes_medicas | TEXT |  |
| dados_pessoais | TEXT |  |


### Tabela: **Cuidador**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_cuidador | INT | PK, AUTO_INCREMENT |
| id_usuario | INT | FK → Usuario(id_usuario), NOT NULL |
| tipo | VARCHAR(50) | CHECK (tipo IN ('familiar','profissional','apoio')) |
| informacoes_experiencia | TEXT |  |


### Tabela: **HistoricoMedico**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_historico | INT | PK, AUTO_INCREMENT |
| id_paciente | INT | FK → Paciente(id_paciente), NOT NULL |
| diagnosticos_passados | TEXT |  |
| alergias | TEXT |  |
| intervencoes | TEXT |  |
| procedimentos | TEXT |  |
| atualizado_em | DATETIME | DEFAULT CURRENT_TIMESTAMP |


### Tabela: **Medicacao**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_medicacao | INT | PK, AUTO_INCREMENT |
| id_paciente | INT | FK → Paciente(id_paciente), NOT NULL |
| nome | VARCHAR(100) | NOT NULL |
| dosagem | VARCHAR(50) | NOT NULL |
| frequencia | VARCHAR(50) |  |
| horario_administracao | DATETIME |  |
 
### Tabela: **MonitoramentoSaude**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_monitoramento | INT | PK, AUTO_INCREMENT |
| id_paciente | INT | FK → Paciente(id_paciente), NOT NULL |
| frequencia_cardiaca | INT |  |
| pressao_arterial | VARCHAR(20) |  |
| temperatura | DECIMAL(4,1) |  |
| glicose | DECIMAL(5,2) |  |
| data_hora | DATETIME | NOT NULL |
| id_dispositivo | INT | FK → Dispositivo(id_dispositivo) |


### Tabela: **Dispositivo**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_dispositivo | INT | PK, AUTO_INCREMENT |
| tipo_dispositivo | VARCHAR(100) | NOT NULL |
| identificacao | VARCHAR(100) | UNIQUE |
| status_conexao | BOOLEAN | DEFAULT TRUE |


### Tabela: **Alerta**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_alerta | INT | PK, AUTO_INCREMENT |
| id_paciente | INT | FK → Paciente(id_paciente), NOT NULL |
| tipo_emergencia | VARCHAR(100) | NOT NULL |
| data_hora | DATETIME | NOT NULL |
| localizacao | VARCHAR(255) |  |
| status | VARCHAR(50) | CHECK (status IN ('aberto','encerrado')) |


### Tabela: **SessaoUsuario**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_sessao | INT | PK, AUTO_INCREMENT |
| id_usuario | INT | FK → Usuario(id_usuario), NOT NULL |
| token_sessao | VARCHAR(255) | UNIQUE, NOT NULL |
| data_inicio | DATETIME | NOT NULL |
| data_fim | DATETIME |  |


### Tabela: **TentativaLogin**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_tentativa | INT | PK, AUTO_INCREMENT |
| id_usuario | INT | FK → Usuario(id_usuario), NOT NULL |
| data_hora | DATETIME | NOT NULL |
| resultado | VARCHAR(20) | CHECK (resultado IN ('sucesso','falha')) |
| ip_origem | VARCHAR(50) |  |


### Tabela: **LogAuditoria**
| Coluna | Tipo | Restrições |
|--------|------|-------------|
| id_log | INT | PK, AUTO_INCREMENT |
| id_usuario | INT | FK → Usuario(id_usuario), NOT NULL |
| acao | VARCHAR(100) | NOT NULL |
| data_hora | DATETIME | NOT NULL |
| descricao | TEXT |  |

Comentários sobre as Tabelas - Projeto da Base de Dados


Tabela Usuario
A tabela Usuario concentra as informações básicas de autenticação e controle de acesso.
- id_usuario: chave primária que identifica unicamente cada usuário.
- email: usado para login; precisa ser único.
- senha_hash: senha criptografada para garantir segurança.
- estado: indica se o usuário está ativo ou inativo no sistema.
- created_at e updated_at: permitem rastrear quando o cadastro foi feito e atualizado.


Tabela Paciente
A tabela Paciente representa usuários cadastrados como pacientes.
- id_paciente: chave primária.
- id_usuario: chave estrangeira que conecta o paciente ao usuário da tabela Usuario.
- condicoes_medicas: histórico geral de condições médicas.
- dados_pessoais: informações como endereço, telefone ou outros dados relevantes.


Tabela Cuidador
A tabela Cuidador identifica usuários que têm papel de cuidado.
- id_cuidador: chave primária.
- id_usuario: chave estrangeira ligada à tabela Usuario.
- tipo: define se o cuidador é familiar, profissional ou de apoio.
- informacoes_experiencia: registra qualificações ou experiências prévias do cuidador.


Tabela HistoricoMedico
Armazena informações clínicas detalhadas do paciente.
- id_historico: chave primária.
- id_paciente: chave estrangeira que relaciona o histórico ao paciente.
- diagnosticos_passados, alergias, intervencoes, procedimentos: registros clínicos importantes.
- atualizado_em: data da última atualização do histórico.


Tabela Medicacao
Registra os medicamentos associados ao paciente.
- id_medicacao: chave primária.
- id_paciente: chave estrangeira conectada ao paciente.
- nome, dosagem e frequencia: detalhes da prescrição médica.
- horario_administracao: horário previsto de administração do medicamento.

## ATENÇÃO!!!

Os três artefatos — **Diagrama de Classes, Modelo ER e Projeto da Base de Dados** — devem ser desenvolvidos de forma sequencial e integrada, garantindo total coerência e compatibilidade entre eles. O diagrama de classes orienta a estrutura e o comportamento do software; o modelo ER traduz essa estrutura para o nível conceitual dos dados; e o projeto da base de dados materializa essas definições no formato físico (tabelas, colunas, chaves e restrições). A construção isolada ou desconexa desses elementos pode gerar inconsistências, dificultar a implementação e comprometer a qualidade do sistema.

## Tecnologias Utilizadas

Para o desenvolvimento da solução, serão utilizadas tecnologias já dominadas pela equipe:

Frontend: HTML5, CSS, C# e JavaScript.

Backend: simulado via LocalStorage ou banco em nuvem simples.

Ferramentas de Versionamento: Git + GitHub (fluxo Gitflow).

Ferramentas de Gestão: GitHub Projects para backlog e Kanban; Microsoft Teams e WhatsApp para comunicação.

Protótipos e Wireframes: Figma / Mermaid.

Diagramas: Lucidchart, Bizagi, Canva.

A imagem abaixo apresenta a interação das tecnologias utilizadas para o desenvolvimento da aplicação.

<img src= "https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/9da4750dbfce38836785c469ae1d224c3f4c260f/docs/img/intereacoes_clickhealth.png">

## Hospedagem

A hospedagem do sistema Click Health será realizada inicialmente em ambiente GitHub Pages, permitindo a publicação direta do protótipo frontend e o acesso público ao sistema por meio de qualquer navegador, sem necessidade de instalação local. Essa escolha garante simplicidade de implantação, custo zero e integração nativa com o repositório GitHub, facilitando o versionamento contínuo e a disponibilização de novas versões para testes e validações.

Como o GitHub Pages suporta apenas aplicações estáticas, o backend nesta fase será simulado por meio do LocalStorage do navegador ou por um banco de dados em nuvem simples, adequado para prototipagem e demonstrações.

Para futuras evoluções, poderá ser considerado a migração para plataformas que ofereçam suporte a aplicações com backend dinâmico, possibilitando a implementação de APIs, autenticação robusta e persistência de dados em bancos de dados relacionais ou não relacionais. Essa estratégia garante que a solução possa começar de forma ágil e enxuta, mas com potencial de escalabilidade para atender cenários reais de uso.
