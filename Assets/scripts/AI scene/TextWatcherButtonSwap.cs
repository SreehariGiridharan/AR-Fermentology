using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWatcherButtonSwap : MonoBehaviour
{
    public TMP_InputField inputField; // or public InputField inputField;
    public GameObject buttonWithText;     // Active when there is text
    public GameObject buttonWithoutText;  // Active when there is no text

    void Start()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(OnInputChanged);
            UpdateButtons(inputField.text);
        }
    }

    void OnInputChanged(string value)
    {
        UpdateButtons(value);
    }

    void UpdateButtons(string currentText)
    {
        bool hasText = !string.IsNullOrWhiteSpace(currentText);
        buttonWithText.SetActive(hasText);
        buttonWithoutText.SetActive(!hasText);
    }

    void OnDestroy()
    {
        if (inputField != null)
            inputField.onValueChanged.RemoveListener(OnInputChanged);
    }
}
