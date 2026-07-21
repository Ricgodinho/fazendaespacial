using UnityEngine;

public class PrototypeHud : MonoBehaviour
{
    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private CropDefinition _cropDefinition;
    private ProcessingStructureDefinition _structureDefinition;
    private string _message;

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

    public void ShowMessage(string message)
    {
        _message = message;
    }

    private void OnGUI()
    {
        bool hasMessage = !string.IsNullOrEmpty(_message);
        float height = 260 + (hasMessage ? 40 : 0);

        GUILayout.BeginArea(new Rect(10, 10, 320, height), GUI.skin.box);
        GUILayout.Label("Prototipo 2 - Loop Hibrido (placeholder)");

        if (hasMessage)
        {
            GUILayout.Space(4);
            GUILayout.Label(_message, GUI.skin.box);
        }

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
