using UnityEngine;
using System.Collections;

public class LinearMovement : MonoBehaviour
{
    /// <summary>
    /// Type for movement completion
    /// </summary>
    public delegate void MovementFinished();
    public MovementFinished movementCompleted = null;

    /// <summary>
    /// Should the movement take place
    /// </summary>
    [SerializeField]
    private bool isActive = false;
    public bool IsActive { get { return isActive; } set { isActive = value; } }

    /// <summary>
    /// Velocity of the movement
    /// </summary>
    [SerializeField]
    private float speed = 1f;
    public float Speed { get { return speed; } set { speed = Mathf.Max(value, 0f); } }

    /// <summary>
    /// Direction of the movement
    /// </summary>
    private Vector3 direction = Vector3.forward;
    public Vector3 Direction { get { return direction; } set { direction = value; } }

    private float percentage = 0f;
    private Vector3 start = Vector3.zero;
    private Vector3 destination = Vector3.zero;

    // Update is called once per frame
    public void Update()
    {
        if (isActive)
        {
            transform.position = Vector3.Lerp(start, destination, percentage);
            percentage += speed * Time.deltaTime;
            if (percentage > 1f)
            {
                if (movementCompleted != null)
                    movementCompleted();
                isActive = false;
                StopMovement();
            }
        }
    }

    /// <summary>
    /// initalizes start and end position of the movement and activates the effect
    /// </summary>
    /// <param name="begin">start position</param>
    /// <param name="end">target position</param>
    public void StartMovement(Vector3 begin, Vector3 end)
    {
        start = begin;
        destination = end;
        isActive = true;
        percentage = 0f;
    }

    public void StopMovement()
    {
        isActive = false;
        gameObject.SetActive(false);
        percentage = 0f;
        //hide the effect below the ground
        //transform.Translate(0f, 0f, -100f);
        if (movementCompleted != null)
            movementCompleted();
    }



}
