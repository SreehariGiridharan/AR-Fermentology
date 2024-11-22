using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LanguageChecker : MonoBehaviour
{
    public GameObject TextEng,TextGer; // The GameObject to deactivate if the language is not English

    void Start()
    {
        // Start the language check
        StartCoroutine(CheckLanguageAndDeactivate());
    }

    private IEnumerator CheckLanguageAndDeactivate()
    {
        // Wait until LocalizationSettings are initialized
        yield return LocalizationSettings.InitializationOperation;

        // Get the current language code (e.g., "en" for English)
        string currentLanguage = LocalizationSettings.SelectedLocale.Identifier.Code;

        Debug.Log("Current Language: " + currentLanguage);

        // Check if the language is not English
        if (currentLanguage != "en")
        {
            if (TextEng != null)
            {
                TextEng.SetActive(false); // Deactivate the target GameObject
                TextGer.SetActive(true); // Activate the German text
                Debug.Log("Target object has been deactivated because the language is not English.");
            }
            else
            {
                Debug.LogError("Target object is not assigned in the Inspector.");
            }
        }
    }
}
