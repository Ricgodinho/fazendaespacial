# Conceito

## Gênero
Simulação de fazenda espacial aconchegante com loop **híbrido**: o jogador
planeja e executa ações ativamente, como plantar, construir, explorar e organizar
recursos, enquanto operações já configuradas continuam avançando de forma
passiva/idle durante e fora das sessões ativas.

A experiência deve transmitir curiosidade, progresso, cuidado e apego aos
planetas, evitando pressão constante, punições severas ou a obrigação de executar
repetidamente tarefas já dominadas.

## Cenário
O jogo ocorre em um sistema solar com múltiplos planetas e luas. Cada mundo
funciona como uma fazenda e também como um **planeta de progressão**, apresentando
clima, gravidade, recursos, cultivos, criaturas e desafios próprios.

Os planetas não funcionam como fases descartáveis. Cada mundo mantém recursos,
funções ou capacidades exclusivas e continua participando da rede produtiva mesmo
depois que novos planetas são desbloqueados.

Planetas avançados podem depender de materiais, tecnologias e produtos originados
em diferentes planetas anteriores.

## Identidade central
O jogador herda uma pequena propriedade agrícola parcialmente abandonada de um
parente distante com quem teve pouco contato. Inicialmente, a propriedade
representa apenas uma oportunidade de recomeçar a vida em um lugar tranquilo.

Durante a restauração da fazenda, o jogador descobre que o antigo proprietário
era um dos últimos responsáveis por uma rede agrícola interplanetária criada
para cultivar, preservar e conectar diferentes mundos.

A rede não foi destruída por uma única catástrofe. Ela entrou em decadência
gradualmente devido a falhas tecnológicas, dificuldades nas rotas de transporte,
isolamento das comunidades e falta de novos cuidadores. O antigo proprietário
manteve parte da operação funcionando até perceber que não conseguiria
reconstruí-la sozinho.

A jornada cresce gradualmente: cuidar da primeira fazenda, compreender o legado
recebido, recuperar tecnologias, melhorar a nave, alcançar novos mundos e
reconectar os diferentes planetas da rede.

A motivação principal deve ser descoberta e transformação. O jogador avança para
levar vida, cuidado e beleza a novos lugares, e não apenas para acumular dinheiro
ou aumentar números.

## Escavações e descobertas
Escavações funcionam como uma atividade de exploração e descoberta, e não como
um sistema baseado em cavar aleatoriamente todo o terreno.

Os locais de escavação são identificados por sinais visuais, mapas, registros,
sensores ou informações encontradas durante a exploração.

Entre as descobertas possíveis estão:

- peças de máquinas e drones danificados;
- fragmentos de projetos e tecnologias;
- sementes e organismos preservados;
- registros pessoais e históricos;
- coordenadas e informações sobre outros mundos;
- objetos decorativos;
- artefatos de diferentes raridades.

Itens necessários para o avanço principal possuem caminhos previsíveis e não
dependem exclusivamente de sorte. Descobertas opcionais podem variar em
localização, conteúdo e raridade.

Tecnologias obtidas em planetas posteriores permitem retornar aos planetas
anteriores e acessar ruínas, áreas ou camadas de escavação antes inacessíveis.

## Princípios de progressão

### Progressão significativa
Novos planetas não devem oferecer apenas versões numericamente superiores de
ferramentas, construções ou recursos. Cada avanço precisa introduzir uma nova
capacidade, alterar a forma de jogar ou permitir novas interações com planetas
anteriores.

### Progressão retroativa
Tecnologias obtidas em planetas avançados também devem transformar os mundos já
visitados. Um novo planeta pode liberar máquinas, drones, cultivos, melhorias,
áreas ou formas de automação aplicáveis aos planetas anteriores.

### Dependência entre planetas
Recursos de planetas antigos continuam relevantes em construções e tecnologias
avançadas. Um projeto do Planeta 5, por exemplo, pode exigir materiais produzidos
nos Planetas 2, 3 e 4.

### Raridade — decisão

**5 níveis de raridade**: comum, incomum, raro, épico e lendário.

A raridade não deve representar somente melhorias numéricas. Itens mais raros
podem possuir aparência, comportamento, aplicação, história ou capacidade
especial própria. Esta regra se aplica a ferramentas, recursos, plantas,
projetos, descobertas e também aos drones.

#### Liberação por progresso do jogador, não por planeta
Cada nível de raridade é liberado a partir de um marco de progressão do
jogador (chegar a um determinado planeta), mas uma vez liberado, pode
aparecer em **qualquer planeta já visitado** — incluindo os primeiros
planetas.

| Raridade | Liberada a partir de | Onde pode aparecer |
|---|---|---|
| Comum | Planeta 1 (sempre) | Qualquer planeta |
| Incomum | Planeta 1 (sempre) | Qualquer planeta |
| Raro | Planeta 3 | Qualquer planeta já visitado |
| Épico | Planeta 4 | Qualquer planeta já visitado |
| Lendário | Planeta 5 | Qualquer planeta já visitado |

Motivo: o gate é pelo progresso do jogador, não pelo planeta em si,
evitando risco de poder desproporcional cedo demais — quando um item
lendário pode aparecer no Planeta 1, o jogador já tem toda a força de jogo
de quem chegou ao Planeta 5.
Isso também mantém sempre algo novo para descobrir em planetas antigos,
reforçando o princípio de progressão retroativa já definido acima.

#### Sementes seguem a mesma tabela de raridade (decisão)
Sementes **não possuem uma escala própria separada** — usam a mesma
tabela de 5 níveis de raridade acima (Comum a Lendário), sujeitas ao
mesmo gate por progresso do jogador.

Dois filtros trabalham juntos, em ordem:
1. **Teto global (progresso do jogador)**: define o que **pode**
   aparecer em qualquer lugar. Sem o jogador ter alcançado o Planeta 4,
   por exemplo, nenhum item Épico aparece em lugar nenhum — nem no
   Planeta 1.
2. **Modificador local (Nível do Viveiro)**: dentro do teto já liberado
   pelo progresso do jogador, o nível de upgrade do Viveiro usado na
   colheita define a **probabilidade** de sair algo mais raro naquela
   coleta específica — um Viveiro de nível mais alto aumenta a chance de
   uma semente de raridade superior, mas nunca ultrapassa o teto global
   já liberado.

Esta regra substitui e descarta uma ideia anterior (nunca formalizada em
documento) de uma "janela deslizante" baseada no planeta do cultivo colhido
— superada por este modelo de dois filtros.

#### Fórmula do modificador local — decisão

Modelo de 2 estágios, aplicável a qualquer estrutura que produza itens
sujeitos a raridade (Viveiro, Estruturas de Processamento, Mina de
Pedra/Minério — pontos de descoberta):

**Estágio 1 — Chance-base, por Nível da estrutura** (ver
`docs/estruturas/00-indice.md` para o sistema geral de níveis):

| Nível | Chance-base de bônus |
|---|---|
| 1 | 2% |
| 2 | 3% |
| 3 | 4% |
| 4 | 6% |
| 5 | 8% |
| 6 | 10% |
| 7 | 15% |
| 8 | 18% |
| 9 | 22% |
| 10 | 28% |

**Estágio 2 — Cascata de magnitude** (se o Estágio 1 acertar, decide
quantos níveis de raridade acima — mesmo modelo de decaimento 10x usado
como referência de mercado, ver pesquisa citada na origem deste sistema):
- 90% → sobe 1 nível de raridade
- 9% → sobe 2 níveis
- 0,9% → sobe 3 níveis
- 0,1% → sobe 4 níveis

Sempre limitado pelo teto global já liberado pelo progresso do jogador —
a cascata nunca ultrapassa esse teto, mesmo que o sorteio indique mais.

Valores de exemplo, a validar em playtest/planilha.

#### Nota para fase futura (mercado entre jogadores — não decidido)
Foi levantada a ideia de, na Fase 3 (multiplayer, ver
`docs/02-roadmap-fases.md`), permitir um mercado entre jogadores onde um
jogador em planeta inicial possa comprar itens de raridade mais alta de outro
jogador, mesmo sem ainda poder obtê-los por conta própria. Esta é apenas uma
ideia registrada para debate futuro, não uma decisão.

## Automação e retorno aos planetas
O jogador começa executando pessoalmente grande parte das atividades da primeira
fazenda. Novos planetas liberam tecnologias que reduzem tarefas repetitivas nos
mundos anteriores.

Como exemplo inicial, o Planeta 2 (lua mineral) pode liberar projetos e materiais
para drones básicos utilizados no Planeta 1. Tecnologias obtidas no Planeta 3 podem
permitir automatizar completamente o ciclo produtivo já configurado no Planeta 1.

Automação completa significa que uma operação conhecida pode funcionar sem
intervenção manual constante. Decisões, expansão, configuração, decoração,
descobertas, eventos e novas melhorias continuam dependendo do jogador.

Os planetas anteriores permanecem visitáveis, personalizáveis e relevantes mesmo
quando sua produção básica estiver automatizada.

## Nave
O jogador herda uma nave antiga junto com a propriedade. Ela funciona como o elo
permanente entre os diferentes planetas e luas, sendo restaurada gradualmente ao
longo da jornada.

A nave serve para transportar o jogador, uma quantidade limitada de recursos e
os drones necessários para cada operação. Ela também funciona como um pequeno
espaço pessoal personalizável.

Seu interior deve permanecer compacto, podendo incluir cabine, armazenamento,
bancada de reparos e espaço limitado para drones. A nave não deve substituir as
fazendas nem permitir grandes plantações, linhas de produção ou expansões
excessivas.

### Desbloqueio de novos planetas
O acesso a um novo planeta ou lua depende de uma combinação de requisitos:

1. descobrir o destino por meio de coordenadas, mapas, registros, sensores,
   ruínas, observatórios ou informações fornecidas por personagens;
2. estabilizar funções essenciais do mundo atual por meio de marcos concretos,
   sem exigir que o planeta esteja completamente concluído;
3. recuperar ou construir um módulo funcional da nave necessário para enfrentar
   as condições do novo destino;
4. reunir os materiais necessários por meio da rede formada pelos planetas já
   visitados.

Os marcos de estabilização podem incluir recuperar uma área agrícola, estabelecer
uma produção estável, restaurar uma estrutura importante, produzir um recurso
especial ou resolver um problema ambiental local.

Os módulos da nave não devem representar apenas aumentos numéricos. Cada melhoria
precisa introduzir uma nova capacidade, como alcançar uma lua próxima, resistir
a temperaturas extremas, atravessar tempestades, pousar sobre água, transportar
organismos ou navegar por regiões gravitacionalmente instáveis.

Os módulos também devem produzir alterações visíveis na nave.

### Primeira expedição e conexão
A primeira viagem para um novo mundo funciona como uma expedição inicial com
capacidade limitada. O jogador explora o local, conhece suas condições e identifica
onde poderá estabelecer a nova fazenda.

Após essa primeira visita, o jogador precisa construir ou restaurar infraestrutura
local para conectar o novo mundo à antiga rede agrícola.

Somente depois dessa conexão são liberadas operações regulares, como transporte
de recursos, envio de drones, rotas logísticas e produção passiva.

As viagens são automáticas e não exigem pilotagem obrigatória. Elas podem incluir
animações e interações leves, mas não devem transformar o jogo em um simulador de
naves nem criar repetição desnecessária.

A estrutura geral de desbloqueio pode permanecer semelhante entre os planetas, mas
os desafios e as formas de cumprir cada requisito devem variar conforme a
identidade de cada planeta.

## Plataforma e escopo
- Alvo final: jogo indie completo, com lançamento em **Steam**.
- Plataforma inicial: PC.
- O projeto deve nascer preparado para localização em múltiplos idiomas.
- Idiomas iniciais planejados: português brasileiro, inglês e espanhol.
- Textos, interfaces e conteúdos narrativos não devem depender de frases fixas
  diretamente incorporadas aos sistemas do jogo.

## Multiplayer
Não faz parte do MVP. Planejado como objetivo de **fase 2 ou 3** do roadmap
(ver [02-roadmap-fases.md](02-roadmap-fases.md)), depois que o loop single-player
estiver validado.

## Direção visual/técnica
Referência de leveza e progressão passiva: **TBH: Task Bar Hero** (RPG idle que roda
em segundo plano, gráficos simples porém charmosos). A meta aqui é usar essa mesma
leveza de performance, mas com um visual mais elaborado e atraente do que um idle
minimalista.

## Em aberto para debate
- O que exatamente se planta, cria e produz (estágios visuais de
  crescimento já definidos em `docs/cultivos/00-indice.md`; nomes e
  detalhes específicos de cada cultivo ainda pendentes).
- Tipos e funções dos drones.
- Categorias definitivas de raridade.
- Objetivo de longo prazo e encerramento da jornada.
## Direção visual — decisão

**Estilo escolhido: 2D estilizado** (visual ilustrativo, tipo Spiritfarer/Cozy
Grove), com **low-poly 3D como plano B** caso o custo de produção do 2D
estilizado se mostre inviável para o orçamento do projeto.

### Requisito técnico decorrente
A câmera do jogo deve permitir **rotação e zoom livres** ao redor da cena.
Isso significa que, na prática, a arte precisa ser implementada como
**modelos 3D com textura/shader pintado à mão** (não ilustração 2D
tradicional nem imagens pré-renderizadas) — só assim a câmera pode
girar livremente mantendo a aparência ilustrativa desejada. Referências de
mercado nesse pipeline: Genshin Impact, Ni no Kuni, World of Warcraft.

### Nível de detalhe
Alto nível de detalhe visual (não um estilo minimalista/vetorial simples).

### Validação realizada
Testes visuais comparativos confirmaram que a diferenciação entre biomas
distintos (ex: bioma tropical vs. bioma de água) funciona bem nos três
estilos avaliados, reforçando que a escolha pode ser feita com base em
custo/qualidade sem prejuízo à identidade visual de cada planeta.

### Próximo passo
Levantar orçamento/viabilidade de produção do 2D estilizado (3D com
shader pintado) junto à fábrica antes de comprometer definitivamente.
Caso o custo não seja viável, migrar para low-poly 3D como estilo
definitivo — decisão de fallback já pré-aprovada, sem necessidade de nova
rodada de debate.
