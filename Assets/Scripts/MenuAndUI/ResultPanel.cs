using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Pause pause;
    [SerializeField] private Player player;
    [SerializeField] private Timer timer;
    private bool check=true;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI killsText;

    private void Update()
    {
        pause.PauseGame();
        if (check)
        {
            timeText.text = "Time: " + timer.GetStringTime();
            check = false;
        }
    }
}
