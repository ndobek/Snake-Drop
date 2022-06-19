using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplayer : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text scoreTextNewHighScore;
    public GameObject scoreDisplayObj;
    public GameObject scoreDisplayObjHighScore;
    public TMP_Text highScoreText;
    public TMP_Text multiplierText;
    public ScoreManager scoreManager;
 
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
    private bool isHighScoreColorSet = false;
    public void NewHighScore()
    {
        if (scoreDisplayObj != null) scoreDisplayObj.SetActive(false);
        if (scoreDisplayObjHighScore != null) scoreDisplayObjHighScore.SetActive(true);
        scoreText = scoreTextNewHighScore;
        isHighScoreColorSet = true;
    }

    private void Update()
    {
        UpdateScoreDisplay(scoreManager.DelayedScore);
        UpdateMultiplierDisplay(scoreManager.Multiplier);
        UpdateHighScoreDisplay(scoreManager.HighScore);
        if (scoreManager.HighScore < scoreManager.Score && isHighScoreColorSet == false)
        {
            NewHighScore();
                
        }
    }
}
