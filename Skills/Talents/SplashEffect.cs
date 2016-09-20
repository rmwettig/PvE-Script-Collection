using UnityEngine;
using System.Collections;

/// <summary>
/// Adds an area of effect to an ability
/// </summary>
public class SplashEffect : Talent
{
    [SerializeField]
    private AttackAbility[] affectedAbility;

    /// <summary>
    /// Diameter within the splash effect takes effect
    /// </summary>
    [SerializeField]
    private float radius = 1f;
    /// <summary>
    /// Percentage of the initial effect value that is applied onto targets.
    /// E.g. a value of 1f indicates in case of damage that the full damage is spread to other targets
    /// </summary>
    [SerializeField]
    private float effectValuePercentage = 1f;

    private TargetSelection targetSelector = null;

    /// <summary>
    /// Determines whether or not the object in the center should be placed in the hit collection.
    /// Default: false
    /// </summary>
    [SerializeField]
    private bool includeOrigin = false;

    public override void Learn()
    {
        for (int i = 0; i < affectedAbility.Length; i++)
            affectedAbility[i].AddSelector(targetSelector);
    }

    public override void Unlearn()
    {
        for (int i = 0; i < affectedAbility.Length; i++)
            affectedAbility[i].RemoveSelector(targetSelector);
    }

    protected override void Initialize()
    {
        base.Initialize();
        targetSelector = new AreaSelection(radius, includeOrigin);
    }
}
