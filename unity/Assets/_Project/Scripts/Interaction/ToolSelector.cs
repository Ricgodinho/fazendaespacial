using UnityEngine;

public class ToolSelector : MonoBehaviour
{
    public ToolType CurrentTool { get; private set; } = ToolType.None;

    public void SelectNone() => CurrentTool = ToolType.None;
    public void SelectPlant() => CurrentTool = ToolType.Plant;
    public void SelectHarvest() => CurrentTool = ToolType.Harvest;
    public void SelectBuild() => CurrentTool = ToolType.Build;
}
