using UnityEngine;

// Salva periodicamente (protecao contra crash/queda de energia) e ao
// fechar o jogo - docs/07-prototipo-2-loop-hibrido.md.
public class SaveRunner : MonoBehaviour
{
    private SaveSystem _saveSystem;
    private TileGrid _grid;
    private PlayerInventory _inventory;

    public void Initialize(SaveSystem saveSystem, TileGrid grid, PlayerInventory inventory, float autoSaveIntervalSeconds)
    {
        _saveSystem = saveSystem;
        _grid = grid;
        _inventory = inventory;
        InvokeRepeating(nameof(Save), autoSaveIntervalSeconds, autoSaveIntervalSeconds);
    }

    private void Save()
    {
        _saveSystem.Save(_grid, _inventory);
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
        }
    }
}
