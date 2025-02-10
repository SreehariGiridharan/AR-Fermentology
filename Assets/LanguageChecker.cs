using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChecker : MonoBehaviour
{
    public GameObject TextEng, TextGer; // GameObjects for English and German text

    void Update()
    {
        // Ensure LocalizationSettings are initialized and the language check hasn't been performed yet
    
        CheckLanguageAndDeactivate();
        
    }

    private void CheckLanguageAndDeactivate()
    {
        // Get the current language code (e.g., "en" for English)
        string currentLanguage = LocalizationSettings.SelectedLocale.Identifier.Code;

        Debug.Log("Current Language: " + currentLanguage);

        // Check if the language is not English
        if (currentLanguage != "en")
        {
            
            
                TextEng.SetActive(false); // Deactivate English text
                TextGer.SetActive(true);  // Activate German text
                Debug.Log("English text deactivated, German text activated.");
        }
            
            else
        {
            
                TextEng.SetActive(true); // Deactivate English text
                TextGer.SetActive(false); 
            
        }
    }
}
