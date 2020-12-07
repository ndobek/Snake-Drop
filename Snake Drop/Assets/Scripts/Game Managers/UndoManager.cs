using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    private Stack<SaveData> moves = new Stack<SaveData>();

    public void Save()
    {
        moves.Push(new SaveData(GameManager.instance));
    }

    public void Undo()
    {
        GameManager.instance.LoadGame(moves.Pop());

    }
}
