using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Pause pause;
    private bool IsPaused { get; set; } = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Menu();
        }
    }

    private void Menu()
    {
        if (!IsPaused)
        {
            menuPanel.SetActive(true);
            IsPaused = true;
            pause.PauseGame();
        }
        else
        {
            menuPanel.SetActive(false);
            IsPaused = false;
            pause.ContinueGame();
        }
    }
}
