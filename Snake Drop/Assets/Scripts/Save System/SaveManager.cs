using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static string defaultSaveName = "planet_";
    public static string HighScoreSaveName = "High Score";

    public static void SaveGame(string name)
    {
        SaveFormatter.Save(name, GameManager.instance.SaveGame());
    }

    public static void SaveHighScore()
    {
        SaveGame(HighScoreSaveName);
    }
    public static SaveData LoadHighScore()
    {
        if (!SaveFormatter.SaveExists(HighScoreSaveName)) SaveHighScore();
        return SaveFormatter.LoadByName(HighScoreSaveName) as SaveData;
    }

    public static void SaveGame()
    {
        int suffix = 0;
        while (!SaveFormatter.SaveExists(defaultSaveName + suffix.ToString())){ suffix += 1; }
        SaveGame(defaultSaveName + suffix.ToString());
    }

}


