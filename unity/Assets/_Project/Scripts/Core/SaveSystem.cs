using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class TileSaveData
{
    public int x;
    public int z;
    public int occupancy; // espelha TileOccupancy: 0 Empty, 1 Crop, 2 Structure
    public string structureKind; // so relevante se occupancy == Structure: "processing", "armazem" ou "hangar"
    public string definitionName; // CropDefinition.displayName (Crop) ou ProcessingStructureDefinition.displayName (structureKind == "processing")
    public float progressSeconds; // AccumulatedSeconds (cultivo) ou ProcessElapsedSeconds (estrutura de processamento)
    public int storedInput;
    public int storedOutput;
}

[Serializable]
public class ResourceSaveData
{
    public string name;
    public int amount;
}

[Serializable]
public class GameSaveData
{
    public List<TileSaveData> tiles = new();
    public List<ResourceSaveData> inventory = new();
    public double savedAtUnixSeconds;
}

// Save local em JSON (MVP single-player). A fonte do horario usado para
// calcular ausencia (CurrentUnixSeconds) e o unico ponto que precisaria
// trocar para um relogio de servidor no multiplayer (docs/07) - o calculo
// de producao offline em si (ProcessingStructure/CropInstance) nao muda.
//
// Cultivos e estruturas de processamento sao identificados no save pelo
// proprio displayName (ja e a chave usada no inventario em todo o resto
// do jogo) - evita manter um segundo identificador em paralelo.
public class SaveSystem
{
    private const string StructureKindProcessing = "processing";
    private const string StructureKindArmazem = "armazem";
    private const string StructureKindHangar = "hangar";
    private const string StructureKindMina = "mina";

    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private readonly string _filePath;

    public SaveSystem(string filePath)
    {
        _filePath = filePath;
    }

    public static double CurrentUnixSeconds()
    {
        return (DateTime.UtcNow - UnixEpoch).TotalSeconds;
    }

    public void Save(TileGrid grid, PlayerInventory inventory)
    {
        var data = new GameSaveData
        {
            savedAtUnixSeconds = CurrentUnixSeconds()
        };

        foreach (var tile in grid.Tiles)
        {
            if (tile.Occupancy == TileOccupancy.Crop && tile.PlantedCrop != null)
            {
                data.tiles.Add(new TileSaveData
                {
                    x = tile.Coord.x,
                    z = tile.Coord.y,
                    occupancy = (int)TileOccupancy.Crop,
                    definitionName = tile.PlantedCrop.Definition.displayName,
                    progressSeconds = tile.PlantedCrop.AccumulatedSeconds
                });
            }
            else if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure is ProcessingStructure processing)
            {
                data.tiles.Add(new TileSaveData
                {
                    x = tile.Coord.x,
                    z = tile.Coord.y,
                    occupancy = (int)TileOccupancy.Structure,
                    structureKind = StructureKindProcessing,
                    definitionName = processing.Definition.displayName,
                    progressSeconds = processing.ProcessElapsedSeconds,
                    storedInput = processing.StoredInput,
                    storedOutput = processing.StoredOutput
                });
            }
            else if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure is ArmazemGeral)
            {
                data.tiles.Add(new TileSaveData
                {
                    x = tile.Coord.x,
                    z = tile.Coord.y,
                    occupancy = (int)TileOccupancy.Structure,
                    structureKind = StructureKindArmazem
                });
            }
            else if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure is HangarDeDrones)
            {
                data.tiles.Add(new TileSaveData
                {
                    x = tile.Coord.x,
                    z = tile.Coord.y,
                    occupancy = (int)TileOccupancy.Structure,
                    structureKind = StructureKindHangar
                });
            }
            else if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure is MinaDePedra mina)
            {
                data.tiles.Add(new TileSaveData
                {
                    x = tile.Coord.x,
                    z = tile.Coord.y,
                    occupancy = (int)TileOccupancy.Structure,
                    structureKind = StructureKindMina,
                    progressSeconds = mina.ProcessElapsedSeconds,
                    storedOutput = mina.StoredOutput
                });
            }
        }

        foreach (var pair in inventory.All)
        {
            data.inventory.Add(new ResourceSaveData { name = pair.Key, amount = pair.Value });
        }

        File.WriteAllText(_filePath, JsonUtility.ToJson(data, prettyPrint: true));
    }

    public GameSaveData Load()
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        return JsonUtility.FromJson<GameSaveData>(File.ReadAllText(_filePath));
    }

    public static bool IsProcessingStructure(TileSaveData tileData) => tileData.structureKind == StructureKindProcessing;
    public static bool IsArmazem(TileSaveData tileData) => tileData.structureKind == StructureKindArmazem;
    public static bool IsHangar(TileSaveData tileData) => tileData.structureKind == StructureKindHangar;
    public static bool IsMina(TileSaveData tileData) => tileData.structureKind == StructureKindMina;
}
