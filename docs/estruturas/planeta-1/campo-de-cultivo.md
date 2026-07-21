# Campo de Cultivo

**Cadeia:** Comida
**Planeta:** 1

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos).

## Níveis — decisão

Métrica numérica principal: quantidade de tiles de plantio simultâneos
(cresce em todo nível). Apenas 3 níveis são breakpoints (introduzem
capacidade nova, não só número maior).

| Nível | Tiles de plantio | Capacidade nova (breakpoint) |
|---|---|---|
| 1 | 4 | — |
| 2 | 6 | — |
| 3 | 9 | — |
| 4 | 14 | **Compatível com Drone de Plantio** — antes deste nível, plantio é sempre manual, mesmo com colheita automática já ativa. |
| 5 | 20 | — |
| 6 | 30 | — |
| 7 | 45 | **Multi-cultivo**: planta 2 tipos de cultivo diferentes ao mesmo tempo no mesmo campo. *Exige material do Planeta 2 para o upgrade.* |
| 8 | 65 | — |
| 9 | 95 | — |
| 10 | 140 | **-20% no tempo de crescimento** de todos os cultivos deste campo, e multi-cultivo passa de 2 para 3 tipos simultâneos. *Exige material dos Planetas 3 e 4 para o upgrade.* |

Valores de tiles são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos nos níveis 7 e 10 (hoje apenas a
  dependência de planeta está fixada, não o item específico).
- Validar a curva de tiles em playtest/planilha antes da implementação.
