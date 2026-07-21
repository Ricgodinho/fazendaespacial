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
    public float progressSeconds; // AccumulatedSeconds (cultivo) ou ProcessElapsedSeconds (estrutura)
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
public class SaveSystem
{
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
                    progressSeconds = tile.PlantedCrop.AccumulatedSeconds
                });
            }
            else if (tile.Occupancy == TileOccupancy.Structure && tile.BuiltStructure != null)
            {
                data.tiles.Add(new TileSaveData
                {
                    x = tile.Coord.x,
                    z = tile.Coord.y,
                    occupancy = (int)TileOccupancy.Structure,
                    progressSeconds = tile.BuiltStructure.ProcessElapsedSeconds,
                    storedInput = tile.BuiltStructure.StoredInput,
                    storedOutput = tile.BuiltStructure.StoredOutput
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
}
