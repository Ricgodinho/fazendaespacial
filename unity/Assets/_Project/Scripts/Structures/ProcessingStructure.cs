using UnityEngine;

public class ProcessingStructure : MonoBehaviour
{
    private static readonly Color IdleColor = Color.gray;
    private static readonly Color ProcessingColorLow = new Color(0.2f, 0.4f, 0.8f);
    private static readonly Color ProcessingColorHigh = new Color(0.3f, 0.75f, 1f);
    private static readonly Color ReadyColor = new Color(0.9f, 0.75f, 0.1f); // mesmo dourado do cultivo maduro

    private const float RotationDegreesPerSecond = 90f;

    public ProcessingStructureDefinition Definition { get; private set; }
    public bool HasOutputReady => StoredOutput > 0;
    public int StoredInput { get; private set; }
    public int StoredOutput { get; private set; }
    public float ProcessElapsedSeconds { get; private set; }

    private Renderer _visualRenderer;
    private Transform _visualTransform;

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

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Visual";
        visual.transform.SetParent(transform, worldPositionStays: false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localScale = new Vector3(0.8f, 1f, 0.8f);

        // O raycast de clique deve sempre acertar o tile, nao a estrutura em cima dele.
        Destroy(visual.GetComponent<Collider>());

        _visualTransform = visual.transform;
        _visualRenderer = visual.GetComponent<Renderer>();
        _visualRenderer.material = RendererTint.SharedUrpLitMaterial;

        RefreshVisual();
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
                break;
            }

            remaining -= remainingForCycle;
            CompleteCycle();
        }

        RefreshVisual();
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
        if (CanProcess())
        {
            ProcessElapsedSeconds += Time.deltaTime;
            _visualTransform.Rotate(Vector3.up, RotationDegreesPerSecond * Time.deltaTime, Space.World);

            if (ProcessElapsedSeconds >= Definition.processTimeSeconds)
            {
                CompleteCycle();
            }
        }

        RefreshVisual();
    }

    // Prioridade visual: processando (progresso em azul) > pronto pra
    // coletar mas parado (dourado, mesma convencao do cultivo maduro) >
    // parado esperando insumo (cinza).
    private void RefreshVisual()
    {
        if (_visualRenderer == null)
        {
            return;
        }

        if (CanProcess())
        {
            float progress = Mathf.Clamp01(ProcessElapsedSeconds / Definition.processTimeSeconds);
            RendererTint.SetColor(_visualRenderer, Color.Lerp(ProcessingColorLow, ProcessingColorHigh, progress));
            return;
        }

        RendererTint.SetColor(_visualRenderer, HasOutputReady ? ReadyColor : IdleColor);
    }
}
