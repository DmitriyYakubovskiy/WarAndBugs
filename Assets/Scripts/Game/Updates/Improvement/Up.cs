using UnityEngine;

public abstract class Up : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected float k;
    private string upgradesName;
    public string UpgradesName { get => upgradesName; set=> upgradesName=value; }
}
