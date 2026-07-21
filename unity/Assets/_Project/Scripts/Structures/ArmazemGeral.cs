using UnityEngine;

// So implementa o efeito de capacidade do Nivel 1 (docs/estruturas/planeta-1/armazem-geral.md).
// Slots por categoria, recebimento automatico de outros planetas e overflow
// automatico sao breakpoints de niveis mais altos, fora de escopo aqui.
public class ArmazemGeral : PlacedStructure
{
    private static readonly Color VisualColor = new Color(0.55f, 0.45f, 0.3f);

    public ArmazemGeralDefinition Definition { get; private set; }

    public void Initialize(ArmazemGeralDefinition definition, PlayerInventory inventory)
    {
        Definition = definition;
        inventory.Capacity = definition.capacity;

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Visual";
        visual.transform.SetParent(transform, worldPositionStays: false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localScale = new Vector3(0.9f, 0.7f, 0.9f);

        Destroy(visual.GetComponent<Collider>());

        var renderer = visual.GetComponent<Renderer>();
        renderer.material = RendererTint.SharedUrpLitMaterial;
        RendererTint.SetColor(renderer, VisualColor);
    }
}
