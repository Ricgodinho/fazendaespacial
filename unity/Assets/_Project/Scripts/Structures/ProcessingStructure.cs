using UnityEngine;

public class ProcessingStructure : MonoBehaviour
{
    public ProcessingStructureDefinition Definition { get; private set; }
    public bool HasOutputReady => StoredOutput > 0;
    public int StoredInput { get; private set; }
    public int StoredOutput { get; private set; }
    public float ProcessElapsedSeconds { get; private set; }

    public void Initialize(
        ProcessingStructureDefinition definition,
        float initialProcessElapsedSeconds = 0f,
        int initialStoredInput = 0,
        int initialStoredOutput = 0)
    {
        Definition = definition;
        ProcessElapsedSeconds = initialProcessElapsedSeconds;
        StoredInput = initialStoredInput;
        StoredOutput = initialStoredOutput;
    }

    public int TryDepositInput(int availableFromInventory)
    {
        int room = Definition.inputAmountRequired - StoredInput;
        int amountToDeposit = Mathf.Clamp(availableFromInventory, 0, room);
        StoredInput += amountToDeposit;
        return amountToDeposit;
    }

    public int CollectOutput()
    {
        int collected = StoredOutput;
        StoredOutput = 0;
        return collected;
    }

    // Simula, de uma vez, o quanto a estrutura teria processado durante uma
    // ausencia (jogo fechado) - docs/07-prototipo-2-loop-hibrido.md. O tempo
    // recebido aqui ja vem limitado pelo teto global de 48h (GameBootstrap).
    public void ApplyOfflineElapsed(float elapsedSeconds)
    {
        float remaining = elapsedSeconds;
        while (remaining > 0f && CanProcess())
        {
            float remainingForCycle = Definition.processTimeSeconds - ProcessElapsedSeconds;
            if (remaining < remainingForCycle)
            {
                ProcessElapsedSeconds += remaining;
                return;
            }

            remaining -= remainingForCycle;
            CompleteCycle();
        }
    }

    private bool CanProcess()
    {
        return StoredInput >= Definition.inputAmountRequired && StoredOutput < Definition.outputStorageCapacity;
    }

    private void CompleteCycle()
    {
        StoredInput -= Definition.inputAmountRequired;
        StoredOutput = Mathf.Min(StoredOutput + Definition.outputAmountProduced, Definition.outputStorageCapacity);
        ProcessElapsedSeconds = 0f;
    }

    private void Update()
    {
        if (!CanProcess())
        {
            return;
        }

        ProcessElapsedSeconds += Time.deltaTime;
        if (ProcessElapsedSeconds >= Definition.processTimeSeconds)
        {
            CompleteCycle();
        }
    }
}
