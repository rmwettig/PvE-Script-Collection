using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Area of effect spell
/// </summary>
public class AoE : AttackAbility
{
    [SerializeField]
    private float radius = 3f;
    public float Radius { get { return radius; } set { radius = Mathf.Max(value, 0f); } }

    /// <summary>
    /// Object for visual effect
    /// </summary>
    private GameObject bullet = null;

    /// <summary>
    /// Method to find targets
    /// </summary>
    TargetSelection targetSelector = null;
    

    public override void Use()
    {
        if (Ready)
        {
            OrbitingMovement om = bullet.GetComponent<OrbitingMovement>();
            om.StartMovement(transform);
            List<GameObject> targets = targetSelector.DetermineTargets(gameObject, collisionLayer);
            ApplyDamage(targets);
            base.Use();
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        InitializeBullet();

        if(skillExperience != null)
            skillExperience.onLevelChanged += LevelUpHandler;

        InitializeDamageLogic();
    }

    private void InitializeDamageLogic()
    {
        targetSelector = new AreaSelection(radius, false);
        impactEffects.Add(new DamageEffect(damage));
    }

    private void InitializeBullet()
    {
        bullet = Instantiate(vfx, transform.position, transform.rotation) as GameObject;
        OrbitingMovement om = bullet.AddComponent<OrbitingMovement>();
        om.Speed = speed;
        om.Distance = radius;
        om.Duration = Duration;
    }

    //private void ApplyDamage()
    //{
    //    //for all objects within the radius
    //    Collider[] hits = Physics.OverlapSphere(transform.position, radius, 1 << collisionLayer);
    //    foreach (Collider col in hits)
    //    {
    //        //get their health component if present
    //        Health h = col.gameObject.GetComponent<Health>();
    //        if (h != null)
    //        {
    //            //apply damage and increase collected experience
    //            h.addHealth(-damage);
    //            GainExperience();
    //        }
    //    }

    //}

    private void ApplyDamage(List<GameObject> targets)
    {
        for (int i = 0; i < targets.Count; i++ )
        {
            ImpactEffectProcessorManager manager = targets[i].GetComponent<ImpactEffectProcessorManager>();
            manager.SendEffects(impactEffects);
            GainExperience();
        }
    }
    
    protected override void LevelUpHandler(int level)
    {
        base.LevelUpHandler(level);
    }


}
