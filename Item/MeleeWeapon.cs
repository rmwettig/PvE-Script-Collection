using UnityEngine;
using System.Collections;

/// <summary>
/// Melee weapon that applies damage directly
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
[RequireComponent(typeof(TargetSelectionOnCollision))]
public class MeleeWeapon : Weapon
{
    protected override void Initialize()
    {
        base.Initialize();
        DamageOnHit doh = GetComponent<DamageOnHit>();
        doh.onHitOccured += GainExperience;

        
        targetSelectors.Add(new TargetSelection()); //immediate hit object
        TargetSelectionOnCollision targetSelection = GetComponent<TargetSelectionOnCollision>();
        targetSelection.Selectors = targetSelectors;
        targetSelection.Effects = impactEffects;
        targetSelection.onHitOccured += GainExperience;
        targetSelection.CollisionLayer = collisionLayer;
    }

    /// <summary>
    /// Does nothing by default as the melee weapon is supposed to move with
    /// a character's strike animation. Thus, every intersected object is hit
    /// without MeleeWeapon specific code.
    /// </summary>
    public override void Use()
    {
    }
}
