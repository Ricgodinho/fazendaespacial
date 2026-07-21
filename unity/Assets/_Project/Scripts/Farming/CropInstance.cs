using UnityEngine;

public class CropInstance : MonoBehaviour
{
    public CropDefinition Definition { get; private set; }
    public bool IsMature => Time.time - _plantedAt >= Definition.growTimeSeconds;

    private float _plantedAt;
    private Renderer _visualRenderer;

    private static readonly Color GrowingColor = new Color(0.4f, 0.7f, 0.2f);
    private static readonly Color MatureColor = new Color(0.9f, 0.75f, 0.1f);

    public void Initialize(CropDefinition definition)
    {
        Definition = definition;
        _plantedAt = Time.time;

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Visual";
        visual.transform.SetParent(transform, worldPositionStays: false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        // O raycast de clique deve sempre acertar o tile, nao a planta em cima dele.
        Destroy(visual.GetComponent<Collider>());

        _visualRenderer = visual.GetComponent<Renderer>();
        _visualRenderer.material = RendererTint.SharedUrpLitMaterial;
        RendererTint.SetColor(_visualRenderer, GrowingColor);
    }

    private void Update()
    {
        if (_visualRenderer == null)
        {
            return;
        }

        RendererTint.SetColor(_visualRenderer, IsMature ? MatureColor : GrowingColor);
    }
}
