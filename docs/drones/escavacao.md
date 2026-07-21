# Drone de Escava??o/Explora??o

Ver `00-indice.md` para categorias gerais, modelo de progress?o, limites
de hangar, regra de desgaste, e a regra de origem dos drones (fabrica??o
no Planeta 2 vs. achado danificado no Planeta 1) aplic?veis a todos os
drones.

## Especifica??o por tier ? decis?o

Cada tier **herda todas as capacidades dos tiers anteriores** e adiciona
uma nova ? nenhuma capacidade ? substitu?da ao subir de tier.

| Tier | Fun??o |
|---|---|
| 1 | Escava 1 tile por vez, precisa estar pr?ximo da Mina/estrutura. Acessa apenas a Camada 1 de escava??o. |
| 2 | Escava pequena ?rea (ex: 3x3), ainda precisa estar pr?ximo. Continua limitado ? Camada 1. |
| 3 | Ganha alcance ? opera sem o jogador/nave por perto (mant?m a ?rea do Tier 2). |
| 4 | Acessa a Camada 2 de escava??o (conecta com o breakpoint j? definido na Mina de Pedra, ver `docs/estruturas/planeta-1/mina-de-pedra.md`, N?vel 4) ? descobertas mais avan?adas passam a aparecer. |
| 5 | ?rea maior (ex: 5x5), mantendo o resto. Prioriza automaticamente descobertas sobre material bruto quando os dois aparecem no mesmo tile ? configur?vel pelo jogador. |

## Conex?o com a Mina de Pedra
Os tiers deste drone acessam as "camadas de escava??o" j? definidas na
Mina de Pedra (ver `docs/estruturas/planeta-1/mina-de-pedra.md`) ?
inclusive as descobertas de drones danificados e projetos/plantas de
drones (ver `docs/drones/00-indice.md`, se??o Origem dos drones).

## Pendente
- Aplica??o da raridade do pr?prio drone dentro de cada tier ? mesma
  pend?ncia j? registrada para os demais drones.
