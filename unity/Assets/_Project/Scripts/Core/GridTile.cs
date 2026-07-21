using System.Collections.Generic;
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
    public PlacedStructure BuiltStructure { get; private set; }

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
        PlantedCrop.Initialize(definition, this, initialAccumulatedSeconds);

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

    public bool BuildProcessingStructure(
        ProcessingStructureDefinition definition,
        float initialProcessElapsedSeconds = 0f,
        int initialStoredInput = 0,
        int initialStoredOutput = 0)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var structure = CreateStructureAnchor(definition.displayName).AddComponent<ProcessingStructure>();
        structure.Initialize(definition, this, initialProcessElapsedSeconds, initialStoredInput, initialStoredOutput);
        BuiltStructure = structure;

        Occupancy = TileOccupancy.Structure;
        RefreshGroundColor();
        return true;
    }

    public bool BuildArmazemGeral(ArmazemGeralDefinition definition, PlayerInventory inventory)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var armazem = CreateStructureAnchor(definition.displayName).AddComponent<ArmazemGeral>();
        armazem.Initialize(definition, this, inventory);
        BuiltStructure = armazem;

        Occupancy = TileOccupancy.Structure;
        RefreshGroundColor();
        return true;
    }

    public bool BuildHangarDeDrones(
        HangarDeDronesDefinition definition,
        TileGrid grid,
        PlayerInventory inventory,
        CropDefinition cropToAutoPlant,
        IEnumerable<ProcessingStructureDefinition> transporteTargets)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var hangar = CreateStructureAnchor(definition.displayName).AddComponent<HangarDeDrones>();
        hangar.Initialize(definition, this, grid, Coord, inventory, cropToAutoPlant, transporteTargets);
        BuiltStructure = hangar;

        Occupancy = TileOccupancy.Structure;
        RefreshGroundColor();
        return true;
    }

    public bool BuildMinaDePedra(
        MinaDePedraDefinition definition,
        float initialProcessElapsedSeconds = 0f,
        int initialStoredOutput = 0)
    {
        if (Occupancy != TileOccupancy.Empty)
        {
            return false;
        }

        var mina = CreateStructureAnchor(definition.displayName).AddComponent<MinaDePedra>();
        mina.Initialize(definition, this, initialProcessElapsedSeconds, initialStoredOutput);
        BuiltStructure = mina;

        Occupancy = TileOccupancy.Structure;
        RefreshGroundColor();
        return true;
    }

    // Remove o que estiver no tile (cultivo ou estrutura), sem gerar
    // nenhum recurso de volta - e uma demolicao, nao uma colheita.
    public void Demolish()
    {
        if (Occupancy == TileOccupancy.Crop && PlantedCrop != null)
        {
            Destroy(PlantedCrop.gameObject);
            PlantedCrop = null;
        }
        else if (Occupancy == TileOccupancy.Structure && BuiltStructure != null)
        {
            Destroy(BuiltStructure.gameObject);
            BuiltStructure = null;
        }

        Occupancy = TileOccupancy.Empty;
        RefreshGroundColor();
    }

    // Parenteado ao TileGrid (escala identidade), nao ao tile em si - o
    // tile tem escala nao-uniforme (achatado no Y) que distorceria
    // qualquer filho parenteado diretamente a ele. Cada tipo de estrutura
    // cria e gerencia seu proprio visual.
    private GameObject CreateStructureAnchor(string displayName)
    {
        var structureObject = new GameObject($"Structure_{displayName}");
        structureObject.transform.SetParent(transform.parent, worldPositionStays: false);
        structureObject.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
        return structureObject;
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
