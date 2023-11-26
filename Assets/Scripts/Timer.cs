using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float time=0;
    private float _timeLeft = 0f;

    private void Start()
    {
        _timeLeft = time;
    }

    private void Update()
    {
        _timeLeft += Time.deltaTime;
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
