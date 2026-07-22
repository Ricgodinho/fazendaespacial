# Guia técnico da prova vertical no Unity

## Propósito

Definir metas iniciais para validar a direção de arte no Unity 6 com URP.
Estes números orientam a prova vertical; não são limites finais. O
orçamento definitivo depende de profiling no hardware-alvo com Unity
Profiler, Rendering Debugger e uma cena representativa.

Referências oficiais:

- [Performance e profiling no URP](https://docs.unity3d.com/6000.0/Documentation/Manual/graphics-performance-and-profiling-in-urp.html)
- [Configuração de Mesh LOD](https://docs.unity3d.com/6000.0/Documentation/Manual/configure-mesh-lod.html)
- [Otimização de draw calls](https://docs.unity3d.com/6000.0/Documentation/Manual/optimizing-draw-calls-choose-method.html)
- [Mipmap Streaming](https://docs.unity3d.com/6000.0/Documentation/Manual/TextureStreaming-configure.html)
- [Adaptive Probe Volumes no URP](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/probevolumes-use.html)

## Cena sandbox — decisão

Usar `Assets/_Project/Scenes/ArtSandbox_Planeta1.unity`, isolada de
`Main.unity` e do bootstrap da mecânica. A cena começa com graybox em
escala real e só recebe arte depois da aprovação de câmera e footprints.

O disco possui 60 m de diâmetro e 1 tile corresponde a aproximadamente
1 m. Usar uma cápsula ou manequim de 1,7 m como referência permanente.

## Câmera — configuração inicial

- perspectiva, FOV vertical 35°;
- pitch padrão 50°, limitado entre 35° e 65°;
- yaw 360°;
- Near Clip 0,1 m;
- Far Clip 150–200 m;
- zoom próximo: 12–16 m visíveis na largura;
- zoom padrão: aproximadamente 28 m;
- visão geral: 65–70 m;
- suavização de 0,15–0,25 s;
- impedir passagem pelo terreno e por volumes grandes.

Começar com controlador orbital simples. Adotar Cinemachine somente se a
prova exigir acompanhamento, transições ou composição que justifiquem a
dependência adicional.

## Iluminação — decisão inicial

- uma luz direcional principal quente;
- ambiente/skybox azul-arroxeado;
- sombras suaves;
- duas cascatas de sombra;
- Shadow Distance inicial de 70–80 m;
- HDR ligado;
- bloom muito discreto apenas para emissivos;
- color grading quente e leve;
- ambient occlusion sutil;
- sem outline global permanente.

Terreno e cenário fixo podem usar iluminação baked ou mixed. Estruturas
posicionáveis, cultivos e drones devem receber iluminação indireta por
Light Probes ou Adaptive Probe Volumes. Não depender de lightmap próprio
para objetos que o jogador move ou constrói.

## Linguagem de interação

- seleção/hover: rim light ou contorno temporário discreto;
- vazio/ocioso: sem emissivo dominante, peças paradas e conteúdo vazio;
- produzindo: movimento cíclico, emissivo ciano suave e efeito específico;
- concluído/cheio: conteúdo visível, sinal âmbar/dourado e movimento de
  produção parado;
- erro/bloqueio: ícone e mudança de forma/ritmo, não apenas cor vermelha;
- zoom geral: marcadores simples em espaço de tela.

Estado nunca deve depender exclusivamente de cor.

## Metas iniciais de geometria

| Categoria | Triângulos de referência no LOD0 |
|---|---:|
| Estrutura grande | 15.000–30.000 |
| Estrutura média | 8.000–18.000 |
| Estrutura pequena | 3.000–8.000 |
| Drone | 2.000–5.000 |
| Árvore principal | 1.000–4.000 |
| Prop | 200–2.000 |
| Cultivo individual | 300–1.500 |

Esses intervalos são tetos de trabalho para a prova, não metas a serem
preenchidas. Silhueta e deformação justificam geometria; detalhes planos
devem preferir textura ou normal map quando fizer sentido visual.

## LOD — metas iniciais

Para estruturas grandes, árvores e outros assets que permaneçam visíveis
na visão geral:

- LOD0: modelo completo;
- LOD1: aproximadamente 50–60% dos triângulos do LOD0;
- LOD2: aproximadamente 15–25%;
- culling somente quando o objeto não contribuir mais para a composição.

Usar no máximo três LODs na prova. Distâncias reais devem ser configuradas
por tamanho relativo na tela e verificadas nos três zooms. Cultivos e props
repetidos devem priorizar malha compartilhada e instancing antes de ganhar
cadeias complexas de LOD.

## Texturas — metas iniciais

| Categoria | Resolução máxima inicial |
|---|---:|
| Estrutura grande | 2048×2048 |
| Estrutura média | 1024×1024 ou 2048×2048 |
| Estrutura pequena | 1024×1024 |
| Drone | 1024×1024 |
| Árvore/prop importante | 512×512 ou 1024×1024 |
| Cultivo | 256×256 ou 512×512, preferencialmente em atlas |

Ativar mipmaps em todas as texturas 3D. Avaliar Mipmap Streaming quando a
cena possuir conteúdo representativo; as configurações de qualidade atuais
ainda o mantêm desativado. Reduzir a resolução quando o zoom próximo não
revelar diferença perceptível.

## Materiais, shader e batching

- um Shader Graph mestre para estruturas;
- um para vegetação;
- um para terreno;
- um para efeitos/seleção;
- ideal de um material por asset, máximo inicial de dois;
- compartilhar materiais e atlas sempre que possível;
- manter SRP Batcher ativo;
- usar GPU instancing para cultivos e props repetidos;
- evitar material exclusivo para cada variação de cor;
- validar `MaterialPropertyBlock` contra SRP Batcher e instancing antes de
  adotá-lo na arte final.

Os estados compartilham modelo-base sempre que possível, combinando
Animator, Shader Graph, emissivo, partículas e peças modulares. As 15
combinações conceituais não significam 15 modelos 3D.

## Transição superfície–Mina — decisão

Superfície e subterrâneo usam cenas separadas:

1. interação com a entrada;
2. aproximação curta da câmera;
3. fade de 0,3–0,5 s;
4. carregamento assíncrono da cena subterrânea;
5. fade de retorno na saída correspondente.

Se a troca levar menos de 0,5 s, apenas o fade é necessário. Acima disso,
usar tela curta com arte ou ícone da Mina. Não empilhar os dois discos na
mesma cena.

## Paleta inicial de teste

| Papel | Hex inicial |
|---|---|
| Vegetação escura | `#456B45` |
| Vegetação clara | `#78A85A` |
| Terra | `#8A6042` |
| Caminho | `#C59A68` |
| Pedra | `#77776F` |
| Madeira | `#76513B` |
| Luz quente | `#F2B85B` |
| Tecnologia ciano | `#59BFC7` |
| Céu profundo | `#252B56` |
| Céu secundário | `#4B477A` |
| Pronto/coleta | `#F4C95D` |

A paleta só se torna definitiva depois de iluminação, tonemapping e color
grading da prova, porque esses sistemas alteram a cor percebida.

## Critérios para fechar a especificação técnica

- 60 FPS estáveis no PC mínimo provisório durante os três zooms;
- estruturas e estados legíveis no zoom padrão;
- ausência de popping incômodo entre LODs e mipmaps;
- mapa inteiro enquadrável sem desaparecerem sombras essenciais;
- materiais compatíveis com SRP Batcher/instancing;
- memória de textura medida com conteúdo representativo;
- conceitos reproduzíveis em mais de um ângulo;
- custo de conversão dos conceitos para asset 3D conhecido.

Somente depois desses critérios devem ser fechados polígonos, resolução,
distâncias de LOD, intensidade de pós-processamento e orçamento por asset.

