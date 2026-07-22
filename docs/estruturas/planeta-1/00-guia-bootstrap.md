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

## A solução: coleta manual + ferramentas de bootstrap

Antes de qualquer estrutura existir, o jogador pode coletar manualmente,
direto do terreno, sem custo:

- **Pedra Ancestral** — pedras soltas no chão.
- **Graveto** — gravetos soltos no chão.
- **Cedro Estelar** — de árvores selvagens já existentes no terreno
  inicial (remanescentes da fazenda antes do abandono, ver
  `docs/01-conceito.md`), sem precisar plantar.

Com Pedra Ancestral + Graveto coletados à mão, o jogador crafta 2
ferramentas (ver `docs/itens/itens.csv`):

- **Picareta** — aumenta velocidade/volume de coleta de Pedra Ancestral.
- **Machado** — aumenta velocidade/volume de coleta de Cedro Estelar.

## Ordem de construção (Nível 1 apenas — exceção)

As 3 estruturas abaixo têm custo de **Nível 1 excepcional** (matéria-prima
crua, sem Cascalho). A partir do **Nível 2**, todas as três retomam o
padrão normal (ver `docs/itens/requisitos_niveis.csv`, coluna `tipo`).

1. **Mina de Pedra (Nível 1)** — custo: Pedra Ancestral + Graveto (coletados
   à mão). Após construída, passa a produzir Pedra Ancestral em volume.
2. **Estrutura de Processamento — Pedra (Nível 1)** — custo: apenas Pedra
   Ancestral. Após construída, passa a produzir **Cascalho Ancestral** —
   o material-base que todas as demais estruturas do Planeta 1 usam.
3. **Área de Plantio de Árvores (Nível 1)** — custo: Cedro Estelar
   (cortado de árvores selvagens) + Graveto. Após construída, assume o
   ciclo de plantio contínuo de árvores.

## A partir daqui: padrão normal

Uma vez que o Cascalho Ancestral existe no jogo (após o passo 2 acima),
**todas as demais estruturas** — incluindo o Nível 2 em diante das 3
estruturas de bootstrap — seguem o padrão já fechado: material-base
(Cascalho Ancestral) + 1 material específico da estrutura, em
quantidade de acordo com o Porte (ver `docs/estruturas/00-indice.md`,
seção Porte da estrutura).

Ver `docs/itens/requisitos_niveis.csv` para os valores exatos de cada
estrutura e nível.

## Pendente
- Definir quantidade exata de Pedra Ancestral/Graveto/Cedro Estelar
  coletável à mão por ação (antes de qualquer ferramenta).
- Definir o quanto a Picareta e o Machado aumentam a velocidade/volume
  de coleta (ex: +50%, +100%).
- Validar em playtest se o período de bootstrap (antes de ter as 3
  estruturas) tem duração agradável — nem rápido demais (sem sensação de
  progresso) nem lento demais (frustrante).
