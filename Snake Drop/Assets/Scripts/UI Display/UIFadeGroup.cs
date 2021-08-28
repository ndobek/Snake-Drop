using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIFadeGroup : UIFade
{
    private void DoToAllInGroup(Action<UIFade> action)
    {
        UIFade[] UIFaders = GetComponentsInChildren<UIFade>();
        foreach (UIFade fader in UIFaders)
        {
            if (!fader.ignoreFadeGroups && fader != this)
            {
                action.Invoke(fader);
            }
        }
    }
    public override void Fade(bool inOut)
    {
        DoToAllInGroup(fader => fader.Fade(inOut));
        base.Fade(inOut);
    }

    public override void SetFadedIn()
    {
        DoToAllInGroup(fader => fader.SetFadedIn());
        base.SetFadedIn();
    }

    public override void SetFadedOut()
    {
        DoToAllInGroup(fader => fader.SetFadedOut());
        base.SetFadedOut();
    }
}