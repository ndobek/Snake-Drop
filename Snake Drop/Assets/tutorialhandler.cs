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

    private void Awake()
    {
        currentSlide = 0;
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


    public void ActivateSlide(int slide)
    {
        for(int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == slide);
        }
        currentSlide = slide;
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
}
