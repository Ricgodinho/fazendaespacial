# Escopo do MVP

Este documento define o menor recorte jogável do projeto: o suficiente para
provar que o loop híbrido (ativo + idle) é divertido, antes de qualquer
expansão de conteúdo, arte final ou passagem para produção.

## Objetivo do MVP
Responder uma única pergunta: **o loop de gerir uma fazenda ativamente e
vê-la progredir passivamente enquanto o jogador está ausente é divertido
o suficiente para sustentar o resto do jogo?**

Tudo que não ajuda a responder essa pergunta fica fora do MVP.

## Dentro do escopo do MVP

- **1 planeta apenas.** Sem sistema solar, sem viagem, sem múltiplos tiers.
- **Loop ativo**: plantar, colher, construir estruturas básicas de produção.
- **Loop passivo/idle**: pelo menos 1 cadeia de produção que continua
  avançando com o jogo fechado (ex: uma plantação automatizada simples),
  com forma clara de comunicar ao jogador o que aconteceu na ausência dele.
- **Economia mínima**: 1 a 2 recursos básicos, 1 forma de progressão
  (ex: desbloquear uma nova estrutura ou ferramenta).
- **UI funcional, sem arte final**: pode (e deve) usar placeholders/assets
  prontos de loja. Nada de gastar tempo de arte customizada ainda.
- **Sem narrativa implementada** — a história do parente/legado fica só no
  documento de conceito por enquanto.

## Fora do escopo do MVP

- Nave, viagens, múltiplos planetas/tiers.
- Escavações e descobertas.
- Raridade de itens.
- Localização em múltiplos idiomas.
- Multiplayer.
- Arte final / direção visual definitiva.
- Qualquer sistema de crafting profundo ou árvore de tecnologia extensa.

## Critério de validação
O MVP está "pronto" quando for possível responder com dados reais (não só
opinião própria):

1. O jogador quer voltar a abrir o jogo depois de um tempo fechado, para ver
   o que a produção passiva gerou?
2. As decisões ativas (o que plantar, o que construir a seguir) parecem
   significativas, ou o jogador só está esperando o tempo passar?
3. O ritmo entre ação e espera está agradável, ou está estressante /
   entediante?

Esse teste deve ser feito com pelo menos algumas pessoas fora do time,
não só validação interna.

## Sequência sugerida de protótipos

1. **Protótipo 0 — só o loop idle**, sem gráficos, até em planilha/texto se
   necessário: validar a matemática da progressão passiva (taxas, tempos,
   custos) antes de programar qualquer interface.
2. **Protótipo 1 — loop ativo básico jogável**: plantar/colher/construir,
   sem idle ainda, pra sentir o "feel" da interação direta.
3. **Protótipo 2 — junta os dois**: loop híbrido completo, ainda com
   assets placeholder.
4. **Playtest externo** do Protótipo 2 antes de decidir sobre arte final,
   escopo de Fase 2 ou envolver uma fábrica de desenvolvimento.

*Este documento deve ser revisado sempre que o design do loop híbrido mudar
de forma significativa.*
