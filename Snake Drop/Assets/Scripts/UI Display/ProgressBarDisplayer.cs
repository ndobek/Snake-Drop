using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarDisplayer : MonoBehaviour
{
    public RectTransform progressGraphic;
    public RectTransform secondaryProgressGraphic;
    public GameObject progressGraphicObj;
    public GameObject secondaryProgressGraphicObj;
    public GameObject powerupButton;

    private float leftPos;
    private float fullPos;
    private float posChange;
   
    private Vector2 rectTransformUpdate; 
    private float xCoord;
    private float yCoord;

    public PowerupManager powerupManager;
    private bool isBanked = false;
    
    

    float GetProgressPercent()
    {
        return powerupManager.ProgressToNextPowerup();
    }
    void initializeProgressRect(RectTransform newRect)
    {
        leftPos = newRect.rect.xMin;
        fullPos = newRect.rect.center.x;
        posChange = (fullPos - leftPos) * 2;

    }

    void ProgressDisplayUpdate(RectTransform graphic)
    {
        xCoord = fullPos - posChange + (posChange * GetProgressPercent());
        rectTransformUpdate = new Vector2(xCoord, yCoord);
        graphic.anchoredPosition = rectTransformUpdate;
    
    }
    void DisplayBanked()
    {
        progressGraphicObj.SetActive(false);
        secondaryProgressGraphicObj.SetActive(true);
        powerupButton.SetActive(true);
        isBanked = true;

    }
    void DisplayNoneBanked()
    {
        progressGraphicObj.SetActive(true);
        secondaryProgressGraphicObj.SetActive(false);
        powerupButton.SetActive(false);
        isBanked = false;
    }    
    private void Start()
    {
        initializeProgressRect(progressGraphic);
    }
    private void Update()
    {

        if (isBanked == false)
        {
            ProgressDisplayUpdate(progressGraphic);
            if (powerupManager.PowerupBank > 0)
            {
                DisplayBanked();
            }
        }
        else
        {
            ProgressDisplayUpdate(secondaryProgressGraphic);
            if (powerupManager.PowerupBank == 0)
            {
                DisplayNoneBanked();
            }
        }
        
    }

}
