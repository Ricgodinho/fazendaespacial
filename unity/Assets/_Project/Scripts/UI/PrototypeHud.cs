using UnityEngine;

public class PrototypeHud : MonoBehaviour
{
    private const string InstructionsText =
        "Como jogar:\n" +
        "1. Escolha uma ferramenta abaixo.\n" +
        "2. Clique num tile do terreno para aplicar.\n" +
        "3. Plante, espere crescer (fica dourado quando maduro) e colha.\n" +
        "4. Construa a estrutura e clique nela para alimentar com o que\n" +
        "   colheu.\n" +
        "5. Ela fica azul enquanto processa; clique de novo quando ficar\n" +
        "   dourada para coletar o resultado.\n" +
        "6. Pode fechar o jogo e voltar depois - a produção continua.";

    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private CropDefinition _cropDefinition;
    private ProcessingStructureDefinition _structureDefinition;
    private string _message;
    private bool _showInstructions = true;

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
        GUILayout.BeginArea(new Rect(10, 10, 340, 500), GUI.skin.box);
        GUILayout.Label("Prototipo 2 - Loop Hibrido (placeholder)");

        if (!string.IsNullOrEmpty(_message))
        {
            GUILayout.Space(4);
            GUILayout.Label(_message, GUI.skin.box);
        }

        GUILayout.Space(6);
        if (GUILayout.Button(_showInstructions ? "Ocultar instrucoes" : "Como jogar?"))
        {
            _showInstructions = !_showInstructions;
        }

        if (_showInstructions)
        {
            GUILayout.Label(InstructionsText, GUI.skin.box);
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
