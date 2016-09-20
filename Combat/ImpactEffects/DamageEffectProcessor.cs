using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Modifies incoming damage and relays the value to the health component.
/// Moreover, it can apply periodic damage.
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
[RequireComponent(typeof(Health))]
public class DamageEffectProcessor : ImpactEffectProcessor
{
    private Health health = null;

    /// <summary>
    /// Percentage by which the damage is decreased
    /// </summary>
    private float damageDampingFactor = 0f;
    public float DamageDampingFactor { get { return damageDampingFactor; } set { damageDampingFactor = Mathf.Clamp(value, 0f, 1f); } }
    /// <summary>
    /// Percentage by which the damage is increased
    /// </summary>
    private float damageEnhancingFactor = 0f;
    public float DamageEnhancingFactor { get { return damageEnhancingFactor; } set { damageEnhancingFactor = Mathf.Max(value, 0f); } }

    public override void Process(List<ImpactEffect> effects)
    {
        foreach (ImpactEffect impactEffect in effects)
        {
            DamageEffect damage = impactEffect as DamageEffect;
            if (damage != null)
            {
                //apply immediate damage
                if (damage.Duration < 0.01f)
                {
                    //calculate the final damage value
                    int damageValue = damage.Damage;
                    //apply enhancement first
                    damageValue = (int) (damageValue * (1 + damageEnhancingFactor));
                    //then take damping into account
                    damageValue = (int) (damageValue * (1 - damageDampingFactor));
                    //Debug.Log("Damage-Processor:damage: " + damage.Damage);
                    //Debug.Log("Damage-Processor:damageValue: " + damageValue);
                    health.addHealth(-damageValue);
                }
            }
        }
    }

    public override void Initialize()
    {
        health = GetComponent<Health>();
    }
}
