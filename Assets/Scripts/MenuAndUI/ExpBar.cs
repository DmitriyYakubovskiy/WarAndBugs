using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private RectTransform purpleLine;
    [SerializeField] private Player player;
    private float maxExp = 100;
    private float fill=0;

    public void FixedUpdate()
    {
        if (purpleLine.GetComponent<Image>().fillAmount != fill)
        { 
            if (fill >= 1) purpleLine.GetComponent<Image>().fillAmount = 0;
            else if (fill < 0) purpleLine.GetComponent<Image>().fillAmount = 0;
            else purpleLine.GetComponent<Image>().fillAmount = fill;
        }
        if (fill >= 1)
        {
            player.Level += 1;
            player.Exp -= maxExp;
            maxExp += 35;
        }
    }    
    
    public float SetMaxExp(float maxExp)
    {
        return this.maxExp = maxExp;
    }

    public float GetMaxExp()
    {
        return maxExp;
    }

    public void ShowExp()
    {
        if (player != null)
        {
            fill = (float)player.Exp / maxExp;
        }
    }

    public void DeleteHealthBar()
    {
        Destroy(gameObject);
    }
}
