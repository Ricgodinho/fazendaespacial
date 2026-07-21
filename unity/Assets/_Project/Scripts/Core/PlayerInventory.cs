using System.Collections.Generic;

public class PlayerInventory
{
    private readonly Dictionary<string, int> _resources = new();

    public IReadOnlyDictionary<string, int> All => _resources;

    public void Add(string resourceName, int amount)
    {
        _resources.TryGetValue(resourceName, out int current);
        _resources[resourceName] = current + amount;
    }

    public bool TryRemove(string resourceName, int amount)
    {
        if (!_resources.TryGetValue(resourceName, out int current) || current < amount)
        {
            return false;
        }

        _resources[resourceName] = current - amount;
        return true;
    }

    public int GetAmount(string resourceName)
    {
        return _resources.GetValueOrDefault(resourceName, 0);
    }
}
