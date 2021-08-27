﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CloudOnce;

public class UndoManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text undosRemainingText;

    [SerializeField]
    private int undosPerWatch;

    private Stack<SaveData> moves = new Stack<SaveData>();

    public void Save()
    {
        SaveData data = new SaveData(GameManager.instance);
        moves.Push(data);
        SaveManager.SaveGame(SaveManager.AutoSaveSaveName, data);
    }

    public void TryUndo()
    {
        if (CloudVariables.UnlimitedUndos || CloudVariables.Undos > 0)
        {
            bool success = Undo();
            if (success && CloudVariables.Undos > 0)
            {
                CloudVariables.Undos -= 1;
                Cloud.Storage.Save();
            }
        }
    }

    public bool Undo()
    {
        if (moves.Count > 0 && !GameManager.instance.pauseManager.paused)
        {
            GameManager m = GameManager.instance;
            SaveData loadedData = moves.Pop();
            m.LoadGame(loadedData);
            m.playerManagers[0].GameOver = false;
            SaveManager.AutoSave();
            return true;
        }
        return false;

    }

    public void GetUndos()
    {
        CloudVariables.Undos += undosPerWatch;
        Cloud.Storage.Save();
    }

    private void Update()
    {
        if (!CloudVariables.UnlimitedUndos)
        {
            undosRemainingText.text = CloudVariables.Undos.ToString();
        } else
        {
            undosRemainingText.text = "";
        }
    }
}
