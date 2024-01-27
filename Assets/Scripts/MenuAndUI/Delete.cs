using UnityEngine;

public class Delete : MonoBehaviour
{
    public void DeleteSaving()
    {
        PlayerPrefs.DeleteAll();
    }
}
