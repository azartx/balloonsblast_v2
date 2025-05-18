using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameNavEvents : MonoBehaviour
{
    
    void Start()
    {
        // Back buttun is not needed?
        //Button button = GetComponent<Button>();
        //if (button != null)
        //{
        //    button.onClick.AddListener(OnBackButtonClicked);
        //}
        //else
        //{
        //    Debug.LogError("Button reference is not set in the inspector.");
        //}
    }
    
    void Update()
    {
        // Проверяем нажатие на кнопку "назад" на мобильном устройстве
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == Screens.MENU)
            {
                Application.Quit();
            }
            else if (sceneIndex == Screens.GAME)
            {
                SceneManager.LoadScene(Screens.MENU);
            }
        }
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene(Screens.MENU);
    }
}