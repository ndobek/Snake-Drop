using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public CheckpointManager checkpointManager;
    [HideInInspector]
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if(checkpointManager != null) checkpointManager.UpdateScore(score);
            ScoreText.text = "Score: " + score.ToString();
        }
    }
    [SerializeField]
    private Text ScoreText;

    [HideInInspector]
    private int multiplier;
    public int Multiplier
    {
        get { return multiplier; }
        set
        {
            multiplier = value;
            MultiplierText.text = "Multiplier: " + multiplier.ToString();
        }
    }
    [SerializeField]
    private Text MultiplierText;

    private int highScore;
    public int HighScore
    {
        get { return highScore; }
        set { HighScoreText.text = "High Score: " + value; highScore = value; }
    }
    public Text HighScoreText;



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
        checkpointManager.ResetGame();
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
        HighScore = oldScore.score;
        if (score > HighScore)
        {
            SaveManager.SaveHighScore();
        }
        //SaveScore();
    }
}
