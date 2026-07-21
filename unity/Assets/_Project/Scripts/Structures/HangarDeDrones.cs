using UnityEngine;

// So implementa a funcao central do Nivel 1: hospeda 1 drone de Colheita e
// 1 drone de Plantio Tier 1 (docs/drones/colheita.md, docs/drones/plantio.md).
//
// Simplificacoes assumidas nesta implementacao (fora de escopo por ora):
// - Sem economia de aquisicao de drones (achado/fabricado, ver
//   docs/drones/00-indice.md) - o Hangar ja nasce com os 2 drones Tier 1.
// - Sem drone entity visivel/com movimento - o efeito liquido (1 tile
//   colhido/plantado por intervalo, dentro de um raio) e simulado
//   diretamente pelo Hangar.
// - Drone de Plantio "usa sementes do Armazem Geral" no design, mas o
//   Viveiro (que produz sementes) ainda nao existe - por ora replanta o
//   proprio cultivo diretamente, sem consumir um recurso "semente" a parte.
// - Sem durabilidade/desgaste de drone (docs/drones/00-indice.md).
public class HangarDeDrones : PlacedStructure
{
    private static readonly Color VisualColor = new Color(0.35f, 0.4f, 0.45f);

    public HangarDeDronesDefinition Definition { get; private set; }

    private TileGrid _grid;
    private Vector2Int _coord;
    private PlayerInventory _inventory;
    private CropDefinition _cropToAutoPlant;
    private float _timer;

    public void Initialize(
        HangarDeDronesDefinition definition,
        TileGrid grid,
        Vector2Int coord,
        PlayerInventory inventory,
        CropDefinition cropToAutoPlant)
    {
        Definition = definition;
        _grid = grid;
        _coord = coord;
        _inventory = inventory;
        _cropToAutoPlant = cropToAutoPlant;

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Visual";
        visual.transform.SetParent(transform, worldPositionStays: false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localScale = new Vector3(1f, 0.6f, 1f);

        Destroy(visual.GetComponent<Collider>());

        var renderer = visual.GetComponent<Renderer>();
        renderer.material = RendererTint.SharedUrpLitMaterial;
        RendererTint.SetColor(renderer, VisualColor);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < Definition.droneActionIntervalSeconds)
        {
            return;
        }

        _timer = 0f;
        TryAutoHarvestOneTile();
        TryAutoPlantOneTile();
    }

    // Drone de Colheita Tier 1: colhe 1 tile por vez, envia ao Armazem Geral
    // (aqui: direto ao inventario do jogador, ja que nao ha etapa de
    // transporte manual modelada).
    private void TryAutoHarvestOneTile()
    {
        foreach (var tile in TilesInRange())
        {
            if (tile.Occupancy != TileOccupancy.Crop || tile.PlantedCrop == null || !tile.PlantedCrop.IsMature)
            {
                continue;
            }

            if (tile.HarvestCrop(out var harvested))
            {
                _inventory.Add(harvested.displayName, harvested.yieldAmount);
            }

            return;
        }
    }

    // Drone de Plantio Tier 1: planta 1 tile por vez.
    private void TryAutoPlantOneTile()
    {
        foreach (var tile in TilesInRange())
        {
            if (tile.Occupancy != TileOccupancy.Empty)
            {
                continue;
            }

            tile.PlantCrop(_cropToAutoPlant);
            return;
        }
    }

    private System.Collections.Generic.IEnumerable<GridTile> TilesInRange()
    {
        foreach (var tile in _grid.Tiles)
        {
            if (Vector2Int.Distance(tile.Coord, _coord) <= Definition.droneRangeInTiles)
            {
                yield return tile;
            }
        }
    }
}
