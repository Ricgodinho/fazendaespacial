# Armazém Geral

**Cadeia:** Todas
**Planeta:** 1

Ver `docs/estruturas/00-indice.md` para a regra geral do sistema de
níveis (10 níveis, crescimento exponencial, breakpoints de capacidade,
dependência de materiais de outros planetas nos níveis mais altos,
incluindo a regra de que o Nível 10 sempre exige o planeta mais avançado
disponível).

## Função — decisão

O Armazém Geral é o **inventário central do planeta**, não apenas um
silo de colheita. Guarda:

- Comida, madeira e pedra colhidas/extraídas pelos drones (ver
  `docs/drones/colheita.md` e demais categorias)
- **Ferramentas** (as usadas na interação ativa do jogador, ver
  `docs/05-prototipo-1-loop-ativo.md`)
- **Itens vindos de outros planetas** via Drone de Transporte —
  materiais usados nas dependências de nível de outras estruturas (ver
  `docs/01-conceito.md`, "Dependência entre planetas")
- **Drones em manutenção** (enviados automaticamente pelo Hangar deste
  mesmo planeta ao zerarem durabilidade, ver `docs/drones/00-indice.md`,
  seção Desgaste), ou preparados para transporte a outros planetas
  (aguardando embarque na nave)

### Manutenção automática (configurável)
O Armazém Geral de **cada planeta** oferece uma configuração de
manutenção automática: por padrão, o reparo de drones é manual (o
jogador aciona e paga o custo); uma vez ativada a configuração, reparos
passam a acontecer automaticamente (ainda com custo, descontado sozinho)
neste planeta especificamente. Não depende de nenhuma outra estrutura ou
planeta — cada Armazém Geral tem sua própria configuração independente.

Diferente da capacidade de armazenamento por cultivo já definida no
Protótipo 0 (ex: Trigo Lunar com teto próprio no campo), o Armazém Geral
é uma segunda camada de capacidade — o destino de tudo depois de sair do
campo/mina, antes de processar, vender, ou embarcar.

## Níveis — decisão

Métrica numérica principal: capacidade total de armazenamento.

| Nível | Capacidade total | Dependência de material | Capacidade nova (breakpoint) |
|---|---|---|---|
| 1 | 100 | Planeta 1 | — |
| 2 | 150 | Planeta 1 | — |
| 3 | 220 | Planeta 1 | — |
| 4 | 330 | Planeta 1 | **Slots dedicados por categoria** — comida, madeira, pedra e ferramentas deixam de competir pelo mesmo espaço; cada categoria ganha reserva própria |
| 5 | 500 | Planeta 1 + 2 | — |
| 6 | 750 | Planeta 1 + 2 | — |
| 7 | 1.100 | Planeta 2 | **Recebimento automático de outros planetas** — passa a receber entregas do Drone de Transporte vindas de outros planetas sem coleta manual |
| 8 | 1.650 | Planeta 2 + 3 | — |
| 9 | 2.500 | Planeta 3 + 4 | — |
| 10 | 3.800 | Planetas 3, 4 e 5 | **Overflow automático a valor mínimo**: o que exceder a capacidade é convertido em dinheiro pelo preço mais baixo possível (rede de segurança, não estratégia de venda) — só desbloqueado neste nível |

Valores são exemplo (progressão ~1,5x por nível) — a validar em
playtest/planilha, mesmo princípio já aplicado ao loop idle (ver
`docs/04-prototipo-0-loop-idle.md` e `planilhas/planilha_loop_idle.xlsx`).

## Pendente
- Definir os materiais exatos exigidos em cada nível.
- Definir o valor exato de venda mínima do overflow automático (Nível 10).
- Definir se drones aguardando manutenção ocupam a capacidade geral do
  Armazém ou um espaço próprio reservado.
- Validar a curva de capacidade em playtest/planilha antes da
  implementação.
