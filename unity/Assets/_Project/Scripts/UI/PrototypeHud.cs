using UnityEngine;

public class PrototypeHud : MonoBehaviour
{
    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private CropDefinition _cropDefinition;
    private ProcessingStructureDefinition _structureDefinition;

    public void Initialize(
        PlayerInventory inventory,
        ToolSelector toolSelector,
        CropDefinition cropDefinition,
        ProcessingStructureDefinition structureDefinition)
    {
        _inventory = inventory;
        _toolSelector = toolSelector;
        _cropDefinition = cropDefinition;
        _structureDefinition = structureDefinition;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 280, 260), GUI.skin.box);
        GUILayout.Label("Prototipo 1 - Loop Ativo (placeholder)");
        GUILayout.Space(6);

        GUILayout.Label($"Ferramenta atual: {_toolSelector.CurrentTool}");
        if (GUILayout.Button("Nenhuma")) _toolSelector.SelectNone();
        if (GUILayout.Button($"Plantar ({_cropDefinition.displayName})")) _toolSelector.SelectPlant();
        if (GUILayout.Button("Colher")) _toolSelector.SelectHarvest();
        if (GUILayout.Button($"Construir ({_structureDefinition.displayName})")) _toolSelector.SelectBuild();

        GUILayout.Space(10);
        GUILayout.Label("Inventario:");
        foreach (var pair in _inventory.All)
        {
            GUILayout.Label($"  {pair.Key}: {pair.Value}");
        }

        GUILayout.EndArea();
    }
}
