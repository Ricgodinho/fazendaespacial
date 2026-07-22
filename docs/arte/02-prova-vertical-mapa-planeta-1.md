# Prova vertical — mapa do Planeta 1

## Objetivo

Começar a definição de arte pelo mapa para validar composição, câmera,
escala, leitura dos footprints e densidade visual antes de gerar fichas
individuais. A primeira imagem não é layout final nem asset Unity: é um
estudo controlado.

## Layout escolhido

O ponto de partida aprovado é **setores orgânicos conectados**. A geração
com três opções permanece útil para comparar composições, mas não reabre
a escolha entre mapa radial, eixo rígido casa–mina e setores: as variações
devem explorar maneiras diferentes de executar setores orgânicos.

A casa fica ligeiramente fora do centro, com clareira inicial de 18–20 m.
O mapa começa com aproximadamente 35–40% de espaço livre, distribuído em
áreas reconhecíveis de expansão.

## Cena de validação no Unity

Criar `Assets/_Project/Scenes/ArtSandbox_Planeta1.unity`, isolada de save,
inventário, produção e `GameBootstrap`. Deve conter apenas:

- disco de 60 m e grade opcional;
- volumes simples de 5×5, 8×8 e 12×12 m;
- referência humana de 1,7 m e drone em escala;
- câmera orbital e três posições predefinidas de zoom;
- luz, ambiente, setores, caminhos e vegetação provisória.

Não expandir diretamente o grid 8×8 atual: ele usa tiles de 1,2 m e foi
criado para testar mecânica, não composição final.

## Dados fixos

- Superfície: disco plano jogável de 60 tiles de diâmetro, 1 tile ≈ 1 m².
- Subterrâneo: disco separado de 40 tiles de diâmetro; não aparece junto
  da superfície na primeira prova.
- Estruturas pequenas: referência de footprint 5×5 tiles.
- Estruturas médias: 8×8 tiles.
- Estruturas grandes: 12×12 tiles.
- Câmera-base: perspectiva, FOV 35°, inclinação 50°, rotação horizontal
  livre.
- Mood: recuperação de uma fazenda antiga, golden hour e céu espacial.

## Prompt 1 — exploração do mapa em três opções

Copiar o bloco mestre e as restrições negativas de
`docs/arte/01-direcao-e-pipeline-prompts.md`, depois acrescentar:

> Função: criar uma folha de exploração de composição para a superfície
> do Planeta 1, não uma ilustração promocional. Mostrar três opções de
> layout lado a lado para o mesmo disco jogável de 60 metros/tile de
> diâmetro.
>
> Escala e câmera: visão 3/4 elevada de jogo de gestão, perspectiva com
> aparência aproximada de FOV 35 graus e inclinação de 50 graus. Manter
> escala coerente: pessoa com altura aproximada de 1,7 tile, drone pequeno,
> estruturas pequenas ocupando 5×5, médias 8×8 e grandes 12×12 tiles.
>
> Composição obrigatória: casa principal modesta e envelhecida; clareira
> central inicial; áreas futuras de expansão legíveis; campos de cultivo;
> área de árvores menos regular; entrada semi-natural da Mina de Pedra;
> caminhos orgânicos; pedras e vegetação nas bordas; pelo menos um planeta
> ou lua no céu. Mostrar tecnologia antiga em azul-ciano suave sem dominar
> a natureza.
>
> Criar três interpretações de setores orgânicos conectados. Em todas, a
> casa fica ligeiramente fora do centro e há uma clareira inicial de
> 18–20 metros. Variar a posição relativa de cultivo, árvores,
> processamento e Mina, sem composição radial perfeita ou eixo rígido.
> Preservar 35–40% de espaço visível para progressão.
>
> Saída: uma única folha 16:9, três painéis do mesmo tamanho, rótulos
> simples A, B e C fora da área do mapa. Alta legibilidade, sem HUD e sem
> textos decorativos.

## Prompt 2 — planta de escala da opção escolhida

Executar somente depois de escolher uma das três interpretações:

> Recriar a opção aprovada como uma planta de escala quase
> ortográfica, vista superior levemente inclinada, preservando o mesmo
> formato e distribuição. Sobrepor uma grade discreta de tiles e marcar,
> com volumes simples e sem detalhamento final, exemplos de footprints
> 5×5, 8×8 e 12×12. Incluir uma pessoa de 1,7 m e um drone pequeno como
> referências. Não redesenhar a composição nem adicionar novas áreas.
> Saída 16:9, limpa, própria para comparar proporções no Unity.

## Prompt 3 — vista padrão de gameplay

Executar depois de validar a planta:

> Mostrar a composição aprovada a partir da câmera padrão de gameplay:
> perspectiva, FOV aproximado de 35 graus, inclinação de 50 graus e cerca
> de 28 tiles visíveis na largura. Enquadrar casa, um campo, uma estrutura
> grande em volume simples e um drone. Priorizar legibilidade de caminhos,
> limites de ocupação e estados interativos; não transformar em key art
> cinematográfica. Saída 1920×1080, sem HUD.

## Prompt 4 — teste dos três zooms

> Criar uma folha técnica com três painéis da mesma área e mesmo ângulo:
> zoom próximo com 12–16 tiles visíveis, zoom padrão com 28 tiles e visão
> geral com aproximadamente 65 tiles, enquadrando todo o disco. Manter
> posição relativa, estilo e iluminação idênticos. Demonstrar o que ainda
> é legível em cada distância e usar marcadores simples apenas na visão
> geral quando drones ou estados ficarem pequenos demais. Saída 16:9,
> três painéis alinhados, sem interface decorativa.

## Critérios de aprovação

- O disco parece um lugar explorável, não uma ilha decorativa minúscula.
- A casa estabelece o ponto inicial sem dominar o mapa inteiro.
- Existe espaço claro para crescimento até níveis altos.
- Footprints 5×5, 8×8 e 12×12 parecem pertencer à mesma escala.
- Caminhos e áreas interativas continuam legíveis ao girar mentalmente a
  câmera.
- O zoom padrão permite reconhecer cultivo, estrutura e drone.
- A visão geral comunica organização mesmo sem mostrar detalhes pequenos.
- A estética parece reproduzível com modelos 3D, não apenas em uma
  pintura de ângulo único.

## Decisões que só podem ser fechadas depois desta prova

- FOV final e limites finais de zoom;
- densidade de props e vegetação;
- paleta hexadecimal;
- nível de detalhe e orçamento de textura/modelo;
- necessidade de contorno, emissivo ou marcadores de interação;
- processo definitivo para converter conceitos em assets Unity;
- templates individuais de prompts para cada estrutura, cultivo e drone.

## Ordem dos assets da prova vertical

1. casa principal;
2. terreno, caminhos e borda do disco;
3. vegetação e pedras;
4. Campo de Cultivo;
5. Armazém Geral;
6. Hangar de Drones;
7. entrada da Mina de Pedra;
8. uma Estrutura de Processamento;
9. Trigo Lunar em cinco estágios;
10. Drone de Transporte vazio e com carga.
