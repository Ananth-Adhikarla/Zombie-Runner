using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float Mindamage = 30f;
    [SerializeField] float Maxdamage = 30f;
    // 0 0.125 14  // 0.03
    [SerializeField] ParticleSystem muzzleFlash;
    // -0.553237 0-2.234365 // 0.025
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    [SerializeField] float timeBetweenShots = 0.5f;
    bool canShoot = true;

    [SerializeField] TextMeshProUGUI ammoText;

    private AudioSource source;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        DisplayAmmo();

        if (Input.GetMouseButton(0) && ammoSlot.GetCurrentAmmo(ammoType) > 0 && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = "Ammo " + currentAmmo.ToString();
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            source.Play();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;

    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }

            target.TakeDamage((int) Random.Range(Mindamage,Maxdamage));
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);

    }
}
