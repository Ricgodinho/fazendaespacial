using UnityEngine;

public static class GameBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        var cropDefinition = Resources.Load<CropDefinition>("TrigoLunar");
        var structureDefinition = Resources.Load<ProcessingStructureDefinition>("ProcessamentoDeTrigo");

        if (cropDefinition == null || structureDefinition == null)
        {
            Debug.LogError("GameBootstrap: definicoes de cultivo/estrutura nao encontradas em Resources.");
            return;
        }

        TileGrid.Create(width: 8, height: 8, tileSize: 1.2f);
        PositionCamera();

        var inventory = new PlayerInventory();

        var toolSelector = new GameObject("ToolSelector").AddComponent<ToolSelector>();

        var clickController = new GameObject("ClickController").AddComponent<ClickController>();
        clickController.Initialize(inventory, toolSelector, cropDefinition, structureDefinition);

        var hud = new GameObject("PrototypeHud").AddComponent<PrototypeHud>();
        hud.Initialize(inventory, toolSelector, cropDefinition, structureDefinition);
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
