using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public int nextPowerupScoreDiff;
    public int currentProgress;
    public int numOfPowerupsObtained;

    public Powerup[] possiblePowerups;
    public Powerup currentPowerup;
    public int extraPowerups;
    public int numAvailablePowerups => extraPowerups + (currentPowerup != null ? 1 : 0);

    public int PowerupBank
    {
        get { return (currentPowerup != null) ? 1 : 0 + extraPowerups; }
    }

    public void ResetGame()
    {
        currentProgress = 0;
        currentPowerup = null;
        extraPowerups = 0;
        numOfPowerupsObtained = 0;
        GetNextPowerupScore();
    }
    public void GetPowerUp()
    {
        GetPowerUp(possiblePowerups[Random.Range(0, possiblePowerups.Length)]);
    }

    public void GetPowerUp(Powerup powerup)
    {
        if (currentPowerup == null) currentPowerup = powerup;
        else extraPowerups += 1;

        numOfPowerupsObtained += 1;
    }

    public void TryActivate()
    {
        if (currentPowerup != null)
        {
            currentPowerup.Activate();
            currentPowerup = null;
            if (extraPowerups > 0)
            {
                extraPowerups -= 1;
                GetPowerUp();
            }
        }
        //UpdateSprite();
    }

    public void UpdateProgress(int amount)
    {
        currentProgress += amount;
        if (ProgressToNextPowerup() >= 1)
        {
            GetPowerUp();
            currentProgress -= nextPowerupScoreDiff;
            GetNextPowerupScore();
        }
        //UpdateSprite();
    }

    public void GetNextPowerupScore()
    {
        ScoreManager score = GameManager.instance.playerManagers[0].Score;
        GameModeManager gameModeManager = GameManager.instance.GameModeManager;

        nextPowerupScoreDiff = (int)gameModeManager.GetPowerupInfo(score.Score, numOfPowerupsObtained);
    }


    public float ProgressToNextPowerup()
    {
        return (float)currentProgress / nextPowerupScoreDiff;
    }
}
