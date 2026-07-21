using System.Collections.Generic;
using UnityEngine;

public class PrototypeHud : MonoBehaviour
{
    private const string InstructionsText =
        "Como jogar:\n" +
        "1. Escolha uma ferramenta ou cultivo abaixo.\n" +
        "2. Clique num tile do terreno para aplicar.\n" +
        "3. Plante, espere crescer (fica dourado quando maduro) e colha.\n" +
        "4. Abra 'Construcao' para escolher o que construir, depois\n" +
        "   clique num tile vazio.\n" +
        "5. Clique numa estrutura ja construida para ver o que tem\n" +
        "   dentro dela (insumo/produto) e alimentar/coletar.\n" +
        "6. Abra a janela de um Hangar para pausar/retomar automacao e\n" +
        "   configurar rotas de Transporte (manual ou automatico).\n" +
        "7. Sem semente no inventario (produzida pelo Viveiro), o drone\n" +
        "   de Plantio nao planta sozinho.\n" +
        "8. Use 'Demolir' pra remover cultivo ou estrutura de um tile.\n" +
        "9. Pode fechar o jogo e voltar depois - a produção continua.";

    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private List<CropDefinition> _crops;
    private List<ProcessingStructureDefinition> _processingStructures;
    private ArmazemGeralDefinition _armazemDefinition;
    private HangarDeDronesDefinition _hangarDefinition;
    private string _message;
    private bool _showInstructions = true;
    private bool _showConstructionWindow;

    private Rect _mainRect = new(10, 10, 360, 480);
    private Rect _constructionRect = new(380, 10, 260, 220);
    private readonly Dictionary<HangarDeDrones, Rect> _hangarWindowRects = new();
    private readonly HashSet<HangarDeDrones> _openHangarWindows = new();
    private readonly Dictionary<PlacedStructure, Rect> _structureWindowRects = new();
    private readonly HashSet<PlacedStructure> _openStructureWindows = new();

    public void Initialize(
        PlayerInventory inventory,
        ToolSelector toolSelector,
        List<CropDefinition> crops,
        List<ProcessingStructureDefinition> processingStructures,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition)
    {
        _inventory = inventory;
        _toolSelector = toolSelector;
        _crops = crops;
        _processingStructures = processingStructures;
        _armazemDefinition = armazemDefinition;
        _hangarDefinition = hangarDefinition;
    }

    public void ShowMessage(string message)
    {
        _message = message;
    }

    // Chamado pelo ClickController ao clicar numa estrutura ja construida.
    // Hangar de Drones reusa a janela dedicada dele; as demais ganham uma
    // janela de conteudo generica.
    public void ToggleStructureWindow(PlacedStructure structure)
    {
        if (structure is HangarDeDrones hangar)
        {
            if (!_openHangarWindows.Remove(hangar))
            {
                _openHangarWindows.Add(hangar);
            }

            return;
        }

        if (!_openStructureWindows.Remove(structure))
        {
            _openStructureWindows.Add(structure);
        }
    }

    private void OnGUI()
    {
        _mainRect = GUILayout.Window(1, _mainRect, DrawMainWindow, "Prototipo - Planeta 1 (placeholder)");

        if (_showConstructionWindow)
        {
            _constructionRect = GUILayout.Window(2, _constructionRect, DrawConstructionWindow, "Construcao");
        }

        int windowId = 100;
        foreach (var hangar in HangarDeDrones.Instances)
        {
            if (!_openHangarWindows.Contains(hangar))
            {
                windowId++;
                continue;
            }

            if (!_hangarWindowRects.TryGetValue(hangar, out var rect))
            {
                rect = new Rect(380, 220 + (windowId - 100) * 30, 320, 260);
                _hangarWindowRects[hangar] = rect;
            }

            int id = windowId++;
            _hangarWindowRects[hangar] = GUILayout.Window(id, rect, _ => DrawHangarWindow(hangar), "Hangar de Drones");
        }

        int structureWindowId = 1000;
        foreach (var structure in _openStructureWindows)
        {
            if (!_structureWindowRects.TryGetValue(structure, out var rect))
            {
                rect = new Rect(380, 220 + (structureWindowId - 1000) * 30, 300, 220);
                _structureWindowRects[structure] = rect;
            }

            int id = structureWindowId++;
            _structureWindowRects[structure] = GUILayout.Window(id, rect, _ => DrawStructureWindow(structure), StructureTitleFor(structure));
        }
    }

    private static string StructureTitleFor(PlacedStructure structure)
    {
        return structure switch
        {
            ProcessingStructure processing => processing.Definition.displayName,
            ArmazemGeral armazem => armazem.Definition.displayName,
            _ => "Estrutura"
        };
    }

    private void DrawMainWindow(int id)
    {
        if (!string.IsNullOrEmpty(_message))
        {
            GUILayout.Label(_message, GUI.skin.box);
        }

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
        if (GUILayout.Button("Colher")) _toolSelector.SelectHarvest();
        if (GUILayout.Button("Demolir")) _toolSelector.SelectDemolish();

        foreach (var crop in _crops)
        {
            if (GUILayout.Button($"Plantar ({crop.displayName})"))
            {
                _toolSelector.SelectPlant(crop);
            }
        }

        GUILayout.Space(6);
        if (GUILayout.Button(_showConstructionWindow ? "Fechar Construcao" : "Construcao"))
        {
            _showConstructionWindow = !_showConstructionWindow;
        }

        if (HangarDeDrones.Instances.Count > 0)
        {
            GUILayout.Space(6);
            GUILayout.Label("Hangares:");
            for (int i = 0; i < HangarDeDrones.Instances.Count; i++)
            {
                var hangar = HangarDeDrones.Instances[i];
                bool isOpen = _openHangarWindows.Contains(hangar);
                if (GUILayout.Button($"{(isOpen ? "Fechar" : "Abrir")} Hangar {i + 1}"))
                {
                    if (isOpen)
                    {
                        _openHangarWindows.Remove(hangar);
                    }
                    else
                    {
                        _openHangarWindows.Add(hangar);
                    }
                }
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

        GUI.DragWindow();
    }

    private void DrawConstructionWindow(int id)
    {
        foreach (var structure in _processingStructures)
        {
            if (GUILayout.Button(structure.displayName))
            {
                _toolSelector.SelectBuild(structure);
                _showConstructionWindow = false;
            }
        }

        if (GUILayout.Button(_armazemDefinition.displayName))
        {
            _toolSelector.SelectBuildArmazem();
            _showConstructionWindow = false;
        }

        if (GUILayout.Button(_hangarDefinition.displayName))
        {
            _toolSelector.SelectBuildHangar();
            _showConstructionWindow = false;
        }

        GUI.DragWindow();
    }

    private void DrawHangarWindow(HangarDeDrones hangar)
    {
        GUILayout.Label($"Drones: {HangarDeDrones.DroneCount} (peso {hangar.WeightUsed}/{hangar.Definition.poolCapacityWeight})");
        GUILayout.Label("Colheita, Plantio, Transporte - Tier 1");
        GUILayout.Space(6);

        string plantioLabel = hangar.PlantioEnabled ? "Pausar Plantio automatico" : "Retomar Plantio automatico";
        if (GUILayout.Button(plantioLabel)) hangar.SetPlantioEnabled(!hangar.PlantioEnabled);

        // Tier 1 so automatiza 1 cultivo por vez (docs/drones/plantio.md) -
        // o jogador escolhe qual, em vez de ficar fixo em codigo. Sem
        // Viveiro proprio pro cultivo escolhido, o drone so fica parado
        // esperando semente, nunca rouba o tile de outro cultivo.
        GUILayout.Label($"Cultivo automatico: {hangar.AutoPlantCrop.displayName}");
        GUILayout.BeginHorizontal();
        foreach (var crop in _crops)
        {
            bool isSelected = hangar.AutoPlantCrop == crop;
            GUI.enabled = !isSelected;
            if (GUILayout.Button(crop.displayName))
            {
                hangar.SetAutoPlantCrop(crop);
            }

            GUI.enabled = true;
        }

        GUILayout.EndHorizontal();

        string colheitaLabel = hangar.ColheitaEnabled ? "Pausar Colheita automatica" : "Retomar Colheita automatica";
        if (GUILayout.Button(colheitaLabel)) hangar.SetColheitaEnabled(!hangar.ColheitaEnabled);

        GUILayout.Space(8);
        GUILayout.Label("Transporte:");
        foreach (var route in hangar.TransporteRoutes)
        {
            bool isDelivery = route.Direction == TransporteDirection.Delivery;
            string routeLabel = isDelivery
                ? $"Armazem -> {route.TargetDefinition.displayName}"
                : $"{route.TargetDefinition.displayName} -> Armazem";
            GUILayout.Label(routeLabel);

            GUILayout.BeginHorizontal();
            GUI.enabled = !route.Busy;
            if (GUILayout.Button(isDelivery ? "Entregar agora" : "Coletar agora"))
            {
                hangar.TryRunRoute(route);
            }

            GUI.enabled = true;
            string autoLabel = route.AutomaticEnabled ? "Automatico: LIGADO" : "Automatico: DESLIGADO";
            if (GUILayout.Button(autoLabel))
            {
                route.AutomaticEnabled = !route.AutomaticEnabled;
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(4);
        }

        GUI.DragWindow();
    }

    private void DrawStructureWindow(PlacedStructure structure)
    {
        switch (structure)
        {
            case ProcessingStructure processing:
                DrawProcessingStructureContents(processing);
                break;
            case ArmazemGeral armazem:
                DrawArmazemContents(armazem);
                break;
        }

        GUI.DragWindow();
    }

    private void DrawProcessingStructureContents(ProcessingStructure processing)
    {
        string status = processing.IsProcessing ? "Processando" : processing.HasOutputReady ? "Pronto para coleta" : "Parado";
        GUILayout.Label($"Status: {status}");
        GUILayout.Label($"Insumo ({processing.Definition.inputCropDefinition.displayName}): {processing.StoredInput}/{processing.Definition.inputAmountRequired}");
        GUILayout.Label($"Produto ({processing.Definition.outputResourceName}): {processing.StoredOutput}/{processing.Definition.outputStorageCapacity}");

        GUILayout.Space(6);
        if (GUILayout.Button($"Alimentar com {processing.Definition.inputCropDefinition.displayName}"))
        {
            string resourceName = processing.Definition.inputCropDefinition.displayName;
            int available = _inventory.GetAmount(resourceName);
            int deposited = processing.TryDepositInput(available);
            if (deposited > 0)
            {
                _inventory.TryRemove(resourceName, deposited);
            }

            GameLog.Log("Janela", "Alimentar", $"estrutura={processing.Definition.displayName} recurso={resourceName} disponivel={available} depositado={deposited}");
        }

        if (GUILayout.Button($"Coletar {processing.Definition.outputResourceName}"))
        {
            int roomInInventory = _inventory.Capacity.HasValue
                ? Mathf.Max(0, _inventory.Capacity.Value - _inventory.Total)
                : int.MaxValue;

            int readyBefore = processing.StoredOutput;
            int toCollect = Mathf.Min(readyBefore, roomInInventory);
            if (toCollect > 0)
            {
                processing.CollectOutput(toCollect);
                _inventory.Add(processing.Definition.outputResourceName, toCollect);
            }

            GameLog.Log("Janela", "Coletar", $"estrutura={processing.Definition.displayName} recurso={processing.Definition.outputResourceName} pronto={readyBefore} coletado={toCollect} espaco_inventario={roomInInventory}");
        }
    }

    private void DrawArmazemContents(ArmazemGeral armazem)
    {
        GUILayout.Label($"Capacidade: {_inventory.Total}/{armazem.Definition.capacity}");
        GUILayout.Space(6);

        if (_inventory.All.Count == 0)
        {
            GUILayout.Label("(vazio)");
            return;
        }

        foreach (var pair in _inventory.All)
        {
            GUILayout.Label($"  {pair.Key}: {pair.Value}");
        }
    }
}
