# Hangar de Drones

**Cadeia:** —
**Planeta:** 1
**Porte:** Grande

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos,
incluindo a regra de que o Nível 10 sempre exige o planeta mais avançado
disponível).

## Função (já estabelecida)

Capacidade de operação simultânea de drones neste planeta — pool único
compartilhado entre categorias, com peso por tier (ver
`docs/drones/00-indice.md`).

## Níveis — decisão

Métrica numérica principal: capacidade do pool (peso total suportado).

| Nível | Capacidade do pool (peso) | Dependência de material | Capacidade nova (breakpoint) |
|---|---|---|---|
| 1 | 10 | Planeta 1 | — |
| 2 | 15 | Planeta 1 | — |
| 3 | 22 | Planeta 1 | — |
| 4 | 33 | Planeta 1 | **Envio automático para manutenção**: drones com durabilidade zerada (ver `docs/drones/00-indice.md`, seção Desgaste) saem do pool automaticamente e vão para o Armazém Geral deste planeta, sem o jogador precisar notar ou mover manualmente |
| 5 | 50 | Planeta 1 + 2 | — |
| 6 | 75 | Planeta 1 + 2 | — |
| 7 | 110 | Planeta 2 | **Desconto de peso para drones raros**: drones de raridade Raro ou superior passam a ocupar 1 ponto de peso a menos no hangar (mínimo 1) |
| 8 | 165 | Planeta 2 + 3 | — |
| 9 | 250 | Planeta 3 + 4 | — |
| 10 | 375 | Planetas 3, 4 e 5 | **Desconto de peso para drones Tier 5**: passam a ocupar peso 6 em vez de 8 (ver tabela de pesos em `docs/drones/00-indice.md`) |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos em cada nível.
- Validar a curva de capacidade em playtest/planilha antes da
  implementação.
