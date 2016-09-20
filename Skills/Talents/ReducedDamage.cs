using UnityEngine;
using System.Collections;

/// <summary>
/// Reduces the taken damage
/// </summary>
/// <remarks>
/// Author:Martin Wettig
/// </remarks>
[RequireComponent(typeof(DamageEffectProcessor))]
public class ReducedDamage : Talent
{
    //[SerializeField]
    //private Health health = null;
    private DamageEffectProcessor processor = null;

    /// <summary>
    /// Denotes the amount of damage reduction.
    /// Default: 1f, i.e. no damage reduction at all, 0.5f means half of original damage value.
    /// </summary>
    [SerializeField]
    private float modifier = 1f;
  
    public override void Learn()
    {
        //health.DamageModifier -= modifier;
        processor.DamageDampingFactor += modifier;
    }

    public override void Unlearn()
    {
        //health.DamageModifier += modifier;
        processor.DamageDampingFactor -= modifier;
    }

    protected override void Initialize()
    {
        base.Initialize();
        processor = GetComponent<DamageEffectProcessor>();
    }
}
