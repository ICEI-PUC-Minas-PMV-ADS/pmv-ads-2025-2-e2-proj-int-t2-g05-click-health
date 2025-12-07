# Plano de Testes de Usabilidade

Os testes de usabilidade permitem avaliar a qualidade da interface com o usuário da aplicação interativa.

Um plano de teste de usabilidade deverá conter: 

## Definição do(s) objetivo(s)

Os testes de usabilidade visam:

- Verificar se os usuários conseguem concluir tarefas essenciais sem dificuldades.
- Identificar barreiras na navegação e interação com o sistema.
- Avaliar a eficiência e a satisfação do usuário ao utilizar a interface.
- Testar a acessibilidade para diferentes perfis de usuários.

## Seleção dos participantes

Para garantir que o teste reflita o uso real do sistema, escolha participantes representativos do público-alvo.

- Cuidadores experientes e iniciantes no sistema.
- Familiares com diferentes níveis de familiaridade tecnológica.
- Usuários com deficiência visual leve ou motora (para testes de acessibilidade).

**Critérios para selecionar participantes:**

| Critério                         | Detalhes                                                        |
|----------------------------------|------------------------------------------------------------------|
| Experiência com tecnologia       | Alta, média e baixa                                              |
| Papel do usuário no sistema      | Cuidador, familiar, administrador                               |
| Necessidades especiais           | Pelo menos 1 participante com baixa visão ou limitação motora   |

**Quantidade recomendada:**

- Mínimo: 5 participantes
- Ideal: 8 a 12 participantes


## Definição de cenários de teste

Os cenários representam tarefas reais que os usuários executam no sistema. Neste projeto, cada grupo deverá definir, no mínimo, **CINCO cenários para a aplicação** e cada cenário deve incluir:

- Objetivo: O que será avaliado.
- Contexto: A situação que leva o usuário a interagir com o sistema.
- Tarefa: A ação que o usuário deve realizar.
- Critério de sucesso: Como determinar se a tarefa foi concluída corretamente.

**Cenário 1**

- **Objetivo:** Avaliar se o cuidador consegue cadastrar um paciente.
- **Contexto:** Um cuidador inicia o uso do sistema e precisa adicionar um novo paciente.
- **Tarefas:**

  **1** Fazer login.  
  **2** Cadastrar paciente.  
  
- **Critério de Sucesso:**
  - Cadastro realizados com sucesso, sem ajuda externa.
  - Familiar consegue visualizar o paciente.


  
 **Cenário 2**
- **Objetivo:** Verificar a clareza no agendamento de medicamentos. 
- **Contexto:** Um cuidador precisa agendar medicamentos.
- **Tarefas:**

  **1** Acessar a aba "Medicação".  
  **2** Adicionar medicamento com dose, horários e duração.  

  
- **Critério de Sucesso:**
  - Medicação criada corretamente.  
 

**Cenário 3**
- **Objetivo:** Avaliar a facilidade em registrar mudanças clínicas do paciente.
- **Contexto:** Após uma consulta, o cuidador registra uma mudança de resultado de exame.
- **Tarefas:**

 
 **1**  Acessar "Histórico Clínico".
 **2** Registrar resultado de exame.


- **Critério de Sucesso:**
  - Dados salvos corretamente.
  - Informações exibidas corretamente no feed.

  

**Critério de Sucesso:**

- A alteração nos dados do paciente é salva corretamente.

- Um registro detalhado da ação aparece na trilha de auditoria.

- O registro contém o nome do usuário que realizou a ação, a data/hora, o tipo de ação e os valores antigo e novo do dado modificado.

- A trilha de auditoria é de fácil acesso e compreensão.

## Métodos de coleta de dados

| **Métrica**                  | **Descrição**                                                                 |
|-----------------------------|------------------------------------------------------------------------------|
|  Taxa de Sucesso           | % de usuários que concluíram cada tarefa com sucesso                        |
|  Tempo por Tarefa          | Tempo médio para completar cada cenário                                     |
|  Número de Erros           | Cliques errados, confusões, campos ignorados                                |
|  Nível de Satisfação       | Avaliação subjetiva (Escala de 1 a 5 ou emojis) após cada tarefa            |
|  Comentários e Dificuldades| Observações qualitativas durante a execução das tarefas             


**Ferramentas Utilizadas** 

- Ambiente: Versão estável da aplicação web
- Registro: Anotações em planilha + gravação de tela (opcional)
- Coleta de feedback: Google Forms ou formulário de avaliação
- Ferramentas de acessibilidade: NVDA, VoiceOver


As referências abaixo irão auxiliá-lo na geração do artefato "Plano de Testes de Usabilidade".

> **Links Úteis**:
> - [Teste De Usabilidade: O Que É e Como Fazer Passo a Passo (neilpatel.com)](https://neilpatel.com/br/blog/teste-de-usabilidade/)
> - [Teste de usabilidade: tudo o que você precisa saber! | by Jon Vieira | Aela.io | Medium](https://medium.com/aela/teste-de-usabilidade-o-que-voc%C3%AA-precisa-saber-39a36343d9a6/)
> - [Planejando testes de usabilidade: o que (e o que não) fazer | iMasters](https://imasters.com.br/design-ux/planejando-testes-de-usabilidade-o-que-e-o-que-nao-fazer/)
> - [Ferramentas de Testes de Usabilidade](https://www.usability.gov/how-to-and-tools/resources/templates.html)
