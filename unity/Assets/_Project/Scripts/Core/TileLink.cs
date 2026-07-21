using UnityEngine;

// Permite que o colisor de um cultivo/estrutura resolva de volta para o
// GridTile correto ao ser clicado. Antes, cultivos/estruturas nao tinham
// colisor proprio (destruido de proposito) para o raycast "passar direto"
// ate o colisor do tile abaixo - mas isso falha quando a estrutura e alta
// o suficiente e a camera angulada faz o raio sair do grid antes de
// acertar o tile certo (bug encontrado em playtest, estrutura perto da
// borda do mapa). Agora o colisor fica, e este componente aponta de volta.
public class TileLink : MonoBehaviour
{
    public GridTile Tile { get; private set; }

    public static void Attach(GameObject target, GridTile tile)
    {
        target.AddComponent<TileLink>().Tile = tile;
    }
}
