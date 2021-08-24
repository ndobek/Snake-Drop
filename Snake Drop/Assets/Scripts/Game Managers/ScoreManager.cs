using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;

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
        set
        {
            highScore = value;
        }
    }

    private float partialScore;
    private float partialMultiplier;



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

    public void IncreaseScorePartially(float amount, bool useMultiplier)
    {
        partialScore += amount;
        int amountToIncrease = 0;

        if (partialScore >= 1)
        {
            float remainder = partialScore % 1;
            amountToIncrease = (int)(partialScore - remainder);
            partialScore = remainder;
        }
        Debug.Log("Amount to Increase: " + amountToIncrease + " partial: " + partialScore);
        IncreaseScore(amountToIncrease, useMultiplier);
    }
    public void IncreaseMultiplierPartially(float amount)
    {
        partialMultiplier += amount;
        int amountToIncrease = 0;

        if (partialMultiplier >= .99f)
        {
            float remainder = partialMultiplier % 1;
            if(remainder >= .99f) remainder = 0;
            amountToIncrease = (int)(partialMultiplier - remainder);
            partialMultiplier = remainder;
        }
        multiplier += amountToIncrease;
    }

    public void IncreaseScore(int amount, bool useMultiplier)
    {
        if (useMultiplier) IncreaseScoreUsingMultiplier(amount);
        else Score += amount;
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
        SubmitHighScore();
    }
    public void SubmitHighScore()
    {
        Leaderboards.High_Score.SubmitScore(Score);
    }

}
