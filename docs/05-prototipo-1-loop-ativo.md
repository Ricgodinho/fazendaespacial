# Protótipo 1 — Loop Ativo

Este documento define as regras do loop **ativo** (interação direta do
jogador), a segunda peça do loop híbrido, sem o componente idle ainda
(esse já foi validado no Protótipo 0). Objetivo: um protótipo jogável
simples que teste se plantar, colher e construir têm "feel" satisfatório
por si só, antes de somar com o idle no Protótipo 2.

## Decisões de interação

### Ferramenta selecionada, depois clique no tile
O jogador seleciona uma ferramenta ou item na barra de ações e então clica
no tile onde quer aplicá-la (plantar, colher, escavar, construir).

Motivo: o jogo já prevê múltiplas ações e ferramentas diferentes por tier
(ver `docs/01-conceito.md`), então clique direto no tile (sem seleção)
não escala bem — vira ambíguo assim que existir mais de 1-2 ações
possíveis no mesmo lugar. Esse padrão também permite mostrar prévia
visual da ação (cursor muda, highlight do tile) antes do clique, o que
ajuda na legibilidade da interface.

## Escopo de construção do Protótipo 1

**Apenas estruturas de produção — sem decoração.**

O objetivo aqui é validar mecânica (plantar → colher → construir →
alimentar produção), não apelo estético. Decoração deve esperar até
depois da direção de arte estar definida (ver "Em aberto para debate"
em `docs/01-conceito.md`), para não gerar retrabalho.

### Estrutura mínima a construir no protótipo
- **1 estrutura de processamento simples**, que recebe o cultivo colhido
  (ex: Trigo Lunar) e produz um recurso derivado. Serve para testar se a
  construção tem propósito percebido pelo jogador, não só decorativo.
- Sem árvore de tecnologia ainda — só 1 estrutura disponível desde o início.

## Ações do jogador no Protótipo 1

1. **Plantar**: selecionar semente/cultivo na barra de ações, clicar em
   tile vazio e válido.
2. **Colher**: selecionar ferramenta de colheita (ou usar a mesma ação
   contextual), clicar em tile com cultivo maduro.
3. **Construir**: selecionar estrutura de processamento na barra de
   construção, clicar em tile vazio.
4. **Coletar da estrutura**: clicar na estrutura para retirar o recurso
   processado (não precisa de ferramenta selecionada para esta ação).

   **Decisão de implementação:** como o Protótipo 1 não tem drone de
   Transporte ainda (ver `docs/drones/00-indice.md`), o mesmo clique que
   coleta o output também alimenta a estrutura com insumo do inventário
   do jogador (deposita o quanto couber na capacidade de insumo), quando
   não há output pronto para coletar. Prioridade: coletar primeiro se
   houver output pronto; alimentar só se não houver. Essa alimentação
   manual deve ser substituída pelo drone de Transporte quando essa
   categoria for implementada.

## Fora do escopo deste protótipo

- Loop idle / produção passiva (já coberto no Protótipo 0 — será somado
  no Protótipo 2).
- Decoração e customização visual da fazenda.
- Múltiplas estruturas, árvore de tecnologia, raridade.
- Nave, viagens, outros planetas.
- Arte final — usar placeholders/assets prontos de loja.

## Perguntas a validar no playtest deste protótipo

1. A sequência plantar → esperar crescer → colher tem ritmo agradável,
   ou parece um passo vazio sem a camada idle ainda?
2. Selecionar ferramenta antes de clicar é intuitivo o suficiente, ou
   precisa de melhor sinalização visual (cursor, highlight)?
3. Construir a estrutura de processamento parece ter propósito claro, ou
   o jogador não entende por que construiu aquilo?

*Este documento deve ser revisado após o playtest do protótipo, antes de
avançar para o Protótipo 2 (junção dos loops ativo e idle).*
