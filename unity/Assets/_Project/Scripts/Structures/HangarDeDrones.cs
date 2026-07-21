using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// So implementa a funcao central do Nivel 1: hospeda 1 drone de Colheita,
// 1 de Plantio e 1 de Transporte Tier 1 (docs/drones/colheita.md,
// docs/drones/plantio.md, docs/drones/transporte.md).
//
// Simplificacoes assumidas nesta implementacao (fora de escopo por ora):
// - Sem economia de aquisicao de drones (achado/fabricado, ver
//   docs/drones/00-indice.md) - o Hangar ja nasce com os 3 drones Tier 1.
// - Sem durabilidade/desgaste de drone (docs/drones/00-indice.md).
// - Transporte: Tier 1 e manual por design, mas o modo "Automatico" por
//   rota ja adianta o comportamento de Tier 2 (repete sozinho), deixado
//   configuravel por rota em vez de esperar um sistema de tier completo.
//
// O visual de voo (DroneVisual) e puramente cosmetico, por cima de uma
// logica de "1 acao por vez por categoria" com pacing por intervalo.
public class HangarDeDrones : PlacedStructure
{
    public static readonly List<HangarDeDrones> Instances = new();

    private static readonly Color VisualColor = new Color(0.35f, 0.4f, 0.45f);
    private static readonly Color PlantioDroneColor = new Color(0.3f, 0.8f, 0.3f);
    private static readonly Color ColheitaDroneColor = new Color(0.9f, 0.6f, 0.15f);
    private static readonly Color TransporteDroneColor = new Color(0.7f, 0.7f, 0.75f);
    private const float DroneFlightHeight = 1.2f;

    public HangarDeDronesDefinition Definition { get; private set; }
    public bool PlantioEnabled { get; private set; } = true;
    public bool ColheitaEnabled { get; private set; } = true;
    public IReadOnlyList<TransporteRoute> TransporteRoutes => _transporteRoutes;

    private TileGrid _grid;
    private Vector2Int _coord;
    private PlayerInventory _inventory;
    private CropDefinition _cropToAutoPlant;
    private string _seedResourceName;

    private readonly List<TransporteRoute> _transporteRoutes = new();

    private float _plantioTimer;
    private float _colheitaTimer;
    private bool _plantioDroneBusy;
    private bool _colheitaDroneBusy;

    public void Initialize(
        HangarDeDronesDefinition definition,
        TileGrid grid,
        Vector2Int coord,
        PlayerInventory inventory,
        CropDefinition cropToAutoPlant,
        ProcessingStructureDefinition viveiroDefinition,
        IEnumerable<ProcessingStructureDefinition> transporteTargets)
    {
        Definition = definition;
        _grid = grid;
        _coord = coord;
        _inventory = inventory;
        _cropToAutoPlant = cropToAutoPlant;
        _seedResourceName = viveiroDefinition.outputResourceName;

        // Uma rota de entrega + uma de coleta por estrutura de processamento
        // existente (Viveiro incluso, ja que reaproveita a mesma classe).
        foreach (var target in transporteTargets)
        {
            _transporteRoutes.Add(new TransporteRoute { TargetDefinition = target, Direction = TransporteDirection.Delivery });
            _transporteRoutes.Add(new TransporteRoute { TargetDefinition = target, Direction = TransporteDirection.Collection });
        }

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

        foreach (var route in _transporteRoutes)
        {
            if (!route.AutomaticEnabled || route.Busy)
            {
                continue;
            }

            route.Timer += Time.deltaTime;
            if (route.Timer >= Definition.droneActionIntervalSeconds)
            {
                route.Timer = 0f;
                TryRunRoute(route);
            }
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

        var legs = new List<(Vector3, Action<DroneVisual>)>
        {
            (tilePoint, drone =>
            {
                if (target.HarvestCrop(out var harvested))
                {
                    _inventory.Add(harvested.displayName, harvested.yieldAmount);
                }

                drone.SetCarrying(true);
            })
        };

        // Colheita Tier 1-4 sempre entrega no Armazem Geral (docs/drones/colheita.md).
        if (ArmazemGeral.Instances.Count > 0)
        {
            legs.Add((ArmazemGeral.Instances[0].transform.position + Vector3.up * DroneFlightHeight, drone => drone.SetCarrying(false)));
        }

        legs.Add((hangarPoint, drone =>
        {
            drone.SetCarrying(false);
            _colheitaDroneBusy = false;
        }));

        DroneVisual.Fly(hangarPoint, ColheitaDroneColor, startCarrying: false, legs);
    }

    private void TryStartPlantTrip()
    {
        // Plantio Tier 1 "usa sementes do Armazem Geral" (docs/drones/plantio.md)
        // - sem semente do Viveiro, o drone nao consegue plantar.
        if (_inventory.GetAmount(_seedResourceName) < 1)
        {
            return;
        }

        var target = FindFirstInRange(t => t.Occupancy == TileOccupancy.Empty);
        if (target == null)
        {
            return;
        }

        _plantioTimer = 0f;
        _plantioDroneBusy = true;

        Vector3 hangarPoint = transform.position + Vector3.up * DroneFlightHeight;
        Vector3 tilePoint = target.transform.position + Vector3.up * DroneFlightHeight;
        bool hasArmazem = ArmazemGeral.Instances.Count > 0;

        var legs = new List<(Vector3, Action<DroneVisual>)>();

        if (hasArmazem)
        {
            // Sai vazio do Hangar, so pega a semente ao chegar no Armazem -
            // mesmo padrao do Drone de Transporte.
            legs.Add((ArmazemGeral.Instances[0].transform.position + Vector3.up * DroneFlightHeight, drone =>
            {
                _inventory.TryRemove(_seedResourceName, 1);
                drone.SetCarrying(true);
            }));
        }
        else
        {
            // Sem Armazem, a retirada e instantanea no Hangar.
            _inventory.TryRemove(_seedResourceName, 1);
        }

        legs.Add((tilePoint, drone =>
        {
            target.PlantCrop(_cropToAutoPlant);
            drone.SetCarrying(false);
        }));

        legs.Add((hangarPoint, _ => _plantioDroneBusy = false));

        DroneVisual.Fly(hangarPoint, PlantioDroneColor, startCarrying: !hasArmazem, legs);
    }

    // Acionado manualmente (botao "Entregar/Coletar agora") ou
    // automaticamente (rota com AutomaticEnabled ligado) - mesma logica,
    // so muda quem aciona.
    public void TryRunRoute(TransporteRoute route)
    {
        if (route.Busy)
        {
            return;
        }

        var target = ProcessingStructure.Instances.FirstOrDefault(s => s.Definition == route.TargetDefinition);
        if (target == null)
        {
            return;
        }

        if (route.Direction == TransporteDirection.Delivery)
        {
            TryRunDeliveryRoute(route, target);
        }
        else
        {
            TryRunCollectionRoute(route, target);
        }
    }

    private void TryRunDeliveryRoute(TransporteRoute route, ProcessingStructure destination)
    {
        string resourceName = destination.Definition.inputCropDefinition.displayName;
        int available = _inventory.GetAmount(resourceName);
        int toCarry = Mathf.Min(available, Definition.transporteCapacidadePorViagem);
        if (toCarry <= 0)
        {
            return;
        }

        route.Busy = true;

        Vector3 hangarPoint = transform.position + Vector3.up * DroneFlightHeight;
        var legs = new List<(Vector3, Action<DroneVisual>)>();
        bool hasArmazem = ArmazemGeral.Instances.Count > 0;

        if (hasArmazem)
        {
            // So sai do inventario quando o drone de fato chega no Armazem
            // pra pegar - nao no momento em que a viagem e decidida.
            legs.Add((ArmazemGeral.Instances[0].transform.position + Vector3.up * DroneFlightHeight, drone =>
            {
                _inventory.TryRemove(resourceName, toCarry);
                drone.SetCarrying(true);
            }));
        }
        else
        {
            // Sem Armazem, a retirada e instantanea no Hangar (nao ha ponto
            // intermediario de coleta pra visitar).
            _inventory.TryRemove(resourceName, toCarry);
        }

        legs.Add((destination.transform.position + Vector3.up * DroneFlightHeight, drone =>
        {
            int deposited = destination.TryDepositInput(toCarry);
            int leftover = toCarry - deposited;
            if (leftover > 0)
            {
                // Nao coube tudo no destino - devolve o resto ao inventario, nada se perde.
                _inventory.Add(resourceName, leftover);
            }

            drone.SetCarrying(false);
        }));

        legs.Add((hangarPoint, _ => route.Busy = false));

        // Sem Armazem, o "insumo" ja sai do Hangar (nao ha ponto intermediario de coleta).
        DroneVisual.Fly(hangarPoint, TransporteDroneColor, startCarrying: !hasArmazem, legs);
    }

    // Traz o produto pronto de uma estrutura (Viveiro ou Processamento) de
    // volta para o Armazem Geral/inventario - direcao oposta da entrega,
    // fecha o ciclo sem precisar clicar manualmente na estrutura.
    private void TryRunCollectionRoute(TransporteRoute route, ProcessingStructure source)
    {
        if (!source.HasOutputReady)
        {
            return;
        }

        int roomInInventory = _inventory.Capacity.HasValue
            ? Mathf.Max(0, _inventory.Capacity.Value - _inventory.Total)
            : int.MaxValue;

        int toCarry = Mathf.Min(source.StoredOutput, Mathf.Min(Definition.transporteCapacidadePorViagem, roomInInventory));
        if (toCarry <= 0)
        {
            return;
        }

        route.Busy = true;
        string resourceName = source.Definition.outputResourceName;
        bool hasArmazem = ArmazemGeral.Instances.Count > 0;
        int collectedAmount = 0;

        Vector3 hangarPoint = transform.position + Vector3.up * DroneFlightHeight;
        var legs = new List<(Vector3, Action<DroneVisual>)>
        {
            (source.transform.position + Vector3.up * DroneFlightHeight, drone =>
            {
                // Sai da estrutura assim que o drone pega - isso e so a
                // retirada. Ainda nao entra no inventario/Armazem aqui.
                collectedAmount = source.CollectOutput(toCarry);
                drone.SetCarrying(true);
            })
        };

        if (hasArmazem)
        {
            legs.Add((ArmazemGeral.Instances[0].transform.position + Vector3.up * DroneFlightHeight, drone =>
            {
                // So agora, chegando no Armazem, o que foi coletado passa a
                // contar no inventario do jogador.
                if (collectedAmount > 0)
                {
                    _inventory.Add(resourceName, collectedAmount);
                }
            }));
        }

        legs.Add((hangarPoint, drone =>
        {
            if (!hasArmazem && collectedAmount > 0)
            {
                // Sem Armazem, a entrega e instantanea de volta no Hangar.
                _inventory.Add(resourceName, collectedAmount);
            }

            drone.SetCarrying(false);
            route.Busy = false;
        }));

        DroneVisual.Fly(hangarPoint, TransporteDroneColor, startCarrying: false, legs);
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
