using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_changer : MonoBehaviour
{
    public GameObject objectToDeactivate; // Object to deactivate
    public GameObject objectToActivate;   // Object to activate
    public GameObject Solution_notification; // Reference to the Solution_notification component
    public float delay = 10.0f;            // Time delay in seconds

    // Start is called before the first frame update

   
    public void SolutionShower()
    {
        // Wait for the specified delay
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogError("No object assigned to deactivate.");
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogError("No object assigned to activate.");
        }
        Solution_notification.SetActive(true);

        // Call the function to change objects
        
    }

}
