using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manager component where processors can register
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class ImpactEffectProcessorManager : MonoBehaviour
{
    private List<ImpactEffectProcessor> processors = new List<ImpactEffectProcessor>();

    public void SendEffects(List<ImpactEffect> effects)
    {
        for (int i = 0; i < processors.Count; i++)
            processors[i].Process(effects);
    }

    public void AddProcessor(ImpactEffectProcessor processor)
    {
        processors.Add(processor);
    }

    public void Remove(ImpactEffectProcessor processor)
    {
        processors.Remove(processor);
    }
}
