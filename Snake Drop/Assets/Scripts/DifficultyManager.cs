﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    [HideInInspector]
    private int score;
    public int Score
    {
        get { return score; }
        set {
            score = value;
            ScoreText.text = "Score: " + score.ToString();
        }
    }
    public Text ScoreText;

    public void ResetGame()
    {
        Score = 0;
    }
}