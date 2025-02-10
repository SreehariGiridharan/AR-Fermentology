using UnityEngine;

public class ToggleText : MonoBehaviour
{
    public GameObject object1; // Reference to the first GameObject
    public GameObject object2; // Reference to the second GameObject

    private void Start()
    {
        // Ensure only one object is active at the start
        object1.SetActive(true);
        object2.SetActive(false);
    }

    public void ToggleObjectVisibility()
    {
        // Toggle visibility of the GameObjects
        bool isObject1Active = object1.activeSelf;
        
        // Set the active state of the GameObjects
        object1.SetActive(!isObject1Active);
        object2.SetActive(isObject1Active);
    }
}
