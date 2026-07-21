using System.Collections.Generic;
using UnityEngine;

public class PrototypeHud : MonoBehaviour
{
    private const string InstructionsText =
        "Como jogar:\n" +
        "1. Escolha uma ferramenta abaixo.\n" +
        "2. Clique num tile do terreno para aplicar.\n" +
        "3. Plante, espere crescer (fica dourado quando maduro) e colha.\n" +
        "4. Abra 'Construcao' para escolher o que construir, depois\n" +
        "   clique num tile vazio.\n" +
        "5. Clique na estrutura de processamento para alimentar/coletar.\n" +
        "6. Abra a janela de um Hangar para pausar/retomar automacao e\n" +
        "   configurar rotas de Transporte (manual ou automatico).\n" +
        "7. Sem semente no inventario (produzida pelo Viveiro), o drone\n" +
        "   de Plantio nao planta sozinho.\n" +
        "8. Use 'Demolir' pra remover cultivo ou estrutura de um tile.\n" +
        "9. Pode fechar o jogo e voltar depois - a produção continua.";

    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private CropDefinition _cropDefinition;
    private ProcessingStructureDefinition _structureDefinition;
    private ArmazemGeralDefinition _armazemDefinition;
    private HangarDeDronesDefinition _hangarDefinition;
    private ProcessingStructureDefinition _viveiroDefinition;
    private string _message;
    private bool _showInstructions = true;
    private bool _showConstructionWindow;

    private Rect _mainRect = new(10, 10, 360, 460);
    private Rect _constructionRect = new(380, 10, 260, 200);
    private readonly Dictionary<HangarDeDrones, Rect> _hangarWindowRects = new();
    private readonly HashSet<HangarDeDrones> _openHangarWindows = new();

    public void Initialize(
        PlayerInventory inventory,
        ToolSelector toolSelector,
        CropDefinition cropDefinition,
        ProcessingStructureDefinition structureDefinition,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition,
        ProcessingStructureDefinition viveiroDefinition)
    {
        _inventory = inventory;
        _toolSelector = toolSelector;
        _cropDefinition = cropDefinition;
        _structureDefinition = structureDefinition;
        _armazemDefinition = armazemDefinition;
        _hangarDefinition = hangarDefinition;
        _viveiroDefinition = viveiroDefinition;
    }

    public void ShowMessage(string message)
    {
        _message = message;
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
            _hangarWindowRects[hangar] = GUILayout.Window(id, rect, windowIndex => DrawHangarWindow(hangar), $"Hangar de Drones");
        }
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
        if (GUILayout.Button($"Plantar ({_cropDefinition.displayName})")) _toolSelector.SelectPlant();
        if (GUILayout.Button("Colher")) _toolSelector.SelectHarvest();
        if (GUILayout.Button("Demolir")) _toolSelector.SelectDemolish();

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
        if (GUILayout.Button(_structureDefinition.displayName))
        {
            _toolSelector.SelectBuildProcessing();
            _showConstructionWindow = false;
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

        if (GUILayout.Button(_viveiroDefinition.displayName))
        {
            _toolSelector.SelectBuildViveiro();
            _showConstructionWindow = false;
        }

        GUI.DragWindow();
    }

    private void DrawHangarWindow(HangarDeDrones hangar)
    {
        string plantioLabel = hangar.PlantioEnabled ? "Pausar Plantio automatico" : "Retomar Plantio automatico";
        if (GUILayout.Button(plantioLabel)) hangar.SetPlantioEnabled(!hangar.PlantioEnabled);

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
}
