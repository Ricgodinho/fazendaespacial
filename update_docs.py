path = 'docs/drones/00-indice.md'
with open(path, encoding='utf-8') as f:
    content = f.read()

marker = '## Origem dos drones: fabricação vs. achado danificado (regra geral) — decisão'
count = content.count(marker)
print(f'Ocorrências antes: {count}')

first_idx = content.find(marker)
second_idx = content.find(marker, first_idx + 1)

if count >= 2:
    content_fixed = content[:second_idx].rstrip() + '\n'
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content_fixed)
    print(f'Corrigido: mantida 1 copia, removidas {count - 1} duplicada(s).')
elif count == 1:
    print('Arquivo já correto (só 1 ocorrência) — nada a fazer.')
else:
    print('AVISO: seção não encontrada — verifique manualmente.')