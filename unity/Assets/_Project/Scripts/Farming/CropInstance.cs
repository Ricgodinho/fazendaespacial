using UnityEngine;

public class CropInstance : MonoBehaviour
{
    private const int StageCount = 5;

    // 5 estagios visuais por % do tempo de crescimento (0/25/50/75/100%),
    // nao por tempo fixo - ver docs/cultivos/00-indice.md.
    private static readonly Color[] StageColors =
    {
        new Color(0.35f, 0.25f, 0.15f), // 0%   - terreno preparado / semente
        new Color(0.35f, 0.6f, 0.25f),  // 25%  - brotinho pequeno
        new Color(0.3f, 0.65f, 0.2f),   // 50%  - broto medio
        new Color(0.6f, 0.7f, 0.15f),   // 75%  - quase maduro
        new Color(0.9f, 0.75f, 0.1f),   // 100% - pronto para colher
    };

    private static readonly float[] StageScales = { 0.12f, 0.2f, 0.28f, 0.35f, 0.42f };

    public CropDefinition Definition { get; private set; }
    public bool IsMature => CurrentStage == StageCount - 1;

    // Acumulador de segundos de crescimento, nao timestamp - permite somar de
    // uma vez o tempo de ausencia calculado no load (docs/07), sem depender
    // de Time.time (que zera a cada sessao de jogo).
    public float AccumulatedSeconds { get; private set; }

    private int CurrentStage
    {
        get
        {
            float progress = Mathf.Clamp01(AccumulatedSeconds / Definition.growTimeSeconds);
            return Mathf.Clamp(Mathf.FloorToInt(progress * (StageCount - 1) + 0.0001f), 0, StageCount - 1);
        }
    }

    private Renderer _visualRenderer;
    private Transform _visualTransform;
    private int _lastStageShown = -1;

    public void Initialize(CropDefinition definition, float initialAccumulatedSeconds = 0f)
    {
        Definition = definition;
        AccumulatedSeconds = initialAccumulatedSeconds;

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Visual";
        visual.transform.SetParent(transform, worldPositionStays: false);
        visual.transform.localPosition = Vector3.zero;

        // O raycast de clique deve sempre acertar o tile, nao a planta em cima dele.
        Destroy(visual.GetComponent<Collider>());

        _visualTransform = visual.transform;
        _visualRenderer = visual.GetComponent<Renderer>();
        _visualRenderer.material = RendererTint.SharedUrpLitMaterial;

        ApplyStage(CurrentStage);
    }

    // Soma de uma vez o tempo de ausencia (ja limitado pelo teto global de
    // 48h em GameBootstrap) durante a reconstrucao a partir de um save.
    public void ApplyOfflineElapsed(float elapsedSeconds)
    {
        AccumulatedSeconds += elapsedSeconds;
    }

    private void Update()
    {
        if (_visualRenderer == null)
        {
            return;
        }

        AccumulatedSeconds += Time.deltaTime;

        int stage = CurrentStage;
        if (stage != _lastStageShown)
        {
            ApplyStage(stage);
        }
    }

    private void ApplyStage(int stage)
    {
        _lastStageShown = stage;
        RendererTint.SetColor(_visualRenderer, StageColors[stage]);
        float scale = StageScales[stage];
        _visualTransform.localScale = new Vector3(scale, scale, scale);
    }
}
