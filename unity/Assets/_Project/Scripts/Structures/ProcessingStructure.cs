using UnityEngine;

public class ProcessingStructure : MonoBehaviour
{
    public ProcessingStructureDefinition Definition { get; private set; }
    public bool HasOutputReady => _storedOutput > 0;

    private int _storedInput;
    private int _storedOutput;
    private float _processStartedAt = -1f;

    public void Initialize(ProcessingStructureDefinition definition)
    {
        Definition = definition;
    }

    public int TryDepositInput(int availableFromInventory)
    {
        int room = Definition.inputAmountRequired - _storedInput;
        int amountToDeposit = Mathf.Clamp(availableFromInventory, 0, room);
        _storedInput += amountToDeposit;
        return amountToDeposit;
    }

    public int CollectOutput()
    {
        int collected = _storedOutput;
        _storedOutput = 0;
        return collected;
    }

    private void Update()
    {
        bool hasEnoughInput = _storedInput >= Definition.inputAmountRequired;
        bool hasRoomForOutput = _storedOutput < Definition.outputStorageCapacity;

        if (_processStartedAt < 0f)
        {
            if (hasEnoughInput && hasRoomForOutput)
            {
                _processStartedAt = Time.time;
            }
            return;
        }

        if (Time.time - _processStartedAt >= Definition.processTimeSeconds)
        {
            _storedInput -= Definition.inputAmountRequired;
            _storedOutput = Mathf.Min(_storedOutput + Definition.outputAmountProduced, Definition.outputStorageCapacity);
            _processStartedAt = -1f;
        }
    }
}
