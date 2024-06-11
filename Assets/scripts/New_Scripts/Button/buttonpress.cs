using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonpress : MonoBehaviour
{
    public GameObject objectToTurnOff; // Reference to the object to turn off
    public GameObject objectToTurnOn1, objectToTurnOn2; // Reference to the object to turn on
    private bool cubeon = false;
    private bool sphereon = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Button pressed");

                // Check if the hit object is the button to toggle
                if (hit.collider.gameObject == gameObject)
                {
                    // Toggle the boolean state of the objects
                    if (objectToTurnOff != null)
                    {
                        sphereon = !sphereon;
                        objectToTurnOff.SetActive(sphereon); // Turn off the object
                    }
                    if (objectToTurnOn1 != null && objectToTurnOn2 != null)
                    {
                        cubeon = !cubeon;
                        objectToTurnOn1.SetActive(cubeon);
                        objectToTurnOn2.SetActive(cubeon); // Turn on the object
                    }
                }
            }
        }
    }
}
