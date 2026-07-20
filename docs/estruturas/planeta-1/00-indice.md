# Estruturas — Planeta 1 (Planeta inicial)

Planeta 1 é exclusivamente agrícola/recursos naturais básicos: comida,
madeira e pedra. Minerais e Fundição só entram a partir do Planeta 2 (ver
`docs/01-conceito.md`, referência à lua mineral de Planeta 2).

## Lista de estruturas — decisão

| # | Estrutura (nome provisório) | Cadeia | Arquivo |
|---|---|---|---|
| 1 | Campo de cultivo | Comida | `campo-de-cultivo.md` |
| 2 | Área de plantio de árvores | Madeira | `area-plantio-arvores.md` |
| 3 | Estrutura de processamento (comida) | Comida | `processamento-comida.md` |
| 4 | Estrutura de processamento (madeira) | Madeira | `processamento-madeira.md` |
| 5 | Mina de pedra | Pedra + Descobertas | `mina-de-pedra.md` |
| 6 | Estrutura de processamento (pedra) | Pedra | `processamento-pedra.md` |
| 7 | Viveiro | Sementes | `viveiro.md` |
| 8 | Armazém geral | Todas | `armazem-geral.md` |
| 9 | Hangar de drones | — | `hangar-de-drones.md` |

## Notas gerais

- **Madeira**: árvores precisam ser plantadas (não são apenas coletadas de
  árvores pré-existentes no mapa).
- **Mina de pedra**: já existe de forma humilde no planeta inicial,
  herdada do antigo proprietário (ver narrativa em `docs/01-conceito.md`).
  Funciona tanto como fonte de pedra quanto como ponto de escavação e
  descoberta (plantas, cartas estelares, outros achados), conectando-se
  ao drone de Escavação/Exploração (`docs/drones/escavacao.md`).
- **Viveiro**: produz sementes a partir de matéria-prima colhida. As
  sementes seguem a mesma tabela de raridade do jogo (ver
  `docs/01-conceito.md`, seção de Raridade) — o teto do que pode
  aparecer é definido pelo progresso do jogador, e o Nível do Viveiro
  modifica a probabilidade dentro desse teto. Fórmula exata de
  probabilidade por nível ainda a definir.

## Pendente
- Detalhar a função específica de cada estrutura (uma por arquivo).
