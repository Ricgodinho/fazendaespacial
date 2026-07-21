# Drone Companheiro

Ver `00-indice.md` para categorias gerais e a base já definida (drone
não-funcional/emocional, modos silencioso/dicas, potencial de
monetização cosmética).

## Identidade narrativa — decisão

O Companheiro foi, no passado, **o drone-mestre do antigo cuidador da
rede** (ver `docs/01-conceito.md`, "Identidade central") — um drone
avançado o suficiente para operar com total autonomia. Sofreu avarias
severas e perdeu a memória de **como executar tarefas específicas**
(não perdeu a autonomia em si — por isso já opera sozinho, sem precisar
estar por perto do jogador, desde o momento em que é encontrado).

## Origem — decisão

Diferente da entrega automática por herança, o Companheiro é um **achado
único e especial de escavação**, na Mina de Pedra do Planeta 1 (ver
`docs/estruturas/planeta-1/mina-de-pedra.md`) — garantido de aparecer
relativamente cedo no jogo, diferente dos drones danificados comuns (que
são achados aleatórios, ver `docs/drones/00-indice.md`, seção Origem dos
drones). Ao ser encontrado, começa sem nenhuma tarefa disponível — apenas
companhia (modos silencioso/dicas).

## Modelo de mercado que valida a mecânica

Jogos idle com sistema de companheiro/pet seguem um padrão consistente:
o companheiro ajuda em tarefas, mas de forma mais lenta/fraca que a
produção dedicada do jogador, e ganha novas capacidades conforme o jogo
avança. O Companheiro deste jogo segue esse padrão, com uma variação
narrativa: capacidades são restauradas por memórias encontradas por
planeta, não por tempo/uso genérico.

## Capacidades por memória de planeta — decisão

Cada planeta já visitado tem sua própria memória a ser encontrada (via
escavação), restaurando permanentemente uma capacidade específica:

| Planeta | Memória restaura |
|---|---|
| 1 | Coleta + Plantio |
| 2 | Escavação |
| 3 | Transporte |
| 4 | Construção |
| 5 | Modo de Rede (ver abaixo) |

### Modo de Rede (Planeta 5)
Remove a limitação central do Companheiro: até então, ele só ajuda
enquanto o jogador está no mesmo planeta onde ele está trabalhando. A
partir desta memória, o jogador escolhe entre **manter o Companheiro
consigo** (viajando junto, como sempre) **ou despachá-lo numa missão**
para ajudar em uma tarefa específica em outro planeta já conectado (ex:
"ajude na coleta do Planeta 1"), mesmo estando ausente de lá. Conecta
com a identidade da Sala de Controle da Rede do Planeta 5 (ver
`docs/estruturas/planeta-5/sala-de-controle-da-rede.md`).

## Regras gerais

- Todas as tarefas do Companheiro são sempre **mais fracas/genéricas**
  que o Tier 1 do drone dedicado equivalente — ele ajuda, não substitui.
- Todas as tarefas operam **automaticamente, sem exigir proximidade do
  jogador**, desde o momento em que a memória correspondente é
  encontrada — traço remanescente de ter sido um drone-mestre.
- Antes do Modo de Rede (Planeta 5), o Companheiro só opera no planeta
  onde o jogador estiver fisicamente no momento.

## Pendente
- Detalhar o conteúdo narrativo específico de cada memória (o que é
  revelado sobre o antigo cuidador a cada capacidade restaurada).
- Definir a força relativa exata de cada tarefa genérica frente ao
  Tier 1 dedicado (ex: percentual de velocidade/quantidade).
- Definir se há limite de quantas missões de despacho (Modo de Rede)
  podem ocorrer simultaneamente.
- Validar em playtest a chance/raridade deste achado único na Mina de
  Pedra.
