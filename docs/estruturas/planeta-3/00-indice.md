# Estruturas — Planeta 3 (Água)

Planeta temático: aquático/submerso. Introduz cultivo debaixo d'água,
sementes aquáticas, material bruto para novas ligas (refinadas no
Planeta 2), e uma fábrica de boosts temporários.

## Lista de estruturas — decisão

| # | Estrutura (nome provisório) | Cadeia | Arquivo |
|---|---|---|---|
| 1 | Campo de cultivo aquático | Comida aquática | `campo-de-cultivo-aquatico.md` |
| 2 | Estrutura de processamento (comida aquática) | Comida aquática | `processamento-comida-aquatica.md` |
| 3 | Viveiro aquático | Sementes aquáticas | `viveiro-aquatico.md` |
| 4 | Poço/Mina Aquática | Material de liga | `poco-mina-aquatica.md` |
| 5 | Fábrica de Boosts | Boosts temporários | `fabrica-de-boosts.md` |
| 6 | Armazém geral (água) | Todas | `armazem-geral.md` |
| 7 | Hangar de drones (água) | — | `hangar-de-drones.md` |

## Notas gerais

### Drones aquáticos: variante ambiental, não categoria nova
Este planeta introduz **variantes aquáticas** dos drones de Plantio e
Escavação já existentes (ver `docs/drones/00-indice.md`) — mesma função
(plantar, coletar), mas com design/aparência adaptada para operar
submerso (visualmente parecido com um submarino). Não é uma nova
categoria de drone.

### Material de liga: sem refino local
O Poço/Mina Aquática extrai o material bruto usado para criar **novas
ligas**, mas o refino acontece na **Fundição do Planeta 2**, via
transporte entre planetas (mesmo princípio já aplicado ao processamento
de pedra do Planeta 2, ver `docs/estruturas/planeta-2/00-indice.md`) —
reforça a dependência entre planetas em vez de duplicar estruturas de
refino.

### Fábrica de Boosts
Produz boosts temporários (ex: 1 hora de duração), aplicáveis a um drone
específico ou a uma plantação. Item consumível, não uma mudança
permanente. Potencial de venda entre jogadores no futuro (Fase 3,
mercado — ver nota em `docs/01-conceito.md`), sujeito ao sistema de
raridade a debater (ver `docs/01-conceito.md`, seção Raridade).

## Pendente
- Detalhar a função específica de cada estrutura (uma por arquivo).
- Definir os níveis de evolução (upgrades) de cada estrutura.
- Aplicação de raridade aos itens produzidos pela Fábrica de Boosts.
