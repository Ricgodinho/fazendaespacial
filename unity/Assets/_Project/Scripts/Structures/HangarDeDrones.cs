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

    // Tier 1 do Plantio so automatiza 1 cultivo por vez (docs/drones/plantio.md,
    // breakpoint de multi-cultivo so no Tier 4) - escolhido pelo jogador, nao
    // fixo em codigo. Sem isso, o drone reocupa qualquer tile vazio (inclusive
    // um que acabou de ser colhido de outro cultivo) sempre com o mesmo tipo.
    public CropDefinition AutoPlantCrop { get; private set; }

    private TileGrid _grid;
    private Vector2Int _coord;
    private PlayerInventory _inventory;

    // "Semente de X" segue a convencao ja usada pelo unico Viveiro que existe
    // hoje (produz "Semente de Trigo Lunar" a partir de Trigo Lunar) - se o
    // jogador trocar o cultivo automatizado para algo sem Viveiro proprio
    // ainda (ex: Cedro Estelar), o drone simplesmente nunca encontra semente
    // e fica parado, sem quebrar nada.
    private string RequiredSeedResourceName => $"Semente de {AutoPlantCrop.displayName}";

    // Identifica o Hangar nos logs quando houver mais de um no mapa.
    private string HangarLabel => $"({_coord.x},{_coord.y})";

    private readonly List<TransporteRoute> _transporteRoutes = new();

    private float _plantioTimer;
    private float _colheitaTimer;
    private bool _plantioDroneBusy;
    private bool _colheitaDroneBusy;

    // Cursores de varredura round-robin - cada busca continua de onde a
    // anterior parou, em vez de sempre recomecar do indice 0. Sem isso,
    // um cultivo que amadurece mais rapido (ex: Trigo Lunar) sempre "ganha"
    // a varredura e cultivos mais lentos (ex: Cedro Estelar) nunca chegam
    // a ser escolhidos depois da primeira vez - bug de fome (starvation)
    // encontrado em playtest.
    private int _colheitaScanCursor;
    private int _plantioScanCursor;

    public void Initialize(
        HangarDeDronesDefinition definition,
        TileGrid grid,
        Vector2Int coord,
        PlayerInventory inventory,
        CropDefinition cropToAutoPlant,
        IEnumerable<ProcessingStructureDefinition> transporteTargets)
    {
        Definition = definition;
        _grid = grid;
        _coord = coord;
        _inventory = inventory;
        AutoPlantCrop = cropToAutoPlant;

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
    public void SetAutoPlantCrop(CropDefinition crop) => AutoPlantCrop = crop;

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
        var target = FindFirstInRange(
            t => t.Occupancy == TileOccupancy.Crop && t.PlantedCrop != null && t.PlantedCrop.IsMature,
            ref _colheitaScanCursor);
        if (target == null)
        {
            GameLog.Log("Colheita", "NoTarget", $"hangar={HangarLabel} raio={Definition.droneRangeInTiles}");
            return;
        }

        _colheitaTimer = 0f;
        _colheitaDroneBusy = true;
        GameLog.Log("Colheita", "TripStart", $"hangar={HangarLabel} tile=({target.Coord.x},{target.Coord.y}) cultivo={target.PlantedCrop.Definition.displayName}");

        Vector3 hangarPoint = transform.position + Vector3.up * DroneFlightHeight;
        Vector3 tilePoint = target.transform.position + Vector3.up * DroneFlightHeight;

        var legs = new List<(Vector3, Action<DroneVisual>)>
        {
            (tilePoint, drone =>
            {
                if (target.HarvestCrop(out var harvested))
                {
                    _inventory.Add(harvested.displayName, harvested.yieldAmount);
                    GameLog.Log("Colheita", "Harvested", $"hangar={HangarLabel} tile=({target.Coord.x},{target.Coord.y}) cultivo={harvested.displayName} quantidade={harvested.yieldAmount}");
                }
                else
                {
                    GameLog.Log("Colheita", "HarvestFailed", $"hangar={HangarLabel} tile=({target.Coord.x},{target.Coord.y})");
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
            GameLog.Log("Colheita", "TripEnd", $"hangar={HangarLabel}");
        }));

        DroneVisual.Fly(hangarPoint, ColheitaDroneColor, startCarrying: false, legs);
    }

    private void TryStartPlantTrip()
    {
        // Plantio Tier 1 "usa sementes do Armazem Geral" (docs/drones/plantio.md)
        // - sem semente do Viveiro, o drone nao consegue plantar.
        if (_inventory.GetAmount(RequiredSeedResourceName) < 1)
        {
            GameLog.Log("Plantio", "NoSeed", $"hangar={HangarLabel} cultivo={AutoPlantCrop.displayName} semente={RequiredSeedResourceName}");
            return;
        }

        var target = FindFirstInRange(t => t.Occupancy == TileOccupancy.Empty, ref _plantioScanCursor);
        if (target == null)
        {
            GameLog.Log("Plantio", "NoEmptyTile", $"hangar={HangarLabel} raio={Definition.droneRangeInTiles}");
            return;
        }

        _plantioTimer = 0f;
        _plantioDroneBusy = true;
        GameLog.Log("Plantio", "TripStart", $"hangar={HangarLabel} cultivo={AutoPlantCrop.displayName} tile=({target.Coord.x},{target.Coord.y})");

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
                _inventory.TryRemove(RequiredSeedResourceName, 1);
                drone.SetCarrying(true);
                GameLog.Log("Plantio", "SeedPickedUp", $"hangar={HangarLabel} semente={RequiredSeedResourceName}");
            }));
        }
        else
        {
            // Sem Armazem, a retirada e instantanea no Hangar.
            _inventory.TryRemove(RequiredSeedResourceName, 1);
        }

        legs.Add((tilePoint, drone =>
        {
            bool planted = target.PlantCrop(AutoPlantCrop);
            drone.SetCarrying(false);
            GameLog.Log("Plantio", planted ? "Planted" : "PlantFailed", $"hangar={HangarLabel} tile=({target.Coord.x},{target.Coord.y}) cultivo={AutoPlantCrop.displayName}");
        }));

        legs.Add((hangarPoint, _ =>
        {
            _plantioDroneBusy = false;
            GameLog.Log("Plantio", "TripEnd", $"hangar={HangarLabel}");
        }));

        DroneVisual.Fly(hangarPoint, PlantioDroneColor, startCarrying: !hasArmazem, legs);
    }

    // Acionado manualmente (botao "Entregar/Coletar agora") ou
    // automaticamente (rota com AutomaticEnabled ligado) - mesma logica,
    // so muda quem aciona.
    public void TryRunRoute(TransporteRoute route)
    {
        if (route.Busy)
        {
            GameLog.Log("Transporte", "RouteBusy", $"hangar={HangarLabel} destino={route.TargetDefinition.displayName} direcao={route.Direction}");
            return;
        }

        var target = ProcessingStructure.Instances.FirstOrDefault(s => s.Definition == route.TargetDefinition);
        if (target == null)
        {
            GameLog.Log("Transporte", "TargetNotBuilt", $"hangar={HangarLabel} destino={route.TargetDefinition.displayName} direcao={route.Direction}");
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
            GameLog.Log("Transporte", "Delivery_NoStock", $"hangar={HangarLabel} recurso={resourceName} disponivel={available}");
            return;
        }

        route.Busy = true;
        GameLog.Log("Transporte", "Delivery_Start", $"hangar={HangarLabel} recurso={resourceName} quantidade={toCarry} destino={destination.Definition.displayName}");

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
            GameLog.Log("Transporte", "Delivery_Delivered", $"hangar={HangarLabel} recurso={resourceName} entregue={deposited} sobra_devolvida={leftover}");
        }));

        legs.Add((hangarPoint, _ =>
        {
            route.Busy = false;
            GameLog.Log("Transporte", "Delivery_TripEnd", $"hangar={HangarLabel}");
        }));

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
            GameLog.Log("Transporte", "Collection_NoOutput", $"hangar={HangarLabel} origem={source.Definition.displayName}");
            return;
        }

        int roomInInventory = _inventory.Capacity.HasValue
            ? Mathf.Max(0, _inventory.Capacity.Value - _inventory.Total)
            : int.MaxValue;

        int toCarry = Mathf.Min(source.StoredOutput, Mathf.Min(Definition.transporteCapacidadePorViagem, roomInInventory));
        if (toCarry <= 0)
        {
            GameLog.Log("Transporte", "Collection_NoRoom", $"hangar={HangarLabel} origem={source.Definition.displayName} output_pronto={source.StoredOutput} espaco_inventario={roomInInventory}");
            return;
        }

        route.Busy = true;
        string resourceName = source.Definition.outputResourceName;
        bool hasArmazem = ArmazemGeral.Instances.Count > 0;
        int collectedAmount = 0;
        GameLog.Log("Transporte", "Collection_Start", $"hangar={HangarLabel} origem={source.Definition.displayName} recurso={resourceName} quantidade={toCarry}");

        Vector3 hangarPoint = transform.position + Vector3.up * DroneFlightHeight;
        var legs = new List<(Vector3, Action<DroneVisual>)>
        {
            (source.transform.position + Vector3.up * DroneFlightHeight, drone =>
            {
                // Sai da estrutura assim que o drone pega - isso e so a
                // retirada. Ainda nao entra no inventario/Armazem aqui.
                collectedAmount = source.CollectOutput(toCarry);
                drone.SetCarrying(true);
                GameLog.Log("Transporte", "Collection_PickedUp", $"hangar={HangarLabel} recurso={resourceName} quantidade={collectedAmount}");
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
            GameLog.Log("Transporte", "Collection_TripEnd", $"hangar={HangarLabel} recurso={resourceName} quantidade={collectedAmount}");
        }));

        DroneVisual.Fly(hangarPoint, TransporteDroneColor, startCarrying: false, legs);
    }

    // Round-robin: comeca a varredura a partir de "cursor" (nao sempre do
    // indice 0), e avanca o cursor para logo apos o tile encontrado - da
    // uma chance justa a todos os tiles ao longo do tempo.
    private GridTile FindFirstInRange(Func<GridTile, bool> predicate, ref int cursor)
    {
        var tiles = _grid.Tiles;
        int count = tiles.Count;
        if (count == 0)
        {
            return null;
        }

        for (int offset = 0; offset < count; offset++)
        {
            int index = (cursor + offset) % count;
            var tile = tiles[index];
            if (Vector2Int.Distance(tile.Coord, _coord) <= Definition.droneRangeInTiles && predicate(tile))
            {
                cursor = (index + 1) % count;
                return tile;
            }
        }

        return null;
    }
}
