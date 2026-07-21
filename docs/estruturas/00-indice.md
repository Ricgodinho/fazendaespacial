# Estruturas — Índice Geral

## Atenção: dois conceitos diferentes neste documento

1. **Planeta**: qual planeta/mundo é esse na progressão do jogo (ver
   `docs/01-conceito.md`). Cada planeta tem seu próprio conjunto de
   estruturas — algumas exclusivas daquele planeta (ex: só o planeta
   inicial tem Mina de Pedra "herdada do antigo dono").
   *Exemplo: "Planeta 1" = o planeta inicial herdado do parente distante.
   Nome definitivo do planeta ainda não decidido — ver
   `docs/01-conceito.md`, seção "Em aberto para debate".*

2. **Nível da Estrutura**: o upgrade/evolução de uma estrutura específica
   dentro do mesmo planeta — o mesmo conceito já usado para os drones
   (ver `docs/drones/colheita.md`, onde o drone de Colheita tem 5 níveis).
   *Exemplo: "Campo de Cultivo Nível 1" vs "Campo de Cultivo Nível 8" — a
   regra geral está definida abaixo; os níveis específicos de cada
   estrutura ainda serão detalhados um a um.*

Cada planeta possui seu próprio conjunto de estruturas, seguindo a mesma
lógica de progressão significativa já definida em `docs/01-conceito.md`
(cada planeta introduz algo novo, não só uma versão mais forte do que já
existia).

Nomes de estruturas abaixo são **provisórios** — a nomenclatura final será
revisada considerando a localização multi-idioma do jogo (ver
`docs/06-stack-tecnica.md`). O foco nesta fase é documentar claramente a
função de cada estrutura, não o nome definitivo.

## Estrutura de pastas

- `planeta-1/00-indice.md` — estruturas do planeta inicial — fechado
- `planeta-2/00-indice.md` — estruturas do segundo planeta (a lua) — fechado
- `planeta-3/00-indice.md` — estruturas do terceiro planeta (água) — fechado
- `planeta-4/00-indice.md` — estruturas do quarto planeta (gelo) — fechado
- `planeta-5/00-indice.md` — estruturas do quinto planeta (origem/núcleo) — fechado

## Status (por Planeta)

| Planeta | Status |
|---|---|
| Planeta 1 (planeta inicial) | ✅ Lista de estruturas fechada — nível de cada estrutura a detalhar |
| Planeta 2 (a lua) | ✅ Lista de estruturas fechada — nível de cada estrutura a detalhar |
| Planeta 3 (água) | ✅ Lista de estruturas fechada — nível de cada estrutura a detalhar |
| Planeta 4 (gelo) | ✅ Lista de estruturas fechada — nível de cada estrutura a detalhar |
| Planeta 5 (origem/núcleo) | ✅ Lista de estruturas fechada — nível de cada estrutura a detalhar |

## Sistema de Níveis de Estrutura (regra geral) — decisão

Aplica-se a **todas** as estruturas de **todos** os planetas.

### Quantidade de níveis
**10 níveis** por estrutura, cobrindo o jogo base completo (Planetas 1 a
5). Não usa o mesmo "5" de raridade/drones/planetas de propósito — 10
níveis dão mais fôlego à progressão de estruturas especificamente, que é
o elemento com o qual o jogador mais interage repetidamente ao longo de
toda a partida (loop híbrido).

**Nota para fase futura (Fase 3/4 — não decidido):** cogitada extensão
para até **15 níveis**, usando novos materiais a serem introduzidos nessa
fase (ver conteúdo do Portal de Exploração,
`docs/estruturas/planeta-5/portal-de-exploracao.md`). Depende da aceitação
do jogo após lançamento — não é compromisso, apenas direção registrada.

### Três regras combinadas por nível

1. **Todo nível aumenta um número** (capacidade, velocidade ou volume de
   produção), com **crescimento exponencial** (cada upgrade multiplica o
   resultado, não soma um valor fixo) — mantém a sensação de progresso
   constante mesmo nos níveis "de transição".
2. **Níveis-marco (breakpoints) introduzem uma capacidade nova**, não
   apenas número maior — mesma regra de progressão significativa já usada
   nos drones (ver `docs/drones/colheita.md`) e definida no conceito
   central (`docs/01-conceito.md`). Nem todo nível precisa ser um
   breakpoint; a maioria só aumenta números, mantendo o ritmo de pequenas
   recompensas frequentes que sustenta o loop idle.
3. **Níveis mais altos exigem materiais de outros planetas** para evoluir,
   reforçando a dependência entre planetas já central ao design (ver
   `docs/01-conceito.md`, "Dependência entre planetas"). Uma estrutura do
   Planeta 1, por exemplo, pode exigir material do Planeta 2 ou 3 para
   evoluir a partir de determinado nível — a progressão de uma única
   estrutura passa a depender da rede interplanetária, não só do próprio
   planeta onde ela está.

### Regra corrigida: Nível 10 sempre inclui o planeta mais avançado disponível
O **Nível 10 (teto) de qualquer estrutura de qualquer planeta** deve
exigir material do planeta mais avançado que exista no jogo (hoje, o
Planeta 5) — não apenas dos planetas "vizinhos". Motivo: sem essa regra,
estruturas exclusivas de planetas avançados (ex: a Lua Satélite do
Planeta 5, que só serve para alimentar a Fundição Central) ficariam sem
propósito fora do próprio planeta onde existem, contrariando o princípio
de progressão retroativa já definido em `docs/01-conceito.md`. Levar
qualquer estrutura do Planeta 1 ao nível máximo deve exigir ter dominado
o jogo inteiro até o Planeta 5 — criando um gancho natural de
"completude pós-jogo".

### Próximo passo
Detalhar, estrutura por estrutura, quais níveis são breakpoints, quais
capacidades cada breakpoint introduz, e quais materiais/quantidades são
exigidos em cada nível — a começar pelo Planeta 1.

## Estados visuais de processamento (regra geral) — decisão

Aplica-se a **toda estrutura que processa matéria-prima ao longo do
tempo** (ex: Estruturas de Processamento), em qualquer planeta. Validado
no protótipo técnico (Protótipo 2, ver
`docs/07-prototipo-2-loop-hibrido.md`) e registrado aqui como requisito
de arte a levar para o setor de arte (ver `docs/arte/00-indice.md`).

3 estados, com prioridade visual nesta ordem:

1. **Processando** (prioridade mais alta): a estrutura tem insumo
   suficiente e espaço de saída disponível, processando ativamente.
   Deve comunicar "trabalho em andamento" e, se possível, o progresso
   do ciclo atual (ex: intensidade de cor, animação contínua).
2. **Pronto para coleta**: não está mais processando (por falta de
   insumo ou por ter enchido a capacidade de saída), mas tem produto
   acumulado esperando o jogador coletar. Deve usar a **mesma
   linguagem visual de "pronto para interagir"** usada no estágio final
   de crescimento dos cultivos (ver `docs/cultivos/00-indice.md`) —
   reforça uma convenção única de "isso aqui brilha/destaca-se quando
   dá pra clicar", em vez de uma convenção por sistema.
3. **Parado/ocioso**: sem insumo suficiente para iniciar um ciclo e sem
   produto pendente. Visual neutro, discreto — não deve competir com os
   outros dois estados, que exigem atenção do jogador.

### Motivo
Sem alguma sinalização entre os 3 estados, o jogador não consegue saber
à distância se uma estrutura precisa de atenção (alimentar com insumo)
ou se já tem algo pronto pra buscar — quebra o princípio de comunicar
claramente o resultado do tempo passivo, já central ao loop híbrido
(`docs/01-conceito.md`, `docs/04-prototipo-0-loop-idle.md`).

### Pendente
- Referência visual concreta (cor exata, tipo de animação, silhueta)
  depende da direção de arte final (2D estilizado vs. low-poly 3D,
  `docs/01-conceito.md`) — o protótipo técnico usa cor + rotação como
  placeholder, não como especificação de arte definitiva.

## Pendências gerais
- Todos os 5 planetas do jogo base têm lista de estruturas fechada.
- Regra geral de níveis de estrutura definida (acima).
- Detalhar os níveis específicos de cada estrutura, uma por uma,
  começando pelo Planeta 1.
