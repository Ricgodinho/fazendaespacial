using UnityEngine;

[CreateAssetMenu(fileName = "NewCrop", menuName = "FazendaEspacial/Cultivo")]
public class CropDefinition : ScriptableObject
{
    public string displayName = "Trigo Lunar";

    [Tooltip("Valor curto para prototipagem. Valor de design real: 2 horas (docs/04-prototipo-0-loop-idle.md).")]
    public float growTimeSeconds = 10f;

    public int yieldAmount = 10;
}
