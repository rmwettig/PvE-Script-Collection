using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for arbitrary skills
/// </summary>
public abstract class Ability : MonoBehaviour
{
    /// <summary>
    /// Ability name for displaying
    /// </summary>
    [SerializeField]
    protected string displayName = "";
    public string DisplayName { get { return displayName; } }
    /// <summary>
    /// Visual effect of the ability
    /// </summary>
    [SerializeField]
    protected GameObject vfx = null;
    /// <summary>
    /// Origin of the vfx
    /// </summary>
    [SerializeField]
    protected Transform spawnPoint = null;

    /// <summary>
    /// Upper range limit of the ability
    /// </summary>
    [SerializeField]
    protected float maximumDistance = 0f;
    public float MaximumDistance { get { return maximumDistance; } set { maximumDistance = Mathf.Max(value, 0); } }
    /// <summary>
    /// Cooldown of the ability
    /// </summary>
    [SerializeField]
    protected float cooldown = 0f;
    public float Cooldown { get { return cooldown; } }

    /// <summary>
    /// Description of the spell
    /// </summary>
    [SerializeField]
    protected string description = "";
    public string Description { get { return description; } }

    protected float timeSinceLastUse = 0f;
    
    public bool Ready { get { return Time.time - timeSinceLastUse > cooldown; } }

    /// <summary>
    /// Duration represents how long a spell lasts
    /// </summary>
    [SerializeField]
    private float duration = 0f;
    public float Duration { get { return duration; } set { duration = Mathf.Max(value, 0f); } }

    /// <summary>
    /// Detemines whether the ability can improve when used
    /// </summary>
    [SerializeField]
    private bool canLevel = false;
 
    protected Experience skillExperience = null;
   
    [SerializeField]
    protected int experiencePerSuccessfulUsage = 1;
    /// <summary>
    /// Percentage of additional experience that is required for the next level
    /// </summary>
    [SerializeField]
    protected float experienceIncreaseRate = 0f;
    public void Awake()
    {
        Initialize();
    }

    public virtual void Use()
    {
        //save the time at the beginning of the frame
        timeSinceLastUse = Time.time;
    }
    protected void GainExperience()
    {
        if (skillExperience != null)
        {
            skillExperience.AddExperience(experiencePerSuccessfulUsage);
        }
    }

    protected virtual void Initialize()
    {
        if (canLevel)
        {
            skillExperience = new Experience();
            skillExperience.ExperienceIncrease = experienceIncreaseRate;
        }
    }

    protected abstract void LevelUpHandler(int level);

}
