using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Representacao puramente cosmetica do trajeto de um drone (Hangar -> tile
// -> Armazem quando existir -> volta). O efeito de jogo (colher/plantar)
// acontece quando o drone chega em cada perna, via callback - nao no
// momento do spawn.
public class DroneVisual : MonoBehaviour
{
    private const float Speed = 4f;
    private const float ArrivalThreshold = 0.05f;

    public static void Fly(Vector3 startPosition, Color color, List<(Vector3 point, Action onArrive)> legs)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = "DroneVisual";
        obj.transform.position = startPosition;
        obj.transform.localScale = Vector3.one * 0.3f;
        Destroy(obj.GetComponent<Collider>());

        var renderer = obj.GetComponent<Renderer>();
        renderer.material = RendererTint.SharedUrpLitMaterial;
        RendererTint.SetColor(renderer, color);

        var drone = obj.AddComponent<DroneVisual>();
        drone.StartCoroutine(drone.RunLegs(legs));
    }

    private IEnumerator RunLegs(List<(Vector3 point, Action onArrive)> legs)
    {
        foreach (var leg in legs)
        {
            while (Vector3.Distance(transform.position, leg.point) > ArrivalThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, leg.point, Speed * Time.deltaTime);
                yield return null;
            }

            leg.onArrive?.Invoke();
        }

        Destroy(gameObject);
    }
}
