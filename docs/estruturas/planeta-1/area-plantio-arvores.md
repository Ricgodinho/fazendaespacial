# Área de Plantio de Árvores

**Cadeia:** Madeira
**Planeta:** 1

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos).

## Níveis — decisão

Métrica numérica principal: quantidade de árvores plantadas
simultaneamente (cresce em todo nível). Apenas 3 níveis são breakpoints
(introduzem capacidade nova, não só número maior).

| Nível | Árvores simultâneas | Capacidade nova (breakpoint) |
|---|---|---|
| 1 | 3 | — |
| 2 | 5 | — |
| 3 | 8 | — |
| 4 | 12 | **Compatível com Drone de Plantio** — replantio automático de árvores após o corte. |
| 5 | 18 | — |
| 6 | 27 | — |
| 7 | 40 | **Corte em área**: corta várias árvores adjacentes de uma vez, em vez de uma por uma. *Exige material do Planeta 2 para o upgrade.* |
| 8 | 60 | — |
| 9 | 90 | — |
| 10 | 135 | **-25% no tempo de crescimento** das árvores, e libera uma 2ª espécie de árvore (madeira de tipo diferente, mais nobre/rara). *Exige material dos Planetas 3, 4 e 5 para o upgrade.* |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Diferença em relação ao Campo de Cultivo
Em vez de multi-cultivo (Nível 7 do Campo de Cultivo), aqui o breakpoint
correspondente é **corte em área** — mais coerente tematicamente com
árvores (abater várias de uma vez) do que plantar dois tipos ao mesmo
tempo.

## Pendente
- Definir os materiais exatos exigidos nos níveis 7 e 10 (hoje apenas a
  dependência de planeta está fixada, não o item específico).
- Definir qual é a "2ª espécie de árvore" liberada no Nível 10.
- Validar a curva de árvores simultâneas em playtest/planilha antes da
  implementação.
