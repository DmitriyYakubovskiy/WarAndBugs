using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float radius;

    private void Start()
    {
        if (SaveSystem.Data.ShopItemNames["Magnet"] == 0) gameObject.SetActive(false);

        int level = SaveSystem.Data.ShopItemNames["Magnet"];
        for (int i = 0; i < level; i++)
        {
            radius *= 1.4f;
        }

        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(radius, radius);
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
