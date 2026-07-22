# Reconciliação — Planeta 1 (materiais, estruturas, escopo)

Documento de repasse rápido para quem já trabalhava no projeto
(implementação, gerência, suporte de arte) — resume decisões tomadas em
sessão de design que alteram ou resolvem pontos anteriores.

## 1. Material-base do Planeta 1: Cascalho Ancestral (não Bloco Ancestral)

`docs/10-pendencia-materiais-por-nivel.md` citava Bloco Ancestral como
candidato a material genérico. Decisão final: **Cascalho Ancestral** é a
base universal (Níveis 1-2 de todas as 9 estruturas), com 1 material
específico por estrutura somado a ela. Ver
`docs/itens/requisitos_niveis.csv` para a tabela completa e
`docs/estruturas/planeta-1/00-guia-bootstrap.md` para a lógica.

## 2. Quatro circularidades identificadas e resolvidas

Mina de Pedra, Processamento de Pedra, Área de Plantio de Árvores e
Campo de Cultivo produziam o próprio material que precisariam para
serem construídas. Resolvido com sequência de bootstrap (coleta manual
+ 3 ferramentas: Picareta, Machado, Foice de Pedra) — ver
`docs/estruturas/planeta-1/00-guia-bootstrap.md`.

## 3. Fibra Estelar: sem estrutura nova

Processa na Estrutura de Processamento (Comida) já existente, junto com
Trigo Lunar — regra geral: uma estrutura de processamento atende toda
uma categoria de matéria-prima, nunca um item isolado (exceção
registrada: Pedra e Metal continuam separados no Planeta 2).

## 4. Mina de Pedra: descoberta adiada

Mecânica de achados/raridade da Mina de Pedra fica para fase futura —
não bloqueia a extração contínua já implementada.

## 5. Campo de Cultivo / Área de Plantio: estrutura-âncora

Ambas existem como **marco físico simples** (não prédio com paredes),
com a área de tiles/árvores que já cresce por nível representando o raio
plantável ao redor da âncora — independente de onde o Hangar de Drones
está posicionado.

## 6. Escala do terreno do Planeta 1 (novo, não estava em nenhum doc anterior)

Sistema de 2 camadas: Superfície (disco de 60 tiles de diâmetro) e
Subterrâneo/Mina (disco separado de 40 tiles), ligadas por
transição de cena, não wraparound nem esfera. Footprint por Porte:
Pequeno 5×5, Médio 8×8, Grande 12×12. Ver
`docs/estruturas/planeta-1/00-guia-estilo-visual.md`.

## 7. Moeda do jogo: Ecos (provisório)

## Documentos afetados por esta reconciliação
- `docs/09-questoes-abertas-implementacao.md` (3 itens marcados resolvidos)
- `docs/10-pendencia-materiais-por-nivel.md` (marcado como superado)
- `docs/itens/itens.csv` (nota do Bloco Ancestral corrigida)
- `docs/estruturas/00-indice.md` (regra de agrupamento por categoria)
- `docs/estruturas/planeta-1/processamento-comida.md`,
  `mina-de-pedra.md`, `campo-de-cultivo.md`, `area-plantio-arvores.md`
