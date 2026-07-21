using UnityEngine;

[CreateAssetMenu(fileName = "NewArmazemGeral", menuName = "FazendaEspacial/Armazem Geral")]
public class ArmazemGeralDefinition : ScriptableObject
{
    public string displayName = "Armazem Geral";

    [Tooltip("Nivel 1, ver docs/estruturas/planeta-1/armazem-geral.md")]
    public int capacity = 100;
}
