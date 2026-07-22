# Pendência para a equipe de desenvolvimento: materiais por nível

> ## ⚠️ SUPERADO — ver `docs/itens/requisitos_niveis.csv`
> Este documento foi escrito antes de uma sessão de design que já
> resolveu completamente esta pendência, incluindo 4 circularidades que
> este documento não havia identificado (Mina de Pedra, Processamento de
> Pedra, Área de Plantio de Árvores, Campo de Cultivo). A resposta real
> está em `docs/itens/requisitos_niveis.csv` (Níveis 1-2, todas as 9
> estruturas) e `docs/estruturas/planeta-1/00-guia-bootstrap.md`.
> **Material-base é Cascalho Ancestral, não Bloco Ancestral** (candidato
> citado abaixo está desatualizado). Conteúdo abaixo mantido como
> registro histórico da pergunta original, não como resposta válida.

**Status: aguardando decisão de design.** Bloqueia a implementação do
sistema de níveis (ver `docs/estruturas/00-indice.md` e
`docs/09-questoes-abertas-implementacao.md`, item 4).

## O que falta decidir

Toda estrutura do Planeta 1 já tem sua tabela de 10 níveis fechada
(valores de exemplo, breakpoints nomeados), mas **nenhuma delas define o
material e a quantidade exatos** gastos para subir de um nível para o
próximo — hoje só existe a coluna "Dependência de material" indicando
**de qual planeta** o material vem, não **qual item específico**.

Cada doc de estrutura já registra isso na própria seção "Pendente" com a
mesma frase ("Definir os materiais exatos exigidos em cada nível") - este
documento só consolida os 9 casos num lugar só, pra facilitar passar pra
quem for decidir.

## O que já pode ser decidido agora (Planeta 1 já existe)

Níveis 1 a 4 de toda estrutura dependem só de material do Planeta 1 -
ou seja, já dá pra decidir usando o catálogo existente
(`docs/itens/itens.csv`), sem esperar o Planeta 2. Candidato já
levantado no próprio catálogo: **Bloco Ancestral** é citado como
"candidato a ser o material genérico de referência do Planeta 1" nas
dependências de nível de outras estruturas (itens.csv, linha do Bloco
Ancestral) - ou seja, pode ser o material-base de upgrade de nível 1→4
de tudo, em vez de cada estrutura pedir um item diferente. Precisa só
confirmar essa escolha e definir a quantidade por nível/estrutura.

## Tabela por estrutura (dependência de planeta já fechada, item específico em aberto)

| Estrutura | Nível 1→2→3→4 | Nível 5→6 | Nível 7 | Nível 8 | Nível 9 | Nível 10 |
|---|---|---|---|---|---|---|
| Armazém Geral | Planeta 1 | Planeta 1+2 | Planeta 2 | Planeta 2+3 | Planeta 3+4 | Planetas 3,4,5 |
| Hangar de Drones | Planeta 1 | Planeta 1+2 | Planeta 2 | Planeta 2+3 | Planeta 3+4 | Planetas 3,4,5 |
| Mina de Pedra | Planeta 1 | Planeta 1+2 | Planeta 2 | Planeta 2+3 | Planeta 3+4 | Planetas 3,4,5 |
| Processamento de Pedra | Planeta 1 | Planeta 1+2 | Planeta 2 | Planeta 2+3 | Planeta 3+4 | Planetas 3,4,5 |
| Processamento de Comida | Planeta 1 | Planeta 1+2 | Planeta 2 | Planeta 2+3 | Planeta 3+4 | Planetas 3,4,5 |
| Processamento de Madeira | Planeta 1 | Planeta 1+2 | Planeta 2 | Planeta 2+3 | Planeta 3+4 | Planetas 3,4,5 |
| Viveiro | Planeta 1 | Planeta 1+2 | Planeta 2 | Planeta 2+3 | Planeta 3+4 | Planetas 3,4,5 |
| Campo de Cultivo | *sem custo definido* | *sem custo definido* | Planeta 2 | *sem custo definido* | *sem custo definido* | Planetas 3,4,5 |
| Área de Plantio de Árvores | *sem custo definido* | *sem custo definido* | Planeta 2 | *sem custo definido* | *sem custo definido* | Planetas 3,4,5 |

Nota sobre as duas últimas linhas: os docs de Campo de Cultivo e Área de
Plantio de Árvores só mencionam material nos breakpoints (Nível 7 e
Nível 10) - não têm coluna de dependência para os outros níveis. Vale
confirmar se isso é intencional (upgrade "grátis" nos demais níveis,
só custando tempo/moeda) ou se também deveriam ter material como as
outras 7 estruturas.

## Perguntas específicas para fechar

1. **Bloco Ancestral vira mesmo o material genérico do Planeta 1** para
   os níveis 1-4 de todas as estruturas, ou cada estrutura pede um item
   próprio (ex: Armazém Geral pede Tábua Estelar, Hangar pede Bloco
   Ancestral, etc.)?
2. **Quantidade por nível**: mesmo fechando o item, falta a curva de
   quantidade (ex: Nível 1→2 custa 20 unidades, Nível 3→4 custa 80?).
3. **Campo de Cultivo / Área de Plantio de Árvores**: confirmar se os
   níveis sem breakpoint têm custo de material ou não (ver nota acima) -
   depende também da decisão pendente no item 3 de
   `docs/09-questoes-abertas-implementacao.md` (se essas duas estruturas
   vão existir de fato antes de ganhar níveis).
4. **Níveis 5+**: ficam bloqueados até o catálogo de itens do Planeta 2
   existir (hoje só a lista de *estruturas* do Planeta 2 está fechada,
   não os itens/materiais que ele produz).

## Encaminhamento sugerido

Decidir e preencher primeiro só os **Níveis 1-4** (não dependem de
Planeta 2) para todas as 9 estruturas - já destrava a primeira fatia
implementável do sistema de níveis sem esperar o resto da progressão
entre planetas.
