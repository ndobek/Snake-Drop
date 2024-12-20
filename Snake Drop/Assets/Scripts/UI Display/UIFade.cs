﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField]
    private Vector2 AnimDistance;
    public bool debug;
    [SerializeField]
    public float Duration;
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
    public bool ignoresRaycasts;

    [SerializeField]
    private bool fadeInOnEnable;
    [SerializeField]
    private bool beginFadedOut;

    [HideInInspector]
    public bool FadedIn = true;
    private Vector3 originalPos;
    private bool rewriteOriginalPos = true;

    private void CheckComponents()
    {
        if (Group == null) Group = GetComponent<CanvasGroup>();
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
    }

    void Awake()
    {
        CheckComponents();
        if (beginFadedOut) SetFadedOut();
    }

    void OnEnable()
    {
        CheckComponents();
        if (fadeInOnEnable) FadeIn();
    }

    public virtual void SetFadedOut()
    {
        CheckComponents();
        Group.alpha = 0;
        Group.blocksRaycasts = false;
        Group.interactable = false;
        FadedIn = false;
    }
    public virtual void SetFadedIn()
    {
        CheckComponents();
        Group.alpha = 1;
        Group.blocksRaycasts = !ignoresRaycasts;
        Group.interactable = true;
        FadedIn = true;
    }

    public void IgnoreFadeGroups(bool onOff)
    {
        ignoreFadeGroups = onOff;
    }
    public void IgnoreFadeGroups()
    {
        IgnoreFadeGroups(true);
    }
    public void StopIgnoringFadeGroups()
    {
        IgnoreFadeGroups(false);
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
        if (FadedIn != inOut)
        {
            StopAllCoroutines();
            StartCoroutine(DoFade(inOut));
        }
    }

    public void ToggleFade()
    {
        Fade(!FadedIn);
    }

    public void SetFaded(bool inOut)
    {
        if (inOut) SetFadedIn();
        else SetFadedOut();
    }

    private IEnumerator DoFade(bool inOut)
    {
        FadedIn = inOut;
        if (Group == null) Group = GetComponent<CanvasGroup>();
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();

        if (rewriteOriginalPos)
        {
            originalPos = rectTransform.anchoredPosition;
        }
        rewriteOriginalPos = false;
        Vector3 targetPos = originalPos + (Vector3)AnimDistance;

        float actualDelay = RandomizeDelay ? Random.Range(0, Delay) : Delay;

        float startTime = Time.time + actualDelay;
        float percentageComplete;

        while (true)
        {
            percentageComplete = Mathf.Clamp((Time.time - startTime) / Duration, 0, 1);
            if (inOut)
            {

                rectTransform.anchoredPosition = Vector3.LerpUnclamped(targetPos, originalPos, AnimCurve.Evaluate(percentageComplete));
                if (debug)
                {
                    debug = true;
                }
                Group.alpha = FadeCurve.Evaluate(percentageComplete);
            }
            else
            {
                rectTransform.anchoredPosition = Vector3.LerpUnclamped(targetPos, originalPos, AnimCurve.Evaluate(1 - percentageComplete));
                Group.alpha = FadeCurve.Evaluate(1 - percentageComplete);
            }

            if (percentageComplete >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        if (inOut) SetFadedIn();
        else SetFadedOut();
        rectTransform.anchoredPosition = originalPos;
        rewriteOriginalPos = true;

    }



}
