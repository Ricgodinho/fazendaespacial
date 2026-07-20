import os

log = []

def edit(path, replacements, create_ok=False):
    if not os.path.exists(path):
        log.append(f"ERRO: arquivo não existe: {path}")
        return
    with open(path, "r", encoding="utf-8") as f:
        content = f.read()
    original = content
    for old, new in replacements:
        if old not in content:
            log.append(f"AVISO ({path}): trecho não encontrado: {old[:60]}...")
            continue
        content = content.replace(old, new)
    if content != original:
        with open(path, "w", encoding="utf-8") as f:
            f.write(content)
        log.append(f"Editado: {path}")
    else:
        log.append(f"Sem mudanças: {path}")

def write_new(path, content):
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    log.append(f"Criado: {path}")

# ============================================================
# PARTE A — limpeza final de terminologia (Tier -> Planeta remanescentes)
# ============================================================

edit(os.path.join("docs", "00-mvp-escopo.md"), [
    ("- **1 planeta apenas.** Sem sistema solar, sem viagem, sem múltiplos tiers.",
     "- **1 planeta apenas.** Sem sistema solar, sem viagem, sem múltiplos planetas."),
    ("- Nave, viagens, múltiplos planetas/tiers.",
     "- Nave, viagens, múltiplos planetas."),
])

edit(os.path.join("docs", "07-prototipo-2-loop-hibrido.md"), [
    ("**A capacidade é upgradável através da progressão entre tiers/planetas**,",
     "**A capacidade é upgradável através da progressão entre planetas**,"),
    ("podem liberar um projeto de upgrade de armazenamento; tecnologias de tiers\nposteriores podem viabilizar drones que ampliam a capacidade de coleta;\nmódulos de nave podem liberar melhorias aplicáveis a estruturas de tiers\nanteriores.",
     "podem liberar um projeto de upgrade de armazenamento; tecnologias de\nplanetas posteriores podem viabilizar drones que ampliam a capacidade de\ncoleta; módulos de nave podem liberar melhorias aplicáveis a estruturas\nde planetas anteriores."),
    ("   tiers posteriores implementados?",
     "   planetas posteriores implementados?"),
])

edit(os.path.join("docs", "estruturas", "planeta-2", "00-indice.md"), [
    ("Planeta Planeta 2: 100% mineral/tecnológico, sem agricultura (ver",
     "Planeta 2: 100% mineral/tecnológico, sem agricultura (ver"),
    ("Além de minério bruto → barras/metal processado, a Fundição também pode\n**criar ligas combinando materiais de múltiplos tiers** — conectando-se à\nregra de dependência entre tiers já definida em `docs/01-conceito.md`\n(\"Planeta 5 pode exigir materiais dos Tiers 2, 3 e 4\").",
     "Além de minério bruto → barras/metal processado, a Fundição também pode\n**criar ligas combinando materiais de múltiplos planetas** —\nconectando-se à regra de dependência entre planetas já definida em\n`docs/01-conceito.md` (\"Planeta 5 pode exigir materiais dos Planetas 2, 3\ne 4\")."),
])

# Placeholders do Planeta 1: **Tier:** 1 -> **Planeta:** 1
for filename in ["campo-de-cultivo.md", "area-plantio-arvores.md", "processamento-comida.md",
                  "processamento-madeira.md", "mina-de-pedra.md", "processamento-pedra.md",
                  "viveiro.md", "armazem-geral.md", "hangar-de-drones.md"]:
    edit(os.path.join("docs", "estruturas", "planeta-1", filename), [
        ("**Tier:** 1", "**Planeta:** 1"),
    ])

# ============================================================
# PARTE B — Item 1+2: Lua satélite (Planeta 5) + Mina de Minério (Planeta 2)
#           como pontos de mineração/descoberta faltantes
# ============================================================

lua_satelite_content = """# Lua Satélite (Mineração Restrita)

**Cadeia:** Mineral (fonte local para a Fundição Central)
**Planeta:** 5 (planeta-origem / núcleo da rede)

## Função — decisão

Satélite natural próximo ao Planeta 5, com acesso restrito: **somente
drones de Mineração/Escavação Tier 5** conseguem operar aqui (ambiente
hostil, condições extremas). Os drones viajam até a lua, mineram, e
retornam automaticamente carregando o material extraído até a Fundição
Central do Planeta 5.

Resolve duas lacunas identificadas em revisão:
1. **Fonte local de minério para a Fundição Central** — sem esta
   estrutura, a Fundição Central não teria fonte própria de matéria-prima,
   contradizendo o motivo de ela existir (autossuficiência do Planeta 5,
   ver `00-indice.md`).
2. **Ponto de escavação/descoberta do Planeta 5** — até esta adição,
   apenas os Planetas 1, 3 e 4 tinham um ponto explícito de escavação
   (ver `docs/01-conceito.md`, seção "Escavações e descobertas"). Por ser
   remota e de acesso restrito, esta lua é um local natural para
   artefatos raros e descobertas específicas do fim de jogo.

## Pendente
- Detalhar mecânica de viagem do drone até a lua (automática, sem
  pilotagem, seguindo o mesmo princípio das viagens de nave já definido
  em `docs/01-conceito.md`).
- Definir tempo de ciclo e quantidade extraída por viagem.
"""
write_new(os.path.join("docs", "estruturas", "planeta-5", "lua-satelite-mineradora.md"), lua_satelite_content)

edit(os.path.join("docs", "estruturas", "planeta-5", "00-indice.md"), [
    ("6. **Antena/Portal de Exploração** → desbloqueada somente após o Reator\n   Central (item 3) ser restaurado. Capta um sinal de origem\n   desconhecida — gancho para eventos, novas sementes, e conteúdo de\n   Fase 3/4 (não decidido em detalhe, apenas registrado como direção\n   futura).\n7. Armazém geral (planeta 5) — armazenamento padrão.\n8. Hangar de drones (planeta 5) — capacidade de operação simultânea.",
     "6. **Antena/Portal de Exploração** → desbloqueada somente após o Reator\n   Central (item 3) ser restaurado. Capta um sinal de origem\n   desconhecida — gancho para eventos, novas sementes, e conteúdo de\n   Fase 3/4 (não decidido em detalhe, apenas registrado como direção\n   futura).\n7. **Lua Satélite (Mineração Restrita)** → fonte local de minério para a\n   Fundição Central (item 5) e ponto de escavação/descoberta deste\n   planeta. Acesso restrito a drones Tier 5.\n8. Armazém geral (planeta 5) — armazenamento padrão.\n9. Hangar de drones (planeta 5) — capacidade de operação simultânea."),
    ("| 6 | Antena/Portal de Exploração | Gancho Fase 3/4 | `portal-de-exploracao.md` |\n| 7 | Armazém geral (planeta 5) | Todas | `armazem-geral.md` |\n| 8 | Hangar de drones (planeta 5) | — | `hangar-de-drones.md` |",
     "| 6 | Antena/Portal de Exploração | Gancho Fase 3/4 | `portal-de-exploracao.md` |\n| 7 | Lua Satélite (Mineração Restrita) | Mineral / Descobertas | `lua-satelite-mineradora.md` |\n| 8 | Armazém geral (planeta 5) | Todas | `armazem-geral.md` |\n| 9 | Hangar de drones (planeta 5) | — | `hangar-de-drones.md` |"),
    ("de ponto autossuficiente e avançado da rede. É uma exceção deliberada à\nregra geral, não um esquecimento — ver nota espelhada em\n`docs/estruturas/planeta-2/fundicao.md`.",
     "de ponto autossuficiente e avançado da rede. É uma exceção deliberada à\nregra geral, não um esquecimento — ver nota espelhada em\n`docs/estruturas/planeta-2/fundicao.md`. Sua fonte local de matéria-prima\né a **Lua Satélite (Mineração Restrita)**, ver item 7 da lista acima."),
])

edit(os.path.join("docs", "estruturas", "planeta-2", "mina-de-minerio.md"), [
    ("Ainda não detalhado. Ver `00-indice.md` desta pasta para o contexto geral\ndo Planeta 2.",
     "Ainda não detalhado. Ver `00-indice.md` desta pasta para o contexto geral\ndo Planeta 2.\n\n## Nota\nAlém de extrair minério, esta estrutura também funciona como o **ponto de\nescavação/descoberta do Planeta 2** — mesma função dupla já estabelecida\npara a Mina de Pedra do Planeta 1 (ver\n`docs/estruturas/planeta-1/mina-de-pedra.md`)."),
])

edit(os.path.join("docs", "estruturas", "planeta-2", "00-indice.md"), [
    ("### Oficina/Fábrica de Drones e Estaleiro\nAmbas conectam-se à identidade deste planeta como o local de \"criação de\ndrones, upgrade de naves\" (ver debate em `docs/01-conceito.md`).",
     "### Oficina/Fábrica de Drones e Estaleiro\nAmbas conectam-se à identidade deste planeta como o local de \"criação de\ndrones, upgrade de naves\" (ver debate em `docs/01-conceito.md`).\n\n### Mina de Minério: também é ponto de escavação/descoberta\nAlém de extrair minério, a Mina de Minério funciona como o ponto de\nescavação/descoberta deste planeta — mesma função dupla da Mina de Pedra\ndo Planeta 1 (ver `docs/estruturas/planeta-1/mina-de-pedra.md`). Resolve\numa lacuna identificada em revisão: sem isso, não havia fonte de\ndescobertas (coordenadas, organismos preservados) neste planeta."),
])

# ============================================================
# PARTE C — Item 3: Módulo de resistência ao frio, construído no Estaleiro (Planeta 2)
# ============================================================

edit(os.path.join("docs", "estruturas", "planeta-2", "estaleiro.md"), [
    ("Ainda não detalhado. Ver `00-indice.md` desta pasta para o contexto geral\ndo Planeta 2.",
     "Ainda não detalhado. Ver `00-indice.md` desta pasta para o contexto geral\ndo Planeta 2.\n\n## Nota — decisão\nO **Módulo de Resistência ao Frio** (necessário para viajar ao Planeta 4,\nver `docs/estruturas/planeta-4/00-indice.md`) é construído aqui, usando\nmateriais já disponíveis nos Planetas 1, 2 e 3 — ou seja, construído\n**antes** da viagem ao Planeta 4, seguindo a regra geral de desbloqueio de\nplanetas já definida em `docs/01-conceito.md` (\"recuperar ou construir um\nmódulo funcional da nave necessário para enfrentar as condições do novo\ndestino\")."),
])

edit(os.path.join("docs", "estruturas", "planeta-4", "00-indice.md"), [
    ("Planeta temático: gelo, com contraste interno geotérmico. Introduz\nmecânicas novas de manipulação de tempo (congelamento), revivescência de\norganismos preservados, e um evento climático raro (aurora) — não é\nreaproveitamento reskinado de estruturas de planetas anteriores.",
     "Planeta temático: gelo, com contraste interno geotérmico. Introduz\nmecânicas novas de manipulação de tempo (congelamento), revivescência de\norganismos preservados, e um evento climático raro (aurora) — não é\nreaproveitamento reskinado de estruturas de planetas anteriores.\n\n**Pré-requisito de acesso:** o **Módulo de Resistência ao Frio** deve ser\nconstruído no Estaleiro do Planeta 2 (ver\n`docs/estruturas/planeta-2/estaleiro.md`) antes da primeira expedição a\neste planeta, seguindo a regra geral de desbloqueio já definida em\n`docs/01-conceito.md`."),
])

# ============================================================
# PARTE D — Item 5: Unificação da mecânica de raridade de sementes
# ============================================================

edit(os.path.join("docs", "01-conceito.md"), [
    ("#### Nota para fase futura (mercado entre jogadores — não decidido)",
     """#### Sementes seguem a mesma tabela de raridade (decisão)
Sementes **não possuem uma escala própria separada** — usam a mesma
tabela de 5 níveis de raridade acima (Comum a Lendário), sujeitas ao
mesmo gate por progresso do jogador.

Dois filtros trabalham juntos, em ordem:
1. **Teto global (progresso do jogador)**: define o que **pode**
   aparecer em qualquer lugar. Sem o jogador ter alcançado o Planeta 4,
   por exemplo, nenhum item Épico aparece em lugar nenhum — nem no
   Planeta 1.
2. **Modificador local (Nível do Viveiro)**: dentro do teto já liberado
   pelo progresso do jogador, o nível de upgrade do Viveiro usado na
   colheita define a **probabilidade** de sair algo mais raro naquela
   coleta específica — um Viveiro de nível mais alto aumenta a chance de
   uma semente de raridade superior, mas nunca ultrapassa o teto global
   já liberado.

Esta regra substitui e descarta uma ideia anterior (nunca formalizada em
documento) de uma "janela deslizante" baseada no planeta do cultivo colhido
— superada por este modelo de dois filtros.

Fórmula exata de probabilidade por nível de Viveiro ainda a definir (ver
pendências em `docs/estruturas/planeta-1/viveiro.md`).

#### Nota para fase futura (mercado entre jogadores — não decidido)"""),
])

edit(os.path.join("docs", "estruturas", "planeta-1", "00-indice.md"), [
    ("- **Viveiro**: produz sementes a partir de matéria-prima colhida,\n  incluindo o sistema de drop de semente por tier (ver debate em\n  `docs/01-conceito.md`, seção de Raridade, para o princípio geral —\n  mecânica específica de probabilidade a fechar).",
     "- **Viveiro**: produz sementes a partir de matéria-prima colhida. As\n  sementes seguem a mesma tabela de raridade do jogo (ver\n  `docs/01-conceito.md`, seção de Raridade) — o teto do que pode\n  aparecer é definido pelo progresso do jogador, e o Nível do Viveiro\n  modifica a probabilidade dentro desse teto. Fórmula exata de\n  probabilidade por nível ainda a definir."),
])

for path in [os.path.join("docs", "estruturas", "planeta-1", "viveiro.md"),
             os.path.join("docs", "estruturas", "planeta-3", "viveiro-aquatico.md")]:
    edit(path, [
        ("Ainda não detalhado. Ver `00-indice.md` desta pasta para o contexto geral",
         "Ainda não detalhado quanto ao restante da função. Ver `01-conceito.md`,\nseção Raridade, para o modelo de dois filtros (teto global por progresso\ndo jogador + probabilidade modificada pelo Nível do Viveiro) já\nfechado — falta apenas a fórmula exata de probabilidade por nível.\n\nVer `00-indice.md` desta pasta para o contexto geral"),
    ])

# ============================================================
# PARTE E — Item 6: Nota distinguindo MVP técnico vs. design completo do jogo
# ============================================================

edit(os.path.join("docs", "00-mvp-escopo.md"), [
    ("# Escopo do MVP",
     """# Escopo do MVP

> **Nota de escopo:** este documento e os Protótipos 0/1/2 descrevem o
> **MVP técnico** — o menor recorte jogável para validar o loop híbrido.
> Documentos em `docs/drones/`, `docs/estruturas/` e as decisões de
> raridade/planetas em `docs/01-conceito.md` descrevem o **design
> completo do jogo** (todas as fases, incluindo conteúdo fora do MVP).
> As duas coisas coexistem de propósito: o design completo serve de
> visão de longo prazo e material para a fábrica de desenvolvimento; o
> MVP técnico é o que se constrói e testa primeiro. Não são
> contraditórios — apenas escopos diferentes, registrados em documentos
> diferentes."""),
])

edit(os.path.join("docs", "02-roadmap-fases.md"), [
    ("# Roadmap por fases",
     """# Roadmap por fases

> **Nota de escopo:** ver `docs/00-mvp-escopo.md` para a distinção entre
> MVP técnico (o que se constrói primeiro, validado por protótipo) e
> design completo do jogo (documentado em `docs/drones/`,
> `docs/estruturas/` e `docs/01-conceito.md`, cobrindo todas as fases)."""),
])

# ============================================================
print("\n".join(log))
print(f"\nTotal de operações: {len(log)}")