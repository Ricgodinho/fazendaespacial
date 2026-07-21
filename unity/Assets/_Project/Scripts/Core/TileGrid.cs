using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public float TileSize { get; private set; }
    public IReadOnlyList<GridTile> Tiles => _tiles;

    private readonly List<GridTile> _tiles = new();
    private readonly Dictionary<Vector2Int, GridTile> _tilesByCoord = new();

    public static TileGrid Create(int width, int height, float tileSize)
    {
        var gridObject = new GameObject("TileGrid");
        var grid = gridObject.AddComponent<TileGrid>();
        grid.Width = width;
        grid.Height = height;
        grid.TileSize = tileSize;
        grid.BuildTiles();
        return grid;
    }

    public GridTile GetTile(int x, int z)
    {
        _tilesByCoord.TryGetValue(new Vector2Int(x, z), out var tile);
        return tile;
    }

    private void BuildTiles()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                var tileObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tileObject.name = $"Tile_{x}_{z}";
                tileObject.transform.SetParent(transform, worldPositionStays: false);
                tileObject.transform.localPosition = new Vector3(x * TileSize, -0.1f, z * TileSize);
                tileObject.transform.localScale = new Vector3(TileSize * 0.95f, 0.2f, TileSize * 0.95f);

                var renderer = tileObject.GetComponent<Renderer>();
                renderer.material = RendererTint.SharedUrpLitMaterial;

                var tile = tileObject.AddComponent<GridTile>();
                var coord = new Vector2Int(x, z);
                tile.Initialize(coord, renderer);

                _tiles.Add(tile);
                _tilesByCoord[coord] = tile;
            }
        }
    }
}
