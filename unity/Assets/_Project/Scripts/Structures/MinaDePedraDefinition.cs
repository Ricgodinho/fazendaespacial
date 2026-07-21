using UnityEngine;

[CreateAssetMenu(fileName = "NewMinaDePedra", menuName = "FazendaEspacial/Mina de Pedra")]
public class MinaDePedraDefinition : ScriptableObject
{
    public string displayName = "Mina de Pedra";

    public string outputResourceName = "Pedra Ancestral";

    [Tooltip("Nivel 1, ver docs/estruturas/planeta-1/mina-de-pedra.md")]
    public int outputAmountProduced = 5;

    [Tooltip("Valor curto para prototipagem, a ajustar depois via playtest.")]
    public float processTimeSeconds = 8f;

    public int outputStorageCapacity = 30;
}
