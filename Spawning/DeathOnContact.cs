using UnityEngine;
using System.Collections;

public class DeathOnContact : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Respawn respawn = other.gameObject.GetComponent<Respawn>();
        if (respawn != null)
        {
            respawn.RespawnObject();
        }
    }
}
