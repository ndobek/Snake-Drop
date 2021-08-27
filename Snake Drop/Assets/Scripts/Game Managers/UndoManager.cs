using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UndoManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text undosRemainingText;
    private int undosRemaining;
    [SerializeField]
    private bool infiniteUndos;

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
        if (infiniteUndos || undosRemaining > 0)
        {
            bool success = Undo();
            if(success && undosRemaining > 0) undosRemaining -= 1;
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
        undosRemaining += undosPerWatch;
    }

    private void Update()
    {
        undosRemainingText.text = undosRemaining.ToString();
    }
}
