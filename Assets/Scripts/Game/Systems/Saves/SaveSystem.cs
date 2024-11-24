using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public partial class SaveData
{

    public Dictionary<string, int> ShopItemNames = new Dictionary<string, int>()
    {
        { "Grenade", 0 } ,
        { "Dash" , 0 },
        { "GrenadeBag", 0 },
        { "Magnet", 0 },

        {"Gun", 1 },
        {"Ar", 0 },
        {"FAMAS", 0 },
        {"ShotGun", 0 },
        {"SIX12", 0 },
        {"Vector", 0 },
        {"LMG", 0 },
        {"AKLONG", 0 },
        {"BMG", 0 },
        {"Arc", 0 },
        {"Flamethrower", 0 },

        {"Drone", 0 },
        {"LaserDrone", 0 },
    };
    public Dictionary<string, string> SelectedItemNames = new Dictionary<string, string>()
    {
        {"selectedGun", "Gun" },
        {"selectedDrone", "" }
    };
    

    public int money = 150;
    public float coefficientOfDifficulty = 1;

    public const float kValueVolume = 20f;
    public float musicVolume = Mathf.Log10(0.5f)*kValueVolume;
    public float soundVolume = Mathf.Log10(0.5f) * kValueVolume;
    public string language = "Russia";

    public SaveData() { }
}

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private ISaveService service;
    private const float AutoSaveInterval = 5f;
    private bool isAutoSave = true;
    private float time = 0;

    public static SaveData Data;

    private void Awake()
    {
        Data = new SaveData();
        service = new BinarySaveService();
        Load();
    }

    private void Update()
    {
        if (isAutoSave)
        {
            if (time <= 0)
            {
                time = AutoSaveInterval;
                Save();
            }
            time -= Time.deltaTime;
        }
    }

    public void Save()
    {
        service.Save(Data);
    }

    public void ResetProgress()
    {
        var emptyData = new SaveData();
        service.Save(emptyData);
        Load();
        Scenes.Restart();
    }

    private void Load()
    {
        Data = service.Load();
    }

    private void OnDestroy()
    {
        Save();
    }
}
