using UnityEngine;

public class ToolSelector : MonoBehaviour
{
    public ToolType CurrentTool { get; private set; } = ToolType.None;
    public CropDefinition SelectedCrop { get; private set; }
    public ProcessingStructureDefinition SelectedProcessingStructure { get; private set; }

    public void SelectNone() => CurrentTool = ToolType.None;

    public void SelectPlant(CropDefinition crop)
    {
        CurrentTool = ToolType.Plant;
        SelectedCrop = crop;
    }

    public void SelectHarvest() => CurrentTool = ToolType.Harvest;

    public void SelectBuild(ProcessingStructureDefinition definition)
    {
        CurrentTool = ToolType.Build;
        SelectedProcessingStructure = definition;
    }

    public void SelectBuildArmazem() => CurrentTool = ToolType.BuildArmazem;
    public void SelectBuildHangar() => CurrentTool = ToolType.BuildHangar;
    public void SelectDemolish() => CurrentTool = ToolType.Demolish;
}
