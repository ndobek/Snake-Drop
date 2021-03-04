using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupsDisplayer : MonoBehaviour
{
    public PowerupManager powerupManager;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Image iconRenderer;
    [SerializeField]
    private Image progressBar;

    public void Update()
    {
        if (text != null)
        {
            int num = powerupManager.extraPowerups;
            if (num > 0) text.text = num.ToString();
            else text.text = " ";
        }

        if (iconRenderer != null)
        {
            Powerup currentPowerup = powerupManager.currentPowerup;
            if (powerupManager.currentPowerup != null && currentPowerup.sprite != null) iconRenderer.sprite = currentPowerup.sprite;
        }


        if (progressBar != null) progressBar.fillAmount = FillBarPercentage();
    }


    public float FillBarPercentage()
    {
        //if (manager.currentPowerup != null) return 1;
        return powerupManager.ProgressToNextPowerup();
    }
}
