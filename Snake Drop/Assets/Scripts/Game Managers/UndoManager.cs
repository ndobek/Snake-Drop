using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    private Stack<SaveData> moves = new Stack<SaveData>();

    public void Save()
    {
        SaveData data = new SaveData(GameManager.instance);
        moves.Push(data);
        SaveManager.SaveGame(SaveManager.AutoSaveSaveName, data);
    }

    public void Undo()
    {
        if (moves.Count > 0 && GameManager.instance.GameInProgress())
        {
            GameManager.instance.LoadGame(moves.Pop());
        }

    }
}
