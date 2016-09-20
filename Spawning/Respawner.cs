using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a respawn location that updates the respawn position of objects
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class Respawner : MonoBehaviour 
{
    /// <summary>
    /// Updates the respawn position on entering the spawning area
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        Respawn r = other.GetComponent<Respawn>();
        if (r != null)
        {
            r.RespawnPoint = transform;
        }
    }
}
