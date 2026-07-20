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

## Subtópicos ainda a debater
- Evolução de cada categoria por tier (o que muda de capacidade entre
  tiers).
- Aplicação da raridade dentro de cada categoria/tier.

*Este documento será atualizado conforme os subtópicos seguintes forem
debatidos e fechados.*
