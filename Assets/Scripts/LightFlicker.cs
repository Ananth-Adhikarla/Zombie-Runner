using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public bool isFlickering = false;
    public bool isEmissive = false;
    public float timeDelay;
    public GameObject lightbulb;
    Renderer bulb;
    private void Start()
    {
        bulb = this.lightbulb.GetComponent<Renderer>();
    }

    void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        isEmissive = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        bulb.material.SetColor("_EmissionColor", Color.black);
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        bulb.material.SetColor("_EmissionColor", Color.yellow);
        timeDelay = timeDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
        isEmissive = true;
    }

}
