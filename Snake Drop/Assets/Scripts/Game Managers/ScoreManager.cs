using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
           GameManager.instance.playerManagers[0].Powerup.UpdateProgress(value - score);
             score = value;
        }
    }
    private int multiplier;
    public int Multiplier
    {
        get { return multiplier; }
        set
        {
            multiplier = value;
        }
    }

    private int highScore;
    public int HighScore
    {
        get { return highScore; }
        set {
            highScore = value; 
        }
    }



    public int numberOfSnakes;
    public int NumberOfSnakes
    {
        get { return numberOfSnakes; }
        set { numberOfSnakes = value; }
    }

    private void Awake()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        UpdateScore();
        Score = 0;
        NumberOfSnakes = 0;
        ResetMultiplier();
    }

    public void ResetMultiplier()
    {
        Multiplier = 1;
    }

    public void IncreaseScoreUsingMultiplier(int amount)
    {
        Score = score + (amount * multiplier);
    }

    public void UpdateScore()
    {
        SaveData oldScore = SaveManager.LoadHighScore();
        if (oldScore != null && oldScore.playerData != null && oldScore.playerData.score != null)
        {
            HighScore = oldScore.playerData.score.score;
        }
        else
        {
            HighScore = 0;
        }
        if (score > HighScore)
        {
            SaveManager.SaveHighScore();
        }
    }

}
