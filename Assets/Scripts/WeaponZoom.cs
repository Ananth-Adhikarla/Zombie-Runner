using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float zoomFOV = 25f;
    [SerializeField] float defaultFOV = 60f;

    [SerializeField] float zoomInSpeed = 10f; //zoomInSpeed & zoomOutSpeed are used to provide a controlled & smooth zoom (IN/OUT) transition
    [SerializeField] float zoomOutSpeed = 10f; //REMINDER -> The smooth zoom transition is only aplied on the hold mouse option

    [SerializeField] bool toggleHoldKey = false; //toggles between click to zoom and hold to zoom 

    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;

    bool isZoomed = false;
    Vector3 originalPos;

    [SerializeField] Canvas reticule;
    [SerializeField] GameObject zoomed;
    [SerializeField] GameObject CurrentWeapon;
    RigidbodyFirstPersonController fpsController;

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();

        originalPos = CurrentWeapon.transform.localPosition;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //press TAB to toggle click or hold to zoom options
        {
            toggleHoldKey = !toggleHoldKey;
        }

        if (toggleHoldKey == false)
        {
            ClickToZoom();
        }
        else
        {
            HoldClickToZoom(zoomFOV, zoomInSpeed, defaultFOV, zoomOutSpeed);
        }
    }

    private void ClickToZoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isZoomed == false)
            {
                isZoomed = true;
                reticule.enabled = false;
                Camera.main.fieldOfView = zoomFOV;
                fpsController.mouseLook.XSensitivity = zoomInSensitivity;
                fpsController.mouseLook.YSensitivity = zoomInSensitivity;
                //GameObject.Find("Carbine").transform.localPosition = GameObject.Find("Zoomed").transform.localPosition
                CurrentWeapon.transform.localPosition = zoomed.transform.localPosition;
            }
            else
            {
                isZoomed = false;
                reticule.enabled = true;
                Camera.main.fieldOfView = defaultFOV;
                fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
                fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
                CurrentWeapon.transform.localPosition = originalPos;
            }
        }
    }

    private void HoldClickToZoom(float newZoomFOV, float speedIN, float defZoomFOV, float speedOUT) //smooth zoom transition with controllable speed
    {
        if (Input.GetMouseButton(1))
        {
            reticule.enabled = false;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, newZoomFOV, Time.deltaTime * speedIN);
            CurrentWeapon.transform.localPosition = zoomed.transform.localPosition;
            fpsController.mouseLook.XSensitivity = zoomInSensitivity;
            fpsController.mouseLook.YSensitivity = zoomInSensitivity;
        }
        else
        {
            reticule.enabled = true;
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defZoomFOV, Time.deltaTime * speedOUT);
            CurrentWeapon.transform.localPosition = originalPos;
            fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
            fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
        }
    }




}


