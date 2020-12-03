using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    private Stack<SaveData> moves = new Stack<SaveData>();

    public void Save()
    {
        moves.Push(GameManager.instance.GetSaveData());
    }

    public void Undo()
    {
        GameManager.instance.LoadGame(moves.Pop());
    }
}
