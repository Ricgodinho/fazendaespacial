# Drones

Este documento é construído por subtópicos, debatidos e fechados um a um.
Cobre: categorias funcionais, evolução por tier, e aplicação da raridade
(ver `docs/01-conceito.md` para a tabela de raridade já definida).

## Subtópico 1: Categorias de drones — decisão

### Categorias funcionais (automação)
Conectam-se à automação retroativa já definida no conceito central (ver
`docs/01-conceito.md`, seção "Automação e retorno aos planetas").

1. **Colheita** — colhe cultivos maduros automaticamente. Conecta-se
   diretamente ao loop idle (`docs/04-prototipo-0-loop-idle.md` e
   `docs/07-prototipo-2-loop-hibrido.md`).
2. **Plantio** — replanta automaticamente após a colheita, fechando o
   ciclo de produção sem intervenção manual.
3. **Transporte/logística** — move recursos entre estruturas do mesmo
   planeta e, futuramente, entre planetas pela rede agrícola
   interplanetária (tema central da narrativa).
4. **Escavação/exploração** — auxilia a identificar e escavar pontos de
   descoberta (ver seção "Escavações e descobertas" em
   `docs/01-conceito.md`).
5. **Construção/reparo** — auxilia a restaurar estruturas antigas ou
   construir novas mais rapidamente, reforçando o tema narrativo de
   restaurar o legado recebido.

### Categoria à parte: drone companheiro (não-funcional)

Diferente das categorias acima, o **drone companheiro** não automatiza
tarefas — ele existe para reforçar o vínculo emocional do jogador com o
jogo, alinhado ao princípio de "cuidado e apego aos planetas" já definido
no conceito central. Um drone que decorasse a fazenda automaticamente
contradiria a regra existente de que decoração é sempre decisão do
jogador; por isso o companheiro é deliberadamente não-funcional.

**Configurável entre dois modos:**
- **Modo silencioso**: apenas acompanha o jogador, sem interferir.
- **Modo dicas**: oferece dicas contextuais ao jogador (ex: avisos sobre
  estruturas cheias, sugestões de próximos passos).

**Potencial de monetização (nota, não decisão):** por ser puramente
cosmético/emocional e não afetar balanceamento de jogo, o drone
companheiro é um bom candidato a skins e acessórios vendáveis (cosméticos
pagos), sem os riscos de pay-to-win. Isso é uma nota para explorar mais
adiante, na definição do modelo de monetização do jogo — ainda não
decidido.

### Nota para fase futura: drone social/anfitrião (Fase 3 — não decidido)

Foi levantada a ideia de um **drone anfitrião/guia**, que apareceria
quando outro jogador visitasse a fazenda (ver "Multiplayer" em
`docs/02-roadmap-fases.md`), mostrando o que foi construído e recebendo o
visitante. Esta é apenas uma ideia registrada para debate futuro na Fase
3, não uma decisão.

## Subtópico 2: Modelo de progressão, crafting e limites — decisão

### Item por tier, não experiência/nível
Drones não evoluem por experiência (XP). Cada tier de um drone é um
**item próprio**, obtido por compra (moeda do jogo), drop, ou construção
— seguindo o mesmo modelo já usado para ferramentas e estruturas. Isso
aproveita a economia por tier e a dependência entre planetas já definidas
em `docs/01-conceito.md`, em vez de criar um sistema de progressão
paralelo.

### Não existe categoria separada de "crafting"
Quem processa/crafta é a **estrutura de processamento** (já definida em
`docs/05-prototipo-1-loop-ativo.md` e `docs/07-prototipo-2-loop-hibrido.md`),
não um drone. A categoria **Transporte/logística** é responsável por
alimentar essas estruturas com a matéria-prima colhida — não é necessária
uma categoria de drone dedicada a crafting.

### Dois limites distintos de quantidade de drones

1. **Capacidade de carga da nave** (temporário, por viagem): quantos
   drones o jogador consegue levar ao expandir para um novo tier/planeta.
   Já previsto na seção "Nave" de `docs/01-conceito.md`.
2. **Capacidade do hangar de drones** (permanente, por planeta): quantos
   drones podem operar simultaneamente numa fazenda já estabelecida,
   independente da capacidade de carga da nave. Requer uma nova estrutura
   — o **hangar de drones** — com capacidade própria e upgradável, seguindo
   o mesmo padrão de capacidade upgradável já usado no loop idle
   (`docs/04-prototipo-0-loop-idle.md`).

### Hangar: pool único compartilhado, com peso por tier
A capacidade do hangar é um **pool único**, compartilhado entre todas as
categorias de drone (não capacidade separada por categoria) — reforça a
decisão estratégica do jogador sobre como distribuir suas vagas.

Cada drone consome um **peso** do pool proporcional ao seu tier, evitando
que o jogador lote o hangar apenas com drones de tier avançado sem custo
estratégico algum.

| Tier do drone | Peso no hangar (exemplo) |
|---|---|
| 1 | 1 |
| 2 | 2 |
| 3 | 3 |
| 4 | 5 |
| 5 | 8 |

Valores de exemplo, a validar em playtest/planilha — mesmo princípio já
aplicado ao loop idle (ver `planilhas/planilha_loop_idle.xlsx`).

## Subtópicos ainda a debater
- Especificação tier a tier do drone de Colheita (em andamento).
- Especificação tier a tier das demais categorias (Plantio, Transporte,
  Escavação, Construção).
- Aplicação da raridade dentro de cada categoria/tier.

*Este documento será atualizado conforme os subtópicos seguintes forem
debatidos e fechados.*
