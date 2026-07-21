using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private TileGrid _grid;
    private ArmazemGeralDefinition _armazemDefinition;
    private HangarDeDronesDefinition _hangarDefinition;
    private List<ProcessingStructureDefinition> _processingStructures;
    private CropDefinition _cropForHangarAutoPlant;
    private ProcessingStructureDefinition _viveiroDefinition;
    private PrototypeHud _hud;

    public void Initialize(
        PlayerInventory inventory,
        ToolSelector toolSelector,
        TileGrid grid,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition,
        List<ProcessingStructureDefinition> processingStructures,
        CropDefinition cropForHangarAutoPlant,
        ProcessingStructureDefinition viveiroDefinition,
        PrototypeHud hud)
    {
        _inventory = inventory;
        _toolSelector = toolSelector;
        _grid = grid;
        _armazemDefinition = armazemDefinition;
        _hangarDefinition = hangarDefinition;
        _processingStructures = processingStructures;
        _cropForHangarAutoPlant = cropForHangarAutoPlant;
        _viveiroDefinition = viveiroDefinition;
        _hud = hud;
    }

    private void Update()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        if (Camera.main == null)
        {
            return;
        }

        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out var hit))
        {
            return;
        }

        var tile = hit.collider.GetComponent<GridTile>();
        if (tile == null)
        {
            return;
        }

        HandleClick(tile);
    }

    private void HandleClick(GridTile tile)
    {
        // Demolir tem prioridade sobre qualquer outra interacao do tile.
        if (_toolSelector.CurrentTool == ToolType.Demolish)
        {
            tile.Demolish();
            return;
        }

        // Clicar em qualquer estrutura ja construida abre a janela de
        // conteudo dela (mostra o que tem dentro), em vez de agir direto.
        if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure != null)
        {
            _hud.ToggleStructureWindow(tile.BuiltStructure);
            return;
        }

        switch (_toolSelector.CurrentTool)
        {
            case ToolType.Plant:
                if (_toolSelector.SelectedCrop != null)
                {
                    tile.PlantCrop(_toolSelector.SelectedCrop);
                }

                break;

            case ToolType.Harvest:
                if (tile.HarvestCrop(out var harvested))
                {
                    _inventory.Add(harvested.displayName, harvested.yieldAmount);
                }

                break;

            case ToolType.Build:
                if (_toolSelector.SelectedProcessingStructure != null)
                {
                    tile.BuildProcessingStructure(_toolSelector.SelectedProcessingStructure);
                }

                break;

            case ToolType.BuildArmazem:
                tile.BuildArmazemGeral(_armazemDefinition, _inventory);
                break;

            case ToolType.BuildHangar:
                tile.BuildHangarDeDrones(
                    _hangarDefinition, _grid, _inventory, _cropForHangarAutoPlant, _viveiroDefinition, _processingStructures);
                break;
        }
    }
}
