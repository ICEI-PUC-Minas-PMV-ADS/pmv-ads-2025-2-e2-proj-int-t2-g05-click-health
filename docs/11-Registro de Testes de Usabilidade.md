# Registro de Testes de Usabilidade

O registro de testes de usabilidade é um documento onde são coletadas e organizadas as informações sobre a experiência dos usuários ao interagir com um sistema. Ele inclui dados como tempo de execução de tarefas, taxa de sucesso, dificuldades encontradas, erros cometidos e feedback dos usuários. Esses registros permitem identificar padrões de uso, obstáculos na interface e oportunidades de melhoria, oferecendo insumos quantitativos e qualitativos para otimizar a experiência do usuário. Também servem como base para análises, correções e iterações futuras do sistema.

---

## Perfil dos usuários participantes

| Usuário | Idade | Escolaridade             | Conhecimento em Tecnologia |
|--------|-------|--------------------------|-----------------------------|
| Usuário 1 | 45 anos | Ensino básico incompleto | Básico                      |
| Usuário 2 | 18 anos | Superior incompleto      | Avançado                    |
| Usuário 3 | 70 anos | Ensino básico incompleto | Básico                      |
| Usuário 4 | 25 anos | Superior completo        | Avançado                    |
| Usuário 5 | 28 anos | Superior completo        | Avançado                    |

---

## Tabelas de registro dos testes de usabilidade

A seguir são apresentados os dados coletados nos três cenários avaliados.

---

# Cenário 1 – Cadastro de Paciente

Objetivo: Avaliar se o cuidador consegue cadastrar um paciente.  
Tarefas: Fazer login e cadastrar paciente.  
Critério de sucesso: Cadastro realizado com sucesso e visualização do paciente.

| Usuário   | Tempo Total (seg) | Quantidade de cliques | Tarefa concluída? | Erros cometidos | Feedback do usuário |
|-----------|--------------------|------------------------|-------------------|------------------|----------------------|
| Usuário 1 | 170                | 22                     | Sim               | 1                | Teve pequena dúvida sobre campos obrigatórios, mas conseguiu concluir. |
| Usuário 2 | 230                | 30                     | Não               | 2                | Não percebeu que alguns campos eram obrigatórios e não finalizou o cadastro. |
| Usuário 3 | 160                | 20                     | Sim               | 0                | Considerou o processo simples, mas sugeriu destacar melhor os campos obrigatórios. |
| Usuário 4 | 210                | 25                     | Sim               | 1                | Só viu o erro de campo obrigatório ao tentar salvar. |
| Usuário 5 | 190                | 23                     | Sim               | 0                | Notou ausência de mensagem clara de confirmação após o cadastro. |

---

# Cenário 2 – Agendamento de Medicação

Objetivo: Verificar a clareza no agendamento de medicamentos e alertas.  
Tarefas: Acessar a aba "Medicação" e cadastrar medicamento com dose, horários e duração.  
Critério de sucesso: Medicação criada corretamente.

| Usuário   | Tempo Total (seg) | Quantidade de cliques | Tarefa concluída? | Erros cometidos | Feedback do usuário |
|-----------|--------------------|------------------------|-------------------|------------------|----------------------|
| Usuário 1 | 220                | 28                     | Sim               | 1                | Demorou a entender que precisava informar a duração do tratamento. |
| Usuário 2 | 260                | 34                     | Não               | 2                | Não percebeu que o horário era obrigatório; faltou indicação visual. |
| Usuário 3 | 240                | 30                     | Sim               | 1                | Teve dúvida entre horário inicial e frequência, mas terminou a tarefa. |
| Usuário 4 | 260                | 32                     | Sim               | 0                | Considerou o fluxo claro, porém recomendou exemplos nos campos. |
| Usuário 5 | 220                | 26                     | Sim               | 1                | Sugeriu uma prévia do resumo da medicação antes de salvar. |

---

# Cenário 3 – Registro de Mudanças Clínicas

Objetivo: Avaliar a facilidade em registrar mudanças clínicas após consulta.  
Tarefas: Acessar "Histórico Clínico" e registrar resultado de exame.  
Critério de sucesso: Dados salvos corretamente.

| Usuário   | Tempo Total (seg) | Quantidade de cliques | Tarefa concluída? | Erros cometidos | Feedback do usuário |
|-----------|--------------------|------------------------|-------------------|------------------|----------------------|
| Usuário 1 | 200                | 22                     | Sim               | 0                | Processo direto, encontrou facilmente o histórico. |
| Usuário 2 | 210                | 24                     | Sim               | 1                | Não percebeu de primeira onde o exame aparecia na tela Detalhes do Registro Clínico. |
| Usuário 3 | 230                | 27                     | Não               | 2                | Não tinha certeza se o dado havia sido salvo, tentou repetir a ação. |
| Usuário 4 | 220                | 25                     | Sim               | 0                | Achou a estrutura do formulário adequada. |
| Usuário 5 | 205                | 23                     | Sim               | 0                | Sugeriu destacar melhor a confirmação de salvamento. |

---

# Relatório dos Testes de Usabilidade

A seguir, apresenta-se a análise das métricas obtidas nos testes, bem como dificuldades encontradas e recomendações de melhoria.

---

## 1. Taxa de Sucesso por Cenário

| Cenário                  | Sucesso | Falha | Taxa de sucesso |
|--------------------------|---------|--------|------------------|
| Cadastro de Paciente     | 4       | 1      | 80%              |
| Agendamento de Medicação | 4       | 1      | 80%              |
| Registro Clínico         | 4       | 1      | 80%              |

A taxa de sucesso de 80% indica que a maior parte dos usuários conseguiu concluir as tarefas, mas ainda há obstáculos relevantes, especialmente para usuários com menor familiaridade tecnológica.

---

## 2. Tempo Médio por Cenário

Cálculo da média considerando os tempos de cada usuário:

- Cenário 1: (170 + 230 + 160 + 210 + 190) / 5 = 192 s  
- Cenário 2: (220 + 260 + 240 + 260 + 220) / 5 = 240 s  
- Cenário 3: (200 + 210 + 230 + 220 + 205) / 5 = 213 s  

| Cenário                  | Tempo médio (seg) |
|--------------------------|--------------------|
| Cadastro de Paciente     | 192 s             |
| Agendamento de Medicação | 240 s             |
| Registro Clínico         | 213 s             |

O Cenário 2 apresentou o maior tempo médio, sugerindo maior complexidade ou menor clareza no fluxo de agendamento.

---

## 3. Número Médio de Erros

Soma de erros por cenário e média por usuário:

- Cenário 1: (1 + 2 + 0 + 1 + 0) = 4 erros → média 0,8 erros/usuário  
- Cenário 2: (1 + 2 + 1 + 0 + 1) = 5 erros → média 1,0 erro/usuário  
- Cenário 3: (0 + 1 + 2 + 0 + 0) = 3 erros → média 0,6 erros/usuário  

| Cenário                  | Média de erros por usuário |
|--------------------------|-----------------------------|
| Cadastro de Paciente     | 0,8 erros                  |
| Agendamento de Medicação | 1,0 erro                   |
| Registro Clínico         | 0,6 erros                  |

Os valores são moderados, mas mostram que ainda existem pontos de confusão, principalmente no agendamento de medicação.

---

## 4. Taxa de Abandono

Taxa de abandono = usuários que não concluíram a tarefa / total de usuários.

Em cada cenário, 1 de 5 usuários não concluiu:

- 1 / 5 = 0,2 → 20%

| Cenário                  | Taxa de abandono |
|--------------------------|-------------------|
| Cadastro de Paciente     | 20%               |
| Agendamento de Medicação | 20%               |
| Registro Clínico         | 20%               |

Uma taxa de 20% é aceitável para testes iniciais, mas indica que ainda há barreiras que precisam ser tratadas, principalmente para perfis com menor experiência digital.

---

![testerf005](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2025-2-e2-proj-int-t2-g05-click-health/blob/main/docs/img/Testes%20de%20usabilidade%20-%20parte%201.gif?raw=true)

---

## 5. Feedback Qualitativo Geral

Principais pontos levantados pelos usuários:

- Em alguns momentos faltou feedback claro de confirmação após salvar ou cadastrar.  
- A indicação de campos obrigatórios não é suficientemente destacada.  
- Alguns usuários tiveram dúvidas ao interpretar campos de dose, duração e horários.  
- A localização das informações no feed nem sempre foi percebida de imediato.  
- Usuários com menor letramento digital demonstraram mais dificuldade nos primeiros contatos, mas conseguiram concluir com algum esforço.

---

# Análise e Identificação de Padrões

### Principais dificuldades enfrentadas

- Dúvidas sobre quais campos são obrigatórios em cadastros e formulários.  
- Falta de destaque para mensagens de confirmação e para itens recém-adicionados.  
- Interpretação de campos relacionados a tempo (duração, horários) no agendamento de medicação.  
- Insegurança quanto ao salvamento efetivo das informações (especialmente no Cenário 3).

### Tarefas concluídas com menos problemas

- Registro no histórico clínico (Cenário 3) apresentou menos erros em média, embora um usuário não tenha concluído por falta de segurança em relação ao salvamento.

### Tarefas com maior esforço

- Agendamento de medicação (Cenário 2) apresentou maior tempo médio e maior média de erros, indicando um fluxo mais complexo ou menos claro.

---

# Classificação dos Problemas por Prioridade

### Problemas Críticos (impedem ou podem impedir o uso em alguns casos)

- Falta de clareza na obrigatoriedade de alguns campos, levando a frustração e, em um caso, à não conclusão da tarefa.  
- Falta de confirmação suficientemente destacada em ações importantes (cadastro, salvamento de registros).

### Problemas Moderados (dificultam, mas não impedem o uso)

- Interpretação de campos de dose, duração e horários no agendamento de medicação.  
- Localização nem sempre evidente do item recém-cadastrado no feed ou lista.  
- Necessidade de reforçar rótulos, textos de ajuda e exemplos nos campos.

### Problemas Leves (melhorias desejáveis)

- Ajustes visuais em botões e elementos de destaque para facilitar a leitura.  
- Melhoria do espaçamento e organização visual dos formulários.  
- Incrementos na comunicação das mensagens de sistema (tom, clareza, consistência).

---

# Recomendações e Ações Propostas

### Ações prioritárias para corrigir problemas críticos

1. Destacar claramente os campos obrigatórios (uso de asterisco, cor diferenciada e legenda explicativa).  
2. Implementar mensagens de confirmação mais evidentes após ações de cadastro e salvamento (por exemplo, banners ou toasts visuais).  
3. Indicar de forma visível quando um registro foi criado ou atualizado, principalmente no feed clínico.

### Melhorias moderadas sugeridas

1. Incluir placeholders e exemplos de preenchimento em campos como dose, duração e horários.  
2. Reforçar a hierarquia visual nas telas, com títulos, subtítulos e seções bem definidas.  
3. Melhorar a indicação de onde as informações salvas aparecem (por exemplo, rolar automaticamente para o item ou destacá-lo brevemente).

### Melhorias leves

1. Aumentar contraste e tamanho de botões principais (como “Salvar”, “Cadastrar” e “Confirmar”).  
2. Revisar espaçamento e alinhamento nos formulários para tornar a leitura mais fluida.  
3. Ajustar o texto das mensagens de erro e confirmação para serm mais diretos e consistentes.

---
