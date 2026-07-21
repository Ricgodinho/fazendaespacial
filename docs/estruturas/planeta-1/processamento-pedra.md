# Estrutura de Processamento (Pedra)

**Cadeia:** Pedra
**Planeta:** 1

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos,
incluindo a regra de que o Nível 10 sempre exige o planeta mais avançado
disponível).

## Produtos (ver `docs/itens/itens.csv` para o catálogo completo)

- **Matéria-prima**: Pedra Ancestral (extraída na Mina de Pedra, ver
  `docs/estruturas/planeta-1/mina-de-pedra.md`)
- **Bloco Ancestral**: material de construção básico — proposto como o
  material genérico de referência usado nas dependências de nível de
  outras estruturas do Planeta 1
- **Cascalho Ancestral**: subproduto simples, uso em decoração/caminhos
- **Pedra Polida**: versão refinada, desbloqueada apenas em nível alto —
  construção/decoração de valor premium

## Níveis — decisão

Métrica numérica principal: unidades processadas por ciclo.

| Nível | Unidades/ciclo | Dependência de material | Capacidade nova (breakpoint) |
|---|---|---|---|
| 1 | 5 | Planeta 1 | — |
| 2 | 8 | Planeta 1 | — |
| 3 | 12 | Planeta 1 | — |
| 4 | 18 | Planeta 1 | **2 produtos simultâneos** (Bloco Ancestral + Cascalho Ancestral) |
| 5 | 27 | Planeta 1 + 2 | — |
| 6 | 40 | Planeta 1 + 2 | — |
| 7 | 60 | Planeta 2 | **Chance de qualidade superior** (mesmo modelo de dois filtros do Viveiro/Comida) — Bloco Ancestral pode sair com raridade acima do normal |
| 8 | 90 | Planeta 2 + 3 | — |
| 9 | 135 | Planeta 3 + 4 | — |
| 10 | 200 | Planetas 3, 4 e 5 | **Libera Pedra Polida** — construção/decoração de valor premium |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos em cada nível (hoje apenas a
  dependência de planeta está fixada, não o item específico).
- Confirmar se Bloco Ancestral vira de fato o material genérico
  referenciado nas dependências de outras estruturas, ou se cada
  estrutura usa materiais próprios.
- Validar a curva de throughput em playtest/planilha antes da
  implementação.
