using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates an object around a center
/// </summary>
/// <remarks>
/// Author: Martin Wettig
/// </remarks>
public class OrbitingMovement : MonoBehaviour
{
    /// <summary>
    /// Point around which the movement takes place
    /// </summary>
    [SerializeField]
    private Transform center = null;
    public Transform Center { get { return center; } set { center = value; } }

    /// <summary>
    /// Orbiting distance from the center
    /// </summary>
    [SerializeField]
    private float distance = 1.0f;
    public float Distance { get { return distance; } set { distance = Mathf.Max(value, 0f); } }

    private bool isActive = false;
    public bool IsActive { get { return isActive; } }

    /// <summary>
    /// Movement speed
    /// </summary>
    [SerializeField]
    private float speed = 1f;
    public float Speed { get { return speed; } set { speed = Mathf.Max(value, 0f); } }
    /// <summary>
    /// Time until movement termination.
    /// Default: 0, i.e. no termination
    /// </summary>
    [SerializeField]
    private float duration = 0f;
    public float Duration { get { return duration; } set { duration = Mathf.Max(value, 0f); } }

    private float startTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //if the movement should be applied and its application time is lower than the specified duration
        if (isActive && (Time.time - startTime) < duration)
        {
            //orbit around a center point
            transform.RotateAround(center.position, Vector3.up, speed * Time.deltaTime);
            //assure that the orbiting object sticks to the x-z plane
            Vector3 pos = transform.position;
            //put the orbit on the same height as the center            
            pos.y = center.position.y;
  
            //Vector3 clampedOrbit = (transform.position - center.position).normalized * distance + center.position;
            //calculate the direction relative to and distance to the center
            Vector3 clampedOrbit = (pos - center.position).normalized * distance + center.position;
            
            //move the object
            transform.position = Vector3.Slerp(transform.position,
                clampedOrbit, speed * Time.deltaTime);

        }
        else
        {
            isActive = false;
            transform.Translate(0f, 0f, -100f);
        }
    }

    public void StartMovement(Transform center)
    {
        this.center = center;
        startTime = Time.time;
        isActive = true;
    }
}
