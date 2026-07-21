using System.Collections.Generic;
using UnityEngine;

// So implementa o efeito de capacidade do Nivel 1 (docs/estruturas/planeta-1/armazem-geral.md).
// Slots por categoria, recebimento automatico de outros planetas e overflow
// automatico sao breakpoints de niveis mais altos, fora de escopo aqui.
public class ArmazemGeral : PlacedStructure
{
    // Usado pelo Hangar de Drones para saber onde o drone de Colheita deve
    // entregar o que colheu (docs/drones/colheita.md).
    public static readonly List<ArmazemGeral> Instances = new();

    private static readonly Color VisualColor = new Color(0.55f, 0.45f, 0.3f);

    public ArmazemGeralDefinition Definition { get; private set; }

    private void OnEnable() => Instances.Add(this);
    private void OnDisable() => Instances.Remove(this);

    public void Initialize(ArmazemGeralDefinition definition, GridTile tile, PlayerInventory inventory)
    {
        Definition = definition;
        inventory.Capacity = definition.capacity;

        var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visual.name = "Visual";
        visual.transform.SetParent(transform, worldPositionStays: false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localScale = new Vector3(0.9f, 0.7f, 0.9f);

        // Mantem o colisor e liga de volta ao tile (ver TileLink) - evita
        // que o raycast erre em estruturas altas perto da borda do grid.
        TileLink.Attach(visual, tile);

        var renderer = visual.GetComponent<Renderer>();
        renderer.material = RendererTint.SharedUrpLitMaterial;
        RendererTint.SetColor(renderer, VisualColor);
    }
}
