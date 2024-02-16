using UnityEngine;

public class Magnet : MonoBehaviour
{
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
