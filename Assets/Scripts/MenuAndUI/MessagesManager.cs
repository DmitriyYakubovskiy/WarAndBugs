using TMPro;
using UnityEngine;

public class MessagesManager : Sound
{
    [SerializeField] private TextMeshProUGUI text;

    public void ShowMessage(string message, int? value = null)
    {
        message=LanguageManager.TranslateText(message);
        gameObject.SetActive(true);
        if(value==null) text.text = message;
        else text.text = $"{message}{value}";
        PlaySound(0,volume);    
        Invoke("DisableManager", 3);
    }

    private void DisableManager()
    {
        gameObject.SetActive(false);
    }


}
