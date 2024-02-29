using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float radius;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Magnet") == 0) gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("Magnet"))
        {
            int level = PlayerPrefs.GetInt("Magnet");
            for (int i = 0; i < level; i++)
            {
                radius *= 1.4f;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Magnet", 0);
        }
        gameObject.GetComponent<CapsuleCollider2D>().size=new Vector2 (radius, radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exp")
        {
            collision.GetComponent<Exp>().Check = true;
            collision.GetComponent<Exp>().Time = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Exp")
        {
            var exp=collision.GetComponent<Exp>();
            exp.Check = false;
            exp.Time = exp.StartTime;
        }
    }
}
