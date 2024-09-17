using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LocaleSelector : MonoBehaviour
{
    [SerializeField] private int defaultLocaleID = 0; // Set the default language ID (0 can be English or your default language)

    private bool active = false;

    private void Start()
    {
        // Check if a language was previously saved, if not, use the default language
        int ID = PlayerPrefs.GetInt("LocaleKey", defaultLocaleID);
        ChangeLocale(ID);  // Apply the language change
    }

    // Method to change the locale based on a locale ID
    public void ChangeLocale(int localeID)
    {
        if (active)
            return;  // If a language change is in progress, do nothing

        StartCoroutine(SetLocale(localeID));  // Start the language change coroutine
    }

    // Coroutine to set the locale
    private IEnumerator SetLocale(int _localeID)
    {
        active = true;  // Prevent other changes while this is running

        yield return LocalizationSettings.InitializationOperation;  // Wait for the localization system to initialize

        // Set the locale using the locale ID
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

        // Save the selected locale in PlayerPrefs so it persists between sessions
        PlayerPrefs.SetInt("LocaleKey", _localeID);

        active = false;  // Reset the active flag when done
    }
}
