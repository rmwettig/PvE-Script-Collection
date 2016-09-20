using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generalized selection of objects
/// </summary>
/// <remarks>
/// Author:Martin Wettig
/// </remarks>
public class TargetSelection
{
    /// <summary>
    /// Finds all objects close to a given object.
    /// By default the input object itself is returned.
    /// </summary>
    /// <param name="impactObject">object whose neighbourhood should examined</param>
    /// <param name="layer">layer index against which should checked</param>
    /// <returns>list of targets</returns>
    public virtual List<GameObject> DetermineTargets(GameObject impactObject, int layer)
    {
        List<GameObject> targets = new List<GameObject>();
        if(impactObject.layer == layer)
            targets.Add(impactObject);
        return targets;
    }
}
