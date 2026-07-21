using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    private PlayerInventory _inventory;
    private ToolSelector _toolSelector;
    private TileGrid _grid;
    private CropDefinition _cropDefinition;
    private ProcessingStructureDefinition _structureDefinition;
    private ArmazemGeralDefinition _armazemDefinition;
    private HangarDeDronesDefinition _hangarDefinition;
    private ProcessingStructureDefinition _viveiroDefinition;

    public void Initialize(
        PlayerInventory inventory,
        ToolSelector toolSelector,
        TileGrid grid,
        CropDefinition cropDefinition,
        ProcessingStructureDefinition structureDefinition,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition,
        ProcessingStructureDefinition viveiroDefinition)
    {
        _inventory = inventory;
        _toolSelector = toolSelector;
        _grid = grid;
        _cropDefinition = cropDefinition;
        _structureDefinition = structureDefinition;
        _armazemDefinition = armazemDefinition;
        _hangarDefinition = hangarDefinition;
        _viveiroDefinition = viveiroDefinition;
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
        // Clicar numa Estrutura de Processamento sempre coleta/alimenta primeiro,
        // independente da ferramenta selecionada (docs/05-prototipo-1-loop-ativo.md).
        // Armazem Geral e Hangar de Drones nao tem interacao manual nesta versao.
        if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure is ProcessingStructure processingStructure)
        {
            HandleStructureClick(processingStructure);
            return;
        }

        switch (_toolSelector.CurrentTool)
        {
            case ToolType.Plant:
                tile.PlantCrop(_cropDefinition);
                break;

            case ToolType.Harvest:
                if (tile.HarvestCrop(out var harvested))
                {
                    _inventory.Add(harvested.displayName, harvested.yieldAmount);
                }
                break;

            case ToolType.BuildProcessing:
                tile.BuildProcessingStructure(_structureDefinition);
                break;

            case ToolType.BuildArmazem:
                tile.BuildArmazemGeral(_armazemDefinition, _inventory);
                break;

            case ToolType.BuildHangar:
                tile.BuildHangarDeDrones(_hangarDefinition, _grid, _inventory, _cropDefinition);
                break;

            case ToolType.BuildViveiro:
                tile.BuildProcessingStructure(_viveiroDefinition);
                break;
        }
    }

    private void HandleStructureClick(ProcessingStructure structure)
    {
        if (structure.HasOutputReady)
        {
            int roomInInventory = _inventory.Capacity.HasValue
                ? Mathf.Max(0, _inventory.Capacity.Value - _inventory.Total)
                : int.MaxValue;

            int toCollect = Mathf.Min(structure.StoredOutput, roomInInventory);
            if (toCollect > 0)
            {
                structure.CollectOutput(toCollect);
                _inventory.Add(structure.Definition.outputResourceName, toCollect);
            }

            return;
        }

        int available = _inventory.GetAmount(structure.Definition.inputCropDefinition.displayName);
        int deposited = structure.TryDepositInput(available);
        if (deposited > 0)
        {
            _inventory.TryRemove(structure.Definition.inputCropDefinition.displayName, deposited);
        }
    }
}
