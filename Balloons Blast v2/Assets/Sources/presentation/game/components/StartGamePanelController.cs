using TMPro;
using UnityEngine;

public class StartGamePanelController : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    private void Start()
    {
        if (StaticPreferences.IsTimedGame())
        {
            SetupTimedGamePanel();
        }
        else
        {
            SetupFreeGamePanel();
        }
    }

    private void SetupFreeGamePanel()
    {
        title.text = "Свободная игра";
        description.text = "Сбивайте шарики без ограничений!";
    }

    private void SetupTimedGamePanel()
    {
        title.text = "На время";
        description.text = "Сбейте как больше шариков за отведенное время";
    }
}