using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private void Awake()
    {
        Pause.ContinueGame();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Menu();
        }
    }

    public void Menu()
    {
        if (!Pause.IsPaused)
        {
            menuPanel.SetActive(true);
            Pause.PauseGame();
        }
        else
        {
            menuPanel.SetActive(false);
            Pause.ContinueGame();
        }
    }
}
