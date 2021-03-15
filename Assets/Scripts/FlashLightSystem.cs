using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FlashLightSystem : MonoBehaviour
{

    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;

    bool lightOn = true;

    Light myLight;
    [SerializeField] TextMeshProUGUI flashlight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        if(lightOn == true)
        {
            flashlight.text = "ON".ToString();
            DecreaseLightAngle();
            DecreaseLightIntensity();
        }
        else
        {
            
            flashlight.text = "OFF".ToString();
        }
        FlashlightSwitch();

    }

    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }

    public void AddLightIntensity(float intensityAmount)
    {
        myLight.intensity += intensityAmount;
    }

    private void DecreaseLightAngle()
    {
        if(myLight.spotAngle <= minimumAngle)
        {
            return;
        }
        else
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }

    private void FlashlightSwitch()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (lightOn == false)
            {
                myLight.enabled = true;
                lightOn = true;

            }
            else
            {
                myLight.enabled = false;
                lightOn = false;
            }
        }
    }

}
