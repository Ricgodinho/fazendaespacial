using UnityEngine;

[CreateAssetMenu(fileName = "NewHangarDeDrones", menuName = "FazendaEspacial/Hangar de Drones")]
public class HangarDeDronesDefinition : ScriptableObject
{
    public string displayName = "Hangar de Drones";

    [Tooltip("Nivel 1, ver docs/estruturas/planeta-1/hangar-de-drones.md")]
    public int poolCapacityWeight = 10;

    [Tooltip("Valor curto para prototipagem - intervalo entre acoes de cada drone Tier 1.")]
    public float droneActionIntervalSeconds = 2f;

    [Tooltip("Raio (em tiles) dentro do qual o drone Tier 1 consegue operar - 'precisa estar proximo da estrutura' (docs/drones/colheita.md e plantio.md).")]
    public int droneRangeInTiles = 3;

    [Tooltip("Drone de Transporte Tier 1 (docs/drones/transporte.md): 'carrega quantidade pequena por viagem'. Viagem e manual (jogador aciona), nao automatica.")]
    public int transporteCapacidadePorViagem = 10;
}
