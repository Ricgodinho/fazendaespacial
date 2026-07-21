# Roadmap de Implementação Técnica

> Diferença em relação a `docs/02-roadmap-fases.md`: aquele documento cobre
> **fases de design** (Fase 1 MVP → Fase 2 expansão → Fase 3 multiplayer),
> em alto nível. Este documento cobre o **passo a passo concreto de
> programação** dentro da Fase 1, consolidando a sequência já sugerida em
> `docs/00-mvp-escopo.md`.

## Etapa 0 — Setup do projeto Unity
- Projeto Unity vive em `fazendaespacial/unity/` — pasta irmã de `docs/`,
  `artes/` e `planilhas/`, no mesmo repositório/pasta do Drive (decisão
  registrada abaixo).
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

## Etapa 1 — Protótipo 0: matemática do loop idle — ✅ concluído
Validado em planilha (`planilhas/planilha_loop_idle.xlsx`), ver
`docs/04-prototipo-0-loop-idle.md`. Capacidade de armazenamento ajustada de
30 para 100 unidades no Trigo Lunar após simulação de jogador casual.

## Etapa 2 — Protótipo 1: loop ativo
Ver `docs/05-prototipo-1-loop-ativo.md` para as regras completas.

- Interação: ferramenta selecionada na barra de ações + clique no tile.
- 1 cultivo plantável/colhível (Trigo Lunar).
- 1 estrutura de processamento (recebe o cultivo colhido, produz recurso derivado).
- Sem loop idle, sem árvore de tecnologia, sem decoração.

**Critério de conclusão:** responder as 3 perguntas de playtest do
documento 05 (ritmo plantar→colher, clareza da seleção de ferramenta,
propósito percebido da estrutura construída).

## Etapa 3 — Protótipo 2: loop híbrido
Ver `docs/07-prototipo-2-loop-hibrido.md` para as regras completas.

- Cálculo de produção offline por timestamp (salvar hora ao fechar,
  comparar ao reabrir).
- Capacidade por estrutura + teto global de 48h de ausência.
- Mecanismo de save/load (sugestão de partida: arquivo JSON local — a
  confirmar).

**Critério de conclusão:** responder as 3 perguntas de playtest do
documento 07 (satisfação do ciclo completo, percepção do teto de 48h,
clareza da progressão de capacidade).

## Etapa 4 — Consolidação do MVP
- Revisar `docs/00` a `docs/07` à luz do que os protótipos revelaram.
- UI funcional, ainda sem arte final (placeholders/assets de loja).
- Preparar build para playtest externo.

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
