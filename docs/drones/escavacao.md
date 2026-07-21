# Drone de Escavação/Exploração

Ver `00-indice.md` para categorias gerais, modelo de progressão, limites
de hangar, regra de desgaste, e a regra de origem dos drones (fabricação
no Planeta 2 vs. achado danificado no Planeta 1) aplicáveis a todos os
drones.

## Especificação por tier — decisão

Cada tier **herda todas as capacidades dos tiers anteriores** e adiciona
uma nova — nenhuma capacidade é substituída ao subir de tier.

| Tier | Função |
|---|---|
| 1 | Escava 1 tile por vez, precisa estar próximo da Mina/estrutura. Acessa apenas a Camada 1 de escavação. |
| 2 | Escava pequena área (ex: 3x3), ainda precisa estar próximo. Continua limitado à Camada 1. |
| 3 | Ganha alcance — opera sem o jogador/nave por perto (mantém a área do Tier 2). |
| 4 | Acessa a Camada 2 de escavação (conecta com o breakpoint já definido na Mina de Pedra, ver `docs/estruturas/planeta-1/mina-de-pedra.md`, Nível 4) — descobertas mais avançadas passam a aparecer. |
| 5 | Área maior (ex: 5x5), mantendo o resto. Prioriza automaticamente descobertas sobre material bruto quando os dois aparecem no mesmo tile — configurável pelo jogador. |

## Conexão com a Mina de Pedra
Os tiers deste drone acessam as "camadas de escavação" já definidas na
Mina de Pedra (ver `docs/estruturas/planeta-1/mina-de-pedra.md`) —
inclusive as descobertas de drones danificados e projetos/plantas de
drones (ver `docs/drones/00-indice.md`, seção Origem dos drones).

## Pendente
- Aplicação da raridade do próprio drone dentro de cada tier — mesma
  pendência já registrada para os demais drones.
