# Lua Satélite (Mineração Restrita)

**Cadeia:** Mineral (fonte local para a Fundição Central)
**Planeta:** 5 (planeta-origem / núcleo da rede)
**Porte:** Grande

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
