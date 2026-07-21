using UnityEngine;

// material.color mutado direto em runtime nem sempre reflete visualmente no
// URP (batching/GPU Resident Drawer pode nao refazer o desenho). Usar um
// MaterialPropertyBlock por instancia e o jeito robusto de tingir objetos
// que compartilham o mesmo material, garantindo atualizacao visual.
public static class RendererTint
{
    private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

    private static Material _sharedUrpLitMaterial;
    private static MaterialPropertyBlock _block;

    public static Material SharedUrpLitMaterial
    {
        get
        {
            if (_sharedUrpLitMaterial == null)
            {
                _sharedUrpLitMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            }

            return _sharedUrpLitMaterial;
        }
    }

    public static void SetColor(Renderer renderer, Color color)
    {
        _block ??= new MaterialPropertyBlock();
        renderer.GetPropertyBlock(_block);
        _block.SetColor(BaseColorId, color);
        renderer.SetPropertyBlock(_block);
    }
}
