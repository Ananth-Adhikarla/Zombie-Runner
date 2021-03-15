using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    float hitPoints = 100f;
    [SerializeField] TextMeshProUGUI hp;

    private void Start()
    {
        hp.text = "HP  " + hitPoints.ToString();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if(hitPoints < 0)
        {
            hp.text = 0.ToString();
        }
        else
        {
            hp.text = "HP  " + hitPoints.ToString();
        }
        
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }

    public void RestoreHitpoints(float restore)
    {
        if(hitPoints < 100f)
        {
            hitPoints += restore;
            if(hitPoints > 100f)
            {
                hitPoints = 100f;
                hp.text = "HP  " + hitPoints.ToString();
            }
            else
            {
                hp.text = "HP  " + hitPoints.ToString();
            }
            
        }
        else
        {
            return;
        }
        
    }
}
