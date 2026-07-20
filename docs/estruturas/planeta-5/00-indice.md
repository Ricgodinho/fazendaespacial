# Estruturas — Planeta 5 (Planeta-origem / Núcleo da Rede)

Planeta temático: megaestrutura tecnológica em decadência — o berço da
rede agrícola interplanetária, ligado à história do antigo proprietário
(ver `docs/01-conceito.md`, "Identidade central"). É o clímax narrativo e
mecânico do jogo base (Fases 1-2), onde o item Lendário é liberado (ver
`docs/01-conceito.md`, seção Raridade), e funciona como hub para conteúdo
futuro de exploração espacial (Fase 3/4).

## Ordem lógica de desbloqueio — decisão

As estruturas deste planeta têm uma ordem de dependência entre si, não
apenas uma lista solta:

1. **Estação de Comunicação** → reconecta o sinal entre todos os planetas
   já visitados. Primeiro passo, habilita o item 2.
2. **Sala de Controle da Rede** → só funciona após o item 1. Painel
   mostrando o status de produção de todos os planetas simultaneamente.
3. **Reator Central** → restauração em múltiplos estágios, cada um
   consumindo materiais específicos dos Planetas 2, 3 e 4 (ver
   `docs/01-conceito.md`, "Dependência entre planetas"). Ao ser
   completado, dispara uma melhoria visual retroativa aplicada
   simultaneamente aos 4 planetas anteriores (rede reconectada visível em
   cada fazenda).
4. **Arquivo Central** → monta os fragmentos de memória do antigo
   cuidador (coletados na Câmara de Revivescência do Planeta 4, ver
   `docs/estruturas/planeta-4/camara-de-revivescencia.md`), revelando a
   história completa. Não depende do Reator Central.
5. **Fundição Central** → ver nota de exceção abaixo.
6. **Antena/Portal de Exploração** → desbloqueada somente após o Reator
   Central (item 3) ser restaurado. Capta um sinal de origem
   desconhecida — gancho para eventos, novas sementes, e conteúdo de
   Fase 3/4 (não decidido em detalhe, apenas registrado como direção
   futura).
7. Armazém geral (planeta 5) — armazenamento padrão.
8. Hangar de drones (planeta 5) — capacidade de operação simultânea.

## Lista de estruturas — decisão

| # | Estrutura (nome provisório) | Cadeia | Arquivo |
|---|---|---|---|
| 1 | Estação de Comunicação | Narrativa / rede | `estacao-de-comunicacao.md` |
| 2 | Sala de Controle da Rede | Gestão / rede | `sala-de-controle-da-rede.md` |
| 3 | Reator Central | Progressão principal | `reator-central.md` |
| 4 | Arquivo Central | Narrativa | `arquivo-central.md` |
| 5 | Fundição Central | Mineral | `fundicao-central.md` |
| 6 | Antena/Portal de Exploração | Gancho Fase 3/4 | `portal-de-exploracao.md` |
| 7 | Armazém geral (planeta 5) | Todas | `armazem-geral.md` |
| 8 | Hangar de drones (planeta 5) | — | `hangar-de-drones.md` |

## Nota de exceção: Fundição Central

Diferente dos Planetas 2, 3 e 4 (que sempre reaproveitam a Fundição do
Planeta 2, sem duplicar estrutura de refino), o Planeta 5 **tem sua
própria Fundição**. Justificativa: este planeta funciona como hub para
exploração espacial profunda (conteúdo futuro de Fase 3/4), e depender de
transporte até o Planeta 2 para processar minério contradiria seu papel
de ponto autossuficiente e avançado da rede. É uma exceção deliberada à
regra geral, não um esquecimento — ver nota espelhada em
`docs/estruturas/planeta-2/fundicao.md`.

## Nota para fase futura: Manufatura de Equipamento de Exploração (não decidido)

Foi levantada a ideia de uma estrutura de manufatura **específica para
exploração** — diferente da Oficina/Fábrica de Drones do Planeta 2 (que
mantém sua identidade própria de "planeta de criação de drones/upgrade de
naves", não deve ser duplicada ou diluída aqui). Esta manufatura
produziria equipamento exclusivo do papel deste planeta como hub de
exploração (ex: sondas, módulos específicos do Portal de Exploração), não
drones ou itens genéricos. Não é uma estrutura fechada — depende do
desenho do conteúdo de Fase 3/4 (Portal de Exploração) para ser
especificada em detalhe.

## Pendente
- Detalhar a função específica de cada estrutura (uma por arquivo).
- Definir os níveis de evolução (upgrades) de cada estrutura.
- Especificar os estágios do Reator Central (quais materiais de quais
  planetas, em que quantidade).
- Especificar o conteúdo do Portal de Exploração e da Manufatura de
  Equipamento de Exploração (fica para debate de Fase 3/4, não bloqueia
  o fechamento do MVP).
