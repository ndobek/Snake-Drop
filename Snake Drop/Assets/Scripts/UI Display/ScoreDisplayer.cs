using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplayer : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text multiplierText;
    //public ScoreManager scoreManager;
    public Color32 highScoreColor;

    public void UpdateScoreDisplay(int score)
    {
        scoreText.SetText(score.ToString());
    }
    public void UpdateMultiplierDisplay(int multiplier)
    {
        multiplierText.SetText("x " + multiplier.ToString());
    }
    public void UpdateHighScoreDisplay(int highScore)
    {
        highScoreText.SetText("High Score: " + highScore.ToString());
    }
    public void NewHighScore()
    {
        scoreText.color = highScoreColor;
    }
}
