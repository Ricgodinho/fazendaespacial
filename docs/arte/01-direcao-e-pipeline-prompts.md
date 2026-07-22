# Direção de arte e pipeline de prompts

## Objetivo

Garantir que imagens geradas em conversas diferentes com o ChatGPT
pertençam ao mesmo jogo. Os prompts são documentos de especificação:
devem preservar linguagem visual, escala, câmera, materiais e elementos
recorrentes, mudando apenas o objeto ou cena solicitado.

## Direção visual fixa

**3D estilizado low-poly refinado, com materiais pintados à mão e
acabamento ilustrado**, para um jogo indie de gestão/fazenda espacial
acolhedor. Formas fortes e arredondadas, silhuetas legíveis, proporções
levemente caricatas, detalhes concentrados nas áreas interativas,
iluminação quente e materiais sem fotorrealismo.

O resultado deve parecer uma cena que possa ser construída com modelos
3D e observada por vários ângulos. Não pedir ilustração 2D plana para um
objeto que precisará existir com câmera giratória.

## Limite do pipeline com IA

O ChatGPT pode gerar:

- concept art e estudos de composição;
- folhas de referência com vistas coerentes;
- exploração de cor, material, silhueta e estados;
- referências para texturas, ícones e efeitos;
- prompts derivados para manter consistência.

Uma imagem gerada não é automaticamente um asset 3D pronto para Unity.
Ainda será necessário produzir ou converter malha, UV, texturas,
materiais, pivô, colisores, LOD, rig e animações. Nenhuma imagem deve ser
marcada como "asset final" antes de passar por essa etapa.

## Estrutura obrigatória de cada prompt

Todo prompt deve conter estes blocos, nesta ordem:

1. **Função:** o que está sendo criado e qual papel tem no jogo.
2. **Invariantes do jogo:** direção visual fixa, mood e tecnologia.
3. **Escala:** dimensões em tiles/metros e relação com pessoa, drone e
   estruturas próximas.
4. **Câmera:** perspectiva, FOV aproximado, inclinação e enquadramento.
5. **Composição:** elementos obrigatórios e hierarquia visual.
6. **Materiais e cores:** materiais dominantes e função de cada cor.
7. **Estados/variações:** quando aplicável, todas as versões que devem
   aparecer na folha.
8. **Restrições negativas:** o que não deve aparecer.
9. **Formato de saída:** proporção, fundo, vistas e organização da folha.
10. **Checklist:** condições objetivas para aceitar ou rejeitar a imagem.

## Bloco mestre reutilizável

Copiar este bloco sem reescrever seu sentido em todo prompt do Planeta 1:

> Projeto: Fazenda Espacial, jogo indie de gestão e fazenda para PC.
> Direção visual: 3D estilizado low-poly refinado, materiais e texturas
> pintados à mão, acabamento ilustrado, formas fortes e arredondadas,
> silhuetas legíveis, proporções levemente caricatas e detalhe moderado.
> Mood: fazenda acolhedora recuperando vida depois de anos de abandono,
> nostalgia sem tristeza, golden hour quente constante e céu espacial
> azul-arroxeado. Contraste entre natureza orgânica em verdes/ocres e
> tecnologia antiga em azul-ciano suave. Deve parecer construível em 3D e
> coerente quando visto por uma câmera giratória. Não copiar designs de
> jogos existentes.

## Restrições negativas globais

> Sem pixel art, sem fotorrealismo, sem estética cyberpunk neon, sem
> plástico brilhante genérico, sem excesso de ruído ou microdetalhes, sem
> interface de celular, sem texto ilegível, sem perspectiva impossível,
> sem objetos fundidos entre si e sem aparência de cenário 2D plano.

## Processo de geração e aprovação

1. Gerar primeiro uma folha de exploração com 3 opções claramente
   diferentes, mantendo os invariantes.
2. Escolher uma opção e registrar o que foi aprovado e rejeitado.
3. Gerar uma folha de consistência da opção escolhida em mais de uma
   vista e escala.
4. Fazer apenas uma mudança principal por iteração; não trocar estilo,
   composição e paleta ao mesmo tempo.
5. Salvar o prompt exato junto da imagem aprovada.
6. Usar a imagem aprovada como referência nas gerações seguintes.
7. Validar escala e câmera no Unity antes de detalhar todos os assets.

## Regra para estruturas

Cada estrutura possui 5 variações:

- Variação A: níveis 1–2;
- Variação B: níveis 3–4;
- Variação C: níveis 5–6;
- Variação D: níveis 7–8;
- Variação E: níveis 9–10.

Cada variação precisa ser mostrada em 3 estados:

- vazio/ocioso;
- produzindo;
- concluído/cheio, pronto para coleta.

A folha conceitual deve mostrar as **15 combinações** em grade 5×3, com
mesma câmera, escala, iluminação-base e identidade da estrutura. O estado
deve mudar por animação sugerida, emissivo, peças móveis, conteúdo
visível ou efeitos; não transformar a estrutura em outro edifício.

## Regra para cultivos e drones

Os prompts individuais serão criados depois da aprovação do mapa e da
prova vertical:

- cultivos: 5 estágios, mesma espécie e câmera, progressão clara de
  0/25/50/75/100%;
- drones: folha com vistas frontal, lateral, traseira e superior, escala
  ao lado de pessoa/estrutura, estado vazio e com carga, sem misturar
  função visual de drones diferentes.

