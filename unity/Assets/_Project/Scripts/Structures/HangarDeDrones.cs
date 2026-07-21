using System;
using System.Collections.Generic;
using UnityEngine;

// So implementa a funcao central do Nivel 1: hospeda 1 drone de Colheita e
// 1 drone de Plantio Tier 1 (docs/drones/colheita.md, docs/drones/plantio.md).
//
// Simplificacoes assumidas nesta implementacao (fora de escopo por ora):
// - Sem economia de aquisicao de drones (achado/fabricado, ver
//   docs/drones/00-indice.md) - o Hangar ja nasce com os 2 drones Tier 1.
// - Drone de Plantio "usa sementes do Armazem Geral" no design, mas o
//   Viveiro (que produz sementes) ainda nao existe - o drone parte do
//   proprio Hangar direto para o tile, sem escala intermediaria de
//   retirada de semente.
// - Sem durabilidade/desgaste de drone (docs/drones/00-indice.md).
//
// O visual de voo (DroneVisual) e puramente cosmetico, por cima de uma
// logica de "1 acao por vez por categoria" com pacing por intervalo.
public class HangarDeDrones : PlacedStructure
{
    public static readonly List<HangarDeDrones> Instances = new();

    private static readonly Color VisualColor = new Color(0.35f, 0.4f, 0.45f);
    private static readonly Color PlantioDroneColor = new Color(0.3f, 0.8f, 0.3f);
    private static readonly Color ColheitaDroneColor = new Color(0.9f, 0.6f, 0.15f);
    private const float DroneFlightHeight = 1.2f;

    public HangarDeDronesDefinition Definition { get; private set; }
    public bool PlantioEnabled { get; private set; } = true;
    public bool ColheitaEnabled { get; private set; } = true;

    private TileGrid _grid;
    private Vector2Int _coord;
    private PlayerInventory _inventory;
    private CropDefinition _cropToAutoPlant;

    private float _plantioTimer;
    private float _colheitaTimer;
    private bool _plantioDroneBusy;
    private bool _colheitaDroneBusy;

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

    private void OnEnable() => Instances.Add(this);
    private void OnDisable() => Instances.Remove(this);

    public void SetPlantioEnabled(bool value) => PlantioEnabled = value;
    public void SetColheitaEnabled(bool value) => ColheitaEnabled = value;

    private void Update()
    {
        _plantioTimer += Time.deltaTime;
        _colheitaTimer += Time.deltaTime;

        if (ColheitaEnabled && !_colheitaDroneBusy && _colheitaTimer >= Definition.droneActionIntervalSeconds)
        {
            TryStartHarvestTrip();
        }

        if (PlantioEnabled && !_plantioDroneBusy && _plantioTimer >= Definition.droneActionIntervalSeconds)
        {
            TryStartPlantTrip();
        }
    }

    private void TryStartHarvestTrip()
    {
        var target = FindFirstInRange(t => t.Occupancy == TileOccupancy.Crop && t.PlantedCrop != null && t.PlantedCrop.IsMature);
        if (target == null)
        {
            return;
        }

        _colheitaTimer = 0f;
        _colheitaDroneBusy = true;

        Vector3 hangarPoint = transform.position + Vector3.up * DroneFlightHeight;
        Vector3 tilePoint = target.transform.position + Vector3.up * DroneFlightHeight;

        var legs = new List<(Vector3, Action)>
        {
            (tilePoint, () =>
            {
                if (target.HarvestCrop(out var harvested))
                {
                    _inventory.Add(harvested.displayName, harvested.yieldAmount);
                }
            })
        };

        // Colheita Tier 1-4 sempre entrega no Armazem Geral (docs/drones/colheita.md).
        if (ArmazemGeral.Instances.Count > 0)
        {
            legs.Add((ArmazemGeral.Instances[0].transform.position + Vector3.up * DroneFlightHeight, null));
        }

        legs.Add((hangarPoint, () => _colheitaDroneBusy = false));

        DroneVisual.Fly(hangarPoint, ColheitaDroneColor, legs);
    }

    private void TryStartPlantTrip()
    {
        var target = FindFirstInRange(t => t.Occupancy == TileOccupancy.Empty);
        if (target == null)
        {
            return;
        }

        _plantioTimer = 0f;
        _plantioDroneBusy = true;

        Vector3 hangarPoint = transform.position + Vector3.up * DroneFlightHeight;
        Vector3 tilePoint = target.transform.position + Vector3.up * DroneFlightHeight;

        var legs = new List<(Vector3, Action)>
        {
            (tilePoint, () => target.PlantCrop(_cropToAutoPlant)),
            (hangarPoint, () => _plantioDroneBusy = false)
        };

        DroneVisual.Fly(hangarPoint, PlantioDroneColor, legs);
    }

    private GridTile FindFirstInRange(Func<GridTile, bool> predicate)
    {
        foreach (var tile in _grid.Tiles)
        {
            if (Vector2Int.Distance(tile.Coord, _coord) <= Definition.droneRangeInTiles && predicate(tile))
            {
                return tile;
            }
        }

        return null;
    }
}
