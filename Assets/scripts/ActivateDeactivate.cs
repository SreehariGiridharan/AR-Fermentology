using UnityEngine;

public class ActivateDeactivate : MonoBehaviour
{
    public GameObject targetObject; // Reference to the GameObject you want to activate or deactivate

    private void Update()
    {
        // Example: Toggle the GameObject with the 'T' key
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleGameObject();
        }
    }

    public void ToggleGameObject()
    {
        // Toggle the active state of the target GameObject
        targetObject.SetActive(!targetObject.activeSelf);
    }

    // Optional method to activate the GameObject
    public void ActivateObject()
    {
        targetObject.SetActive(true);
    }

    // Optional method to deactivate the GameObject
    public void DeactivateObject()
    {
        targetObject.SetActive(false);
    }
}
