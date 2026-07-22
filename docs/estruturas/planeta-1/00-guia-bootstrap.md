# Guia de Bootstrap — Planeta 1

Este guia resolve um problema de circularidade identificado durante o
detalhamento dos requisitos de nível: três estruturas produzem o próprio
material que normalmente seria necessário para construí-las. A solução é
um período inicial de "bootstrap" (mesmo princípio usado em jogos de
sobrevivência/crafting): coleta manual sem estrutura, ferramentas
simples, depois construção das primeiras estruturas com custo reduzido.

## O problema

- **Mina de Pedra** produz Pedra Ancestral, mas normalmente pediria
  Cascalho Ancestral (processado a partir de Pedra Ancestral) para ser
  construída — circular.
- **Estrutura de Processamento (Pedra)** produz Cascalho Ancestral, mas
  normalmente pediria o próprio Cascalho para ser construída — circular.
- **Área de Plantio de Árvores** produz Cedro Estelar, mas normalmente
  pediria Cedro Estelar para ser construída — circular.
- **Campo de Cultivo** produz Trigo Lunar, mas normalmente pediria Trigo
  Lunar para ser construído — circular (mesma categoria de problema,
  identificada em revisão posterior às 3 primeiras).

## A solução: coleta manual + ferramentas de bootstrap

Antes de qualquer estrutura existir, o jogador pode coletar manualmente,
direto do terreno, sem custo:

- **Pedra Ancestral** — pedras soltas no chão.
- **Graveto** — gravetos soltos no chão.
- **Cedro Estelar** — de árvores selvagens já existentes no terreno
  inicial (remanescentes da fazenda antes do abandono, ver
  `docs/01-conceito.md`), sem precisar plantar.
- **Trigo Lunar** — de plantações selvagens já crescendo perto da
  fazenda inicial (re-semeadas sozinhas após o abandono), sem precisar
  plantar.

Com Pedra Ancestral + Graveto coletados à mão, o jogador crafta 2
ferramentas (ver `docs/itens/itens.csv`):

- **Picareta** — aumenta velocidade/volume de coleta de Pedra Ancestral.
- **Machado** — aumenta velocidade/volume de coleta de Cedro Estelar.
- **Foice de Pedra** — aumenta velocidade/volume de coleta de Trigo Lunar
  selvagem.

## Ordem de construção (Nível 1 apenas — exceção)

As 4 estruturas abaixo têm custo de **Nível 1 excepcional** (matéria-prima
crua, sem Cascalho). A partir do **Nível 2**, todas as quatro retomam o
padrão normal (ver `docs/itens/requisitos_niveis.csv`, coluna `tipo`).

1. **Mina de Pedra (Nível 1)** — custo: Pedra Ancestral + Graveto (coletados
   à mão). Após construída, passa a produzir Pedra Ancestral em volume.
2. **Estrutura de Processamento — Pedra (Nível 1)** — custo: apenas Pedra
   Ancestral. Após construída, passa a produzir **Cascalho Ancestral** —
   o material-base que todas as demais estruturas do Planeta 1 usam.
3. **Área de Plantio de Árvores (Nível 1)** — custo: Cedro Estelar
   (cortado de árvores selvagens) + Graveto. Após construída, assume o
   ciclo de plantio contínuo de árvores.
4. **Campo de Cultivo (Nível 1)** — custo: Trigo Lunar (colhido de
   plantações selvagens) + Graveto. Após construído, assume o ciclo de
   plantio contínuo de cultivos.

## A partir daqui: padrão normal

Uma vez que o Cascalho Ancestral existe no jogo (após o passo 2 acima),
**todas as demais estruturas** — incluindo o Nível 2 em diante das 3
estruturas de bootstrap — seguem o padrão já fechado: material-base
(Cascalho Ancestral) + 1 material específico da estrutura, em
quantidade de acordo com o Porte (ver `docs/estruturas/00-indice.md`,
seção Porte da estrutura).

Ver `docs/itens/requisitos_niveis.csv` para os valores exatos de cada
estrutura e nível.

## Taxa de coleta manual — decisão

| Recurso | Sem ferramenta | Com ferramenta |
|---|---|---|
| Pedra Ancestral | 1 unidade por ação | 2 unidades por ação (Picareta) |
| Cedro Estelar | 1 unidade por ação | 2 unidades por ação (Machado) |
| Trigo Lunar | 1 unidade por ação | 2 unidades por ação (Foice de Pedra) |

## Custo de craft das ferramentas — decisão

| Ferramenta | Custo |
|---|---|
| Picareta | 5 Pedra Ancestral + 5 Graveto |
| Machado | 5 Pedra Ancestral + 5 Graveto |
| Foice de Pedra | 5 Pedra Ancestral + 5 Graveto |

Ver `docs/itens/requisitos_niveis.csv` (linhas com `nivel = craft`).

## Sem trava dura entre as estruturas de bootstrap — decisão

Não existe verificação de pré-requisito obrigatório entre Mina de Pedra,
Processamento de Pedra, Área de Plantio de Árvores e Campo de Cultivo —
um jogador pode,
tecnicamente, coletar Pedra Ancestral à mão indefinidamente sem nunca
construir a Mina. Isso é **intencional, não uma lacuna**: a taxa de
coleta manual (1 unidade, 2 com ferramenta) é propositalmente lenta o
suficiente para tornar essa escolha irracional na prática frente à
produção em volume e passiva das estruturas — um "soft gate" econômico,
não uma trava de regra dura. Mantém a filosofia de decisões na mão do
jogador (ver `docs/01-conceito.md`) sem forçar uma ordem artificial.

## Pendente
- Validar em playtest se o período de bootstrap (antes de ter as 4
  estruturas) tem duração agradável — nem rápido demais (sem sensação de
  progresso) nem lento demais (frustrante).
- Validar se a taxa 1/2 realmente desincentiva a coleta manual prolongada
  na prática, ou se precisa ser ainda mais lenta.
