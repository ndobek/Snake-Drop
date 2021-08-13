using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeGroup : UIFade
{
    public override void Fade(bool inOut)
    {
        UIFade[] UIFaders = GetComponentsInChildren<UIFade>();
        foreach (UIFade fader in UIFaders)
        {
            if (!fader.ignoreFadeGroups && fader != this)
            {
                fader.Fade(inOut);
            }
        }

        base.Fade(inOut);
    }

}