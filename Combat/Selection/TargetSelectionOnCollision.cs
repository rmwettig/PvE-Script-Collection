using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Applies effects on specific targets when a collision occurs
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class TargetSelectionOnCollision : MonoBehaviour
{
    /// <summary>
    /// One or more methods that are used to select targets
    /// </summary>
    private List<TargetSelection> selectors = null;
    /// <summary>
    /// Sets the desired methods
    /// </summary>
    public List<TargetSelection> Selectors { set { selectors = value; } }
    /// <summary>
    /// One or more effects that should be applied during a collision
    /// </summary>
    private List<ImpactEffect> effects = null;
    /// <summary>
    /// Sets the desired effects
    /// </summary>
    public List<ImpactEffect> Effects { set { effects = value; } }

    /// <summary>
    /// Determine which layer should be checked for collisions
    /// </summary>
    private int collisionLayer = 0;
    //public string collisionLayerName = "Default";
    public int CollisionLayer { get { return collisionLayer; } set { collisionLayer = value; } }
    /// <summary>
    /// Notify registered classes if a hit occured
    /// </summary>
    public delegate void HitOccured();
    public HitOccured onHitOccured;


    public void OnTriggerEnter(Collider col)
    {
        //if other object is not on the desired layer skip
        //if (col.gameObject.layer != collisionLayer) return;
        //for all selection methods
        for(int i = 0; i < selectors.Count; i++)
        {
            //find the corresponding targets
            List<GameObject> targets = selectors[i].DetermineTargets(col.gameObject, collisionLayer);
            //apply the attached impact effects
            for (int j = 0; j < targets.Count; j++)
            {
                if (targets[j] == gameObject) continue; //skip itself
                targets[j].GetComponent<ImpactEffectProcessorManager>().SendEffects(effects);
            }
        }
        //notify any registered class
        if (onHitOccured != null)
            onHitOccured();

    }
}
