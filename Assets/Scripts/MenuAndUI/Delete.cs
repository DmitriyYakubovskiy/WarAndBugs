using UnityEngine;

public class Delete : MonoBehaviour
{
    [SerializeField] BuySystem buySystem;
    public void DeleteSaving()
    {
        PlayerPrefs.DeleteAll();
        buySystem.UpdateGunButtons();
        buySystem.UpdateOtherButtons();
    }
}
