using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/saves")) Directory.CreateDirectory(Application.persistentDataPath + "/saves");

        string path = Application.persistentDataPath + "/saves/" + saveName + ".planet";

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        return new BinaryFormatter();
    }

}
