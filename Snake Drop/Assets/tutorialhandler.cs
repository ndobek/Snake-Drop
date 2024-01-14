using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialhandler : MonoBehaviour
{
    public GameObject[] slides;
    private int currentSlide;

    [SerializeField]
    private UIFade TutorialFader;
    [SerializeField]
    private UIFade OpenButtonFader;

    [SerializeField]
    private TMPro.TMP_Text slideNumberText;

    private void Awake()
    {
        currentSlide = 0;
        UpdateText();
    }

    public void Open()
    {
        TutorialFader.FadeIn();
        OpenButtonFader.FadeOut();
        ActivateSlide(currentSlide);
    }
    public void Close()
    {
        TutorialFader.FadeOut();
        OpenButtonFader.FadeIn();
    }

    public void UpdateText()
    {
        if (slideNumberText != null) slideNumberText.text = (currentSlide + 1).ToString() + "/" + slides.Length.ToString();
    }


    public void ActivateSlide(int slide)
    {
        for(int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == slide);
        }
        currentSlide = slide;
        UpdateText();
    }
    public void Left()
    {
        if (currentSlide == 0) ActivateSlide(slides.Length - 1);
        else ActivateSlide(currentSlide - 1);
    }
    public void Right()
    {
        if (currentSlide == slides.Length - 1) ActivateSlide(0);
        else ActivateSlide(currentSlide + 1);
    }

    public void PopUpToolTip(int slide)
    {
        if (CloudOnce.CloudVariables.Tooltips)
        {
            Open();
            ActivateSlide(slide);
        }
    }
}
