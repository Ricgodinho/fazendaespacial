# Drone de Transporte/Logística

Ver `00-indice.md` para categorias gerais, modelo de progressão, limites
de hangar, regra de desgaste, e a regra de origem dos drones (fabricação
no Planeta 2 vs. achado danificado no Planeta 1) aplicáveis a todos os
drones.

## Especificação por tier — decisão

Cada tier **herda todas as capacidades dos tiers anteriores** e adiciona
uma nova — nenhuma capacidade é substituída ao subir de tier. Rotas
descritas abaixo são **locais** (dentro do mesmo planeta), salvo a nota
de capacidade interplanetária ao final.

| Tier | Função |
|---|---|
| 1 | Carrega quantidade pequena por viagem, atende 1 rota fixa local, definida pelo jogador. Viagem manual — jogador aciona cada entrega. |
| 2 | Carga maior. A rota se repete automaticamente, sem o jogador precisar acionar toda entrega. |
| 3 | Atende até 2 rotas simultâneas, ainda dentro do mesmo planeta. |
| 4 | Prioriza automaticamente qual rota atender primeiro quando a demanda excede a capacidade simultânea. |
| 5 | 3 rotas simultâneas, e prioriza automaticamente entregas para estruturas prestes a estourar capacidade de armazenamento (evita perda de produção por overflow, ver `docs/04-prototipo-0-loop-idle.md` e `docs/07-prototipo-2-loop-hibrido.md`). |

## Capacidade interplanetária — exceção ligada à fabricação/renovação no Planeta 2

Drones de Transporte **fabricados (ou renovados) na Oficina/Fábrica de
Drones do Planeta 2** (ver
`docs/estruturas/planeta-2/oficina-de-drones.md`), a partir do **Tier 2**,
ganham a capacidade extra de realizar rotas **entre planetas** já
conectados pela rede — além das rotas locais da tabela acima.

Drones de Transporte **encontrados danificados no Planeta 1** e
reparados continuam limitados a rotas locais, mesmo em Tier 2 ou
superior — a menos que sejam enviados ao Planeta 2 para passar pela
Oficina (renovação). Isso é o que alimenta, na prática, todas as
dependências de nível de estrutura já fechadas (ex: "Dependência:
Planeta 2") em `docs/estruturas/planeta-1/`.

## Pendente
- Aplicação da raridade do próprio drone dentro de cada tier — mesma
  pendência já registrada para os demais drones.
