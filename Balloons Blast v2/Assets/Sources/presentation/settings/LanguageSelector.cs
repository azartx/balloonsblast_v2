using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{
    public void ChangeLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
        PlayerPrefs.SetInt("SelectedLanguage", languageIndex);
        PlayerPrefs.Save();
    }
    
    void Start()
    {
        // Load saved language preference
        if (PlayerPrefs.HasKey("SelectedLanguage"))
        {
            int savedLanguage = PlayerPrefs.GetInt("SelectedLanguage");
            ChangeLanguage(savedLanguage);
        }
    }
}