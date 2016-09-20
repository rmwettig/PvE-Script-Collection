using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Allows to select objects around a centered object
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class AreaSelection : TargetSelection
{
    /// <summary>
    /// Radius of the checked area
    /// </summary>
    private float radius = 1f;
    /// <summary>
    /// Determines whether or not the object in the center should be placed in the hit collection.
    /// Default: false
    /// </summary>
    private bool includeOrigin = false;
  //  private int layer = 0;

    public AreaSelection(float checkRadius, bool keepOrigin)//, int checkLayer)
    {
        radius = checkRadius;
        includeOrigin = keepOrigin;
       // layer = checkLayer;
    }

    /// <summary>
    /// Selects objects in the neighbourhood of the given object
    /// </summary>
    /// <param name="impactObject">center object</param>
    /// <returns>list of objects in the neighbourhood</returns>
    public override List<GameObject> DetermineTargets(GameObject impactObject, int layer)
    {
        List<GameObject> targets = new List<GameObject>();//base.DetermineTargets(impactObject, layer);
        //targets.RemoveAt(0); //remove initial object from area selection
        Collider[] hits = Physics.OverlapSphere(impactObject.transform.position, radius, 1 << layer);
        //for all objects in the area
        for (int i = 0; i < hits.Length; i++)
        {
            //if center object should be counted towards the hits
            if(includeOrigin)
                targets.Add(hits[i].gameObject);
            else
            {
                //check if a hit is the center object and exlude if so
                GameObject hit = hits[i].gameObject;
                if (hit != impactObject)
                    targets.Add(hit);
            }
        }
        return targets;
    }
}
