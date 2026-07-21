using UnityEngine;

public class PrototypeHud : MonoBehaviour
{
    private const string InstructionsText =
        "Como jogar:\n" +
        "1. Escolha uma ferramenta abaixo.\n" +
        "2. Clique num tile do terreno para aplicar.\n" +
        "3. Plante, espere crescer (fica dourado quando maduro) e colha.\n" +
        "4. Construa a estrutura de processamento e clique nela para\n" +
        "   alimentar com o que colheu.\n" +
        "5. Ela fica azul enquanto processa; clique de novo quando ficar\n" +
        "   dourada para coletar o resultado.\n" +
        "6. Construa o Armazem Geral para ter capacidade de estoque, e o\n" +
        "   Hangar de Drones para automatizar plantio e colheita perto dele.\n" +
        "7. Pode fechar o jogo e voltar depois - a produção continua.";

    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private CropDefinition _cropDefinition;
    private ProcessingStructureDefinition _structureDefinition;
    private ArmazemGeralDefinition _armazemDefinition;
    private HangarDeDronesDefinition _hangarDefinition;
    private string _message;
    private bool _showInstructions = true;

    public void Initialize(
        PlayerInventory inventory,
        ToolSelector toolSelector,
        CropDefinition cropDefinition,
        ProcessingStructureDefinition structureDefinition,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition)
    {
        _inventory = inventory;
        _toolSelector = toolSelector;
        _cropDefinition = cropDefinition;
        _structureDefinition = structureDefinition;
        _armazemDefinition = armazemDefinition;
        _hangarDefinition = hangarDefinition;
    }

    public void ShowMessage(string message)
    {
        _message = message;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 340, 620), GUI.skin.box);
        GUILayout.Label("Prototipo - Planeta 1 (placeholder)");

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
        if (GUILayout.Button($"Construir {_structureDefinition.displayName}")) _toolSelector.SelectBuildProcessing();
        if (GUILayout.Button($"Construir {_armazemDefinition.displayName}")) _toolSelector.SelectBuildArmazem();
        if (GUILayout.Button($"Construir {_hangarDefinition.displayName}")) _toolSelector.SelectBuildHangar();

        if (HangarDeDrones.Instances.Count > 0)
        {
            GUILayout.Space(10);
            GUILayout.Label("Automacao (Hangar de Drones):");
            for (int i = 0; i < HangarDeDrones.Instances.Count; i++)
            {
                var hangar = HangarDeDrones.Instances[i];
                string prefix = HangarDeDrones.Instances.Count > 1 ? $"[Hangar {i + 1}] " : "";

                string plantioLabel = prefix + (hangar.PlantioEnabled ? "Pausar Plantio automatico" : "Retomar Plantio automatico");
                if (GUILayout.Button(plantioLabel)) hangar.SetPlantioEnabled(!hangar.PlantioEnabled);

                string colheitaLabel = prefix + (hangar.ColheitaEnabled ? "Pausar Colheita automatica" : "Retomar Colheita automatica");
                if (GUILayout.Button(colheitaLabel)) hangar.SetColheitaEnabled(!hangar.ColheitaEnabled);
            }
        }

        GUILayout.Space(10);
        string capacityText = _inventory.Capacity.HasValue
            ? $"Inventario ({_inventory.Total}/{_inventory.Capacity.Value}):"
            : "Inventario (sem Armazem Geral - sem limite ainda):";
        GUILayout.Label(capacityText);
        foreach (var pair in _inventory.All)
        {
            GUILayout.Label($"  {pair.Key}: {pair.Value}");
        }

        GUILayout.EndArea();
    }
}
