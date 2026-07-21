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

## Direções da rota — clarificação (validado no protótipo)

Uma rota de Transporte cobre dois sentidos possíveis, ambos já testados
no protótipo técnico:

- **Entrega**: Armazém Geral → estrutura (leva insumo para processar,
  ex: Trigo Lunar até o Viveiro).
- **Coleta**: estrutura → Armazém Geral (traz o produto pronto, ex:
  Semente de Trigo Lunar do Viveiro de volta ao inventário). Sem essa
  direção, o produto acumulado fica preso dentro da própria estrutura
  até o jogador clicar nela manualmente para coletar.

Ambas as direções já existem desde o Tier 1 no protótipo (uma rota de
entrega e uma de coleta por estrutura), em vez de esperar por tiers
mais altos — decisão de escopo para validar a mecânica antes de
formalizar a progressão completa de tier.

## Modo manual/automático — decisão de escopo do protótipo

A tabela de tiers acima define Tier 1 como manual e Tier 2 como
automático. No protótipo técnico, os dois modos **coexistem por rota**,
escolhidos pelo jogador (botão "Entregar/Coletar agora" para o modo
manual, toggle "Automático" para repetir sozinho no mesmo intervalo dos
drones de Colheita/Plantio) — em vez de travar o modo automático atrás
de um sistema de tier ainda não implementado. Quando o sistema de tier
for formalizado, a expectativa é que o toggle "Automático" só fique
disponível a partir do Tier 2, conforme a tabela acima.

## Regra de simulação — pegar/largar acontece na chegada (decisão)

Ao implementar o drone, ficou clara uma regra que deve valer para
**qualquer** drone que carregue algo entre dois pontos: o item só sai
da origem e só entra no destino no momento exato em que o drone chega
visualmente em cada ponto — nunca no instante em que a viagem é
decidida. Ver a mesma regra registrada de forma geral em
`docs/drones/00-indice.md`.

## Pendente
- Aplicação da raridade do próprio drone dentro de cada tier — mesma
  pendência já registrada para os demais drones.
- Capacidade real por viagem (Nível 1) — protótipo usa valor de teste
  (10 unidades), a validar em playtest/planilha como os demais números.
