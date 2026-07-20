# Stack Técnica

Este documento registra as decisões técnicas de base do projeto, para
alinhamento com a fábrica de desenvolvimento antes do início da produção.

## Engine: Unity

**Decisão:** Unity foi escolhida como engine do projeto.

### Motivos

1. **Multiplayer (Fase 3 do roadmap).** Unity possui soluções de rede
   maduras e testadas em produção (Netcode for GameObjects, Photon,
   Mirror), reduzindo o risco de retrabalho ao implementar fazenda
   cooperativa/compartilhada no futuro.
2. **Mercado de desenvolvedores.** Unity é o padrão de mercado entre
   estúdios indie e freelancers, o que facilita a contratação da fábrica
   e reduz a curva de aprendizado na transição do protótipo para produção.
3. **Asset Store.** Grande volume de assets prontos de fazenda, farming
   e ficção científica, o que acelera a fase de protótipo e MVP (uso de
   placeholders, conforme já definido em `docs/00-mvp-escopo.md`).
4. **Compatibilidade com ferramentas de desenvolvimento assistido por IA
   (ex: Claude Code).** C# e a documentação da Unity possuem grande volume
   de material de referência, o que tende a gerar código mais confiável
   e com menos erros de API do que engines mais nicho.

### Trade-off conhecido
Unity possui modelo de licenciamento com cobrança acima de determinado
faturamento anual. Não é uma preocupação na fase atual do projeto, mas
deve ser revisitado caso o jogo atinja esse patamar de faturamento.

## Linguagem
**C#**, linguagem padrão de scripting da Unity.

## Plataforma alvo
PC (Steam), conforme definido em `docs/01-conceito.md`.

## Em aberto para debate
- Estrutura de pastas do projeto Unity.
- Pipeline de arte (2D vs 3D, conforme decisão de estilo visual ainda
  pendente em `docs/01-conceito.md`).
- Ferramentas de terceiros (ex: solução de rede específica para
  multiplayer, sistemas de save/load, localização).

*Este documento deve ser atualizado conforme decisões técnicas adicionais
forem tomadas, antes da entrega à fábrica de desenvolvimento.*
