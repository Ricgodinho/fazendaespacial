using UnityEngine;

// Rotulo de texto flutuante acima de cada estrutura construida, so pra
// facilitar identificar qual e qual durante o playtest - placeholder,
// sem relacao com a arte final.
public class StructureLabels : MonoBehaviour
{
    private const float LabelHeightOffset = 1.1f;

    private GUIStyle _style;

    private void OnGUI()
    {
        if (Camera.main == null)
        {
            return;
        }

        _style ??= new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white }
        };

        foreach (var structure in ProcessingStructure.Instances)
        {
            DrawLabel(structure.transform.position, structure.Definition.displayName);
        }

        foreach (var armazem in ArmazemGeral.Instances)
        {
            DrawLabel(armazem.transform.position, armazem.Definition.displayName);
        }

        foreach (var hangar in HangarDeDrones.Instances)
        {
            DrawLabel(hangar.transform.position, hangar.Definition.displayName);
        }
    }

    private void DrawLabel(Vector3 worldPosition, string text)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(worldPosition + Vector3.up * LabelHeightOffset);
        if (screenPoint.z <= 0f)
        {
            return; // atras da camera
        }

        float guiY = Screen.height - screenPoint.y;
        var size = _style.CalcSize(new GUIContent(text));

        GUI.Box(new Rect(screenPoint.x - size.x / 2f - 4f, guiY - size.y / 2f - 2f, size.x + 8f, size.y + 4f), GUIContent.none);
        GUI.Label(new Rect(screenPoint.x - size.x / 2f, guiY - size.y / 2f, size.x, size.y), text, _style);
    }
}
