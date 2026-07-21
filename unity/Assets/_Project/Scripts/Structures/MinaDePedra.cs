using System.Collections.Generic;
using UnityEngine;

// Extrai Pedra Ancestral continuamente (nao precisa de insumo, diferente da
// Estrutura de Processamento) - so o Nivel 1 (docs/estruturas/planeta-1/mina-de-pedra.md).
// A funcao dupla de escavacao/descoberta (achados, itens raros) ainda nao
// esta implementada - fora de escopo desta passada.
public class MinaDePedra : PlacedStructure
{
    public static readonly List<MinaDePedra> Instances = new();

    private static readonly Color ProducingColorLow = new Color(0.2f, 0.4f, 0.8f);
    private static readonly Color ProducingColorHigh = new Color(0.3f, 0.75f, 1f);
    private static readonly Color ReadyColor = new Color(0.9f, 0.75f, 0.1f); // mesmo dourado usado no resto do jogo

    private const float RotationDegreesPerSecond = 60f;

    public MinaDePedraDefinition Definition { get; private set; }
    public bool HasOutputReady => StoredOutput > 0;
    public int StoredOutput { get; private set; }
    public float ProcessElapsedSeconds { get; private set; }

    private Renderer _visualRenderer;
    private Transform _visualTransform;

    public void Initialize(MinaDePedraDefinition definition, GridTile tile, float initialProcessElapsedSeconds = 0f, int initialStoredOutput = 0)
    {
        Definition = definition;
        ProcessElapsedSeconds = initialProcessElapsedSeconds;
        StoredOutput = initialStoredOutput;

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Visual";
        visual.transform.SetParent(transform, worldPositionStays: false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localScale = new Vector3(0.7f, 0.5f, 0.7f);

        TileLink.Attach(visual, tile);

        _visualTransform = visual.transform;
        _visualRenderer = visual.GetComponent<Renderer>();
        _visualRenderer.material = RendererTint.SharedUrpLitMaterial;

        RefreshVisual();
    }

    private void OnEnable() => Instances.Add(this);
    private void OnDisable() => Instances.Remove(this);

    private bool CanProduce() => StoredOutput < Definition.outputStorageCapacity;

    public int CollectOutput(int maxAmount)
    {
        int collected = Mathf.Min(StoredOutput, maxAmount);
        StoredOutput -= collected;
        return collected;
    }

    // Simula, de uma vez, a producao durante uma ausencia (jogo fechado) -
    // mesmo principio de docs/07-prototipo-2-loop-hibrido.md.
    public void ApplyOfflineElapsed(float elapsedSeconds)
    {
        float remaining = elapsedSeconds;
        while (remaining > 0f && CanProduce())
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

    private void CompleteCycle()
    {
        StoredOutput = Mathf.Min(StoredOutput + Definition.outputAmountProduced, Definition.outputStorageCapacity);
        ProcessElapsedSeconds = 0f;
        GameLog.Log("Estrutura", "CicloCompleto", $"estrutura={Definition.displayName} produto_pronto={StoredOutput}");
    }

    private void Update()
    {
        if (CanProduce())
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

    private void RefreshVisual()
    {
        if (_visualRenderer == null)
        {
            return;
        }

        if (CanProduce())
        {
            float progress = Mathf.Clamp01(ProcessElapsedSeconds / Definition.processTimeSeconds);
            RendererTint.SetColor(_visualRenderer, Color.Lerp(ProducingColorLow, ProducingColorHigh, progress));
            return;
        }

        RendererTint.SetColor(_visualRenderer, ReadyColor);
    }
}
