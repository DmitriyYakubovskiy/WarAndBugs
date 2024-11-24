using UnityEngine;

public class Delete : MonoBehaviour
{
    [SerializeField] BuySystem buySystem;
    public void DeleteSaving()
    {
        buySystem.UpdateGunButtons();
        buySystem.UpdateOtherButtons();
    }
}
