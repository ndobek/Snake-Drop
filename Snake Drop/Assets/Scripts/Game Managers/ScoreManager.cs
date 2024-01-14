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
    public int DelayedScore
    {
        get { return score - totalDelay(); }
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
    [HideInInspector]
    public int scoreAtLastCrash;
    [HideInInspector]
    public int numberOfSnakes;
    public int NumberOfSnakes
    {
        get { return numberOfSnakes; }
        set { numberOfSnakes = value; }
    }
    private float timeSinceAllScoresFinalized;
    public float maxTimeWithoutScoresFinalized = 10;


    public void ResetGame()
    {
        FinalizeAllScores();
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

    public void IncreaseScore(int amount, bool useMultiplier, GameObject delayReferenceObject = null)
    {
        int finalAmount = amount;
        if (useMultiplier) finalAmount *= multiplier;
        if (delayReferenceObject != null) DelayScore(delayReferenceObject, finalAmount);
        Score += finalAmount;
    }

    public void IncreaseScoreUsingMultiplier(int amount)
    {
        IncreaseScore(amount, true);
    }

    public void UpdateScore()
    {
        //SaveData oldScore = SaveManager.LoadHighScore();
        //if (oldScore != null && oldScore.playerData != null && oldScore.playerData.score != null)
        //{
        //    HighScore = oldScore.playerData.score.score;
        //}
        //else
        //{
        //    HighScore = 0;
        //}
        SubmitHighScore();
        HighScore = CloudVariables.HighScore;
    }
    public void SubmitHighScore()
    {
        PlayerManager p = GameManager.instance.playerManagers[0];
        Leaderboards.High_Score.SubmitScore(Score);
        Leaderboards.Highest_Stage.SubmitScore(p.Powerup.numOfPowerupsUsed + 1);
        if (p.Powerup.numOfPowerupsUsed == 0) Leaderboards.Highest_Score_Before_Board_Clear.SubmitScore(Score);
        CloudVariables.HighScore = score;

        CheckAchievements();
    }
    public void OnCrash()
    {
        Leaderboards.Highest_Single_Combo.SubmitScore(Score - scoreAtLastCrash);
        scoreAtLastCrash = score;
    }


    private Dictionary<GameObject, int> delayedScores = new Dictionary<GameObject, int>();

    public void DelayScore(GameObject referenceObject, int amount)
    {
        if (!delayedScores.ContainsKey(referenceObject))
        {
            delayedScores.Add(referenceObject, amount);
        }
    }

    public void FinalizeScore(GameObject referenceObject)
    {
        if (delayedScores.ContainsKey(referenceObject))
        {
            delayedScores.Remove(referenceObject);
        }
    }

    public void FinalizeAllScores()
    {
        Dictionary<GameObject, int>.KeyCollection keys = delayedScores.Keys;
        List<GameObject> keyList = new List<GameObject>();
        foreach (GameObject key in keys)
        {
            keyList.Add(key);
        }
        foreach(GameObject key in keyList)
        {
            FinalizeScore(key);
        }
    }

    public int totalDelay()
    {
        Dictionary<GameObject, int>.ValueCollection values = delayedScores.Values;
        int result = 0;
        foreach (int value in values)
        {
            result += value;
        }
        return result;
    }

    private void Update()
    {
        if(totalDelay() == 0) { timeSinceAllScoresFinalized = 0; }
        else { timeSinceAllScoresFinalized += Time.deltaTime; }

        if(timeSinceAllScoresFinalized > maxTimeWithoutScoresFinalized) { FinalizeAllScores(); }
    }

    private void CheckAchievements()
    {
        PlayerManager p = GameManager.instance.playerManagers[0];
        int stage = p.Powerup.numOfPowerupsUsed + 1;

        if (stage >= 2) CloudOnce.Achievements.stageTwo.Unlock();
        if (stage >= 5) CloudOnce.Achievements.stageFive.Unlock();
        if (stage >= 10) CloudOnce.Achievements.stageTen.Unlock();
        if (stage >= 15) CloudOnce.Achievements.stageFifteen.Unlock();
        if (stage >= 20) CloudOnce.Achievements.stageTwenty.Unlock();
        if (stage >= 26) CloudOnce.Achievements.stageFinal.Unlock();

        if (score > 1319897) CloudOnce.Achievements.beatMe.Unlock();
    }

}
