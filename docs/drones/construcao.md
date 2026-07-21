# Drone de Construção/Reparo

Ver `00-indice.md` para categorias gerais, modelo de progressão, limites
de hangar, regra de desgaste, e a regra de origem dos drones (fabricação
no Planeta 2 vs. achado danificado no Planeta 1) aplicáveis a todos os
drones.

## Especificação por tier — decisão

Cada tier **herda todas as capacidades dos tiers anteriores** e adiciona
uma nova — nenhuma capacidade é substituída ao subir de tier.

| Tier | Função |
|---|---|
| 1 | Constrói/repara 1 estrutura por vez, precisa estar próximo. Só estruturas de Nível 1-3 disponíveis. |
| 2 | Reduz o tempo de construção/reparo pela metade, ainda precisa estar próximo. |
| 3 | Ganha alcance — opera sem o jogador/nave por perto (mantém a redução de tempo do Tier 2). |
| 4 | Passa a construir/reparar estruturas de qualquer nível (remove o limite de Nível 1-3). |
| 5 | 2 construções/reparos simultâneos — pode trabalhar em duas estruturas diferentes ao mesmo tempo. |

## Conexão com o sistema de Nível de Estrutura
Os tiers 1 e 4 deste drone conectam diretamente com o sistema de Nível
de Estrutura já fechado (ver `docs/estruturas/00-indice.md`) — no início,
o drone só acompanha construções/reparos mais simples (Nível 1-3),
precisando de um tier mais avançado para acompanhar a evolução plena de
qualquer estrutura do jogo.

## Pendente
- Aplicação da raridade do próprio drone dentro de cada tier — mesma
  pendência já registrada para os demais drones.
