using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupsDisplayer : MonoBehaviour
{
    public PowerupManager powerupManager;

    [SerializeField]
    private TMP_Text textObject;
    [SerializeField]
    private string text;
    [SerializeField]
    private Image iconRenderer;
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private GameObject powerupButton;

    public void Update()
    {
        if (textObject != null)
        {
            int num = powerupManager.extraPowerups;
            if (num > 0) textObject.text = text + " x" + (num + 1).ToString();
            else textObject.text = text;
        }

        if (iconRenderer != null)
        {
            Powerup currentPowerup = powerupManager.currentPowerup;
            if (powerupManager.currentPowerup != null && currentPowerup.sprite != null) iconRenderer.sprite = currentPowerup.sprite;
        }


        if (progressBar != null) progressBar.fillAmount = FillBarPercentage();
        
        if(powerupButton != null) powerupButton.SetActive(powerupManager.numAvailablePowerups > 0);
    }


    public float FillBarPercentage()
    {
        //if (manager.currentPowerup != null) return 1;
        return powerupManager.ProgressToNextPowerup();
    }
}
