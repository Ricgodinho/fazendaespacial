using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public float TileSize { get; private set; }

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

    private void BuildTiles()
    {
        var shader = Shader.Find("Universal Render Pipeline/Lit");

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
                renderer.material = new Material(shader);

                var tile = tileObject.AddComponent<GridTile>();
                tile.Initialize(new Vector2Int(x, z), renderer);
            }
        }
    }
}
