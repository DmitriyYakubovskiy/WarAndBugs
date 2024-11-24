using TMPro;
using UnityEngine;

public class MessageWindow : MonoBehaviour
{
    [SerializeField] private GameObject parrent;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private string text;
    public string Text
    {
        get=> text;
        set
        {
            text= value;
            textBox.text = text;
        }
    }

    public void SetText(string ruText, string euText, Color color)
    {   
        textBox.color = color;
        if(SaveSystem.Data.language == "Russian") Text=ruText;
        else Text=euText;
    }

    public void DestroyMessage()
    {
        Destroy(parrent);
    }

    private void Start()
    {
        var windows = GameObject.FindObjectsOfType<MessageWindow>();
        foreach (var window in windows)
        {
            if(window!=this) window.DestroyMessage();
        }
        Destroy(parrent, 2);   
    }
}
