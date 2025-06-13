using UnityEngine;

public class DeactivateGameObject : MonoBehaviour
{
    private string objectKey;
    public GameObject canvas;

    void Start()
    {
        objectKey = gameObject.name + "_deactivated"; // Unique key for each object

        // Check if this object was previously deactivated
        if (PlayerPrefs.GetInt(objectKey, 0) == 1)
        {
            gameObject.SetActive(false);
            canvas.SetActive(true);
        }
    }

    public void DeactivateObject()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetInt(objectKey, 1); // Save state
        PlayerPrefs.Save(); // Ensure data is written
    }
}
