using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Simple shot ability
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class Shot : AttackAbility
{
    /// <summary>
    /// Bullet pool size
    /// </summary>
    [SerializeField]
    private int vfxNumber = 10;

    /// <summary>
    /// Bullet pool
    /// </summary>
    private ObjectPool pool = null;

    public override void Use()
    {
        if (Ready)
        {
            GameObject bullet = pool.GetObject();
            if (bullet == null) return;
            bullet.SetActive(true);
            LinearMovement lm = bullet.GetComponent<LinearMovement>();
            lm.StartMovement(spawnPoint.position, spawnPoint.position + spawnPoint.forward * maximumDistance);

            base.Use();
        }
    }

    protected override void Initialize()
    {
        //prepare appropriate collision layer first
        base.Initialize();
        //set up the collision properties
        //in particular, connect the damage effect with the damage update notifyer
        DamageEffect damageEffect = new DamageEffect(damage);
        onDamageChange += damageEffect.ChangeDamageValue;
        impactEffects.Add(damageEffect);

        targetSelectors.Add(new TargetSelection());
        InitializePool();

    }

    private void InitializePool()
    {
        pool = new ObjectPool(vfxNumber);
        for (int i = 0; i < vfxNumber; i++)
        {
            //shot gets a shot prefab that needs to be instantiated
            GameObject bullet = Instantiate(vfx);
            bullet.SetActive(false);
            //rigidbody is needed to perform collision detection
            //as we might re-use the bullet prefab for other purposes
            //the rigidbody is added dynamically
            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            //disable gravity as the shot should fly straight forward
            rb.useGravity = false;

            //prepare collision handler
            TargetSelectionOnCollision collisionSelection = bullet.AddComponent<TargetSelectionOnCollision>();
            //add the set up target selection methods and impact effects
            //references to the lists are passed as content changes are
            //propagated immediately
            collisionSelection.Selectors = targetSelectors;
            collisionSelection.Effects = impactEffects;

            collisionSelection.CollisionLayer = collisionLayer;

            //straight forward movement
            LinearMovement movement = bullet.AddComponent<LinearMovement>();
            //stop movement on hit
            collisionSelection.onHitOccured += movement.StopMovement;

            //if skill can level
            if (skillExperience != null)
            {
                //get experience on successful hit
                collisionSelection.onHitOccured += GainExperience;
            }
            pool.AddObject(bullet);
        }
        skillExperience.onLevelChanged += LevelUpHandler;
    }

    /// <summary>
    /// Executes parent level up behaviour and notfies
    /// internally registered components of damage changes
    /// </summary>
    /// <param name="level"></param>
    protected override void LevelUpHandler(int level)
    {
        base.LevelUpHandler(level);
        if (onDamageChange != null)
            onDamageChange(damage);
    }
}
