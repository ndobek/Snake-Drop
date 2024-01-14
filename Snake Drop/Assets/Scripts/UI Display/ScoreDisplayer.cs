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
    public void UpdateMultiplierDisplay(int multiplier, int multiplierTwo = 1)
    {
        string text = "x" + multiplier.ToString();
        if (multiplierTwo != 1) text += " x" + multiplierTwo.ToString();
        multiplierText.SetText(text);
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
        UpdateMultiplierDisplay(scoreManager.Multiplier, scoreManager.MultiplierTwo);
        UpdateHighScoreDisplay(scoreManager.HighScore);
        if (scoreManager.HighScore < scoreManager.Score && isHighScoreColorSet == false)
        {
            NewHighScore();
                
        }
    }
}
