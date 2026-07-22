# Revisão técnica do protótipo — 2026-07-22

## Escopo

Revisão estática do protótipo Unity, cruzando o código em
`unity/Assets/_Project/Scripts/` com as regras descritas em `docs/`.

Foram avaliados principalmente:

- integridade da economia e do inventário;
- concorrência entre drones e rotas;
- persistência e produção offline;
- consistência entre ações manuais e automáticas;
- cobertura de testes.

O Unity não registrava erros de compilação durante a revisão. Não havia testes
automatizados no projeto. Nenhuma correção de código faz parte desta auditoria.

## Resumo

| ID | Severidade | Categoria | Achado | Estado |
|---|---|---|---|---|
| AUD-001 | Crítica | Economia / concorrência | Drones podem duplicar recursos e sementes | Confirmado |
| AUD-002 | Alta | Inventário | Colheitas podem desaparecer com inventário cheio | Confirmado |
| AUD-003 | Alta | Inventário / estruturas | Demolir o armazém mantém a capacidade concedida | Confirmado |
| AUD-004 | Alta | Design / economia | Plantio e construção não possuem custo | Decisão pendente |
| AUD-005 | Média | Persistência | Configurações dos hangares não são salvas | Confirmado |
| AUD-006 | Média | Persistência | Save não é atômico nem tolerante a corrupção | Confirmado |
| AUD-007 | Média | Produção offline | Relógio local permite manipular a ausência | Confirmado |
| AUD-008 | Média | Qualidade | Não existem testes automatizados | Confirmado |
| AUD-009 | Crítica | Drones / persistência | Carga em trânsito pode desaparecer ao salvar ou fechar | Confirmado |
| AUD-010 | Alta | Produção / capacidade | Ciclo pode consumir insumo e perder parte do produto | Confirmado |
| AUD-011 | Alta | Drones / rotas | Rotas atendem somente à primeira estrutura de cada tipo | Confirmado |
| AUD-012 | Alta | Drones / demolição | Demolição durante uma missão pode quebrar a rota e perder carga | Confirmado |
| AUD-013 | Alta | Interação / UI | Cliques na interface podem atingir o terreno atrás dela | Confirmado |
| AUD-014 | Média | UI / ciclo de vida | Janelas de estruturas demolidas permanecem registradas | Confirmado |
| AUD-015 | Média | Diagnóstico / arquivos | Log pode falhar em instalações sem permissão de escrita | Confirmado |
| AUD-016 | Baixa | UI | Mina de Pedra não recebe rótulo de identificação | Confirmado |

## AUD-001 — Drones podem duplicar recursos e sementes

- **Severidade:** Crítica
- **Estado:** Confirmado
- **Categoria:** Economia / concorrência
- **Evidência:** `HangarDeDrones.cs`, métodos `TryStartPlantTrip` e
  `TryRunDeliveryRoute`.

A viagem de transporte calcula a carga com base no estoque disponível, mas a
remoção acontece mais tarde, quando o drone chega ao armazém. O retorno de
`PlayerInventory.TryRemove` é ignorado e a carga previamente calculada é
entregue mesmo quando a remoção falha.

Duas rotas ou dois hangares podem observar o mesmo estoque e iniciar viagens
simultâneas. A primeira viagem consome o recurso; a segunda falha ao removê-lo,
mas ainda deposita a carga completa no destino. O mesmo padrão existe no
plantio: dois drones podem observar a última semente, apenas um removê-la e
ambos plantarem.

**Correção sugerida:** representar cada drone como uma máquina de estados
persistente, com identidade, missão atual, origem, destino, carga própria,
capacidade, horário de início e previsão de chegada. O recurso só sai da origem
quando o drone efetivamente o coleta e, a partir desse momento, passa a
pertencer ao armazenamento do drone. O destino deve receber apenas a quantidade
que o drone realmente carrega. A missão e a carga devem sobreviver ao save e
continuar durante a ausência, conforme detalhado em `AUD-009`.

## AUD-002 — Colheitas podem desaparecer com inventário cheio

- **Severidade:** Alta
- **Estado:** Confirmado
- **Categoria:** Inventário
- **Evidência:** `ClickController.cs`, método `HandleClick`; e
  `HangarDeDrones.cs`, método `TryStartHarvestTrip`.

A colheita remove o cultivo do tile e depois chama `PlayerInventory.Add`. Como
o retorno de `Add` é ignorado, toda quantidade que não couber no inventário é
perdida. O problema afeta colheitas manuais e automáticas.

**Correção sugerida:** verificar o espaço antes da colheita ou manter no tile
ou no drone a parte que não puder ser armazenada. O log deve registrar a
quantidade realmente adicionada, não o rendimento nominal.

## AUD-003 — Demolir o armazém mantém a capacidade concedida

- **Severidade:** Alta
- **Estado:** Confirmado
- **Categoria:** Inventário / estruturas
- **Evidência:** `ArmazemGeral.cs`, método `Initialize`; e `GridTile.cs`, método
  `Demolish`.

O Armazém Geral atribui sua capacidade ao inventário durante a inicialização.
Ao demolir a estrutura, o objeto é destruído, mas a capacidade não é
recalculada ou removida. Também não existe uma regra implementada para
combinar múltiplos armazéns: uma nova inicialização apenas sobrescreve o valor.

**Correção sugerida:** centralizar o cálculo da capacidade e recalculá-lo em
toda construção, melhoria e demolição de armazém. Antes disso, decidir se
múltiplos armazéns são permitidos e se suas capacidades acumulam.

## AUD-004 — Plantio e construção não possuem custo

- **Severidade:** Alta
- **Estado:** Decisão pendente
- **Categoria:** Design / economia
- **Evidência:** `ClickController.cs`, método `HandleClick`.

O jogador pode plantar manualmente sem consumir sementes e construir minas,
hangares, armazéns e processadores sem gastar recursos. Isso permite contornar
o Viveiro: sementes são obrigatórias para o drone, mas não para o jogador.
Construções ilimitadas também reduzem o peso das decisões econômicas que o MVP
pretende validar.

**Encaminhamento sugerido:** decidir se a ausência de custos é uma
simplificação temporária do protótipo ou uma regra pretendida. Se for
temporária, documentar quais custos mínimos devem entrar antes do playtest
externo para que o teste represente a economia desejada.

## AUD-005 — Configurações dos hangares não são salvas

- **Severidade:** Média
- **Estado:** Confirmado
- **Categoria:** Persistência
- **Evidência:** `SaveSystem.cs`, método `Save`; e `GameBootstrap.cs`, método
  `LoadIfAvailable`.

O save registra a existência do hangar, mas não persiste:

- pausa de plantio e colheita;
- cultivo automático selecionado;
- rotas automáticas habilitadas;
- timers e estado das rotas.

Ao reabrir o jogo, o hangar volta aos valores padrão, inclusive com Trigo
Lunar selecionado e automações ligadas.

**Correção sugerida:** criar dados de save próprios para o hangar, usando
identificadores estáveis para cultivos e rotas, e restaurar a configuração
antes de retomar a simulação.

## AUD-006 — Save não é atômico nem tolerante a corrupção

- **Severidade:** Média
- **Estado:** Confirmado
- **Categoria:** Persistência
- **Evidência:** `SaveSystem.cs`, métodos `Save` e `Load`.

O arquivo principal é sobrescrito diretamente com `File.WriteAllText`. Uma
interrupção durante a escrita pode deixar JSON parcial. A leitura e a
desserialização não possuem tratamento de exceções, validação de esquema,
backup ou recuperação.

**Correção sugerida:** escrever em arquivo temporário, validar o conteúdo e
substituir o arquivo principal somente depois do sucesso. Manter uma cópia de
backup e tratar saves ausentes, truncados ou incompatíveis sem impedir a
inicialização do jogo.

## AUD-007 — Relógio local permite manipular a ausência

- **Severidade:** Média
- **Estado:** Confirmado
- **Categoria:** Produção offline
- **Evidência:** `SaveSystem.CurrentUnixSeconds` e
  `GameBootstrap.LoadIfAvailable`.

A produção offline usa `DateTime.UtcNow`. Alterar o relógio do sistema permite
obter repetidamente até o teto de 48 horas de produção. O cálculo já limita
valores negativos e ausências superiores ao teto, mas não detecta saltos
artificiais.

**Encaminhamento sugerido:** aceitar e documentar a limitação durante o MVP
single-player ou armazenar sinais adicionais para detectar regressões e saltos
suspeitos. Um relógio de servidor só se torna necessário se a integridade
econômica externa ou multiplayer entrar no escopo.

## AUD-008 — Não existem testes automatizados

- **Severidade:** Média
- **Estado:** Confirmado
- **Categoria:** Qualidade
- **Evidência:** não foram encontrados assemblies ou diretórios de teste no
  projeto Unity.

Os fluxos de inventário, save e produção são sensíveis a casos de borda e
regressões. A validação exclusivamente manual tende a não reproduzir disputas
entre drones ou estados raros de persistência.

**Cobertura inicial sugerida:**

1. duas rotas disputando o mesmo estoque;
2. dois drones disputando uma semente;
3. colheita manual e automática com inventário cheio;
4. demolição do último armazém e presença de múltiplos armazéns;
5. save truncado, incompatível ou com valores inválidos;
6. produção offline próxima do teto da estrutura e do teto global;
7. persistência das configurações do hangar;
8. save e encerramento em cada etapa de uma missão de drone;
9. destino demolido, cheio ou inexistente durante uma missão;
10. múltiplas estruturas do mesmo tipo atendidas por rotas distintas;
11. ciclo de produção quando resta menos espaço que o rendimento completo.

## AUD-009 — Carga em trânsito pode desaparecer ao salvar ou fechar

- **Severidade:** Crítica
- **Estado:** Confirmado
- **Categoria:** Drones / persistência
- **Evidência:** `HangarDeDrones.cs`, métodos `TryStartPlantTrip`,
  `TryRunDeliveryRoute` e `TryRunCollectionRoute`; e `SaveSystem.cs`, método
  `Save`.

Durante uma missão, o recurso pode já ter saído da origem sem ainda ter
chegado ao destino. O save atual não registra drones em voo, estado da missão,
carga, origem, destino ou progresso do trajeto. Se houver autosave,
encerramento ou falha nesse intervalo, a carga desaparece. Isso afeta insumos
enviados a processadores, produtos coletados e sementes retiradas antes do
plantio.

O voo não deve ser reduzido a uma representação visual: o drone possui
armazenamento próprio e a carga deve existir economicamente nele durante o
trajeto.

**Correção sugerida:** persistir cada missão como uma máquina de estados. No
mínimo, salvar:

- ID estável do drone e do Hangar;
- estado atual (`IndoBuscar`, `IndoEntregar`, `Retornando` ou equivalente);
- recurso e quantidade armazenados no drone;
- capacidade do drone;
- IDs estáveis da origem e do destino;
- horário de início da etapa e horário previsto de chegada;
- ação que deve ser executada na chegada.

Ao carregar, se a chegada ainda estiver no futuro, o drone deve reaparecer na
posição proporcional da rota. Se a chegada já tiver passado, o jogo deve
concluir essa etapa e avançar quantas etapas couberem no tempo offline, sempre
preservando a carga.

Para o Tier 1, recomenda-se um único tipo de recurso por viagem. Se o destino
estiver cheio, a carga permanece no drone e ele não aceita outra missão até
conseguir descarregar ou retornar a um local válido.

## AUD-010 — Ciclo pode consumir insumo e perder parte do produto

- **Severidade:** Alta
- **Estado:** Confirmado
- **Categoria:** Produção / capacidade
- **Evidência:** `ProcessingStructure.cs`, métodos `CanProcess` e
  `CompleteCycle`.

A estrutura inicia um ciclo sempre que `StoredOutput` é menor que a capacidade,
mesmo quando o espaço restante é menor que `outputAmountProduced`. Ao concluir,
consome todo o insumo e limita o produto com `Mathf.Min`, descartando a parte
que não couber. Por exemplo, com 29 de 30 unidades armazenadas e rendimento 3,
o ciclo consome o insumo completo e entrega apenas 1 unidade.

Isso contradiz a regra documentada de que a produção pausa ao atingir a
capacidade e nada é perdido.

**Correção sugerida:** um ciclo só pode começar quando houver espaço para seu
rendimento completo. Como alternativa futura, a estrutura pode manter um
buffer interno separado, mas o MVP não deve consumir insumo para produto que
será descartado.

## AUD-011 — Rotas atendem somente à primeira estrutura de cada tipo

- **Severidade:** Alta
- **Estado:** Confirmado
- **Categoria:** Drones / rotas
- **Evidência:** `HangarDeDrones.cs`, método `TryRunRoute`.

Cada rota identifica apenas uma `ProcessingStructureDefinition`. Na execução,
o destino é resolvido com `FirstOrDefault`, portanto todas as rotas de um mesmo
tipo apontam para a primeira instância construída. Estruturas adicionais não
recebem nem entregam recursos automaticamente.

**Correção sugerida:** a rota deve guardar o ID estável de uma instância
concreta, além do tipo da estrutura. Para o MVP, a coordenada do tile pode
compor esse ID; a solução definitiva deve sobreviver a save/load sem depender
da ordem da lista estática.

## AUD-012 — Demolição durante uma missão pode quebrar a rota e perder carga

- **Severidade:** Alta
- **Estado:** Confirmado
- **Categoria:** Drones / demolição
- **Evidência:** `HangarDeDrones.cs`, callbacks das etapas de voo; e
  `GridTile.cs`, método `Demolish`.

As etapas guardam referências diretas à origem, ao destino, ao armazém e ao
tile. Esses objetos podem ser demolidos antes da chegada. A callback pode então
acessar uma referência destruída, interromper a corrotina, deixar a rota em
`Busy` e perder a carga.

**Regras sugeridas para o MVP:**

- bloquear a demolição do Hangar enquanto houver drones em missão ou com carga;
- permitir demolir o destino, mas fazer o drone retornar com a carga;
- ao retornar, tentar descarregar no Armazém Geral;
- sem Armazém ou sem espaço, manter a carga no drone;
- manter o drone indisponível enquanto não conseguir descarregar;
- nunca destruir ou converter carga implicitamente.

Essas regras devem ser confirmadas no design antes da implementação, mas o
tratamento de referências inválidas e a preservação da carga são obrigatórios.

## AUD-013 — Cliques na interface podem atingir o terreno atrás dela

- **Severidade:** Alta
- **Estado:** Confirmado
- **Categoria:** Interação / UI
- **Evidência:** `ClickController.cs`, método `Update`; e `PrototypeHud.cs`,
  método `OnGUI`.

O clique no mundo é processado sem verificar se o ponteiro está sobre uma
janela da interface. Clicar em um botão pode também plantar, colher, construir
ou demolir no tile atrás da janela, gerando ações não intencionais durante o
playtest.

**Correção sugerida:** centralizar a decisão de consumo do clique e impedir o
raycast quando a interface capturar o ponteiro. Ao substituir a UI placeholder,
usar o mecanismo de eventos da solução escolhida em vez de manter duas fontes
independentes de clique.

## AUD-014 — Janelas de estruturas demolidas permanecem registradas

- **Severidade:** Média
- **Estado:** Confirmado
- **Categoria:** UI / ciclo de vida
- **Evidência:** `PrototypeHud.cs`, coleções `_openHangarWindows` e
  `_openStructureWindows`; e `GridTile.cs`, método `Demolish`.

A HUD mantém referências às estruturas com janela aberta. A demolição não
remove essas referências. No desenho seguinte, a interface pode tentar acessar
um objeto Unity já destruído e gerar exceção ou uma janela inválida.

**Correção sugerida:** notificar a HUD quando uma estrutura for removida e,
como proteção adicional, eliminar referências nulas ou destruídas antes de
desenhar as janelas.

## AUD-015 — Log pode falhar em instalações sem permissão de escrita

- **Severidade:** Média
- **Estado:** Confirmado
- **Categoria:** Diagnóstico / arquivos
- **Evidência:** `GameLog.cs`, métodos `EnsureInitialized` e `Log`.

O log é criado relativamente a `Application.dataPath`, próximo aos arquivos da
instalação. Em uma pasta protegida, a criação ou escrita pode falhar. As
exceções de arquivo não são tratadas, permitindo que uma ferramenta de
diagnóstico interrompa uma ação normal do jogo.

**Correção sugerida:** gravar em `Application.persistentDataPath`, tratar falhas
de I/O e desativar o log da sessão depois de uma falha, emitindo no máximo um
aviso controlado.

## AUD-016 — Mina de Pedra não recebe rótulo de identificação

- **Severidade:** Baixa
- **Estado:** Confirmado
- **Categoria:** UI
- **Evidência:** `StructureLabels.cs`, método `OnGUI`.

Os rótulos percorrem processadores, armazéns e hangares, mas não percorrem
`MinaDePedra.Instances`.

**Correção sugerida:** incluir as minas na geração de rótulos ou substituir as
listas específicas por uma fonte comum de estruturas rotuláveis.

## Ordem recomendada

1. Definir a máquina de estados, carga própria e persistência das missões de
   drone (`AUD-001` e `AUD-009`).
2. Preservar carga quando o destino for demolido, estiver cheio ou deixar de
   existir (`AUD-012`).
3. Impedir perda de colheita e de produto por falta de espaço (`AUD-002` e
   `AUD-010`).
4. Dar identidade persistente às estruturas e rotas (`AUD-011`).
5. Definir e corrigir o ciclo de capacidade do armazém (`AUD-003`).
6. Persistir a configuração dos hangares (`AUD-005`).
7. Tornar o save recuperável (`AUD-006`).
8. Bloquear interação com o terreno através da UI (`AUD-013`).
9. Decidir os custos mínimos para o playtest (`AUD-004`).
10. Criar testes de regressão para esses fluxos (`AUD-008`).
