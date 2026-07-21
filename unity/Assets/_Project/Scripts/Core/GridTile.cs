using UnityEngine;

public enum TileOccupancy
{
    Empty,
    Crop,
    Structure
}

public class GridTile : MonoBehaviour
{
    public Vector2Int Coord { get; private set; }
    public TileOccupancy Occupancy { get; private set; } = TileOccupancy.Empty;
    public CropInstance PlantedCrop { get; private set; }
    public ProcessingStructure BuiltStructure { get; private set; }

    private Renderer _groundRenderer;

    private static readonly Color EmptyColor = new Color(0.3f, 0.55f, 0.25f);
    private static readonly Color OccupiedColor = new Color(0.45f, 0.3f, 0.15f);

    public void Initialize(Vector2Int coord, Renderer groundRenderer)
    {
        Coord = coord;
        _groundRenderer = groundRenderer;
        RefreshGroundColor();
    }

    public bool PlantCrop(CropDefinition definition, float initialAccumulatedSeconds = 0f)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var cropObject = new GameObject($"Crop_{definition.displayName}");
        // Parenteado ao TileGrid (escala identidade), nao ao tile em si -
        // o tile tem escala nao-uniforme (achatado no Y) que distorceria
        // qualquer filho parenteado diretamente a ele.
        cropObject.transform.SetParent(transform.parent, worldPositionStays: false);
        cropObject.transform.position = transform.position + new Vector3(0f, 0.2f, 0f);

        PlantedCrop = cropObject.AddComponent<CropInstance>();
        PlantedCrop.Initialize(definition, initialAccumulatedSeconds);

        Occupancy = TileOccupancy.Crop;
        RefreshGroundColor();
        return true;
    }

    public bool HarvestCrop(out CropDefinition harvested)
    {
        harvested = null;
        if (Occupancy != TileOccupancy.Crop || PlantedCrop == null || !PlantedCrop.IsMature)
        {
            return false;
        }

        harvested = PlantedCrop.Definition;
        Destroy(PlantedCrop.gameObject);
        PlantedCrop = null;
        Occupancy = TileOccupancy.Empty;
        RefreshGroundColor();
        return true;
    }

    public bool BuildStructure(
        ProcessingStructureDefinition definition,
        float initialProcessElapsedSeconds = 0f,
        int initialStoredInput = 0,
        int initialStoredOutput = 0)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var structureObject = new GameObject($"Structure_{definition.displayName}");
        // Mesmo motivo do PlantCrop: parenteado ao TileGrid, nao ao tile,
        // para nao herdar a escala achatada do tile. A propria
        // ProcessingStructure cria e gerencia seu visual (cor/rotacao
        // conforme o estado de processamento).
        structureObject.transform.SetParent(transform.parent, worldPositionStays: false);
        structureObject.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);

        BuiltStructure = structureObject.AddComponent<ProcessingStructure>();
        BuiltStructure.Initialize(definition, initialProcessElapsedSeconds, initialStoredInput, initialStoredOutput);

        Occupancy = TileOccupancy.Structure;
        RefreshGroundColor();
        return true;
    }

    private void RefreshGroundColor()
    {
        if (_groundRenderer == null)
        {
            return;
        }

        RendererTint.SetColor(_groundRenderer, Occupancy == TileOccupancy.Empty ? EmptyColor : OccupiedColor);
    }
}
