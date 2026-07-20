import os

# Lê o arquivo existente
with open(os.path.join("docs", "08-drones.md"), "r", encoding="utf-8") as f:
    original = f.read()

# Pontos de corte, usando os cabeçalhos que já existem no arquivo
marker_subtopico3 = "## Subtópico 3: Drone de Colheita — decisão"
marker_pendencias = "## Subtópicos ainda a debater"

idx_sub3 = original.index(marker_subtopico3)
idx_pend = original.index(marker_pendencias)

# Parte 1: cabeçalho + Subtópico 1 + Subtópico 2 (tudo antes do Subtópico 3)
parte_indice = original[:idx_sub3].strip()

# Parte 2: Subtópico 3 (drone de colheita), sem incluir a seção de pendências
parte_colheita = original[idx_sub3:idx_pend].strip()

# Ajusta o cabeçalho do índice (era "# Drones", vira "# Drones — Índice" com nova intro)
parte_indice = parte_indice.replace(
    "# Drones\n\nEste documento é construído por subtópicos, debatidos e fechados um a um.\nCobre: categorias funcionais, evolução por tier, e aplicação da raridade\n(ver `docs/01-conceito.md` para a tabela de raridade já definida).",
    "# Drones — Índice\n\n"
    "Este é o índice do sistema de Drones. Cada categoria de drone tem seu\n"
    "próprio arquivo nesta pasta. Este documento cobre as decisões que se\n"
    "aplicam a todas as categorias: funções gerais, modelo de progressão,\n"
    "crafting e limites de quantidade.\n\n"
    "## Arquivos desta pasta\n\n"
    "- `colheita.md` — especificação do drone de Colheita (fechado)\n"
    "- `plantio.md` — especificação do drone de Plantio (a debater)\n"
    "- `transporte.md` — especificação do drone de Transporte/logística (a debater)\n"
    "- `escavacao.md` — especificação do drone de Escavação (a debater)\n"
    "- `construcao.md` — especificação do drone de Construção/reparo (a debater)\n"
    "- `companheiro.md` — especificação do drone companheiro (a debater)\n"
    "- `arte/00-indice.md` — referências visuais específicas de drones\n"
    "- `audio/00-indice.md` — efeitos sonoros específicos de drones"
)

# Adiciona rodapé de pendências gerais ao índice
parte_indice += (
    "\n\n## Pendências gerais do sistema de Drones\n"
    "- Especificação tier a tier das categorias ainda não fechadas (Plantio,\n"
    "  Transporte, Escavação, Construção).\n"
    "- Aplicação da raridade dentro de cada categoria/tier.\n\n"
    "*Este índice é atualizado conforme decisões gerais (aplicáveis a todas as\n"
    "categorias) forem debatidas. Decisões específicas de cada drone ficam no\n"
    "arquivo próprio da categoria.*\n"
)

# Ajusta o cabeçalho da colheita e adiciona referência ao índice
parte_colheita = parte_colheita.replace(
    marker_subtopico3,
    "# Drone de Colheita\n\n"
    "Ver `00-indice.md` para categorias gerais, modelo de progressão e limites\n"
    "de hangar aplicáveis a todos os drones.\n\n"
    "## Especificação por tier — decisão"
)
parte_colheita += "\n\n## Pendente\n- Aplicação da raridade dentro de cada tier deste drone.\n"

# Placeholders para as categorias ainda não debatidas
def placeholder(titulo):
    return f"# {titulo}\n\nAinda não debatido. Ver `00-indice.md` para o contexto geral do sistema\nde Drones.\n"

arte_placeholder = (
    "# Drones — Arte (índice)\n\n"
    "Referências visuais específicas de drones. Ver `docs/01-conceito.md` para\n"
    "a decisão de direção visual geral do jogo (2D estilizado / fallback\n"
    "low-poly 3D).\n\nAinda não populado.\n"
)
audio_placeholder = (
    "# Drones — Áudio (índice)\n\n"
    "Referências e decisões de efeitos sonoros específicos de drones.\n\n"
    "Ainda não populado.\n"
)

files = {
    os.path.join("docs", "drones", "00-indice.md"): parte_indice + "\n",
    os.path.join("docs", "drones", "colheita.md"): parte_colheita + "\n",
    os.path.join("docs", "drones", "plantio.md"): placeholder("Drone de Plantio"),
    os.path.join("docs", "drones", "transporte.md"): placeholder("Drone de Transporte/Logística"),
    os.path.join("docs", "drones", "escavacao.md"): placeholder("Drone de Escavação/Exploração"),
    os.path.join("docs", "drones", "construcao.md"): placeholder("Drone de Construção/Reparo"),
    os.path.join("docs", "drones", "companheiro.md"): placeholder("Drone Companheiro"),
    os.path.join("docs", "drones", "arte", "00-indice.md"): arte_placeholder,
    os.path.join("docs", "drones", "audio", "00-indice.md"): audio_placeholder,
}

for path, content in files.items():
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    print(f"Criado: {path}")

os.remove(os.path.join("docs", "08-drones.md"))
print("Removido (migrado): docs/08-drones.md")