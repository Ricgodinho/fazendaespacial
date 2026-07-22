# Guia de Estilo Visual — Planeta 1

**Propósito deste documento**: descrição de referência para ser
repassada a uma IA geradora de imagens (ou artista humano) produzir a
arte conceitual do Planeta 1. Não contém nem gera imagens — apenas
especifica o que deve ser gerado, com linguagem visual concreta.

## Estilo técnico (já decidido, ver `docs/01-conceito.md`)
2D estilizado (ilustrativo), implementado como **3D com shader/textura
pintada à mão** — câmera livre (rotação e zoom), alto nível de detalhe.
Referências de pipeline: Genshin Impact, Ni no Kuni, World of Warcraft.

## Escala e formato do terreno — decisão

O planeta **não é uma esfera com wraparound** (tecnicamente complexo,
raro no gênero) nem um cilindro com geometria contínua — é um sistema de
**2 camadas separadas** (discos planos independentes, ligados por uma
entrada/transição de cena, mesmo princípio já usado por jogos do gênero
para acesso a minas):

### Camada 1 — Superfície
Disco plano de **60 tiles de diâmetro** (~2.827 tiles de área, 1 tile ≈
1 metro²). Contém todas as estruturas do Planeta 1, exceto a Mina de
Pedra. Folga generosa sobre o mínimo funcional calculado (~1.052 tiles
somando footprint de estruturas + área de plantio no Nível 10 máximo).

### Camada 2 — Subterrâneo (Mina)
Disco plano separado de **40 tiles de diâmetro** (~1.257 tiles de área),
acessado via entrada/transição de cena a partir da Mina de Pedra na
Camada 1 — não há wraparound nem geometria contínua entre as camadas.

Este espaço físico é o que dá corpo às **"camadas de escavação"** já
definidas em `docs/estruturas/planeta-1/mina-de-pedra.md` (Nível 4 libera
acesso a mais área desta camada; Nível 10 libera a camada final) — a
progressão de nível da Mina expande literalmente o território jogável
aqui, não é só um efeito numérico.

## Mood / sensação
Fazenda acolhedora carregando peso de legado — não é um lugar novo, é um
lugar **recuperando vida** após anos de abandono. Luz quente e
nostálgica, tipo "golden hour" constante, em contraste com um céu
estrelado sempre visível ao fundo (reforçando que é um planeta pequeno e
isolado no espaço).

## Paleta de cores

| Papel | Descrição | Referência de cor (ponto de partida) |
|---|---|---|
| Base — vegetação | Verdes naturais, folhagem viva | Verde-oliva a verde-esmeralda suave |
| Base — terreno | Tons terrosos | Marrom-terra, bege, ocre |
| Acento quente | Luz do sol, janelas iluminadas | Dourado / âmbar |
| Acento tecnológico | Drone, painéis, tecnologia | Azul suave, quase ciano — contraste proposital entre orgânico e tecnológico |
| Fundo (Camada 1) | Céu noturno/espacial sempre visível | Azul-arroxeado escuro, estrelas pontuais |
| Fundo (Camada 2) | Subterrâneo, sem céu | Marrom-escuro/cinza-pedra, iluminação pontual (tochas/cristais) |

## Elementos-chave de composição (Camada 1)
- Casa principal — modesta, envelhecida mas não em ruínas, luz quente
  vazando de dentro
- Campos de cultivo — fileiras organizadas, mistura de terra preparada e
  plantas em diferentes estágios de crescimento
- Área de árvores — algumas árvores selvagens já existentes, dispersas,
  não uniformes
- Entrada da Mina de Pedra — modesta, semi-natural, sugerindo uso humilde
  prévio (não industrial), com transição visível para a Camada 2
- Céu — sempre visível ao fundo, com pelo menos 1 outro planeta/lua
  visível a distância, reforçando a escala do sistema solar
- Drone companheiro — pequeno, com sinais visíveis de desgaste/avaria
  (não brilhante e novo)

## Contraste com os demais planetas
O Planeta 1 deve ser o único **verde e vivo** de todo o jogo — os demais
(Planeta 2: lua seca/mineral, Planeta 3: água, Planeta 4: gelo,
Planeta 5: tecnológico/decadente) são mais estéreis ou artificiais. A
composição deve enfatizar essa vitalidade orgânica como identidade única
deste planeta.

## Referências
- `artes/exemplos estilos primeiro planeta.png` (já escolhida como
  direção aprovada)
- Jogos de referência de estilo: Spiritfarer, Cozy Grove

## Pendente
- Gerar arte conceitual final a partir desta descrição (IA externa ou
  artista).
- Definir paleta de cores em hexadecimal exato após primeira geração de
  referência.
- Definir a mecânica exata de transição entre Camada 1 e Camada 2
  (loading/fade, animação de descida, etc.) — decisão de implementação,
  não de arte.
- Repetir este mesmo formato de guia para os Planetas 2 a 5 quando
  chegar a vez de cada um (considerando se cada um também terá sistema
  de camadas, caso a caso).
