# Questões Abertas de Implementação

Registro de pontos encontrados durante a implementação (Etapa 6, ver
`docs/08-roadmap-implementacao.md`) que dependem de uma decisão de design
antes de seguir, ou que expõem uma diferença entre o que os docs já
especificam e o que existe hoje no jogo. Objetivo: dar visibilidade pra
quem cuida do design, sem travar o desenvolvimento — cada item tem uma
sugestão de encaminhamento, não é bloqueante por si só.

## 1. Fibra Estelar sem estrutura de uso definida

`docs/itens/itens.csv` já lista Fibra Estelar como cultivo do Planeta 1
("Matéria-prima (cultivo avançado)"), mas:
- Não tem estrutura de processamento própria documentada (diferente de
  Trigo Lunar → Processamento de Comida, Cedro Estelar → Processamento
  de Madeira, Pedra Ancestral → Processamento de Pedra).
- `docs/cultivos/00-indice.md` só cita o nome como exemplo, sem detalhar
  tempo de crescimento, aparência ou uso.
- `usa_raridade` no itens.csv está marcado "A definir".

**Sugestão:** decidir qual estrutura processa Fibra Estelar (e em quê)
antes de implementar - hoje não dá pra seguir o mesmo padrão dos outros
cultivos sem inventar essa peça.

## 2. Mina de Pedra: só a extração foi implementada, não a descoberta

`docs/estruturas/planeta-1/mina-de-pedra.md`, seção "Função dupla",
define que a Mina de Pedra também funciona como ponto de
escavação/descoberta (achados, itens raros) - mesma função dupla da
Mina de Minério do Planeta 2. A implementação atual só cobre a extração
contínua de Pedra Ancestral (Nível 1); a mecânica de descoberta não
existe ainda, e o próprio doc marca isso como pendente ("detalhar o que
muda entre Camada 1/2/final em termos de tipos de descoberta").

**Sugestão:** tratar como item separado do roadmap, não como parte do
"Mina de Pedra pronta" - a extração já está funcional e testável
isoladamente.

## 3. Campo de Cultivo e Área de Plantio de Árvores não existem como estruturas construíveis

Este é o ponto mais relevante antes de começar o sistema de níveis (ver
pergunta abaixo).

`docs/estruturas/planeta-1/campo-de-cultivo.md` e
`docs/estruturas/planeta-1/area-plantio-arvores.md` descrevem **duas
estruturas construíveis próprias**, cada uma com sua tabela de 10
níveis, onde o nível controla quantos tiles de plantio/quantas árvores
simultâneas o jogador tem disponível - e o Nível 4 de cada uma é
justamente o breakpoint que libera compatibilidade com o Drone de
Plantio (ou seja, pelos docs, plantio automático só deveria funcionar
a partir do Nível 4 dessas estruturas).

Hoje, a implementação não tem essas estruturas: qualquer tile vazio do
grid pode receber qualquer cultivo diretamente (`GridTile.PlantCrop`),
sem precisar construir nada antes, e o Drone de Plantio já automatiza
isso sem nenhum gate de nível. Ou seja, a implementação atual pulou
direto para o que os docs descrevem como "Nível 4" dessas duas
estruturas, sem elas existirem de fato.

**Sugestão:** decidir se isso é aceito como simplificação permanente
(loop mais simples, sem gerenciar mais um tipo de estrutura) ou se
Campo de Cultivo / Área de Plantio de Árvores precisam ser
retrofitados como estruturas reais antes do sistema de níveis - do
contrário, o sistema de níveis para essas duas fica sem base para se
apoiar (não tem uma estrutura no nível 1 pra evoluir).

## 4. Sistema de níveis: confirmação do que já foi decidido

Pergunta recorrente: quantos níveis, e o que já foi fechado. Resposta,
direto de `docs/estruturas/00-indice.md`:

- **10 níveis por estrutura** (não 5, para dar mais fôlego à progressão
  do elemento com que o jogador mais interage). Já é decisão fechada,
  aplicável a **todas** as estruturas de **todos** os planetas.
- Nota registrada (não decisão) para fase futura (Fase 3/4): possível
  extensão a até 15 níveis usando materiais do Portal de Exploração
  (Planeta 5) - depende da aceitação do jogo pós-lançamento, não é
  compromisso.
- **3 regras combinadas por nível**, já fechadas:
  1. Todo nível aumenta um número (capacidade/velocidade/volume) com
     crescimento exponencial.
  2. Níveis-marco (breakpoints) introduzem uma capacidade nova, não só
     número maior.
  3. Níveis mais altos exigem material de outros planetas para evoluir.
- **Nível 10 sempre exige o planeta mais avançado disponível** (hoje,
  Planeta 5) - regra corrigida especificamente para não deixar
  estruturas de planetas avançados sem propósito fora do próprio
  planeta.
- **Progressão visual**: 5 variações visuais cobrem os 10 níveis, em
  pares (1-2, 3-4, 5-6, 7-8, 9-10) - 15 assets de arte por estrutura
  (5 variações × 3 estados de processamento), não 30.

**Já detalhado, estrutura por estrutura, para todo o Planeta 1**: cada
doc em `docs/estruturas/planeta-1/` já tem uma tabela completa de 10
níveis com valores de exemplo e breakpoints nomeados (Armazém Geral,
Hangar de Drones, Campo de Cultivo, Área de Plantio de Árvores, Mina de
Pedra, Processamento de Comida/Madeira/Pedra, Viveiro). Ou seja, a
etapa de design já está pronta para implementação no Planeta 1 - falta
"só" o código.

**Ainda pendente em todos esses docs** (marcado em cada um como
"Pendente"):
- Os **materiais exatos** exigidos em cada nível (hoje só a dependência
  de planeta está fixada - ex: "Planeta 1 + 2" - não o item específico).
  Ver `docs/10-pendencia-materiais-por-nivel.md` - pendência formal
  passada para a equipe decidir, com tabela consolidada das 9
  estruturas e sugestão de começar só pelos Níveis 1-4 (não dependem de
  Planeta 2 existir).
- Os **valores numéricos** de cada tabela são "exemplo" (progressão
  ~1,5x), a validar em playtest/planilha.
- Itens específicos de alguns breakpoints do Nível 10 (ex: qual é a "2ª
  espécie de árvore", qual é a "receita avançada" do Processamento de
  Comida).

**Conclusão prática:** dá para implementar os **Níveis 1-4 de todas as
estruturas do Planeta 1** hoje (não dependem de material de outro
planeta), respeitando o item 3 acima (decidir o que fazer com Campo de
Cultivo/Área de Plantio de Árvores primeiro). Níveis 5+ ficam
bloqueados até existir Planeta 2 e os materiais específicos serem
definidos.

## 5. Capacidade do Armazém será observada sem limitar o protótipo

A capacidade total por nível continua sendo uma regra de design, mas sua
aplicação efetiva ainda está aberta para playtest. Bloquear coleta e produção
agora pode impedir a avaliação de outras partes do loop e confundir um problema
de balanceamento com um problema de interação.

**Decisão experimental:** durante o protótipo, a capacidade será nominal. O
inventário pode ultrapassá-la sem perder ou recusar recursos, enquanto o jogo
registra quanto teria excedido e quais ações teriam sido bloqueadas. A
especificação dos eventos e métricas está em
`docs/estruturas/planeta-1/armazem-geral.md`, seção "Regra experimental no
protótipo — capacidade observacional".

Essa decisão também cobre o caso em que o jogador acumula mais de 100 unidades
antes de construir o primeiro Armazém: o estado é válido para o experimento e
deve ser registrado, não corrigido ou truncado automaticamente.

**Perguntas para o playtest:**

- O jogador precisa de uma capacidade pessoal antes do Armazém?
- O Armazém deve impor o primeiro limite ou ampliar um limite já existente?
- A capacidade nominal de 100 cria uma decisão interessante ou interromperia
  cedo demais o aprendizado?
- Com que frequência e por quanto tempo o inventário ultrapassa o limite?
- Construir o Armazém cedo parece uma escolha útil ou apenas burocrática?

**Critério de encerramento:** só ativar um limite real depois de analisar os
logs e registrar a decisão de design. Independentemente do resultado, nenhum
excedente pode desaparecer silenciosamente; bloqueio, buffer, tolerância ou
overflow precisam ter comportamento explícito.
