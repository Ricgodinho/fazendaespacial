# Mina de Pedra

**Cadeia:** Pedra + Descobertas
**Planeta:** 1

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos,
incluindo a regra de que o Nível 10 sempre exige o planeta mais avançado
disponível).

## Função dupla (já estabelecida)
Extrai Pedra Ancestral (ver `docs/itens/itens.csv`) e funciona como ponto
de escavação/descoberta deste planeta (ver `docs/01-conceito.md`, seção
"Escavações e descobertas") — mesma função dupla espelhada na Mina de
Minério do Planeta 2 (ver `docs/estruturas/planeta-2/mina-de-minerio.md`).

## Níveis — decisão

Métrica numérica principal: unidades de Pedra Ancestral extraídas por
ciclo. Os breakpoints usam a linguagem de "camadas de escavação" já
citada em `docs/01-conceito.md` ("acessar ruínas, áreas ou camadas de
escavação antes inacessíveis").

| Nível | Pedra Ancestral/ciclo | Dependência de material | Capacidade nova (breakpoint) |
|---|---|---|---|
| 1 | 5 | Planeta 1 | — |
| 2 | 8 | Planeta 1 | — |
| 3 | 12 | Planeta 1 | — |
| 4 | 18 | Planeta 1 | **Camada 2 de escavação**: acesso a uma camada mais profunda, com novos tipos de descoberta (ex: fragmentos de projetos/tecnologias, além de objetos decorativos simples) |
| 5 | 27 | Planeta 1 + 2 | — |
| 6 | 40 | Planeta 1 + 2 | — |
| 7 | 60 | Planeta 2 | **Chance de descoberta rara aumenta** (mesmo modelo de dois filtros já usado no Viveiro, ver `docs/01-conceito.md`) — dentro do teto já liberado pelo progresso do jogador, melhora a chance de achado raro nesta mina |
| 8 | 90 | Planeta 2 + 3 | — |
| 9 | 135 | Planeta 3 + 4 | — |
| 10 | 200 | Planetas 3, 4 e 5 | **Camada final de escavação**: acesso à camada mais profunda, maior frequência de achados de todos os tipos (incluindo peças de drones danificados e artefatos raros) |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos em cada nível.
- Fórmula de probabilidade: ver `docs/01-conceito.md`, seção Raridade
  ("Fórmula do modificador local").
- Detalhar o que exatamente muda entre "Camada 1", "Camada 2" e "Camada
  final" em termos de tipos de descoberta específicos.
- Validar a curva de extração em playtest/planilha antes da
  implementação.
