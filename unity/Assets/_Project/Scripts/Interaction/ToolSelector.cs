using UnityEngine;

public class ToolSelector : MonoBehaviour
{
    public ToolType CurrentTool { get; private set; } = ToolType.None;

    public void SelectNone() => CurrentTool = ToolType.None;
    public void SelectPlant() => CurrentTool = ToolType.Plant;
    public void SelectHarvest() => CurrentTool = ToolType.Harvest;
    public void SelectBuildProcessing() => CurrentTool = ToolType.BuildProcessing;
    public void SelectBuildArmazem() => CurrentTool = ToolType.BuildArmazem;
    public void SelectBuildHangar() => CurrentTool = ToolType.BuildHangar;
    public void SelectBuildViveiro() => CurrentTool = ToolType.BuildViveiro;
    public void SelectDemolish() => CurrentTool = ToolType.Demolish;
}
