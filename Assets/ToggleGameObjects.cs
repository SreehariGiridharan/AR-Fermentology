using UnityEngine;

public class ToggleGameObjects : MonoBehaviour
{
    public GameObject Text1,Text2,Text3,Formula1,Formula2,Formula3; // Reference to the first GameObject
   


    void Start()
    {
        // Initially set the states of the GameObjects
        Text1.SetActive(true);
        Text2.SetActive(true);
        Text3.SetActive(true);
        Formula1.SetActive(false);
        Formula2.SetActive(false);
        Formula3.SetActive(false);
    }

    public void Swapping()
    {
        
        Text1.SetActive(!Text1.activeSelf);
        Text2.SetActive(!Text2.activeSelf);
        Text3.SetActive(!Text3.activeSelf);
        Formula1.SetActive(!Formula1.activeSelf);
        Formula2.SetActive(!Formula2.activeSelf);
        Formula3.SetActive(!Formula3.activeSelf);

    }
}
