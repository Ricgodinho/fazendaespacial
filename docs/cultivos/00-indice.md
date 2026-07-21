# Cultivos — Índice

Este é o índice do sistema de Cultivos. Cobre regras gerais aplicáveis a
qualquer cultivo, de qualquer planeta. Cultivos específicos (Trigo
Lunar, Fibra Estelar, Cedro Estelar) já aparecem como exemplos em
`docs/04-prototipo-0-loop-idle.md` e `docs/itens/itens.csv`, mas ainda
não têm arquivo próprio nesta pasta.

## Estágios visuais de crescimento — decisão

Todo cultivo (qualquer planeta) usa **5 estágios visuais**, baseados em
**porcentagem do tempo total de crescimento daquele cultivo específico**
— não em tempo fixo. Isso garante que a regra funcione igualmente para
cultivos rápidos (ex: Trigo Lunar, 2h) e lentos (ex: Fibra Estelar, 18h),
sem necessidade de regra separada por cultivo.

| Estágio | % do tempo de crescimento | Visual |
|---|---|---|
| 0 | 0% | Terreno preparado / semente plantada |
| 1 | 25% | Brotinho pequeno |
| 2 | 50% | Broto médio |
| 3 | 75% | Quase maduro |
| 4 | 100% | Pronto para colher |

Motivo de 5 estágios (não 2): padrão consolidado do gênero (farming
sims), que usa múltiplos estágios visuais para dar feedback constante de
progresso — reforça a satisfação de "ver crescendo", central ao loop
idle já definido em `docs/04-prototipo-0-loop-idle.md`.

## Pendente
- Nomear e detalhar cada cultivo específico (além dos exemplos
  provisórios já usados: Trigo Lunar, Fibra Estelar, Cedro Estelar).
- Definir se algum cultivo foge da regra de 5 estágios (ex: cultivos
  muito rápidos ou muito lentos podem precisar de tratamento especial).
- Aplicação de raridade a cultivos em si (distinta da raridade de
  sementes já fechada em `docs/01-conceito.md`).
