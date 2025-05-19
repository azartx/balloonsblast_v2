using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro; 

[RequireComponent(typeof(TMP_Dropdown))]
public class LanguageDropdown : MonoBehaviour
{
    private void Start()
    {
        var dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(ChangeLanguage);
        
        // Initialize dropdown options based on available locales
        dropdown.ClearOptions();
        dropdown.AddOptions(
            LocalizationSettings.AvailableLocales.Locales
                .ConvertAll(locale => locale.name)
        );
        
        // Set current selection
        dropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(
            LocalizationSettings.SelectedLocale
        );
    }

    private void ChangeLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = 
            LocalizationSettings.AvailableLocales.Locales[index];
    }
}