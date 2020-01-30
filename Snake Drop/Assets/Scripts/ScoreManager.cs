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

    private void Awake()
    {
        Score = 0;
    }
}
