using System.Collections.Generic;
using System.Linq;

public class PlayerInventory
{
    private readonly Dictionary<string, int> _resources = new();

    public IReadOnlyDictionary<string, int> All => _resources;

    // Nulo = sem limite (antes do Armazem Geral ser construido). Uma vez
    // construido, o Armazem define esse valor (docs/estruturas/planeta-1/armazem-geral.md).
    public int? Capacity { get; set; }

    public int Total => _resources.Values.Sum();

    // Retorna quanto foi de fato adicionado (pode ser menor que o pedido se
    // a capacidade do Armazem Geral estiver cheia) - nada e perdido, quem
    // chama decide o que fazer com a sobra (ex: manter guardado na origem).
    public int Add(string resourceName, int amount)
    {
        int room = Capacity.HasValue ? System.Math.Max(0, Capacity.Value - Total) : amount;
        int actuallyAdded = System.Math.Min(amount, room);
        if (actuallyAdded <= 0)
        {
            return 0;
        }

        _resources.TryGetValue(resourceName, out int current);
        _resources[resourceName] = current + actuallyAdded;
        return actuallyAdded;
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
