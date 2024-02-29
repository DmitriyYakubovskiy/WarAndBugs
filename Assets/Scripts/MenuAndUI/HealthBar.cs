using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject barObject;
    [SerializeField] private RectTransform healthLine;
    private float maxHealth;
    private float fill=1;

    public float MaxHealth { get=>maxHealth; set => maxHealth = value; }

    public void Awake()
    {
        fill = 1f;
    }

    public void FixedUpdate()
    {
        if (fill == 1f)
        {
            barObject.SetActive(false);
        }
        if (fill != 1f)
        {
            barObject.SetActive(true);
        }

        if (healthLine.GetComponent<Image>().fillAmount != fill)
        {
            healthLine.GetComponent<Image>().fillAmount = fill;
        }
    }

    public void ShowHealth(Entity entity)
    {
        if (entity.Lives >= 0 && entity != null)
        {
            fill = entity.Lives / maxHealth;
        }
    }

    public void DeleteHealthBar()
    {
        Destroy(barObject);
    }
}
