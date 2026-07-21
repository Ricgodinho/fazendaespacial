using UnityEngine;

public static class GameBootstrap
{
    // Teto global de ausencia considerado para producao offline -
    // docs/07-prototipo-2-loop-hibrido.md. Valor de partida sugerido: 48h.
    private const float GlobalAbsenceCapSeconds = 48f * 3600f;
    private const float AutoSaveIntervalSeconds = 30f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        var cropDefinition = Resources.Load<CropDefinition>("TrigoLunar");
        var structureDefinition = Resources.Load<ProcessingStructureDefinition>("ProcessamentoDeTrigo");
        var armazemDefinition = Resources.Load<ArmazemGeralDefinition>("ArmazemGeral");
        var hangarDefinition = Resources.Load<HangarDeDronesDefinition>("HangarDeDrones");
        var viveiroDefinition = Resources.Load<ProcessingStructureDefinition>("Viveiro");

        if (cropDefinition == null || structureDefinition == null || armazemDefinition == null
            || hangarDefinition == null || viveiroDefinition == null)
        {
            Debug.LogError("GameBootstrap: definicoes nao encontradas em Resources.");
            return;
        }

        var grid = TileGrid.Create(width: 8, height: 8, tileSize: 1.2f);
        PositionCamera();

        var inventory = new PlayerInventory();
        var saveSystem = new SaveSystem(Application.persistentDataPath + "/savegame.json");

        string welcomeBackMessage = LoadIfAvailable(
            saveSystem, grid, inventory, cropDefinition, structureDefinition, armazemDefinition, hangarDefinition, viveiroDefinition);

        var toolSelector = new GameObject("ToolSelector").AddComponent<ToolSelector>();

        var clickController = new GameObject("ClickController").AddComponent<ClickController>();
        clickController.Initialize(
            inventory, toolSelector, grid, cropDefinition, structureDefinition, armazemDefinition, hangarDefinition, viveiroDefinition);

        var hud = new GameObject("PrototypeHud").AddComponent<PrototypeHud>();
        hud.Initialize(inventory, toolSelector, cropDefinition, structureDefinition, armazemDefinition, hangarDefinition, viveiroDefinition);
        if (!string.IsNullOrEmpty(welcomeBackMessage))
        {
            hud.ShowMessage(welcomeBackMessage);
        }

        new GameObject("StructureLabels").AddComponent<StructureLabels>();

        var saveRunner = new GameObject("SaveRunner").AddComponent<SaveRunner>();
        saveRunner.Initialize(saveSystem, grid, inventory, AutoSaveIntervalSeconds);
    }

    private static string LoadIfAvailable(
        SaveSystem saveSystem,
        TileGrid grid,
        PlayerInventory inventory,
        CropDefinition cropDefinition,
        ProcessingStructureDefinition structureDefinition,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition,
        ProcessingStructureDefinition viveiroDefinition)
    {
        var save = saveSystem.Load();
        if (save == null)
        {
            return null;
        }

        double elapsedSinceSave = SaveSystem.CurrentUnixSeconds() - save.savedAtUnixSeconds;
        float cappedOfflineSeconds = Mathf.Clamp((float)elapsedSinceSave, 0f, GlobalAbsenceCapSeconds);

        foreach (var tileData in save.tiles)
        {
            var tile = grid.GetTile(tileData.x, tileData.z);
            if (tile == null)
            {
                continue;
            }

            if (tileData.occupancy == (int)TileOccupancy.Crop)
            {
                tile.PlantCrop(cropDefinition, tileData.progressSeconds);
                tile.PlantedCrop.ApplyOfflineElapsed(cappedOfflineSeconds);
            }
            else if (tileData.occupancy == (int)TileOccupancy.Structure && SaveSystem.IsProcessingStructure(tileData))
            {
                tile.BuildProcessingStructure(structureDefinition, tileData.progressSeconds, tileData.storedInput, tileData.storedOutput);
                ((ProcessingStructure)tile.BuiltStructure).ApplyOfflineElapsed(cappedOfflineSeconds);
            }
            else if (tileData.occupancy == (int)TileOccupancy.Structure && SaveSystem.IsViveiro(tileData))
            {
                tile.BuildProcessingStructure(viveiroDefinition, tileData.progressSeconds, tileData.storedInput, tileData.storedOutput);
                ((ProcessingStructure)tile.BuiltStructure).ApplyOfflineElapsed(cappedOfflineSeconds);
            }
            else if (tileData.occupancy == (int)TileOccupancy.Structure && SaveSystem.IsArmazem(tileData))
            {
                tile.BuildArmazemGeral(armazemDefinition, inventory);
            }
            else if (tileData.occupancy == (int)TileOccupancy.Structure && SaveSystem.IsHangar(tileData))
            {
                // Simplificacao: a automacao do Hangar nao simula catch-up
                // offline (retoma o tick normalmente a partir da reabertura).
                tile.BuildHangarDeDrones(hangarDefinition, grid, inventory, cropDefinition);
            }
        }

        foreach (var resource in save.inventory)
        {
            inventory.Add(resource.name, resource.amount);
        }

        double elapsedHours = elapsedSinceSave / 3600.0;
        bool wasCapped = elapsedSinceSave > GlobalAbsenceCapSeconds;
        return wasCapped
            ? $"Bem-vindo de volta! Ausente por {elapsedHours:F1}h (producao calculada ate o teto de 48h)."
            : $"Bem-vindo de volta! Ausente por {elapsedHours:F1}h.";
    }

    private static void PositionCamera()
    {
        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return;
        }

        mainCamera.transform.position = new Vector3(4f, 8f, -4f);
        mainCamera.transform.rotation = Quaternion.Euler(55f, 0f, 0f);
    }
}
