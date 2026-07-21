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

    public bool PlantCrop(CropDefinition definition)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var cropObject = new GameObject($"Crop_{definition.displayName}");
        cropObject.transform.SetParent(transform, worldPositionStays: false);
        cropObject.transform.localPosition = new Vector3(0f, 0.2f, 0f);

        PlantedCrop = cropObject.AddComponent<CropInstance>();
        PlantedCrop.Initialize(definition);

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

    public bool BuildStructure(ProcessingStructureDefinition definition)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var structureObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        structureObject.name = $"Structure_{definition.displayName}";
        structureObject.transform.SetParent(transform, worldPositionStays: false);
        structureObject.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        structureObject.transform.localScale = new Vector3(0.8f, 1f, 0.8f);

        // O raycast de clique deve sempre acertar o tile, nao a estrutura em cima dele.
        Destroy(structureObject.GetComponent<Collider>());

        var renderer = structureObject.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        renderer.material.color = Color.gray;

        BuiltStructure = structureObject.AddComponent<ProcessingStructure>();
        BuiltStructure.Initialize(definition);

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

        _groundRenderer.material.color = Occupancy == TileOccupancy.Empty ? EmptyColor : OccupiedColor;
    }
}
