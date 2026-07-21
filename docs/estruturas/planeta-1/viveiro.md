# Viveiro

**Cadeia:** Sementes
**Planeta:** 1

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos,
incluindo a regra de que o Nível 10 sempre exige o planeta mais avançado
disponível).

## Modelo de raridade (já fechado)

Ver `docs/01-conceito.md`, seção Raridade: sementes seguem a mesma tabela
de 5 níveis de raridade do jogo, com dois filtros — teto global (progresso
do jogador) e modificador local (Nível do Viveiro, detalhado abaixo).

## Níveis — decisão

Métrica numérica principal: sementes produzidas por ciclo.

| Nível | Sementes/ciclo | Dependência de material | Capacidade nova (breakpoint) |
|---|---|---|---|
| 1 | 3 | Planeta 1 | — |
| 2 | 5 | Planeta 1 | — |
| 3 | 8 | Planeta 1 | — |
| 4 | 12 | Planeta 1 | **2 sementes por ciclo, de cultivos diferentes simultaneamente** — antes deste nível, só produz semente de 1 cultivo por vez |
| 5 | 18 | Planeta 1 + 2 | — |
| 6 | 27 | Planeta 1 + 2 | — |
| 7 | 40 | Planeta 2 | **Chance de raridade superior aumenta** — este é o efeito central do Viveiro (modelo de dois filtros), ficando mais forte a partir daqui |
| 8 | 60 | Planeta 2 + 3 | — |
| 9 | 90 | Planeta 3 + 4 | — |
| 10 | 135 | Planetas 3, 4 e 5 | **Piso de raridade elevado**: sementes produzidas neste nível nunca saem na raridade mínima (Comum) — sempre pelo menos Incomum, respeitando o teto já liberado pelo progresso do jogador |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos em cada nível.
- Fórmula de probabilidade: ver `docs/01-conceito.md`, seção Raridade
  ("Fórmula do modificador local") — já definida, aplicável aqui.
- Validar a curva de produção em playtest/planilha antes da
  implementação.
