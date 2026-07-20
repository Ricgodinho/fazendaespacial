# Protótipo 0 — Matemática do loop idle

Este documento define as regras numéricas do loop passivo/idle, para
validar em planilha (sem código, sem interface) antes de qualquer
implementação. Objetivo: testar se o ritmo de espera e retorno é
agradável, ajustando números livremente até "sentir certo" no papel.

## Princípios definidos

- **Taxa de produção fixa** por estrutura, enquanto o jogador está ausente
  (upgrades mudam a taxa por decisão ativa, não pelo tempo ausente).
- **Capacidade máxima de armazenamento**: ao atingir o teto, a produção
  pausa. Nada é perdido — só para de acumular até o jogador coletar.
- **Nunca punir ausência longa com perda de recursos.** Ausência longa =
  oportunidade perdida (silo ficou parado), nunca prejuízo.
- **Tempo de crescimento variável por cultivo**: cultivos básicos crescem
  rápido (retorno em minutos/poucas horas), cultivos de progressão
  crescem devagar (retorno em dias), criando gatilhos de retorno em
  ritmos diferentes.

## Estrutura de exemplo (para testar em planilha)

### Cultivo básico — "Trigo Lunar" (nome provisório)
| Parâmetro | Valor exemplo |
|---|---|
| Tempo de crescimento | 2 horas |
| Produção por ciclo | 10 unidades |
| Capacidade de armazenamento | 100 unidades (10 ciclos) |
| Tempo até saturar (sem coleta) | 20 horas |

> Valor ajustado após simulação em planilha (ver `planilhas/planilha_loop_idle.xlsx`):
> capacidade de 30 unidades fazia o jogador casual (1x/dia) perder 75% da
> produção; com 100 unidades a perda cai para ~17%, mantendo uma leve
> sensação de urgência sem punir demais quem joga com pouca frequência.

### Cultivo de progressão — "Fibra Estelar" (nome provisório)
| Parâmetro | Valor exemplo |
|---|---|
| Tempo de crescimento | 18 horas |
| Produção por ciclo | 5 unidades |
| Capacidade de armazenamento | 15 unidades (3 ciclos) |
| Tempo até saturar (sem coleta) | 54 horas (~2,25 dias) |

## Perguntas a validar na planilha

Preencher uma planilha com essas taxas e simular cenários de jogador:

1. **Jogador casual** (abre 1x por dia, ~15 min): quantos ciclos de
   Trigo Lunar ele perde por dia por não coletar a tempo? Está
   aceitável ou é frustrante?
2. **Jogador dedicado** (abre 3-4x por dia): a capacidade de
   armazenamento do Trigo Lunar é grande o suficiente para não parecer
   "aperto artificial", ou pequena o suficiente para não parecer
   irrelevante?
3. **Primeira semana**: em quantos dias o jogador consegue upgrade de
   capacidade de armazenamento, assumindo custo em unidades de Trigo
   Lunar? (definir custo hipotético e testar, ex: 100 unidades = ~3,3
   dias de produção casual)
4. **Fibra Estelar**: o tempo de 18h de crescimento cria um ritmo de
   "checagem 1x por dia" saudável, ou é longo demais para o início do
   jogo e deveria entrar só depois de um certo marco de progressão?

## Regras de ajuste

- Se o teste mostrar que o jogador casual perde produção demais, **aumentar
  capacidade de armazenamento**, não a taxa de produção (evita inflação de
  economia).
- Se o jogo parecer "sempre no teto, sem graça", **reduzir capacidade** ou
  **aumentar tempo de ciclo** — não reduzir a produção por ciclo, que afeta
  a sensação de recompensa por coleta.
- Os valores acima são ponto de partida, não definitivos. Ajustar em
  planilha até o ritmo parecer certo antes de decidir por escrito.

## Fora do escopo deste documento

- Custo de upgrades e progressão de estruturas (fica para o Protótipo 1/2).
- Múltiplos cultivos além dos dois exemplos.
- Qualquer interface ou notificação — isso é só a matemática crua.

*Este documento deve ser atualizado com os números finais depois da
validação em planilha, antes de iniciar o Protótipo 0 em código.*
