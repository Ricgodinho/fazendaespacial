// Rota configuravel do Drone de Transporte (docs/drones/transporte.md):
// Tier 1 = manual (jogador aciona cada entrega); Tier 2 = repete
// automaticamente. Aqui os dois modos convivem por rota, escolhidos pelo
// jogador na janela de configuracao do Hangar.
public class TransporteRoute
{
    public ProcessingStructureDefinition TargetDefinition;
    public bool AutomaticEnabled;
    public bool Busy;
    public float Timer;
}
