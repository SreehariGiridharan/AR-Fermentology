using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Dropdown sceneDropdown; // Reference to the Dropdown component

    void Start()
    {
        sceneDropdown.value = 1;
        // Add a listener to call the OnDropdownValueChanged method whenever the dropdown value changes
        sceneDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    // This method is called whenever the dropdown value changes
    void OnDropdownValueChanged(int index)
    {
        switch (index)
        {
            case 0:
                // Load the scene corresponding to the first dropdown option
                SceneManager.LoadScene("5 deg Reaction");
                break;
            case 1:
                // Load the scene corresponding to the second dropdown option
                SceneManager.LoadScene("35 deg Reaction");
                break;
            case 2:
                // Load the scene corresponding to the third dropdown option
                SceneManager.LoadScene("100 deg Reaction");
                break;
            // Add more cases for additional scenes
            default:
                break;
        }
    }
}
