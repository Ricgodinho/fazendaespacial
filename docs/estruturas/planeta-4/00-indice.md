# Estruturas — Planeta 4 (Gelo)

Planeta temático: gelo, com contraste interno geotérmico. Introduz
mecânicas novas de manipulação de tempo (congelamento), revivescência de
organismos preservados, e um evento climático raro (aurora) — não é
reaproveitamento reskinado de estruturas de planetas anteriores.

**Pré-requisito de acesso:** o **Módulo de Resistência ao Frio** deve ser
construído no Estaleiro do Planeta 2 (ver
`docs/estruturas/planeta-2/estaleiro.md`) antes da primeira expedição a
este planeta, seguindo a regra geral de desbloqueio já definida em
`docs/01-conceito.md`.

## Lista de estruturas — decisão

| # | Estrutura (nome provisório) | Cadeia | Arquivo |
|---|---|---|---|
| 1 | Estufa Geotérmica | Comida geotérmica | `estufa-geotermica.md` |
| 2 | Estrutura de processamento (comida geotérmica) | Comida | `processamento-comida-geotermica.md` |
| 3 | Câmara de Revivescência | Organismos preservados / narrativa | `camara-de-revivescencia.md` |
| 4 | Poço de Gelo Profundo | Material de liga | `poco-de-gelo-profundo.md` |
| 5 | Armazém Criogênico | Sementes/frutas (preservação indefinida) | `armazem-criogenico.md` |
| 6 | Câmara Criônica de Estase | Cultivos plantados (pausa de crescimento) | `camara-crionica-de-estase.md` |
| 7 | Observatório de Auroras | Evento climático raro | `observatorio-de-auroras.md` |
| 8 | Armazém geral (gelo) | Todas (armazenamento padrão) | `armazem-geral.md` |
| 9 | Hangar de drones (gelo) | — | `hangar-de-drones.md` |

## Notas gerais — mecânicas novas introduzidas aqui

### Armazém Criogênico vs. Câmara Criônica de Estase (não são a mesma coisa)
- **Armazém Criogênico**: congela **itens já colhidos** (sementes, frutas).
  Enquanto congelados, não estragam — armazenamento indefinido. Ao serem
  retirados (descongelados), ganham uma **janela de tempo limitada** para
  serem plantados/processados antes de estragarem de vez. Funciona como
  um "cofre de raridades" para jogadores em estágio avançado guardarem
  itens raros achados em qualquer planeta.
- **Câmara Criônica de Estase**: congela um **cultivo ainda plantado no
  campo**, pausando seu crescimento indefinidamente até o jogador decidir
  descongelar. Interage diretamente com o sistema de produção offline por
  timestamp já definido em `docs/07-prototipo-2-loop-hibrido.md` — um
  cultivo em estase não deve contar para o teto global de horas (48h) nem
  para capacidade de armazenamento enquanto congelado. **Detalhar essa
  interação tecnicamente antes da implementação.**

### Câmara de Revivescência
Dá uso prático a "sementes e organismos preservados" já citados como
possível descoberta de escavação desde o início do conceito (ver
`docs/01-conceito.md`, seção "Escavações e descobertas") — até este
planeta, esse tipo de descoberta não tinha aplicação mecânica definida.
Organismos/sementes preservados podem ser achados em escavação de
**qualquer planeta já visitado**, não apenas no Planeta 4, e trazidos
aqui para revivescência. Também extrai fragmentos de memória do antigo
cuidador da rede agrícola — avanço narrativo específico deste planeta.

### Poço de Gelo Profundo
Segue o mesmo princípio já usado nos Planetas 2 e 3: extrai material
bruto, mas o refino em liga acontece na **Fundição do Planeta 2** via
transporte entre planetas — sem estrutura de refino local duplicada.

### Observatório de Auroras
Recurso raro só disponível durante um evento climático cronometrado
(aurora), não continuamente disponível como os demais recursos do jogo —
introduz uma janela de tempo real de disponibilidade, distinta da lógica
de produção contínua usada em outros planetas.

## Pendente
- Detalhar a função específica de cada estrutura (uma por arquivo).
- Definir os níveis de evolução (upgrades) de cada estrutura.
- Especificar tecnicamente a interação da Câmara Criônica de Estase com
  o teto global de 48h e capacidade de armazenamento (ver
  `docs/07-prototipo-2-loop-hibrido.md`).
- Especificar a periodicidade/gatilho do evento de aurora.
