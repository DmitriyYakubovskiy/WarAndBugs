using UnityEngine;

public class DieScreen : Sound
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Transform diePanel;
    private float defaultFov;
    private bool check = true;

    private void Awake()
    {
        defaultFov = Camera.main.orthographicSize;
        diePanel=GetComponent<Transform>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) AudioPause();
        else AudioStart();
        if (check)
        {
            Invoke("ShowResultPanel", 3);
            PlaySound(0);
            check = false;
        }
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 3f, 0.2f * Time.deltaTime);
        diePanel.localScale= new(Mathf.MoveTowards(diePanel.localScale.x, 3, 0.2f * Time.deltaTime), Mathf.MoveTowards(diePanel.localScale.y,2, 0.2f * Time.deltaTime));
    }

    public void ShowResultPanel()
    {
        gameObject.SetActive(false);
        resultPanel.SetActive(true);
    }
}
