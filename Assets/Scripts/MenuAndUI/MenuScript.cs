using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Menu();
        }
    }

    private void Menu()
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
