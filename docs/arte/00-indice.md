# Arte — Índice Geral

Ponto de entrada para o setor de arte: reúne a direção visual já
decidida e os requisitos de arte levantados durante o design e a
prototipagem técnica, organizados por planeta.

## Direção visual geral
Ver `docs/01-conceito.md`, seção "Direção visual — decisão": estilo 2D
estilizado (tipo Spiritfarer/Cozy Grove), implementado como modelos 3D
com shader pintado à mão e câmera livre (rotação/zoom), com low-poly 3D
como fallback pré-aprovado caso o custo do estilo principal não seja
viável.

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
- **Estruturas de processamento — 3 estados visuais** (processando /
  pronto para coleta / parado), ver seção "Estados visuais de
  processamento" em `docs/estruturas/00-indice.md`. O estado "pronto
  para coleta" deve compartilhar a mesma linguagem visual do estágio
  final de crescimento dos cultivos — uma única convenção de "isso
  brilha/destaca quando dá pra clicar", não uma por sistema.

## Pendente
- Levantamento de orçamento do estilo 2D estilizado (custo de produção)
  antes de comprometer definitivamente a direção visual — ver
  `docs/01-conceito.md`.
- Referências visuais concretas por planeta além das 3 já existentes
  (`artes/planeta-1`, `artes/planeta-3`, `artes/planeta-4`) — faltam
  planeta-2 e planeta-5.
