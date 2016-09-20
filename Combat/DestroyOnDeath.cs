using UnityEngine;
using System.Collections;

/// <summary>
/// Removes an object from the world if its health reaches zero
/// </summary>
/// <remarks>
/// Author:Martin Wettig
/// </remarks>
public class DestroyOnDeath : MonoBehaviour
{
    /// <summary>
    /// Health percentage threshold that determines death
    /// </summary>
    [SerializeField]
    private float threshold = 0.00001f;

    // Use this for initialization
    void Start()
    {
        Health health = GetComponent<Health>();
        health.onHealthChanged += DestroySelf;
    }

    /// <summary>
    /// Remove object if health is zero
    /// </summary>
    /// <param name="p">Health percentage</param>
    public void DestroySelf(float p)
    {
        if (p < threshold)
            Destroy(gameObject);
    }
}
