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
        timerText.text = GetStringTime();
    }

    public string GetStringTime()
    {
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        return string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
