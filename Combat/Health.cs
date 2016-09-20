using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// Health component for any object
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class Health : MonoBehaviour 
{
    [SerializeField]
    private int currentHealth = 1;
    public int CurrentHealth { get { return currentHealth; } }

    [SerializeField]
    private int maximumHealth = 1;
    public int MaximumHealth { get { return maximumHealth; } }
    
    /// <summary>
    /// Determines whether an object has still health left
    /// </summary>
    public bool Alive { get { return currentHealth > 0; } }

    public delegate void HealthChanged(float percentage);
    public HealthChanged onHealthChanged;
  
    /// <summary>
    /// changes the value of the current health.
    /// The new health is clamped to fit [0, maximumHealth]
    /// </summary>
    /// <param name="value">number by which health is changed</param>
    public void addHealth(int value)
    {
        //Debug.Log("Health: pre-clamp: " + value);
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maximumHealth);
        //Debug.Log("Health: clamped: " + currentHealth);
        if (onHealthChanged != null)
            onHealthChanged(currentHealth / (float)maximumHealth);
    }
}
