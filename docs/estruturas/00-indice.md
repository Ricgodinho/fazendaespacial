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

1. **Produzindo** (prioridade mais alta): a estrutura tem insumo
   suficiente e espaço de saída disponível, processando ativamente.
   Deve comunicar "trabalho em andamento" e, se possível, o progresso
   do ciclo atual (ex: intensidade de cor, animação contínua).
2. **Concluído/cheio**: não está mais produzindo (por falta de insumo
   para outro ciclo ou por ter enchido a capacidade de saída), mas tem
   produto acumulado esperando o jogador coletar. Deve usar a **mesma
   linguagem visual de "pronto para interagir"** usada no estágio final
   de crescimento dos cultivos (ver `docs/cultivos/00-indice.md`) —
   reforça uma convenção única de "isso aqui brilha/destaca-se quando
   dá pra clicar", em vez de uma convenção por sistema.
3. **Vazio/ocioso**: sem insumo suficiente para iniciar um ciclo e sem
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
  será definida na prova do 3D estilizado low-poly refinado
  (`docs/01-conceito.md`) — o protótipo técnico usa cor + rotação como
  placeholder, não como especificação de arte definitiva.

## Progressão visual por nível (regra geral) — decisão

Aplica-se a **toda estrutura**, em qualquer planeta, para evitar custo de
arte proporcional aos 10 níveis definidos em "Sistema de Níveis de
Estrutura" (acima).

**5 variações visuais** cobrem os 10 níveis, em pares:

| Variação visual | Níveis cobertos |
|---|---|
| A | 1–2 |
| B | 3–4 |
| C | 5–6 |
| D | 7–8 |
| E | 9–10 |

Cada uma das 5 variações deve ser definida e apresentada nos 3 "Estados
visuais de processamento" (acima) — **15 combinações visuais por
estrutura**, não 30 (5 variações × 3 estados, em vez de 10 níveis × 3
estados).

Isso não obriga a produzir 15 modelos 3D completos e independentes. A
prova de arte técnica deve definir o reaproveitamento adequado entre
modelo-base, peças ativadas por nível, animação, material, emissivo e
efeitos. Para geração conceitual por IA, porém, cada ficha deve mostrar
explicitamente as 15 combinações para evitar que um estado ou faixa de
nível fique sem direção visual.

### Motivo
Reforça a mesma lógica de "nem todo nível precisa ser um evento visual
próprio" já usada nos breakpoints de capacidade (acima) — a maioria dos
níveis dentro de um par só muda número (capacidade/velocidade), a
aparência do modelo só muda ao subir de par.

### Pendente
- Definir, na prova de cada estrutura, quais peças, animações ou efeitos
  comunicam os breakpoints sem exigir um sexto modelo-base.

### Relação com breakpoints — decisão

As 5 variações de modelo e os breakpoints funcionais são **independentes**.
A base visual muda ao entrar em uma nova faixa (níveis 3, 5, 7 e 9),
preservando a regra fechada dos pares. Quando uma capacidade nova é
desbloqueada dentro da mesma faixa — por exemplo, um breakpoint no nível
4 ou 10 — ela deve aparecer por peça modular, conteúdo visível, animação,
efeito ou sinal funcional, sem exigir outra variação completa do edifício.

Assim, todo breakpoint importante continua visível, mas o orçamento
permanece em 5 modelos-base por estrutura e 15 combinações conceituais de
faixa × estado.

## Pendências gerais
- Todos os 5 planetas do jogo base têm lista de estruturas fechada.
- Regra geral de níveis de estrutura definida (acima).
- Detalhar os níveis específicos de cada estrutura, uma por uma,
  começando pelo Planeta 1.

## Porte da estrutura (regra geral) — decisão

Cada estrutura possui um **Porte**: Pequeno, Médio ou Grande — usado para
diferenciar (a) a quantidade de material exigida por nível (ver
`docs/itens/requisitos_niveis.csv`) e (b) o **footprint físico** (espaço
ocupado no terreno), em vez de toda estrutura pedir a mesma quantidade ou
ocupar o mesmo espaço.

| Porte | Exemplo (Planeta 1) | Footprint (tiles) |
|---|---|---|
| Pequeno | Viveiro | 5×5 = 25 |
| Médio | Campo de Cultivo, Área de Plantio de Árvores, Estruturas de Processamento | 8×8 = 64 |
| Grande | Mina de Pedra, Armazém Geral, Hangar de Drones | 12×12 = 144 |

Footprint usado para calcular escala de terreno por planeta — ver
exemplo aplicado em `docs/estruturas/planeta-1/00-guia-estilo-visual.md`,
seção "Escala e formato do terreno".

Quantidade de material por nível escala com o porte (Grande > Médio >
Pequeno), mantendo a mesma progressão exponencial por nível já definida
para todas.

## Agrupamento por categoria, não por item (regra geral) — decisão

Toda estrutura de processamento atende **uma categoria inteira** de
matéria-prima, nunca um item específico isolado. Ex: Estrutura de
Processamento (Comida) processa todo cultivo de comida (Trigo Lunar,
Fibra Estelar, e futuros), não só um deles — mesmo princípio para
Processamento de Pedra (toda matéria-prima de pedra).

**Exceção confirmada — Planeta 2:** Pedra e Metal/Mineral permanecem
categorias **separadas**, mesmo estando no mesmo planeta. Pedra extraída
no Planeta 2 é enviada ao Processamento de Pedra do Planeta 1 (decisão
de teste já registrada em `docs/estruturas/planeta-2/00-indice.md`);
Metal/Mineral processa localmente na Fundição. Não fundir as duas
categorias em uma só.

