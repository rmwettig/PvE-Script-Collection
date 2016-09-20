using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base class for weapons that contains a leveling feature
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public abstract class Weapon : MonoBehaviour 
{

    /// <summary>
    /// Provide a mean to notify damage effects of updated damage values.
    /// This delegate can only relay information within weapons
    /// </summary>
    /// <param name="newDamage">new damage value</param>
    protected delegate void UpdateDamage(int newDamage);
    protected UpdateDamage onDamageChange;

    /// <summary>
    /// Determine which layer should be checked for collisions
    /// </summary>
    protected int collisionLayer = 0;
    [SerializeField]
    protected string collisionLayerName = "Default";

    /// <summary>
    /// Effects that should be applied to targets
    /// </summary>
    protected List<ImpactEffect> impactEffects = new List<ImpactEffect>();
    protected List<TargetSelection> targetSelectors = new List<TargetSelection>();

    /// <summary>
    /// Experience for a successful hit
    /// </summary>
    [SerializeField]
    protected int experienceForSuccessfulUsage = 1;

    /// <summary>
    /// Weapon damage
    /// </summary>
    [SerializeField]
    protected int damage = 1;
    public int Damage { get { return damage; } }

    /// <summary>
    /// Weapon experience
    /// </summary>
    protected Experience experience = null;

    public void Awake()
    {
        Initialize();
    }
	
    protected virtual void Initialize()
    {
        experience = new Experience();
        experience.onLevelChanged += LevelUpHandler;

        DamageEffect de = new DamageEffect(damage);
        onDamageChange += de.ChangeDamageValue; //connect damage updating with damage effect
        impactEffects.Add(de);
    }

    protected void LevelUpHandler(int level)
    {
        damage = damage + damage * level / 10;
        if (onDamageChange != null)
            onDamageChange(damage);
    }

    protected void GainExperience()
    {
        experience.AddExperience(experienceForSuccessfulUsage);
    }

    public abstract void Use();
}
