# Estruturas — Tier 2 (A Lua)

Planeta Tier 2: 100% mineral/tecnológico, sem agricultura (ver
`docs/01-conceito.md`). Tema central: minério, criação de drones e
upgrade de módulos de nave.

## Lista de estruturas — decisão

| # | Estrutura (nome provisório) | Cadeia | Arquivo |
|---|---|---|---|
| 1 | Mina de Minério | Mineral | `mina-de-minerio.md` |
| 2 | Mina de Pedra (lua) | Pedra | `mina-de-pedra.md` |
| 3 | Fundição | Mineral | `fundicao.md` |
| 4 | Oficina/Fábrica de Drones | — | `oficina-de-drones.md` |
| 5 | Estaleiro/Doca de Reparo da Nave | — | `estaleiro.md` |
| 6 | Armazém geral (lua) | Todas | `armazem-geral.md` |
| 7 | Hangar de drones (lua) | — | `hangar-de-drones.md` |

## Notas gerais

### Processamento de pedra — decisão de teste (a validar em playtest)
Diferente do Tier 1, o Tier 2 **não tem estrutura própria de processamento
de pedra**. A pedra extraída na Mina de Pedra (lua) deve ser transportada
até o Tier 1 para ser processada lá, usando o sistema de logística entre
planetas já previsto (ver `docs/07-prototipo-2-loop-hibrido.md`).

**Motivo:** reforça a dependência entre planetas e a "rede agrícola
interplanetária" como pilar central do jogo (ver `docs/01-conceito.md`),
em vez de duplicar uma estrutura já existente sem necessidade.

**Nota para feedback de playtest:** se essa dependência se mostrar
incômoda ou gerar fricção excessiva na prática (ex: jogadores reclamando
de ter que esperar transporte só para processar pedra simples), avaliar
adicionar uma estrutura de processamento de pedra local também na lua.
Esta é uma decisão reversível, não definitiva.

### Fundição — escopo ampliado
Além de minério bruto → barras/metal processado, a Fundição também pode
**criar ligas combinando materiais de múltiplos tiers** — conectando-se à
regra de dependência entre tiers já definida em `docs/01-conceito.md`
("Tier 5 pode exigir materiais dos Tiers 2, 3 e 4").

### Oficina/Fábrica de Drones e Estaleiro
Ambas conectam-se à identidade deste planeta como o local de "criação de
drones, upgrade de naves" (ver debate em `docs/01-conceito.md`).

## Pendente
- Detalhar a função específica de cada estrutura (uma por arquivo).
- Definir os níveis de evolução (upgrades) de cada estrutura.
