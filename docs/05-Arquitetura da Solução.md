# Arquitetura da Solução

A arquitetura da solução define como o sistema Click Health será organizado em termos de componentes de software, estrutura de dados e ambiente de hospedagem. Essa etapa é fundamental para assegurar que os requisitos funcionais e não funcionais sejam devidamente atendidos, garantindo escalabilidade, usabilidade e segurança.

## Diagrama de Classes

O diagrama de classes representa graficamente a estrutura lógica do software, destacando as classes, seus atributos e métodos, bem como os relacionamentos entre elas. No Click Health, esse artefato foi elaborado a partir das histórias de usuário e requisitos funcionais, servindo como guia para o desenvolvimento orientado a objetos.

As classes modeladas incluem elementos como Usuário, Paciente, Plano de Cuidado, Agenda, Histórico Clínico e Notificações, refletindo as principais entidades do sistema. Os relacionamentos explicitam como essas entidades interagem para viabilizar a experiência de cuidado colaborativo.

<img src= "https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/89da45d34cb23d8c1643d2743da836ac8c2cede0/docs/img/Classe%20UML-%20click%20health.png">

<img width="960" height="309" alt="diagrama_monitoramento - clickhealth" src="https://github.com/user-attachments/assets/7ef4cc0f-d674-452d-91cf-6dae5a88c0fc" />


## Modelo ER (Projeto Conceitual)

O Modelo Entidade-Relacionamento (ER) traduz a estrutura conceitual dos dados, representando entidades (Usuário, Paciente, Exame, Medicação etc.) e seus relacionamentos (ex.: “Usuário gerencia Paciente”, “Paciente possui Exames”).

Esse artefato serve de ponte entre a modelagem orientada a objetos e a modelagem de banco de dados, assegurando que os dados necessários às funcionalidades sejam corretamente estruturados.
O Modelo ER representa através de um diagrama como as entidades (coisas, objetos) se relacionam entre si na aplicação interativa.

Sugestão de ferramentas para geração deste artefato: LucidChart e Draw.io.

A referência abaixo irá auxiliá-lo na geração do artefato “Modelo ER”.

> - [Como fazer um diagrama entidade relacionamento | Lucidchart](https://www.lucidchart.com/pages/pt/como-fazer-um-diagrama-entidade-relacionamento)

## Projeto da Base de Dados

O projeto da base de dados corresponde à representação das entidades e relacionamentos identificadas no Modelo ER, no formato de tabelas, com colunas e chaves primárias/estrangeiras necessárias para representar corretamente as restrições de integridade.
 
Para mais informações, consulte o microfundamento "Modelagem de Dados".

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

A hospedagem do sistema será realizada em ambiente GitHub Pages para o protótipo frontend, permitindo acesso público direto via navegador.
Para futuras evoluções, prevê-se a migração para plataformas com suporte a backend.
> **Links Úteis**:
>
> - [Website com GitHub Pages](https://pages.github.com/)
> - [Programação colaborativa com Repl.it](https://repl.it/)
> - [Getting Started with Heroku](https://devcenter.heroku.com/start)
> - [Publicando Seu Site No Heroku](http://pythonclub.com.br/publicando-seu-hello-world-no-heroku.html)
