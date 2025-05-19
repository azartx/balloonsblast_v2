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
        title.text = "��������� ����";
        description.text = "�������� ������ ��� �����������!";
    }

    private void SetupTimedGamePanel()
    {
        title.text = "�� �����";
        description.text = "������ ��� ������ ������� �� ���������� �����";
    }
}