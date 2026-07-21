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
    private PrototypeHud _hud;

    public void Initialize(
        PlayerInventory inventory,
        ToolSelector toolSelector,
        TileGrid grid,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition,
        List<ProcessingStructureDefinition> processingStructures,
        CropDefinition cropForHangarAutoPlant,
        PrototypeHud hud)
    {
        _inventory = inventory;
        _toolSelector = toolSelector;
        _grid = grid;
        _armazemDefinition = armazemDefinition;
        _hangarDefinition = hangarDefinition;
        _processingStructures = processingStructures;
        _cropForHangarAutoPlant = cropForHangarAutoPlant;
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
            // Clique pode ter acertado o colisor do proprio cultivo/estrutura
            // (nao o tile diretamente) - resolve pelo TileLink.
            var link = hit.collider.GetComponentInParent<TileLink>();
            tile = link != null ? link.Tile : null;
        }

        if (tile == null)
        {
            return;
        }

        HandleClick(tile);
    }

    private void HandleClick(GridTile tile)
    {
        string coord = $"({tile.Coord.x},{tile.Coord.y})";

        // Demolir tem prioridade sobre qualquer outra interacao do tile.
        if (_toolSelector.CurrentTool == ToolType.Demolish)
        {
            string demolishedWhat = tile.Occupancy.ToString();
            tile.Demolish();
            GameLog.Log("Click", "Demolish", $"tile={coord} ocupacao_antes={demolishedWhat}");
            return;
        }

        // Clicar em qualquer estrutura ja construida abre a janela de
        // conteudo dela (mostra o que tem dentro), em vez de agir direto.
        if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure != null)
        {
            _hud.ToggleStructureWindow(tile.BuiltStructure);
            GameLog.Log("Click", "OpenStructureWindow", $"tile={coord} estrutura={StructureNameFor(tile.BuiltStructure)}");
            return;
        }

        switch (_toolSelector.CurrentTool)
        {
            case ToolType.Plant:
                if (_toolSelector.SelectedCrop == null)
                {
                    GameLog.Log("Click", "Plant_Fail", $"tile={coord} motivo=nenhum_cultivo_selecionado");
                    break;
                }

                bool planted = tile.PlantCrop(_toolSelector.SelectedCrop);
                GameLog.Log("Click", planted ? "Plant_OK" : "Plant_Fail",
                    $"tile={coord} cultivo={_toolSelector.SelectedCrop.displayName}" + (planted ? "" : " motivo=tile_ocupado"));
                break;

            case ToolType.Harvest:
                if (tile.HarvestCrop(out var harvested))
                {
                    _inventory.Add(harvested.displayName, harvested.yieldAmount);
                    GameLog.Log("Click", "Harvest_OK", $"tile={coord} cultivo={harvested.displayName} quantidade={harvested.yieldAmount}");
                }
                else
                {
                    GameLog.Log("Click", "Harvest_Fail", $"tile={coord} motivo=sem_cultivo_maduro");
                }

                break;

            case ToolType.Build:
                if (_toolSelector.SelectedProcessingStructure == null)
                {
                    GameLog.Log("Click", "Build_Fail", $"tile={coord} motivo=nenhuma_estrutura_selecionada");
                    break;
                }

                bool built = tile.BuildProcessingStructure(_toolSelector.SelectedProcessingStructure);
                GameLog.Log("Click", built ? "Build_OK" : "Build_Fail",
                    $"tile={coord} estrutura={_toolSelector.SelectedProcessingStructure.displayName}" + (built ? "" : " motivo=tile_ocupado"));
                break;

            case ToolType.BuildArmazem:
                bool builtArmazem = tile.BuildArmazemGeral(_armazemDefinition, _inventory);
                GameLog.Log("Click", builtArmazem ? "BuildArmazem_OK" : "BuildArmazem_Fail", $"tile={coord}");
                break;

            case ToolType.BuildHangar:
                bool builtHangar = tile.BuildHangarDeDrones(
                    _hangarDefinition, _grid, _inventory, _cropForHangarAutoPlant, _processingStructures);
                GameLog.Log("Click", builtHangar ? "BuildHangar_OK" : "BuildHangar_Fail", $"tile={coord}");
                break;

            default:
                GameLog.Log("Click", "NoOp", $"tile={coord} ferramenta={_toolSelector.CurrentTool}");
                break;
        }
    }

    private static string StructureNameFor(PlacedStructure structure)
    {
        return structure switch
        {
            ProcessingStructure processing => processing.Definition.displayName,
            ArmazemGeral armazem => armazem.Definition.displayName,
            HangarDeDrones hangar => hangar.Definition.displayName,
            _ => structure.GetType().Name
        };
    }
}
