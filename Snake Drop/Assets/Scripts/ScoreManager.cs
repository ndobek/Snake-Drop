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
        Score = 0;
        NumberOfSnakes = 0;
        ResetRound();
    }

    public void ResetRound()
    {
        Multiplier = 1;
    }

    public void IncreaseScoreUsingMultiplier(int amount)
    {
        Score = score + (amount * multiplier);
    }


}
