using UnityEngine;
using System.Collections;

public class SplashDamageOnHit : MonoBehaviour
{

    [SerializeField]
    private int damage = 0;
    public int Damage { get { return damage; } set { damage = Mathf.Max(value, 0); } }

    /// <summary>
    /// Determine which layer should be checked for collisions
    /// </summary>
    private int collisionLayer = 0;
    //public string collisionLayerName = "Default";
    public int CollisionLayer { get { return collisionLayer; } set { collisionLayer = value; } }

    /// <summary>
    /// VFX for the splash damage
    /// </summary>
    [SerializeField]
    private GameObject splashParticles = null;

    /// <summary>
    /// Notify registered classes if a hit occured
    /// </summary>
    public delegate void HitOccured();
    public HitOccured onHitOccured;

    private float radius = 1f;


    public void OnTriggerEnter(Collider col)
    {
        GameObject other = col.gameObject;
        if (other.layer == collisionLayer)
        {

            //for all objects within the radius
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, 1 << collisionLayer);
            foreach (Collider coll in hits)
            {
                //get their health component if present
                Health h = coll.gameObject.GetComponent<Health>();
                if (h != null)
                {
                    //apply damage
                    h.addHealth(-damage);
                    if (onHitOccured != null)
                        onHitOccured();

                }
            }
        }
    }

    public void Initialize(float distance, int damageValue)
    {
        radius = distance;
        damage = damageValue;
    }
}
