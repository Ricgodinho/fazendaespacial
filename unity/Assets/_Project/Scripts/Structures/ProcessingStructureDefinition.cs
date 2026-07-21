using UnityEngine;

[CreateAssetMenu(fileName = "NewProcessingStructure", menuName = "FazendaEspacial/Estrutura de Processamento")]
public class ProcessingStructureDefinition : ScriptableObject
{
    public string displayName = "Processamento de Trigo";

    // Nome do recurso exigido como insumo (chave no PlayerInventory) - nao
    // precisa ser um CropDefinition (ex: "Pedra Ancestral" vem de uma mina,
    // nao de um cultivo plantavel).
    public string inputResourceName = "Trigo Lunar";
    public int inputAmountRequired = 5;

    public string outputResourceName = "Farinha Lunar";
    public int outputAmountProduced = 3;

    [Tooltip("Valor curto para prototipagem, a ajustar depois via playtest (docs/05).")]
    public float processTimeSeconds = 8f;

    public int outputStorageCapacity = 30;
}
