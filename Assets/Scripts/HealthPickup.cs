using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] float AddHealth = 30f;

    PlayerHealth playerHealth;


    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerHealth>().RestoreHitpoints(AddHealth);
        Destroy(gameObject);
    }
}
