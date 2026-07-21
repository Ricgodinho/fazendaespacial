using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
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
        // Clicar numa estrutura sempre coleta/alimenta primeiro, independente da
        // ferramenta selecionada (docs/05-prototipo-1-loop-ativo.md: "nao precisa
        // de ferramenta selecionada para esta acao"). Sem drones ainda, a
        // alimentacao manual do inventario para a estrutura acontece no mesmo clique.
        if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure != null)
        {
            HandleStructureClick(tile.BuiltStructure);
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

            case ToolType.Build:
                tile.BuildStructure(_structureDefinition);
                break;
        }
    }

    private void HandleStructureClick(ProcessingStructure structure)
    {
        if (structure.HasOutputReady)
        {
            int collected = structure.CollectOutput();
            _inventory.Add(structure.Definition.outputResourceName, collected);
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
