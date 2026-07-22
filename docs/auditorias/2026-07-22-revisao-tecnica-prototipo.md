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

**Correção sugerida:** reservar ou retirar a carga atomicamente antes de
iniciar a viagem. A ação no destino deve usar somente a quantidade realmente
retirada. Em caso de cancelamento, a reserva deve voltar à origem.

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
7. persistência das configurações do hangar.

## Ordem recomendada

1. Corrigir a reserva/remoção de recursos nos drones (`AUD-001`).
2. Impedir perda de colheita por falta de espaço (`AUD-002`).
3. Definir e corrigir o ciclo de capacidade do armazém (`AUD-003`).
4. Decidir os custos mínimos para o playtest (`AUD-004`).
5. Persistir a configuração dos hangares (`AUD-005`).
6. Tornar o save recuperável (`AUD-006`).
7. Criar testes de regressão para esses fluxos (`AUD-008`).

