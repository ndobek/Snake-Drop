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
    private Animator mustUseIndicator;
    [SerializeField]
    private float powerupFlareDuration;

    private float lastFramePercentage;
    private int lastFramePowerups;
    private bool wait;
    private int tweensRunning;

    void Awake()
    {
        powerupFader = powerupButton.GetComponent<UIFade>();
    }

    private void Start()
    {

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
        int powerups = powerupManager.numAvailablePowerups;
        if (!wait)
        {
            if (powerups > lastFramePowerups)
            {
                ResetAnimations();
                StartCoroutine(tweenGetPowerup(percentage));
            }
            else if (percentage > lastFramePercentage)
            {
                ResetAnimations();
                if (quickProgressBar != null) StartCoroutine(tweenFill(quickProgressBar, percentage, QuickProgressAniDuration, QuickProgressAniCurve));
                if (progressBar != null) StartCoroutine(tweenFill(progressBar, percentage, ProgressAniDuration, ProgressAniCurve));
            }
            else if (percentage < lastFramePercentage)
            {
                //Durations reversed
                ResetAnimations();
                if (quickProgressBar != null) StartCoroutine(tweenFill(quickProgressBar, percentage, ProgressAniDuration, ProgressAniCurve));
                if (progressBar != null) StartCoroutine(tweenFill(progressBar, percentage, QuickProgressAniDuration, QuickProgressAniCurve));
            }
        }

        if (tweensRunning == 0)
        {
            progressBar.fillAmount = percentage;
            quickProgressBar.fillAmount = percentage;
        }

        PlayerManager player = GameManager.instance.playerManagers[0];

        if (powerupButton != null)
        {
            powerupFader.Fade(powerupManager.numAvailablePowerups > 0 && player.GameInProgress);
        }

        if (mustUseIndicator != null)
        {

            if (player.GameInProgress)
            {
                mustUseIndicator.SetBool("Must Use", !player.RoundInProgress && !player.entranceManager.CheckForValidEntrancesToGrid(player, player.playGrid) && player.Powerup.currentPowerup != null);
                //mustUseIndicator.SetFade(!player.RoundInProgress && !player.entranceManager.CheckForValidEntrancesToGrid(player, player.playGrid) && player.Powerup.currentPowerup != null);
            }
        }

        lastFramePercentage = percentage;
        lastFramePowerups = powerups;
    }

    private void ResetAnimations(){
        tweensRunning = 0;
        StopAllCoroutines();
    }

    private IEnumerator tweenFill(Image bar, float target, float duration, AnimationCurve curve)
    {
        tweensRunning += 1;
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

        bar.fillAmount = targetValue;
        tweensRunning -= 1;
    }

    private IEnumerator tweenGetPowerup(float percentage)
    {
        wait = true;
        if (quickProgressBar != null && progressBar != null)
        {
            StartCoroutine(tweenFill(quickProgressBar, 1, QuickProgressAniDuration, QuickProgressAniCurve));

            StartCoroutine(tweenFill(progressBar, percentage, ProgressAniDuration, ProgressAniCurve));
            yield return new WaitForSeconds(powerupFlareDuration);
            //Durations reversed
            StartCoroutine(tweenFill(quickProgressBar, percentage, ProgressAniDuration, ProgressAniCurve));
            yield return new WaitForSeconds(ProgressAniDuration);
        }
        wait = false;
    }

    public float FillBarPercentage()
    {
        //if (manager.currentPowerup != null) return 1;
        return powerupManager.ProgressToNextPowerup();
    }
}
