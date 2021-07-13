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
            //ScoreText.text = "Score: " + score.ToString();
        }
    }
   // [SerializeField]
   // //private Text ScoreText;
   //// private ScoreDisplayer scoreDisplay;
   // [HideInInspector]
    private int multiplier;
    public int Multiplier
    {
        get { return multiplier; }
        set
        {
            multiplier = value;
            //MultiplierText.text = "Multiplier: " + multiplier.ToString();
        }
    }
    //[SerializeField]
    //private Text MultiplierText;

    private int highScore;
    public int HighScore
    {
        get { return highScore; }
        set {
            //HighScoreText.text = "High Score: " + value; 
            highScore = value; 
        }
    }
    //public Text HighScoreText;



    public int numberOfSnakes;
    public int NumberOfSnakes
    {
        get { return numberOfSnakes; }
        set { numberOfSnakes = value; }
    }

    private void Start()
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
