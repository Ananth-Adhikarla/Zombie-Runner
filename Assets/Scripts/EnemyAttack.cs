using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    PlayerHealth target;
    float damage;

    private AudioSource source;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        source = GetComponent<AudioSource>();
    }


    public void AttackHitEvent()
    {
        if(target == null)
        {
            return;
        }
        target.GetComponent<PlayerHealth>().TakeDamage(Random.Range(10,20));
        source.Play();
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }

}
