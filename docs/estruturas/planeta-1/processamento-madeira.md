# Estrutura de Processamento (Madeira)

**Cadeia:** Madeira
**Planeta:** 1

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos,
incluindo a regra de que o Nível 10 sempre exige o planeta mais avançado
disponível).

## Produtos (ver `docs/itens/itens.csv` para o catálogo completo)

- **Matéria-prima**: Cedro Estelar (colhido na Área de Plantio de Árvores)
- **Tábua Estelar**: usada em construções
- **Lenha Estelar**: combustível/aquecimento inicial para o Planeta 4 (gelo)
- **Semente de Cedro Estelar**: chance média como subproduto (sem sorteio
  de raridade — isso fica só com o Viveiro, ver
  `docs/estruturas/planeta-1/viveiro.md`)
- **Carvão Estelar**: combustível para a Fundição do Planeta 2 (ver
  `docs/estruturas/planeta-2/fundicao.md`) — cria dependência
  Planeta 1 → Planeta 2, no sentido inverso ao fluxo usual

## Níveis — decisão

Métrica numérica principal: unidades processadas por ciclo.

| Nível | Unidades/ciclo | Dependência de material | Capacidade nova (breakpoint) |
|---|---|---|---|
| 1 | 5 | Planeta 1 | — |
| 2 | 8 | Planeta 1 | — |
| 3 | 12 | Planeta 1 | — |
| 4 | 18 | Planeta 1 | **2 produtos simultâneos** (ex: Tábua + Lenha ao mesmo tempo, sem fila) |
| 5 | 27 | Planeta 1 + 2 | — |
| 6 | 40 | Planeta 1 + 2 | — |
| 7 | 60 | Planeta 2 | **Chance média de semente aumenta** — a chance de sair Semente de Cedro Estelar como subproduto melhora neste nível |
| 8 | 90 | Planeta 2 + 3 | — |
| 9 | 135 | Planeta 3 + 4 | — |
| 10 | 200 | Planetas 3, 4 e 5 | **Libera Carvão Estelar** — antes deste nível, a estrutura só produz Tábua/Lenha/Semente |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos em cada nível (hoje apenas a
  dependência de planeta está fixada, não o item específico).
- Definir o valor exato da "chance média" de semente no Nível 7 e como
  ela aumenta.
- Definir quantidade de Carvão Estelar produzida por ciclo no Nível 10.
- Validar a curva de throughput em playtest/planilha antes da
  implementação.
