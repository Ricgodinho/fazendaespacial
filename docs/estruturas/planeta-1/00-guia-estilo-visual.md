# Guia de Estilo Visual — Planeta 1

**Propósito deste documento**: descrição de referência para ser
repassada a uma IA geradora de imagens (ou artista humano) produzir a
arte conceitual do Planeta 1. Não contém nem gera imagens — apenas
especifica o que deve ser gerado, com linguagem visual concreta.

## Estilo técnico — decisão para a prova visual

**3D estilizado low-poly refinado, com materiais/texturas pintados à mão
e acabamento ilustrado.** Formas simples e fortes, bordas suavizadas,
silhuetas fáceis de reconhecer, detalhes concentrados nos pontos de
interação e textura sem fotorrealismo.

Spiritfarer e Cozy Grove são referências de cor, aconchego e
personalidade, não de pipeline. Referências de execução 3D: Ni no Kuni,
World of Warcraft e cenários estilizados de Genshin Impact, adaptados a
um orçamento indie e sem copiar personagens, construções ou composição.

Evitar: pixel art, fotorrealismo, visual plástico genérico, excesso de
microdetalhes, contornos pretos pesados em todos os objetos e aparência
de render 2D que não possa ser reproduzida ao girar a câmera.

As imagens geradas pelo ChatGPT serão usadas como conceito e
especificação visual. A transformação em asset Unity ainda exige modelo
3D, UV, material, pivô, colisão e configuração técnica.

## Câmera de referência — decisão para a prova vertical

- Projeção: perspectiva.
- Campo de visão vertical inicial: **35°**, com faixa de teste entre
  30° e 40°.
- Rotação horizontal: **360°** ao redor do ponto de interesse.
- Inclinação vertical: limitada entre **35° e 65°**; padrão em **50°**.
  "Câmera livre" significa rotação horizontal livre e zoom contínuo,
  não permitir ângulos rasantes ou visão por baixo do terreno.
- Vista padrão: aproximadamente **28 tiles** de largura visível.
- Zoom próximo: aproximadamente **12–16 tiles** de largura, usado para
  acompanhar drones, cultivos e estados de estruturas.
- Zoom de visão geral: aproximadamente **65 tiles** de largura, suficiente
  para enquadrar o disco de 60 tiles; detalhes pequenos podem usar ícones
  ou indicadores em espaço de tela nesse zoom.
- Resolução-base para avaliação: **1920×1080, 16:9**. Verificar também
  2560×1440 e ultrawide antes de fechar UI e composição, sem produzir
  assets adicionais nesta etapa.
- Near Clip Plane inicial: **0,1 m**.
- Far Clip Plane inicial: **150–200 m**.
- Movimento e rotação com suavização curta, aproximadamente
  **0,15–0,25 s**.
- A câmera orbita um ponto de interesse e nunca entra no terreno, passa
  por baixo do disco ou atravessa estruturas.

Esses valores são parâmetros iniciais de mercado para um jogo de gestão
acolhedor em perspectiva 3/4. Devem ser testados com volumes simples no
Unity antes de virar especificação definitiva de produção.

No zoom geral, drones, estado de produção e pontos de interação podem
usar marcadores discretos em espaço de tela. Não aumentar fisicamente os
modelos apenas para mantê-los legíveis nessa distância.

## Escala e formato do terreno — decisão

O planeta **não é uma esfera com wraparound** (tecnicamente complexo,
raro no gênero) nem um cilindro com geometria contínua — é um sistema de
**2 camadas separadas** (discos planos independentes, ligados por uma
entrada/transição de cena, mesmo princípio já usado por jogos do gênero
para acesso a minas):

### Camada 1 — Superfície
Disco plano de **60 tiles de diâmetro** (~2.827 tiles de área, 1 tile ≈
1 metro²). Contém todas as estruturas do Planeta 1, exceto a Mina de
Pedra. Folga generosa sobre o mínimo funcional calculado (~1.052 tiles
somando footprint de estruturas + área de plantio no Nível 10 máximo).

#### Organização do mapa — decisão

O layout usa **setores orgânicos conectados**, com a casa principal
ligeiramente fora do centro como referência de orientação:

- clareira inicial de aproximadamente 18–20 metros ao redor da casa;
- Campo de Cultivo próximo da casa;
- Área de Plantio de Árvores irregular em uma lateral;
- zona de processamento em outra lateral;
- Armazém Geral e Hangar próximos de um caminho logístico principal;
- entrada da Mina próxima da borda, oposta à área mais acolhedora;
- perímetro com vegetação, pedras, ruínas e áreas inicialmente bloqueadas;
- aproximadamente 35–40% do disco livre no início para comunicar
  expansão futura.

A composição não deve parecer uma grade urbana nem uma ilha decorativa
minúscula. Caminhos, vegetação e limites naturais suavizam a grade de
construção, mas não podem esconder os espaços utilizáveis.

### Camada 2 — Subterrâneo (Mina)
Disco plano separado de **40 tiles de diâmetro** (~1.257 tiles de área),
acessado via entrada/transição de cena a partir da Mina de Pedra na
Camada 1 — não há wraparound nem geometria contínua entre as camadas.

Este espaço físico é o que dá corpo às **"camadas de escavação"** já
definidas em `docs/estruturas/planeta-1/mina-de-pedra.md` (Nível 4 libera
acesso a mais área desta camada; Nível 10 libera a camada final) — a
progressão de nível da Mina expande literalmente o território jogável
aqui, não é só um efeito numérico.

## Mood / sensação
Fazenda acolhedora carregando peso de legado — não é um lugar novo, é um
lugar **recuperando vida** após anos de abandono. Luz quente e
nostálgica, tipo "golden hour" constante, em contraste com um céu
estrelado sempre visível ao fundo (reforçando que é um planeta pequeno e
isolado no espaço).

## Paleta de cores

| Papel | Descrição | Referência de cor (ponto de partida) |
|---|---|---|
| Base — vegetação | Verdes naturais, folhagem viva | Verde-oliva a verde-esmeralda suave |
| Base — terreno | Tons terrosos | Marrom-terra, bege, ocre |
| Acento quente | Luz do sol, janelas iluminadas | Dourado / âmbar |
| Acento tecnológico | Drone, painéis, tecnologia | Azul suave, quase ciano — contraste proposital entre orgânico e tecnológico |
| Fundo (Camada 1) | Céu noturno/espacial sempre visível | Azul-arroxeado escuro, estrelas pontuais |
| Fundo (Camada 2) | Subterrâneo, sem céu | Marrom-escuro/cinza-pedra, iluminação pontual (tochas/cristais) |

## Elementos-chave de composição (Camada 1)
- Casa principal — modesta, envelhecida mas não em ruínas, luz quente
  vazando de dentro
- Campos de cultivo — fileiras organizadas, mistura de terra preparada e
  plantas em diferentes estágios de crescimento
- Área de árvores — algumas árvores selvagens já existentes, dispersas,
  não uniformes
- Entrada da Mina de Pedra — modesta, semi-natural, sugerindo uso humilde
  prévio (não industrial), com transição visível para a Camada 2
- Céu — sempre visível ao fundo, com pelo menos 1 outro planeta/lua
  visível a distância, reforçando a escala do sistema solar
- Drone companheiro — pequeno, com sinais visíveis de desgaste/avaria
  (não brilhante e novo)

## Contraste com os demais planetas
O Planeta 1 deve ser o único **verde e vivo** de todo o jogo — os demais
(Planeta 2: lua seca/mineral, Planeta 3: água, Planeta 4: gelo,
Planeta 5: tecnológico/decadente) são mais estéreis ou artificiais. A
composição deve enfatizar essa vitalidade orgânica como identidade única
deste planeta.

## Referências
- `artes/planeta-1/exemplos-estilos.png` (já escolhida como
  direção aprovada)
- Jogos de referência de estilo: Spiritfarer, Cozy Grove

## Pendente
- Executar os prompts da prova vertical definidos em
  `docs/arte/02-prova-vertical-mapa-planeta-1.md`.
- Definir paleta de cores em hexadecimal exato após primeira geração de
  referência.
- Validar FOV, inclinação e três faixas de zoom dentro do Unity com o mapa
  em escala.
- Ajustar a densidade de vegetação e props depois do teste de oclusão e
  leitura dos footprints.
- Definir a mecânica exata de transição entre Camada 1 e Camada 2
  (loading/fade, animação de descida, etc.) — decisão de implementação,
  não de arte.
- Repetir este mesmo formato de guia para os Planetas 2 a 5 quando
  chegar a vez de cada um (considerando se cada um também terá sistema
  de camadas, caso a caso).
