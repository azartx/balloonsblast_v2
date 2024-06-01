using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameNavEvents : MonoBehaviour
{
    
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnBackButtonClicked);
        }
        else
        {
            Debug.LogError("Button reference is not set in the inspector.");
        }
    }
    
    private void OnBackButtonClicked()
    {
        SceneManager.LoadScene(Screens.MENU);
    }
}