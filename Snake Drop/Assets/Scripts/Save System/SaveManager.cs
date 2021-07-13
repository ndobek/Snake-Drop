using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static string defaultPath = Application.persistentDataPath + "/saves/";
    public static string defaultSaveName = "planet_";
    public static string defaultSuffix = ".planet";
    public static string HighScoreSaveName = "High Score";
    public static string AutoSaveSaveName = "Auto Save";


    public static void SaveGame(string name, SaveData data)
    {
        CheckDirExists();
        SaveFormatter.Save(GetPath(name), data);
    }
    public static void SaveGame(string name)
    {
        SaveGame(name, new SaveData(GameManager.instance));
    }

    public static bool SaveExists(string name)
    {
        return File.Exists(GetPath(name));
    }

    public static SaveData LoadGame(string name)
    {
        return SaveFormatter.Load(GetPath(name)) as SaveData;
    }

    public static void SaveHighScore()
    {
        SaveGame(HighScoreSaveName);
    }
    public static void AutoSave()
    {
        SaveGame(AutoSaveSaveName);
    }
    public static SaveData LoadHighScore()
    {
        if (!SaveExists(HighScoreSaveName)) return null;
        return LoadGame(HighScoreSaveName);
    }
    public static SaveData LoadAutoSave()
    {
        if (!SaveExists(AutoSaveSaveName)) return null;
        return LoadGame(AutoSaveSaveName);
    }


    public static void SaveGame()
    {
        int suffix = 0;
        while (SaveExists(defaultSaveName + suffix.ToString())){ suffix += 1; }
        SaveGame(defaultSaveName + suffix.ToString());
    }
    public static string[] GetAllSaveDataPaths()
    {
        CheckDirExists();
        return Directory.GetFiles(defaultPath);
    }

    public static string[] GetAllSaveNames()
    {
        string[] result = GetAllSaveDataPaths();
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = GetName(result[i]);
        }
        return result;
    }

    public static List<SaveData> LoadAll()
    {
        string[] paths = GetAllSaveDataPaths();
        List<SaveData> result = new List<SaveData>();
        foreach(string path in paths)
        {
            result.Add(SaveFormatter.Load(path) as SaveData);
        }

        return result;
    }

    public static void DeleteSave(string name)
    {
        if (SaveExists(name)) File.Delete(GetPath(name));
    }

    private static void CheckDirExists()
    {
        if (!Directory.Exists(defaultPath)) Directory.CreateDirectory(defaultPath);
    }

    public static string GetPath(string name)
    {
        return defaultPath + name + defaultSuffix;
    }

    public static string GetName(string path)
    {
        string result = path.Replace(defaultPath, "");
        result = result.Replace(defaultSuffix, "");

        return result;
    }

    private static void DeleteAll()
    {
        foreach (string name in GetAllSaveNames()) DeleteSave(name);
    }
}


