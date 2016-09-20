using UnityEngine;
using System.Collections;

/// <summary>
/// Leveling system
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class Experience
{
    [SerializeField]
    private int currentLevel = 1;
    public int CurrentLevel { get { return currentLevel; } }

    /// <summary>
    /// Required experience points for a level up
    /// </summary>
    [SerializeField]
    private int experienceForLevelUp = 10;
    public int ExperienceForLevelUp { get { return experienceForLevelUp; } }
    /// <summary>
    /// Currently gathered experience
    /// </summary>
    private int currentExperience = 0;
    public int CurrentExperience { get { return currentExperience; } }
    /// <summary>
    /// Percentage by which the required experience for level up increases.
    /// Default: 0
    /// </summary>
    [SerializeField]
    private float experienceIncrease = 0f;
    public float ExperienceIncrease { get { return experienceIncrease; } set { experienceIncrease = Mathf.Max(value, 0f); } }
    
    /// <summary>
    /// Notifies other classes of the new level value
    /// </summary>
    /// <param name="level">reached level</param>
    public delegate void LevelChanged(int level);
    public LevelChanged onLevelChanged;

    public void AddExperience(int e)
    {
        //add the gathered experience
        currentExperience += e;

        //total experience gathered exceeds the amount required for level up
        if (currentExperience >= experienceForLevelUp)
        {
            currentLevel += 1;
            //remove the consumed experience and keep the rest for the next level
            currentExperience = currentExperience - experienceForLevelUp;
            //update the amount of required experience
            experienceForLevelUp = (int)(experienceForLevelUp * (1 + experienceIncrease));
            if (onLevelChanged != null)
            {
                onLevelChanged(currentLevel);
            }
        }
    }

}
