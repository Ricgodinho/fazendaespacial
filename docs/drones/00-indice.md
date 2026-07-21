# Drones — Índice

Este é o índice do sistema de Drones. Cada categoria de drone tem seu
próprio arquivo nesta pasta. Este documento cobre as decisões que se
aplicam a todas as categorias: funções gerais, modelo de progressão,
crafting e limites de quantidade.

## Arquivos desta pasta

- `colheita.md` — especificação do drone de Colheita (fechado; Tier 1 validado no protótipo técnico)
- `plantio.md` — especificação do drone de Plantio (fechado; Tier 1 validado no protótipo técnico)
- `transporte.md` — especificação do drone de Transporte/logística (fechado; Tier 1 validado no protótipo técnico)
- `escavacao.md` — especificação do drone de Escavação (fechado)
- `construcao.md` — especificação do drone de Construção/reparo (fechado)
- `companheiro.md` — especificação do drone companheiro (fechado)
- `arte/00-indice.md` — referências visuais específicas de drones
- `audio/00-indice.md` — efeitos sonoros específicos de drones

## Subtópico 1: Categorias de drones — decisão

### Categorias funcionais (automação)
Conectam-se à automação retroativa já definida no conceito central (ver
`docs/01-conceito.md`, seção "Automação e retorno aos planetas").

1. **Colheita** — colhe cultivos maduros automaticamente. Conecta-se
   diretamente ao loop idle (`docs/04-prototipo-0-loop-idle.md` e
   `docs/07-prototipo-2-loop-hibrido.md`).
2. **Plantio** — replanta automaticamente após a colheita, fechando o
   ciclo de produção sem intervenção manual.
3. **Transporte/logística** — move recursos entre estruturas do mesmo
   planeta e, futuramente, entre planetas pela rede agrícola
   interplanetária (tema central da narrativa).
4. **Escavação/exploração** — auxilia a identificar e escavar pontos de
   descoberta (ver seção "Escavações e descobertas" em
   `docs/01-conceito.md`).
5. **Construção/reparo** — auxilia a restaurar estruturas antigas ou
   construir novas mais rapidamente, reforçando o tema narrativo de
   restaurar o legado recebido.

### Categoria à parte: drone companheiro (não-funcional)

Diferente das categorias acima, o **drone companheiro** não automatiza
tarefas — ele existe para reforçar o vínculo emocional do jogador com o
jogo, alinhado ao princípio de "cuidado e apego aos planetas" já definido
no conceito central. Um drone que decorasse a fazenda automaticamente
contradiria a regra existente de que decoração é sempre decisão do
jogador; por isso o companheiro é deliberadamente não-funcional.

**Configurável entre dois modos:**
- **Modo silencioso**: apenas acompanha o jogador, sem interferir.
- **Modo dicas**: oferece dicas contextuais ao jogador (ex: avisos sobre
  estruturas cheias, sugestões de próximos passos).

**Potencial de monetização (nota, não decisão):** por ser puramente
cosmético/emocional e não afetar balanceamento de jogo, o drone
companheiro é um bom candidato a skins e acessórios vendáveis (cosméticos
pagos), sem os riscos de pay-to-win. Isso é uma nota para explorar mais
adiante, na definição do modelo de monetização do jogo — ainda não
decidido.

### Nota para fase futura: drone social/anfitrião (Fase 3 — não decidido)

Foi levantada a ideia de um **drone anfitrião/guia**, que apareceria
quando outro jogador visitasse a fazenda (ver "Multiplayer" em
`docs/02-roadmap-fases.md`), mostrando o que foi construído e recebendo o
visitante. Esta é apenas uma ideia registrada para debate futuro na Fase
3, não uma decisão.

## Subtópico 2: Modelo de progressão, crafting e limites — decisão

### Item por tier, não experiência/nível
Drones não evoluem por experiência (XP). Cada tier de um drone é um
**item próprio**, obtido por compra (moeda do jogo), drop, ou construção
— seguindo o mesmo modelo já usado para ferramentas e estruturas. Isso
aproveita a economia por tier e a dependência entre planetas já definidas
em `docs/01-conceito.md`, em vez de criar um sistema de progressão
paralelo.

### Não existe categoria separada de "crafting"
Quem processa/crafta é a **estrutura de processamento** (já definida em
`docs/05-prototipo-1-loop-ativo.md` e `docs/07-prototipo-2-loop-hibrido.md`),
não um drone. A categoria **Transporte/logística** é responsável por
alimentar essas estruturas com a matéria-prima colhida — não é necessária
uma categoria de drone dedicada a crafting.

### Dois limites distintos de quantidade de drones

1. **Capacidade de carga da nave** (temporário, por viagem): quantos
   drones o jogador consegue levar ao expandir para um novo tier/planeta.
   Já previsto na seção "Nave" de `docs/01-conceito.md`.
2. **Capacidade do hangar de drones** (permanente, por planeta): quantos
   drones podem operar simultaneamente numa fazenda já estabelecida,
   independente da capacidade de carga da nave. Requer uma nova estrutura
   — o **hangar de drones** — com capacidade própria e upgradável, seguindo
   o mesmo padrão de capacidade upgradável já usado no loop idle
   (`docs/04-prototipo-0-loop-idle.md`).

### Hangar: pool único compartilhado, com peso por tier
A capacidade do hangar é um **pool único**, compartilhado entre todas as
categorias de drone (não capacidade separada por categoria) — reforça a
decisão estratégica do jogador sobre como distribuir suas vagas.

Cada drone consome um **peso** do pool proporcional ao seu tier, evitando
que o jogador lote o hangar apenas com drones de tier avançado sem custo
estratégico algum.

| Tier do drone | Peso no hangar (exemplo) |
|---|---|
| 1 | 1 |
| 2 | 2 |
| 3 | 3 |
| 4 | 5 |
| 5 | 8 |

Valores de exemplo, a validar em playtest/planilha — mesmo princípio já
aplicado ao loop idle (ver `planilhas/planilha_loop_idle.xlsx`).

## Regra de simulação: pegar/largar acontece na chegada (decisão)

Validado durante a implementação do protótipo técnico (drones de
Colheita, Plantio e Transporte, todos Tier 1): qualquer drone que
carregue algo entre dois pontos só **retira** o item da origem e só
**entrega** o item no destino no momento exato em que chega
visualmente em cada ponto — nunca no instante em que a viagem é
decidida.

Motivo: sem essa regra, outros sistemas (ex: outro drone que dependa do
mesmo recurso) enxergam o item como já disponível/já removido antes do
trajeto realmente acontecer, quebrando a leitura visual do jogo e
criando inconsistências (ex: um drone de Plantio usando uma semente que
o drone de Transporte ainda nem chegou a entregar no Armazém).

Aplica-se tanto a retirar quanto a depositar, em qualquer estrutura
(Armazém Geral, Viveiro, Estruturas de Processamento, etc.).

## Pendências gerais do sistema de Drones
- Especificação tier a tier das categorias ainda não fechadas (Plantio,
  Transporte, Escavação, Construção).
- Aplicação da raridade dentro de cada categoria/tier.

*Este índice é atualizado conforme decisões gerais (aplicáveis a todas as
categorias) forem debatidas. Decisões específicas de cada drone ficam no
arquivo próprio da categoria.*

## Variantes ambientais (não são categorias novas)

Alguns planetas introduzem **variantes ambientais** de categorias já
existentes — mesma função, design/aparência adaptada ao ambiente do
planeta. Exemplo: o Planeta 3 (água) introduz variantes aquáticas dos
drones de Plantio e Escavação (visualmente parecidas com um submarino),
que continuam sendo apenas Plantio e Escavação, não uma 6ª/7ª categoria.

## Desgaste e manutenção (regra geral) — decisão

Drones não são eternos. Cada drone possui **durabilidade** (começa em
100%), que diminui a cada ciclo de operação. Ao zerar, o drone precisa de
manutenção antes de voltar a operar.

### Fluxo de manutenção
1. O Hangar detecta o drone com durabilidade zerada e o move
   automaticamente para o **Armazém Geral do mesmo planeta** onde estava
   trabalhando (ver `docs/estruturas/planeta-1/armazem-geral.md`) — sem
   necessidade de viagem entre planetas.
2. O reparo tem **custo** (material/moeda, valor exato a definir).
3. Cada reparo restaura a durabilidade ao máximo, mas **reduz o teto
   máximo dela** — após vários reparos, o teto fica baixo o suficiente
   para que trocar o drone (comprar/craftar um novo) valha mais a pena
   que continuar consertando. Isso cria obsolescência natural sem
   depender de sorte ou quebra aleatória.

### Curva por Tier do drone
Desgaste por ciclo e redução de teto por reparo **melhoram com o tier**
— drones mais avançados são mais duráveis, reforçando a diferença entre
tiers além de capacidade/velocidade.

| Tier do drone | Desgaste por ciclo de uso | Redução de teto por reparo |
|---|---|---|
| 1 | 2% | -8% |
| 2 | 1,5% | -6% |
| 3 | 1% | -5% |
| 4 | 0,75% | -3% |
| 5 | 0,5% | -2% |

Valores de exemplo, a validar em playtest/planilha.

### Pendente
- Definir custo exato de cada reparo (material/moeda).
- Definir se manutenção automática (configurável no Armazém Geral, ver
  `docs/estruturas/planeta-1/armazem-geral.md`) tem custo adicional além
  do reparo em si.

## Origem dos drones: fabricação vs. achado danificado (regra geral) — decisão

**Apenas a Oficina/Fábrica de Drones do Planeta 2** fabrica drones de
verdade (ver `docs/estruturas/planeta-2/oficina-de-drones.md`). O
Planeta 1 não possui fabricação própria de drones.

### O que se encontra no Planeta 1 (via escavação na Mina de Pedra)
Dois tipos de achado distintos, ambos conectados à seção "Escavações e
descobertas" de `docs/01-conceito.md`:

1. **Drones danificados**: precisam de reparo (ver seção Desgaste e
   Manutenção acima) antes de funcionar. Tier 1 tem chance razoável de
   ser encontrado; Tier 2 é bem mais raro. Após reparado, funciona
   normalmente, mas **não carrega capacidades especiais de fabricação**
   (ex: rota interplanetária do Drone de Transporte a partir do Tier 2) —
   a menos que seja enviado ao Planeta 2 para passar pela Oficina.
2. **Projetos/plantas de drones** (blueprints): ao serem levados até a
   Oficina do Planeta 2, **liberam a fabricação** daquele tipo/tier de
   drone por lá.

### A Oficina do Planeta 2 como "renovação"
Um drone danificado encontrado e reparado no Planeta 1, se enviado à
Oficina do Planeta 2, pode passar por uma **renovação** — processo que o
eleva ao padrão de um drone fabricado, incluindo capacidades especiais
(como a rota interplanetária do Transporte). Isso torna a origem/histórico
de cada drone uma decisão estratégica real: vale a pena mandar este
específico para renovação, ou não?

Este ciclo (descoberta no Planeta 1 → projeto levado ao Planeta 2 →
fabricação/renovação → drone melhor retorna) é o mesmo princípio de
progressão retroativa já central ao design (ver `docs/01-conceito.md`).
