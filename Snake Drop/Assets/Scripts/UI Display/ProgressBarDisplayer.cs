using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarDisplayer : MonoBehaviour
{
    //TODO: make it so the collider positions update with the progress bar position
    //when the screen is resized

    public Camera cam;
    public Image progressGraphic;
    public GameObject powerupButton;
    public RectTransform progressBackground;

    public GameObject particleColliderObj;
    public GameObject particleMagnetFieldObj;

    public PowerupManager powerupManager;
    private bool isBanked = false;
    
    void UpdateColliderPos(RectTransform background, Camera cam)
    {
        Vector3 pos = cam.ScreenToWorldPoint(background.transform.position);
        particleColliderObj.transform.position = pos;
        particleMagnetFieldObj.transform.position = pos;
        
    }
    private void Start()
    {
        UpdateColliderPos(progressBackground, cam);
    }
    private void Update()
    {
        progressGraphic.fillAmount = powerupManager.ProgressToNextPowerup();
        powerupButton.SetActive(powerupManager.numAvailablePowerups > 0);
    }

}
