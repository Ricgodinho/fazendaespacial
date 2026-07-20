# Protótipo 2 — Loop Híbrido Completo

Este documento junta o loop ativo (`docs/05-prototipo-1-loop-ativo.md`) e o
loop idle (`docs/04-prototipo-0-loop-idle.md`) em um único protótipo
jogável. É o último protótipo antes do MVP completo (ver
`docs/00-mvp-escopo.md`) e antes da entrega à fábrica.

## Cálculo da produção offline (regra técnica central)

A produção enquanto o jogo está fechado é calculada por **timestamp**, não
por um processo rodando em segundo plano:

1. Ao fechar o jogo (ou periodicamente, como proteção contra crash/queda de
   energia), o horário atual é salvo.
2. Ao abrir o jogo novamente, o horário salvo é comparado ao horário atual
   do dispositivo.
3. A diferença entre os dois horários é o tempo de ausência, usado para
   calcular a produção acumulada de cada estrutura.

### Fonte do horário: local no MVP, servidor no multiplayer
No MVP (single-player), o horário usado é o relógio local do dispositivo
do jogador — simples e sem necessidade de servidor.

Quando o multiplayer for implementado (Fase 3, ver
`docs/02-roadmap-fases.md`), essa fonte de horário deve ser substituída por
um **relógio de servidor**, pelos seguintes motivos:

- **Anti-trapaça**: relógio local pode ser adiantado manualmente pelo
  jogador para gerar produção falsa.
- **Consistência**: em fazendas compartilhadas ou visitas entre jogadores,
  não pode existir mais de uma fonte de horário "verdadeira".

A lógica de cálculo (tempo decorrido → produção) não muda entre os dois
casos — muda apenas de onde vem o valor do horário. O sistema deve ser
construído de forma que essa troca seja possível sem reescrever o cálculo
de produção.

## Limites à produção offline

Duas camadas de limite trabalham juntas:

### 1. Capacidade por estrutura (já definida no Protótipo 0)
Cada estrutura tem uma capacidade máxima de armazenamento. Ao atingir o
teto, a produção pausa até o jogador coletar. Nada é perdido pela pausa.

**A capacidade é upgradável através da progressão entre tiers/planetas**,
seguindo o mesmo princípio de progressão retroativa do conceito central
(ver `docs/01-conceito.md`): por exemplo, minerais extraídos de uma lua
podem liberar um projeto de upgrade de armazenamento; tecnologias de tiers
posteriores podem viabilizar drones que ampliam a capacidade de coleta;
módulos de nave podem liberar melhorias aplicáveis a estruturas de tiers
anteriores.

### 2. Teto global de horas (novo, inspirado em referências do gênero)
Além da capacidade por estrutura, existe um limite máximo de tempo de
ausência que o jogo considera para cálculo de produção, independente da
capacidade individual de cada estrutura.

- **Valor de partida sugerido: 48 horas.** Após esse período, a produção
  offline não continua sendo calculada além do teto — o jogo considera
  apenas as primeiras 48 horas de ausência para fins de produção.
- Motivo: evita que ausências muito longas (semanas ou meses) gerem
  cálculos desproporcionais ou tenham que ser simulados hora a hora,
  além de manter uma janela de retorno mais previsível para o jogador.
- Este valor é um ponto de partida, não definitivo — pode ser ajustado
  conforme testes de playtest indicarem necessidade.

### Ordem de aplicação dos dois limites
O tempo de ausência é primeiro limitado pelo teto global (48h), e só então
a produção é calculada estrutura por estrutura respeitando a capacidade
individual de cada uma. Ou seja, o teto global limita o tempo considerado;
a capacidade por estrutura limita o quanto essa estrutura acumula dentro
desse tempo.

## Loop completo do Protótipo 2

1. Jogador planta um cultivo (loop ativo, ferramenta selecionada + clique
   no tile).
2. Cultivo cresce ao longo do tempo (mesmo com o jogo fechado).
3. Jogador constrói estrutura de processamento (loop ativo).
4. Estrutura processa automaticamente o cultivo colhido, respeitando
   capacidade e teto global (loop idle).
5. Jogador retorna, coleta o que foi produzido (ativo), e decide os
   próximos passos — plantar mais, construir novo upgrade de capacidade,
   etc.

## Fora do escopo deste protótipo

- Múltiplos planetas, nave, viagens.
- Escavação e descobertas.
- Multiplayer e relógio de servidor (documentado aqui apenas como
  requisito futuro de arquitetura).
- Arte final.

## Perguntas a validar no playtest deste protótipo

1. O ciclo completo (plantar → fechar o jogo → reabrir → coletar →
   construir upgrade) parece satisfatório e compreensível?
2. O teto global de 48h é perceptível de forma incômoda, ou passa
   despercebido para a maioria dos padrões de uso?
3. A progressão de capacidade via upgrade (mineral → drone → módulo de
   nave) está clara o suficiente neste estágio inicial, mesmo sem os
   tiers posteriores implementados?

*Este é o último protótipo antes do MVP completo. Após validação, os
documentos `00` a `06` devem ser revisados e consolidados antes da entrega
à fábrica de desenvolvimento.*
