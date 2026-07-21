# Roadmap de Implementação Técnica

> Diferença em relação a `docs/02-roadmap-fases.md`: aquele documento cobre
> **fases de design** (Fase 1 MVP → Fase 2 expansão → Fase 3 multiplayer),
> em alto nível. Este documento cobre o **passo a passo concreto de
> programação** dentro da Fase 1, consolidando a sequência já sugerida em
> `docs/00-mvp-escopo.md`.

## Etapa 0 — Setup do projeto Unity — ✅ concluído
- Projeto Unity vive em `unity/`, pasta irmã de `docs/`, `artes/` e
  `planilhas/`. O repositório inteiro foi movido do Google Drive para
  `C:\Users\ricgo\dev\fazendaespacial` (local, git único com remoto no
  GitHub) depois que a criação do projeto crashou na pasta do Drive —
  o banco de assets da Unity (LMDB) não é compatível com o driver de
  sincronização do Google Drive (mmap não suportado).
- Versão do Editor: **Unity 6000.5.4f1** (já instalada via Unity Hub).
  Não é a LTS clássica (6.3 LTS, suporte até dez/2027), é a versão de
  "update release" mais recente da Unity — considerada production-ready
  pelo modelo de release atual da Unity. Mantida por já estar instalada,
  evitando novo download.
- Save/load do MVP: **arquivo JSON local** (confirmado).
- Estrutura inicial de pastas (`Assets/_Project/Scripts/{Core,Farming,Structures,Interaction,UI}`,
  `Prefabs/`, `ScriptableObjects/`, `Scenes/`), conforme decidido na conversa
  de arquitetura — apenas as pastas que a Etapa 2 já vai usar, nada
  especulativo para Drones/Nave ainda.
- Grid em 3D desde o início (câmera livre), mesmo com placeholders,
  conforme decisão de arte em `docs/01-conceito.md`.
- **URP (Universal Render Pipeline) 17.5.0** instalado e configurado como
  pipeline padrão, com Renderer vinculado.
- **Input System novo** instalado (`com.unity.inputsystem@1.20.0`),
  Input Manager legado desativado (`activeInputHandler: 1`) — decisão
  tomada antes de escrever o código de interação da Etapa 2, para não
  precisar migrar depois.

## Etapa 1 — Protótipo 0: matemática do loop idle — ✅ concluído
Validado em planilha (`planilhas/planilha_loop_idle.xlsx`), ver
`docs/04-prototipo-0-loop-idle.md`. Capacidade de armazenamento ajustada de
30 para 100 unidades no Trigo Lunar após simulação de jogador casual.

## Etapa 2 — Protótipo 1: loop ativo — ✅ concluído
Ver `docs/05-prototipo-1-loop-ativo.md` para as regras completas.

- Interação: ferramenta selecionada na barra de ações + clique no tile
  (Input System novo, raycast do mouse).
- 1 cultivo plantável/colhível (Trigo Lunar), com os 5 estágios visuais de
  crescimento definidos em `docs/cultivos/00-indice.md`.
- 1 estrutura de processamento (recebe o cultivo colhido, produz recurso
  derivado), alimentação/coleta manual via clique.
- Sem árvore de tecnologia, sem decoração — conforme escopo do MVP.

**Critério de conclusão — respondido em playtest informal (dentro do time):**
ritmo plantar→colher aprovado, seleção de ferramenta clara, propósito da
estrutura compreendido (alimentação/coleta manual sem confusão). Playtest
formal com pessoas de fora do time ainda pendente (ver Etapa 5).

## Etapa 3 — Protótipo 2: loop híbrido — ✅ concluído
Ver `docs/07-prototipo-2-loop-hibrido.md` para as regras completas.

- Cálculo de produção offline por acumulador de segundos + timestamp real
  (UTC) salvo no JSON — cultivo e estrutura continuam progredindo mesmo
  com o jogo fechado.
- Teto global de 48h de ausência aplicado no load, antes de distribuir o
  tempo capado entre cultivo/estrutura.
- Capacidade por estrutura já respeitada (produção pausa ao encher, nada
  se perde).
- Save/load em **JSON local** (`Application.persistentDataPath`),
  autosave a cada 30s + salva ao fechar o jogo/pausar.
- Mensagem de "bem-vindo de volta" no HUD ao carregar um save existente,
  informando quanto tempo se passou (comunica ao jogador o que aconteceu
  na ausência, conforme `docs/04`).

**Critério de conclusão — respondido em playtest informal (dentro do time):**
ciclo completo (plantar → parar → voltar → coletar) testado e aprovado,
inclusive simulando ausência longa editando o timestamp do save
manualmente. Playtest formal com pessoas de fora do time ainda pendente
(ver Etapa 5).

## Etapa 4 — Consolidação do MVP — ✅ concluído
- Docs `04`, `05` e `06` revisados e reconciliados com o que os
  protótipos revelaram (nota de consolidação no `04` sobre o modelo de
  capacidade se aplicar às estruturas, não aos cultivos; decisão de
  auto-alimentação da estrutura registrada no `05`; itens resolvidos
  movidos de "em aberto" no `06`).
- UI ganhou painel "Como jogar?" com instruções do fluxo completo, ainda
  placeholder (OnGUI, sem arte final) mas suficiente para alguém de fora
  jogar sem explicação verbal.
- Build Windows standalone gerado com sucesso
  (`unity/Builds/Windows/FazendaEspacial.exe`, 0 erros) — cena
  `Assets/_Project/Scenes/Main.unity` criada só para satisfazer a Build
  Settings; todo o conteúdo continua montado em runtime pelo
  `GameBootstrap`.

## Etapa 5 — Playtest externo
- Recrutar testadores fora do time.
- Coletar respostas aos 3 critérios de validação definidos em
  `docs/00-mvp-escopo.md` (retorno após ausência, significância das
  decisões ativas, ritmo ação/espera).
- Resultado define se avança para Fase 2 (expansão do sistema solar) e
  para o envolvimento de uma fábrica de desenvolvimento.

*Este roadmap deve ser atualizado conforme decisões pendentes (repositório
do projeto Unity, versão do Unity, mecanismo de save/load) forem fechadas
e conforme cada etapa for concluída.*
