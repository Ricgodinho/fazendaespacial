# Drone de Plantio

Ver `00-indice.md` para categorias gerais, modelo de progressão, limites
de hangar e regra de desgaste aplicáveis a todos os drones.

## Especificação por tier — decisão

Cada tier **herda todas as capacidades dos tiers anteriores** e adiciona
uma nova — nenhuma capacidade é substituída ao subir de tier.

| Tier | Função |
|---|---|
| 1 | Planta 1 tile por vez, precisa estar próximo da estrutura/jogador. Usa sementes do Armazém Geral. |
| 2 | Planta pequena área (ex: 3x3) de uma vez, ainda precisa estar próximo. |
| 3 | Ganha alcance — opera sem o jogador/nave por perto (mantém a área do Tier 2). |
| 4 | Prioriza automaticamente qual semente plantar quando há múltiplos tipos de cultivo disponíveis no Armazém (mantém alcance do Tier 3 e área do Tier 2). |
| 5 | Área de plantio maior (ex: 5x5), mantendo o resto. **Configurável por raridade**: o jogador escolhe se o drone prioriza plantar sementes de maior raridade primeiro (mais arriscado, mais recompensa) ou economiza as raras plantando as comuns primeiro (mais conservador). |

## Conexão com o sistema de raridade
O Tier 5 se conecta diretamente ao modelo de raridade de sementes já
fechado (ver `docs/01-conceito.md`, seção Raridade, e
`docs/estruturas/planeta-1/viveiro.md`) — o jogador ganha uma decisão
estratégica real sobre como usar sementes raras, em vez de o drone
decidir sozinho.

## Pendente
- Aplicação da raridade do próprio drone (não da semente) dentro de cada
  tier — mesma pendência já registrada para o drone de Colheita.
