import os
import re

# --------------------------------------------------------------------
# 1. Corrige o texto em docs/01-conceito.md (raridade e automação)
# --------------------------------------------------------------------
conceito_path = os.path.join("docs", "01-conceito.md")
with open(conceito_path, "r", encoding="utf-8") as f:
    conceito = f.read()

replacements_conceito = [
    (
        "Cada nível de raridade é liberado a partir de um marco de progressão do\njogador (chegar a um determinado tier), mas uma vez liberado, pode aparecer\nem **qualquer planeta já visitado** — incluindo os primeiros tiers.",
        "Cada nível de raridade é liberado a partir de um marco de progressão do\njogador (chegar a um determinado planeta), mas uma vez liberado, pode\naparecer em **qualquer planeta já visitado** — incluindo os primeiros\nplanetas."
    ),
    (
        "| Comum | Tier 1 (sempre) | Qualquer planeta |\n| Incomum | Tier 1 (sempre) | Qualquer planeta |\n| Raro | Tier 3 | Qualquer planeta já visitado |\n| Épico | Tier 4 | Qualquer planeta já visitado |\n| Lendário | Tier 5 | Qualquer planeta já visitado |",
        "| Comum | Planeta 1 (sempre) | Qualquer planeta |\n| Incomum | Planeta 1 (sempre) | Qualquer planeta |\n| Raro | Planeta 3 | Qualquer planeta já visitado |\n| Épico | Planeta 4 | Qualquer planeta já visitado |\n| Lendário | Planeta 5 | Qualquer planeta já visitado |"
    ),
    (
        "Motivo: o gate é pelo progresso do jogador, não pelo planeta, evitando risco\nde poder desproporcional cedo demais — quando um item lendário pode aparecer\nno Tier 1, o jogador já tem toda a força de jogo de quem chegou ao Tier 5.",
        "Motivo: o gate é pelo progresso do jogador, não pelo planeta em si,\nevitando risco de poder desproporcional cedo demais — quando um item\nlendário pode aparecer no Planeta 1, o jogador já tem toda a força de jogo\nde quem chegou ao Planeta 5."
    ),
    (
        "jogador em tier inicial possa comprar itens de raridade mais alta de outro",
        "jogador em planeta inicial possa comprar itens de raridade mais alta de outro"
    ),
    (
        "O jogador começa executando pessoalmente grande parte das atividades da primeira\nfazenda. Novos tiers liberam tecnologias que reduzem tarefas repetitivas nos\nmundos anteriores.",
        "O jogador começa executando pessoalmente grande parte das atividades da primeira\nfazenda. Novos planetas liberam tecnologias que reduzem tarefas repetitivas nos\nmundos anteriores."
    ),
    (
        "Como exemplo inicial, uma lua mineral de Tier 2 pode liberar projetos e materiais\npara drones básicos utilizados no Tier 1. Tecnologias obtidas no Tier 3 podem\npermitir automatizar completamente o ciclo produtivo já configurado no Tier 1.",
        "Como exemplo inicial, o Planeta 2 (lua mineral) pode liberar projetos e materiais\npara drones básicos utilizados no Planeta 1. Tecnologias obtidas no Planeta 3 podem\npermitir automatizar completamente o ciclo produtivo já configurado no Planeta 1."
    ),
]

for old, new in replacements_conceito:
    if old not in conceito:
        print(f"AVISO: trecho não encontrado em 01-conceito.md (pulando): {old[:50]}...")
        continue
    conceito = conceito.replace(old, new)

with open(conceito_path, "w", encoding="utf-8") as f:
    f.write(conceito)
print("Atualizado: docs/01-conceito.md")

# --------------------------------------------------------------------
# 2. Renomeia as pastas tier-1 -> planeta-1, tier-2 -> planeta-2
# --------------------------------------------------------------------
renames = [
    (os.path.join("docs", "estruturas", "tier-1"), os.path.join("docs", "estruturas", "planeta-1")),
    (os.path.join("docs", "estruturas", "tier-2"), os.path.join("docs", "estruturas", "planeta-2")),
]
for old_dir, new_dir in renames:
    if os.path.exists(old_dir):
        os.rename(old_dir, new_dir)
        print(f"Renomeado: {old_dir} -> {new_dir}")
    else:
        print(f"AVISO: pasta não encontrada (pulando): {old_dir}")

# --------------------------------------------------------------------
# 3. Corrige o texto dentro de todos os arquivos de docs/estruturas/
# --------------------------------------------------------------------
def fix_text(content):
    content = content.replace("**Tier do Planeta:** 2 (a lua)", "**Planeta:** 2 (a lua)")
    content = content.replace("**Tier do Planeta:** 1", "**Planeta:** 1")
    content = content.replace("# Estruturas — Tier 1 (Planeta inicial)", "# Estruturas — Planeta 1 (Planeta inicial)")
    content = content.replace("# Estruturas — Tier 2 (A Lua)", "# Estruturas — Planeta 2 (A Lua)")
    content = re.sub(r"\bTier 1\b", "Planeta 1", content)
    content = re.sub(r"\bTier 2\b", "Planeta 2", content)
    content = re.sub(r"\bTier 3\b", "Planeta 3", content)
    content = re.sub(r"\bTier 4\b", "Planeta 4", content)
    content = re.sub(r"\bTier 5\b", "Planeta 5", content)
    content = content.replace("Tier do Planeta", "Planeta")
    content = content.replace("Tier de Planeta", "Planeta")
    content = content.replace("tier-1/", "planeta-1/")
    content = content.replace("tier-2/", "planeta-2/")
    return content

estruturas_root = os.path.join("docs", "estruturas")
for dirpath, _, filenames in os.walk(estruturas_root):
    for filename in filenames:
        if filename.endswith(".md"):
            filepath = os.path.join(dirpath, filename)
            with open(filepath, "r", encoding="utf-8") as f:
                original = f.read()
            fixed = fix_text(original)
            if fixed != original:
                with open(filepath, "w", encoding="utf-8") as f:
                    f.write(fixed)
                print(f"Corrigido: {filepath}")

print("\nConcluído. Revise 'git status' e 'git diff' antes de commitar.")