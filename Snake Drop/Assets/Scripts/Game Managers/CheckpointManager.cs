using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private GridAction OnCheckpoint;
    public int CheckpointFrequency;
    private int nextCheckpoint;
    public bool CheckpointDue = false;
    public Image ProgressBar;

    private int previousScore;


    private void Awake()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        nextCheckpoint = CheckpointFrequency;
        CheckpointDue = false;
    }

    public void UpdateScore(int newScore)
    {
        if (previousScore < nextCheckpoint && newScore >= nextCheckpoint && CheckpointFrequency != 0) CheckpointReached();
        previousScore = newScore;
        UpdateProgressBar();
    }

    public void CheckpointReached()
    {
        nextCheckpoint += CheckpointFrequency;
        CheckpointDue = true;
        //OnCheckpointReached.Invoke(GameManager.instance.playerManagers[0].playGrid);
    }

    public void TryCheckpoint()
    {
        if (CheckpointDue)
        {
            OnCheckpoint.Invoke(GameManager.instance.playerManagers[0].playGrid);
        }
        CheckpointDue = false;
    }

    public void UpdateProgressBar()
    {
        if(ProgressBar != null) ProgressBar.fillAmount = (float)(previousScore - (nextCheckpoint - CheckpointFrequency)) / (nextCheckpoint - (nextCheckpoint - CheckpointFrequency)) ;
    }

}
