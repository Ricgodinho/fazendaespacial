using System.Collections.Generic;
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
        var trigoLunar = Resources.Load<CropDefinition>("TrigoLunar");
        var cedroEstelar = Resources.Load<CropDefinition>("CedroEstelar");
        var bagaEstelar = Resources.Load<CropDefinition>("BagaEstelar");
        var processamentoDeTrigo = Resources.Load<ProcessingStructureDefinition>("ProcessamentoDeTrigo");
        var viveiro = Resources.Load<ProcessingStructureDefinition>("Viveiro");
        var processamentoDeMadeira = Resources.Load<ProcessingStructureDefinition>("ProcessamentoDeMadeira");
        var armazemDefinition = Resources.Load<ArmazemGeralDefinition>("ArmazemGeral");
        var hangarDefinition = Resources.Load<HangarDeDronesDefinition>("HangarDeDrones");

        if (trigoLunar == null || cedroEstelar == null || bagaEstelar == null || processamentoDeTrigo == null || viveiro == null
            || processamentoDeMadeira == null || armazemDefinition == null || hangarDefinition == null)
        {
            Debug.LogError("GameBootstrap: definicoes nao encontradas em Resources.");
            return;
        }

        var crops = new List<CropDefinition> { trigoLunar, cedroEstelar, bagaEstelar };
        var processingStructures = new List<ProcessingStructureDefinition> { processamentoDeTrigo, viveiro, processamentoDeMadeira };

        var cropsByName = new Dictionary<string, CropDefinition>();
        foreach (var crop in crops)
        {
            cropsByName[crop.displayName] = crop;
        }

        var structuresByName = new Dictionary<string, ProcessingStructureDefinition>();
        foreach (var structure in processingStructures)
        {
            structuresByName[structure.displayName] = structure;
        }

        var grid = TileGrid.Create(width: 8, height: 8, tileSize: 1.2f);
        PositionCamera();

        var inventory = new PlayerInventory();
        var saveSystem = new SaveSystem(Application.persistentDataPath + "/savegame.json");

        string welcomeBackMessage = LoadIfAvailable(
            saveSystem, grid, inventory, cropsByName, structuresByName, armazemDefinition, hangarDefinition, processingStructures, trigoLunar);

        var toolSelector = new GameObject("ToolSelector").AddComponent<ToolSelector>();

        var hud = new GameObject("PrototypeHud").AddComponent<PrototypeHud>();

        var clickController = new GameObject("ClickController").AddComponent<ClickController>();
        clickController.Initialize(
            inventory, toolSelector, grid, armazemDefinition, hangarDefinition, processingStructures, trigoLunar, hud);

        hud.Initialize(inventory, toolSelector, crops, processingStructures, armazemDefinition, hangarDefinition);
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
        Dictionary<string, CropDefinition> cropsByName,
        Dictionary<string, ProcessingStructureDefinition> structuresByName,
        ArmazemGeralDefinition armazemDefinition,
        HangarDeDronesDefinition hangarDefinition,
        List<ProcessingStructureDefinition> processingStructures,
        CropDefinition cropForHangarAutoPlant)
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
                // definitionName pode vir nulo de um save de um esquema
                // anterior (schema mudou) - pula o tile em vez de quebrar.
                if (string.IsNullOrEmpty(tileData.definitionName) || !cropsByName.TryGetValue(tileData.definitionName, out var cropDefinition))
                {
                    Debug.LogWarning($"Save antigo/incompativel: cultivo '{tileData.definitionName}' nao reconhecido no tile ({tileData.x},{tileData.z}) - ignorado.");
                    continue;
                }

                tile.PlantCrop(cropDefinition, tileData.progressSeconds);
                tile.PlantedCrop.ApplyOfflineElapsed(cappedOfflineSeconds);
            }
            else if (tileData.occupancy == (int)TileOccupancy.Structure && SaveSystem.IsProcessingStructure(tileData))
            {
                if (string.IsNullOrEmpty(tileData.definitionName) || !structuresByName.TryGetValue(tileData.definitionName, out var structureDefinition))
                {
                    Debug.LogWarning($"Save antigo/incompativel: estrutura '{tileData.definitionName}' nao reconhecida no tile ({tileData.x},{tileData.z}) - ignorada.");
                    continue;
                }

                tile.BuildProcessingStructure(structureDefinition, tileData.progressSeconds, tileData.storedInput, tileData.storedOutput);
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
                tile.BuildHangarDeDrones(hangarDefinition, grid, inventory, cropForHangarAutoPlant, processingStructures);
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
