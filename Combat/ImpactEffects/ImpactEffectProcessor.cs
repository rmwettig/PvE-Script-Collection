using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base class for impact effect processing depending on the effect type
/// </summary>
[RequireComponent(typeof(ImpactEffectProcessorManager))]
public abstract class ImpactEffectProcessor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<ImpactEffectProcessorManager>().AddProcessor(this);
        Initialize();
    }

    public abstract void Process(List<ImpactEffect> effects);
    public abstract void Initialize();
}
