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

    public static void SaveGame(string name)
    {
        CheckPathExists();
        SaveFormatter.Save(defaultPath + name + defaultSuffix, GameManager.instance.GetSaveData());
    }

    public static bool SaveExists(string saveName)
    {
        return File.Exists(defaultPath + saveName + ".planet");
    }

    public static object LoadGame(string name)
    {
        return SaveFormatter.Load(defaultPath + name + ".planet");
    }

    public static void SaveHighScore()
    {
        SaveGame(HighScoreSaveName);
    }
    public static SaveData LoadHighScore()
    {
        if (!SaveExists(HighScoreSaveName)) SaveHighScore();
        return LoadGame(HighScoreSaveName) as SaveData;
    }

    public static void SaveGame()
    {
        int suffix = 0;
        while (SaveExists(defaultSaveName + suffix.ToString())){ suffix += 1; }
        SaveGame(defaultSaveName + suffix.ToString());
    }
    public static string[] GetAllSaveDataPaths()
    {
        CheckPathExists();
        return Directory.GetFiles(defaultPath);
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

    private static void CheckPathExists()
    {
        if (!Directory.Exists(defaultPath)) Directory.CreateDirectory(defaultPath);
    }
}


