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
            score = value;
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
    }

    public void ResetMultiplier()
    {
        Multiplier = 1;
    }

    public void IncreaseScoreUsingMultiplier(int amount)
    {
        Score = score + (amount * multiplier);
    }

    private void SaveScore()
    {
        //PlayerPrefs.SetInt("High Score", score);
        SaveFormatter.Save("High Score", GameManager.instance.SaveGame());
    }

    private int LoadScore()
    {
        //return PlayerPrefs.GetInt("High Score");
        SaveData highScoreSave = SaveFormatter.LoadByName("High Score") as SaveData;

        if (highScoreSave != null) return highScoreSave.score;
        else return 0;
    }
    public void UpdateScore()
    {
        HighScore = LoadScore();
        if (score > HighScore)
        {
            SaveScore();
        }
        //SaveScore();
    }
}
