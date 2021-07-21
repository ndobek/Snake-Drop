using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupsDisplayer : MonoBehaviour
{
    public PowerupManager powerupManager;
    [SerializeField]
    private AnimationCurve ProgressAniCurve;
    [SerializeField]
    private float ProgressAniDuration;
    [SerializeField]
    private AnimationCurve QuickProgressAniCurve;
    [SerializeField]
    private float QuickProgressAniDuration;

    [SerializeField]
    private TMP_Text textObject;
    [SerializeField]
    private string text;
    [SerializeField]
    private Image iconRenderer;
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private Image quickProgressBar;
    [SerializeField]
    private GameObject powerupButton;
    private UIFade powerupFader;

    [SerializeField]
    private UIFade mustUseIndicator;

    private float lastFramePercentage;

    void Awake()
    {
        powerupFader = powerupButton.GetComponent<UIFade>();
    }

    private void Start()
    {
        powerupFader.SetFadedOut();
        lastFramePercentage = FillBarPercentage();
    }
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

        float percentage = FillBarPercentage();
        if (quickProgressBar != null && percentage != lastFramePercentage) StartCoroutine(tweenFill(quickProgressBar, percentage, QuickProgressAniDuration, QuickProgressAniCurve));
        if (progressBar != null && percentage != lastFramePercentage) StartCoroutine(tweenFill(progressBar, percentage, ProgressAniDuration, ProgressAniCurve));

        if (powerupButton != null)
        {
            powerupFader.SetFade(powerupManager.numAvailablePowerups > 0);
        }

        if (mustUseIndicator != null)
        {
            PlayerManager player = GameManager.instance.playerManagers[0];
            if (player.GameInProgress && !player.RoundInProgress)
            {
                mustUseIndicator.SetFade(!player.entranceManager.CheckForValidEntrancesToGrid(player, player.playGrid) && player.Powerup.currentPowerup != null);
            }
        }

        lastFramePercentage = percentage;
    }

    private IEnumerator tweenFill(Image bar, float target, float duration, AnimationCurve curve)
    {
        float startValue = bar.fillAmount;
        float targetValue = target;

        float startTime = Time.time;
        float percentageComplete = (Time.time - startTime) / duration;

        while (true)
        {
            percentageComplete = (Time.time - startTime) / duration;
            bar.fillAmount = Mathf.Lerp(startValue, targetValue, curve.Evaluate(percentageComplete));

            if (percentageComplete >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        progressBar.fillAmount = targetValue;

    }

    public float FillBarPercentage()
    {
        //if (manager.currentPowerup != null) return 1;
        return powerupManager.ProgressToNextPowerup();
    }
}
