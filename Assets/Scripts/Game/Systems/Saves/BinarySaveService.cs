using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveService : ISaveService
{
    private const string savesName = "/saves.fun";

    public SaveData Load()
    {
        string path = Application.persistentDataPath + savesName;
        SaveData data;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            File.Create(path).Close();
            data = new SaveData();
        }
        return data;
    }

    public void Save(SaveData data)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + savesName;
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
