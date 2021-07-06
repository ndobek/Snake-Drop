using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField]
    private Vector2 AnimDistance;

    [SerializeField]
    private float Duration;
    [SerializeField]
    private float Delay;
    [SerializeField]
    private bool RandomizeDelay;
    [SerializeField]
    private AnimationCurve FadeCurve;
    [SerializeField]
    private AnimationCurve AnimCurve;

    [SerializeField]
    private CanvasGroup Group;
    [SerializeField]
    private RectTransform rectTransform;

    public bool ignoreFadeGroups;

    [SerializeField]
    private bool fadeInOnEnable;
    [SerializeField]
    private bool beginFadedOut;

    private bool FadedIn = true;

    void Awake()
    {
        if (Group == null) Group = GetComponent<CanvasGroup>();
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        if(beginFadedOut) SetFadedOut();
        if (fadeInOnEnable) FadeIn();
    }

    public void SetFadedOut()
    {
        Group.alpha = 0;
        Group.blocksRaycasts = false;
        Group.interactable = false;
        FadedIn = false;
    }
    public void SetFadedIn()
    {
        Group.alpha = 1;
        Group.blocksRaycasts = true;
        Group.interactable = true;
        FadedIn = true;
    }

    public void FadeIn()
    {
        Fade(true);
    }

    public void FadeOut()
    {
        Fade(false);
    }

    public virtual void Fade(bool inOut)
    {
        StartCoroutine(DoFade(inOut));
    }

    public void ToggleFade()
    {
        Fade(!FadedIn);
    }

    public void SetFade(bool inOut)
    {
        if (FadedIn != inOut)
        {
            FadedIn = inOut;
            Fade(inOut);
        }
    }

    private IEnumerator DoFade(bool inOut)
    {
        Vector3 originalPos = rectTransform.position;
        Vector3 targetPos = originalPos + (Vector3)AnimDistance;

        float actualDelay = RandomizeDelay ? Random.Range(0, Delay) : Delay;

        float startTime = Time.time + actualDelay;
        float endTime = startTime + Duration;
        float percentageComplete = (Time.time - startTime) / Duration;

        while (true)
        {
            percentageComplete = (Time.time - startTime) / Duration;

            if (inOut)
            {

                rectTransform.position = Vector3.LerpUnclamped(targetPos, originalPos, AnimCurve.Evaluate(percentageComplete));
                Group.alpha = FadeCurve.Evaluate(percentageComplete);
            }
            else
            {
                rectTransform.position = Vector3.LerpUnclamped(targetPos, originalPos, AnimCurve.Evaluate(1 - percentageComplete));
                Group.alpha = FadeCurve.Evaluate(1 - percentageComplete);
            }

            if (percentageComplete >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        if (inOut) SetFadedIn();
        else SetFadedOut();

        rectTransform.position = originalPos;

    }



}
