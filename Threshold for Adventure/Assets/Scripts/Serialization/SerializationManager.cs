using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SerializationManager 
{
    public static bool Save(string saveName, object saveData)
    {
        //Converts to string then saves as text file
        string JSONString = JsonUtility.ToJson(saveData);

        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        File.WriteAllText(Application.persistentDataPath + "/saves/" + saveName + ".save", JSONString);

        return true;
    }

    public static SaveData Load(string path)
    {
        //Checks file exists, then deserializes it and returns original format
        if (!File.Exists(path)){ return null; }

        string JSONString = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(JSONString);
        return saveData;
    }
}
