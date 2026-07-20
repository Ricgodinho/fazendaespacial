# Drone de Colheita

Ver `00-indice.md` para categorias gerais, modelo de progressão e limites
de hangar aplicáveis a todos os drones.

## Especificação por tier — decisão

Cada tier **herda todas as capacidades dos tiers anteriores** e adiciona
uma nova — nenhuma capacidade é substituída ao subir de tier.

| Tier | Função |
|---|---|
| 1 | Colhe 1 tile por vez, precisa estar próximo da estrutura/jogador. Envia ao armazém geral. |
| 2 | Colhe pequena área (ex: 3x3), ainda precisa estar próximo. Envia ao armazém geral. |
| 3 | Ganha alcance — opera sem o jogador/nave por perto (mantém a área do Tier 2). Envia ao armazém geral. |
| 4 | Prioriza automaticamente entre múltiplos cultivos diferentes (mantém alcance do Tier 3 e área do Tier 2). Envia ao armazém geral. |
| 5 | Área de colheita maior (ex: 5x5), mantendo alcance e priorização dos tiers anteriores. Jogador escolhe o destino do que foi colhido: processamento direto na estrutura ou armazém geral (para vender como matéria-prima ou guardar). |

### Destino do que é colhido (regra geral)
- **Tiers 1 a 4**: sempre envia ao **armazém geral**. Para processar, o
  jogador precisa de um drone de Transporte (ou ação manual) para levar
  do armazém até a estrutura de processamento.
- **Tier 5**: elimina essa etapa intermediária ao permitir entrega direta
  na estrutura de processamento, mas mantém a opção de armazém geral como
  escolha do jogador — preservando o princípio de que decisões de
  economia/venda continuam na mão do jogador (ver `docs/01-conceito.md`).

## Pendente
- Aplicação da raridade dentro de cada tier deste drone.

