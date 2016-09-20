using UnityEngine;
using System.Collections;

/// <summary>
/// Increases the range of the specified skills
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class IncreaseRange : Talent
{
    [SerializeField]
    private int bonusValue = 0;

    [SerializeField]
    protected Ability[] affectedSkills;

    /// <summary>
    /// Indicates whether the bonus value should be interpreted as a percentage
    /// </summary>
    [SerializeField]
    private bool isPercentage = false;

    public override void Learn()
    {
        for (int i = 0; i < affectedSkills.Length; i++)
        {
            Ability a = affectedSkills[i];
            if (isPercentage)
                a.MaximumDistance += a.MaximumDistance * bonusValue * 0.01f;
            else
                a.MaximumDistance += bonusValue;
        }
    }

    public override void Unlearn()
    {
        for (int i = 0; i < affectedSkills.Length; i++)
        {
            Ability a = affectedSkills[i];
            if (isPercentage)
                a.MaximumDistance = a.MaximumDistance/(1 + bonusValue * 0.01f ); //calculate original value
            else
                a.MaximumDistance -= bonusValue;
        }
    }

}
