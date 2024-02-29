using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Player player;
    [SerializeField] private Timer timer;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI killsText;
    private bool check=true;

    private void Update()
    {
        Pause.PauseGame();
        if (check)
        {
            timeText.text = LanguageManager.TranslateText("Time: ") + timer.GetStringTime();
            check = false;
        }
    }

    private void OnDestroy()
    {
        Pause.ContinueGame();
    }
}
