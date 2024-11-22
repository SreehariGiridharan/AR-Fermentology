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
   
    // Coroutine to run the function after a delay
    public bool Temperature100 = false;
    private IEnumerator ActivateAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
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


    // Function to deactivate one object and activate another
    public void ChangeObjects()
    {
        if(Temperature100)
        {
        StartCoroutine(ActivateAfterDelay());
        }
    }
}
