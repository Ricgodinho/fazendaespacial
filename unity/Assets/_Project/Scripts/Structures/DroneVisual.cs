using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Representacao puramente cosmetica do trajeto de um drone (Hangar -> tile
// -> Armazem quando existir -> volta). O efeito de jogo (colher/plantar)
// acontece quando o drone chega em cada perna, via callback - nao no
// momento do spawn. O callback recebe o proprio DroneVisual para poder
// alternar o indicador de carga (SetCarrying).
public class DroneVisual : MonoBehaviour
{
    private const float Speed = 4f;
    private const float ArrivalThreshold = 0.05f;

    private static readonly Color CargoColor = Color.white;

    private GameObject _cargoIndicator;

    public static DroneVisual Fly(
        Vector3 startPosition,
        Color color,
        bool startCarrying,
        List<(Vector3 point, Action<DroneVisual> onArrive)> legs)
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
        drone.CreateCargoIndicator();
        drone.SetCarrying(startCarrying);
        drone.StartCoroutine(drone.RunLegs(legs));
        return drone;
    }

    public void SetCarrying(bool carrying)
    {
        if (_cargoIndicator != null)
        {
            _cargoIndicator.SetActive(carrying);
        }
    }

    private void CreateCargoIndicator()
    {
        _cargoIndicator = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _cargoIndicator.name = "CargoIndicator";
        _cargoIndicator.transform.SetParent(transform, worldPositionStays: false);
        _cargoIndicator.transform.localPosition = new Vector3(0f, -0.4f, 0f);
        _cargoIndicator.transform.localScale = new Vector3(0.55f, 0.35f, 0.55f);
        Destroy(_cargoIndicator.GetComponent<Collider>());

        var renderer = _cargoIndicator.GetComponent<Renderer>();
        renderer.material = RendererTint.SharedUrpLitMaterial;
        RendererTint.SetColor(renderer, CargoColor);
    }

    private IEnumerator RunLegs(List<(Vector3 point, Action<DroneVisual> onArrive)> legs)
    {
        foreach (var leg in legs)
        {
            while (Vector3.Distance(transform.position, leg.point) > ArrivalThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, leg.point, Speed * Time.deltaTime);
                yield return null;
            }

            leg.onArrive?.Invoke(this);
        }

        Destroy(gameObject);
    }
}
