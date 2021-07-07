using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    private int powerupFrequency;
    public int currentProgress;

    public Powerup[] possiblePowerups;
    public Powerup currentPowerup;
    public int extraPowerups;
    public int numAvailablePowerups => extraPowerups + (currentPowerup != null? 1:0);

    public int PowerupBank
    {
        get{return (currentPowerup != null) ? 1: 0 + extraPowerups;}
    }

    public void ResetGame()
    {
        currentProgress = 0;
    }
    public void GetPowerUp()
    {
        GetPowerUp(possiblePowerups[Random.Range(0, possiblePowerups.Length)]);
    }

    public void GetPowerUp(Powerup powerup)
    {
        if (currentPowerup == null) currentPowerup = powerup;
        else extraPowerups += 1;
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
            currentProgress -= powerupFrequency;
        }
        //UpdateSprite();
    }
    public float ProgressToNextPowerup()
    {
        return (float)currentProgress / powerupFrequency;
    }
}
