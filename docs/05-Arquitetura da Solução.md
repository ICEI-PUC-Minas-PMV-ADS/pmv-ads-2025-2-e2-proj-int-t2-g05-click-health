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

## Projeto da Base de Dados

O projeto da base de dados corresponde à representação das entidades e relacionamentos identificadas no Modelo ER, no formato de tabelas, com colunas e chaves primárias/estrangeiras necessárias para representar corretamente as restrições de integridade.
 
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
