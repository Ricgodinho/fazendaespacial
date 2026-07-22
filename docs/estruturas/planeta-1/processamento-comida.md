# Estrutura de Processamento (Comida)

**Cadeia:** Comida
**Planeta:** 1
**Porte:** Médio

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos,
incluindo a regra de que o Nível 10 sempre exige o planeta mais avançado
disponível).

## Níveis — decisão

Métrica numérica principal: unidades processadas por ciclo. A coluna de
**Dependência** mostra de quais planetas vem o material necessário para
alcançar aquele nível — cresce gradualmente, começando só local.

| Nível | Unidades/ciclo | Dependência de material | Capacidade nova (breakpoint) |
|---|---|---|---|
| 1 | 5 | Planeta 1 | — |
| 2 | 8 | Planeta 1 | — |
| 3 | 12 | Planeta 1 | — |
| 4 | 18 | Planeta 1 | **2 receitas simultâneas** — antes deste nível, processa só 1 tipo de produto por vez; a partir daqui, processa 2 receitas diferentes ao mesmo tempo, sem fila. |
| 5 | 27 | Planeta 1 + 2 | — |
| 6 | 40 | Planeta 1 + 2 | — |
| 7 | 60 | Planeta 2 | **Chance de qualidade superior**: reaproveitando o mesmo modelo de dois filtros já fechado para sementes (ver `docs/01-conceito.md`, seção Raridade), o produto processado ganha uma chance de sair numa raridade acima do normal. |
| 8 | 90 | Planeta 2 + 3 | — |
| 9 | 135 | Planeta 3 + 4 | — |
| 10 | 200 | **Planetas 3, 4 e 5** | **Libera receita avançada**: um produto de valor ainda maior, usando matéria-prima de múltiplos planetas como ingrediente adicional. |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos em cada nível (hoje apenas a
  dependência de planeta está fixada, não o item específico).
- Fórmula de probabilidade: ver `docs/01-conceito.md`, seção Raridade
  ("Fórmula do modificador local").
- Definir qual é a "receita avançada" liberada no Nível 10.
- Validar a curva de throughput em playtest/planilha antes da
  implementação.
