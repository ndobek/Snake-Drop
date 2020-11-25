using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    private int powerupFrequency;
    private int currentProgress;

    public Powerup[] possiblePowerups;
    public Powerup currentPowerup; 

    [SerializeField]
    private Image iconRenderer;
    [SerializeField]
    private Image progressBar;

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
        if(currentPowerup == null) currentPowerup = powerup;
    }

    public void TryActivate()
    {
        if (currentPowerup != null)
        {
            currentPowerup.Activate();
            currentPowerup = null;
        }
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (currentPowerup != null && currentPowerup.sprite != null) iconRenderer.sprite = currentPowerup.sprite;
        if (progressBar != null) progressBar.fillAmount = ProgressPercentage();
    }
    public void UpdateProgress(int amount)
    {
        currentProgress += amount;
        if (ProgressPercentage() >= 1) GetPowerUp();
        UpdateSprite();
    }
    public float ProgressPercentage()
    {
        if (currentPowerup != null) return 1;
        return (float)currentProgress / powerupFrequency;
    }
}
