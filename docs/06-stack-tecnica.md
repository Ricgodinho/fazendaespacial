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

## Versão do Editor e render pipeline — decisão
- **Unity 6000.5.4f1**, instalada via Unity Hub. Não é a LTS clássica
  (6.3 LTS, suporte até dez/2027) — é a versão de "update release" mais
  recente no momento da criação do projeto, considerada production-ready
  pelo modelo de release atual da Unity.
- **URP (Universal Render Pipeline)**, com Renderer próprio configurado —
  necessário para o pipeline de arte 2D estilizado (modelos 3D com shader
  pintado à mão, câmera livre), conforme `docs/01-conceito.md`.
- **Input System novo** (`com.unity.inputsystem`), Input Manager legado
  desativado desde o início do MVP técnico.

## Estrutura de pastas do projeto Unity — decisão
`Assets/_Project/`, com subpastas por domínio (não por tipo técnico):
`Scripts/{Core,Farming,Structures,Interaction,UI}`, `Prefabs/`,
`ScriptableObjects/`, `Scenes/`, `Settings/`, `Resources/` (para os
ScriptableObjects de dados carregados em runtime, ex: definições de
cultivo/estrutura). Pastas novas são criadas conforme o escopo
implementado exige — nada especulativo para sistemas ainda não
implementados (ex: Drones, Nave).

## Save/load — decisão
Arquivo **JSON local** (`Application.persistentDataPath`), com autosave
periódico e ao fechar o jogo. Ver `docs/07-prototipo-2-loop-hibrido.md`
para o modelo de cálculo de produção offline que usa esse save.

## Em aberto para debate
- Pipeline de arte final: 2D estilizado (decisão principal) vs. low-poly
  3D (fallback pré-aprovado) — depende de levantamento de orçamento,
  ver `docs/01-conceito.md`.
- Solução de rede específica para multiplayer (Fase 3 — ainda não é
  prioridade).
- Ferramenta de localização (Unity Localization package ou solução
  própria) — o projeto já nasce preparado para múltiplos idiomas
  (`docs/01-conceito.md`), mas a ferramenta técnica ainda não foi
  escolhida.

*Este documento deve ser atualizado conforme decisões técnicas adicionais
forem tomadas, antes da entrega à fábrica de desenvolvimento.*
