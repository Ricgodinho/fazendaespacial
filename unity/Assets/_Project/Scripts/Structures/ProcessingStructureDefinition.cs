using UnityEngine;

[CreateAssetMenu(fileName = "NewProcessingStructure", menuName = "FazendaEspacial/Estrutura de Processamento")]
public class ProcessingStructureDefinition : ScriptableObject
{
    public string displayName = "Processamento de Trigo";

    public CropDefinition inputCropDefinition;
    public int inputAmountRequired = 5;

    public string outputResourceName = "Farinha Lunar";
    public int outputAmountProduced = 3;

    [Tooltip("Valor curto para prototipagem, a ajustar depois via playtest (docs/05).")]
    public float processTimeSeconds = 8f;

    public int outputStorageCapacity = 30;

    [Tooltip("Marca esta definicao como o Viveiro (docs/estruturas/planeta-1/viveiro.md) - mesma classe, so muda o insumo/produto. Usado pelo save/load para reconstruir o tipo certo.")]
    public bool isViveiro;
}
