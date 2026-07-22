# Arte — Índice Geral

Ponto de entrada para o setor de arte: reúne a direção visual já
decidida e os requisitos de arte levantados durante o design e a
prototipagem técnica, organizados por planeta.

## Direção visual geral

**Direção fechada para a prova visual:** **3D estilizado low-poly
refinado, com materiais/texturas pintados à mão e acabamento
ilustrado**. É o ponto de encontro entre a legibilidade e o custo
controlado do low-poly e a personalidade do estilo ilustrativo escolhido
nas referências.

Não usar "2D estilizado" como nome do pipeline: o jogo usa modelos 3D e
câmera giratória. Spiritfarer e Cozy Grove permanecem referências de
cor, aconchego e personalidade; referências de execução 3D incluem Ni
no Kuni, World of Warcraft e cenários estilizados de Genshin Impact, sem
buscar o mesmo orçamento ou densidade desses jogos.

Ver também:

- `docs/arte/01-direcao-e-pipeline-prompts.md` — linguagem visual,
  limites do uso de IA e estrutura obrigatória dos prompts;
- `docs/arte/02-prova-vertical-mapa-planeta-1.md` — primeiro exercício,
  começando pelo mapa, câmera e escala.
- `docs/arte/03-guia-tecnico-prova-vertical-unity.md` — cena sandbox,
  iluminação, materiais, texturas, LODs e metas iniciais para Unity.

## Onde ficam as referências visuais
Pasta `artes/`, na raiz do projeto, organizada **por planeta** (arte é
naturalmente específica de cada bioma, diferente das regras de
mecânica, que ficam em `docs/estruturas/`, `docs/drones/`,
`docs/cultivos/`, organizadas por categoria):

- `artes/planeta-1/` — planeta inicial
- `artes/planeta-2/` — a lua
- `artes/planeta-3/` — água
- `artes/planeta-4/` — gelo
- `artes/planeta-5/` — origem/núcleo

Convenção: quando houver material específico de uma estrutura, drone ou
cultivo dentro de um planeta, criar uma subpasta correspondente dentro
da pasta daquele planeta (ex: `artes/planeta-1/estruturas/`). Não criar
subpastas vazias antecipadamente — só quando houver conteúdo real.

Exceção já existente: `docs/drones/arte/00-indice.md` e
`docs/drones/audio/00-indice.md` cobrem referências de drones
organizadas por categoria (não por planeta), por já serem tratadas
como domínio próprio antes desta reorganização.

## Requisitos de arte levantados durante a prototipagem

Estes vieram de decisões tomadas durante o protótipo técnico (não são
arte final, são a especificação do que a arte final precisa comunicar):

- **Cultivos — 5 estágios visuais de crescimento** (0/25/50/75/100% do
  tempo de crescimento), ver `docs/cultivos/00-indice.md`.
- **Estruturas de processamento — 3 estados visuais** (vazio/ocioso /
  produzindo / concluído/cheio), ver seção "Estados visuais de
  processamento" em `docs/estruturas/00-indice.md`. O estado "pronto
  para coleta" deve compartilhar a mesma linguagem visual do estágio
  final de crescimento dos cultivos — uma única convenção de "isso
  brilha/destaca quando dá pra clicar", não uma por sistema.
- **Estruturas — progressão visual por nível**: 5 variações visuais
  cobrindo os 10 níveis em pares (1–2, 3–4, 5–6, 7–8, 9–10), cada uma
  apresentada nos 3 estados acima — 15 combinações visuais por estrutura,
  não necessariamente 15 modelos independentes. Ver seção
  "Progressão visual por nível" em `docs/estruturas/00-indice.md`.

## Ordem de definição e geração

1. Mapa do Planeta 1 em setores orgânicos, em vista de planejamento e
   vista de gameplay.
2. Teste de câmera, escala e legibilidade com volumes simples.
3. Prova vertical com terreno, um cultivo, uma estrutura pequena, uma
   estrutura grande e um drone.
4. Guia técnico de produção após aprovação da prova vertical.
5. Fichas e prompts individuais de estruturas, cultivos e drones.
6. Produção em escala somente depois de validar consistência, custo e
   comportamento dentro do Unity.

## Pendente
- Executar e aprovar a prova visual do mapa do Planeta 1.
- Definir o processo que transformará as referências geradas pelo
  ChatGPT em modelos 3D utilizáveis no Unity. As imagens geradas são
  especificação/conceito, não substituem malha, UV, pivô, colisão,
  materiais, rig, animação e LOD.
- Referências visuais concretas por planeta além das 3 já existentes
  (`artes/planeta-1`, `artes/planeta-3`, `artes/planeta-4`) — faltam
  planeta-2 e planeta-5.
