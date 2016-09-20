using UnityEngine;
using System.Collections;


public class Bow : Weapon
{
    /// <summary>
    /// Appearance of arrows
    /// </summary>
    [SerializeField]
    private GameObject arrowPrefab = null;

    /// <summary>
    /// Spawning position for arrows
    /// </summary>
    [SerializeField]
    private Transform arrowSpawn = null;

    /// <summary>
    /// Maximum distance the bow can shoot
    /// </summary>
    [SerializeField]
    private float range = 40f;

    [SerializeField]
    private int poolSize = 10;

    private ObjectPool pool = null;

    public override void Use()
    {
        GameObject arrow = pool.GetObject();
        if (arrow == null) return;
        LinearMovement lm = arrow.GetComponent<LinearMovement>();
        lm.StartMovement(arrowSpawn.position, arrowSpawn.position + arrowSpawn.forward * range);
    }

    protected override void Initialize()
    {
        base.Initialize();
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new ObjectPool(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            //shot gets a shot prefab that needs to be instantiated
            GameObject bullet = Instantiate(arrowPrefab);
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

            //if component can level
            if (experience != null)
            {
                //get experience on successful hit
                collisionSelection.onHitOccured += GainExperience;
                experience.onLevelChanged += LevelUpHandler;
            }
            pool.AddObject(bullet);
        }
    }
}
