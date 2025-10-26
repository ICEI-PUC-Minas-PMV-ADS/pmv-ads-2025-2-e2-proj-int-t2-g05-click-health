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

- **Objetivo:** Avaliar se o cuidador consegue cadastrar um paciente e compartilhar o acesso com outro usuário.
- **Contexto:** Um cuidador inicia o uso do sistema e precisa adicionar um novo paciente, concedendo acesso ao familiar.
- **Tarefas:**

  **1** Fazer login.
  **2** Cadastrar paciente.
  **3** Compartilhar o acesso com um familiar (modo leitura).
  
- **Critério de Sucesso:**
  - Cadastro e compartilhamento realizados com sucesso, sem ajuda externa.
  - Familiar consegue visualizar o paciente.


  
 **Cenário 2**
- **Objetivo:** Verificar a clareza no agendamento de medicamentos e configuração de alertas.
- **Contexto:** Um cuidador precisa agendar medicamentos e lembretes diários para um paciente.
- **Tarefas:**
- 
  **1** Acessar a aba "Medicação".
  **2** Adicionar medicamento com dose, horários e duração.
  **3** Configurar lembrete.
  
- **Critério de Sucesso:**
  - Medicação e alertas criados corretamente.
  - Lembretes são recebidos no tempo definido.
 

**Cenário 3**
- **Objetivo:** Avaliar a facilidade em registrar mudanças clínicas do paciente.
- **Contexto:** Após uma consulta, o cuidador registra uma mudança de resultado de exame.
- **Tarefas:**

 
 **1**  Acessar "Histórico Clínico".
 **2** Registrar resultado de exame.


- **Critério de Sucesso:**
  - Dados salvos corretamente.
  - Informações exibidas corretamente no feed.



  **Cenário 4**
  
**Objetivo:** Avaliar a eficiência e clareza da funcionalidade de exportação de dados do paciente para diferentes formatos.

**Contexto:** Um cuidador precisa gerar um relatório completo de um paciente para compartilhar com um médico especialista ou para arquivamento pessoal.

**Tarefas:**

 **1** Fazer login.

 **2** Acessar o perfil de um paciente que contenha histórico de medicações, compromissos e anotações.

 **3** Selecionar a opção "Exportar Dados".

 **4** Escolher o formato PDF e iniciar o download.

 **5** Escolher o formato CSV e iniciar o download.

 **6** Abrir os arquivos exportados e verificar se o conteúdo está completo e organizado.

**Critério de Sucesso:**

- Os arquivos são gerados e baixados com sucesso para os formatos PDF e CSV.

- O conteúdo dos arquivos reflete todos os dados relevantes do paciente (histórico, medicações, compromissos).

- A interface de exportação é intuitiva e o processo é concluído sem erros.

**Cenário 5**

**Objetivo:** Verificar a usabilidade e a precisão da trilha de auditoria para monitorar ações críticas do cuidador.

**Contexto:** Um cuidador realiza uma alteração importante nos dados cadastrais de um paciente e precisa confirmar que essa ação foi devidamente registrada para fins de conformidade ou verificação posterior.

**Tarefas:**

**1** Fazer login.

**2** Acessar o perfil de um paciente.

**3** Editar um dado cadastral básico (ex: mudar o sobrenome ou a data de nascimento) e salvar a alteração.

**4** Navegar até a área de "Trilha de Auditoria" ou "Logs de Atividade" (localizada, por exemplo, nas configurações ou em uma seção de administração).

**5** Localizar o registro da alteração recém-realizada.

**6** Verificar os detalhes do registro, incluindo quem fez, o que foi alterado, quando e os valores antes e depois da modificação.

**Critério de Sucesso:**

- A alteração nos dados do paciente é salva corretamente.

- Um registro detalhado da ação aparece na trilha de auditoria.

- O registro contém o nome do usuário que realizou a ação, a data/hora, o tipo de ação e os valores antigo e novo do dado modificado.

- A trilha de auditoria é de fácil acesso e compreensão.


<img width="1366" height="728" alt="dados" src="https://github.com/user-attachments/assets/eeed85c4-2f16-49c6-a3de-d05716eb0f01" />

<img width="1366" height="760" alt="novo paciente" src="https://github.com/user-attachments/assets/9bb42d2d-9140-4337-8b96-f82cc2aec963" />

<img width="1366" height="728" alt="feed" src="https://github.com/user-attachments/assets/90c578b6-bb4a-4c93-8ccb-b4588f72ede1" />

<img width="1366" height="728" alt="dados" src="https://github.com/user-attachments/assets/f717db58-26b6-47d5-84b0-73cfc45003c5" />

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
