using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{

    public static BGManager inst;

    [System.Serializable]
    public struct BGSettings
    {
        public SpriteRenderer BGRenderer;
        public SpriteRenderer BlurredBGRenderer;
        public ColorOverride[] ColorOverrides;
    }
    [System.Serializable]
    public struct ColorOverride
    {
        public BlockColor Overridden;
        public BlockColor Override;
    }
    private void Awake()
    {
        if (!inst) inst = this;
        SnapTo(ActiveBG);
    }

    private int activeBGIndex = 0;
    public int ActiveBGIndex
    {
        get { return activeBGIndex; }
        set {
            SnapTo(ActiveBG, false);
            Fade(ActiveBG, false);
            activeBGIndex = value;
            Fade(ActiveBG, true);
        }
    }
    private bool bgBlurred;
    public bool BGBlurred
    {
        get { return bgBlurred; }
        set
        {
            if(value != bgBlurred) StartCoroutine(Fade(ActiveBG.BlurredBGRenderer, value));
            bgBlurred = value;
        }
    }
    public BGSettings[] backgrounds;
    public float fadeTime;
    public BGSettings ActiveBG
    {
        get { return backgrounds[activeBGIndex % backgrounds.Length]; }
    }

    public BlockSpriteAnimator GetAnimator(BlockColor blockColor, BlockType blockType)
    {
        foreach (ColorOverride ColorObj in ActiveBG.ColorOverrides)
        {
            if (ColorObj.Overridden == blockColor && ColorObj.Override != null) return ColorObj.Override.GetAnimator(blockType, false);
        }        
        return null;
    }

    public Color getDeathParticleColor(BlockColor blockColor)
    {
        foreach (ColorOverride ColorObj in ActiveBG.ColorOverrides)
        {
            if (ColorObj.Overridden == blockColor && ColorObj.Override != null) return ColorObj.Override.deathParticleColor;
        }        
        return blockColor.deathParticleColor;

    }

    private void Update()
    {
        PlayerManager p = GameManager.instance.playerManagers[0];

        int i = p.Powerup.numOfPowerupsUsed;
        if (i != ActiveBGIndex) ActiveBGIndex = i;

        p.playGrid.UpdateAllSprites();
        p.previewGrid.UpdateAllSprites();
    }

    private IEnumerator Fade(SpriteRenderer BGRenderer, bool inOut)
    {
        float startTime = Time.time;
        float percentageComplete;
        Color c = BGRenderer.color;
        float startingAlpha = c.a;

        while (true)
        {
            percentageComplete = (Time.time - startTime) / fadeTime;


            if (inOut)
            {
                c.a = Mathf.Clamp(percentageComplete, startingAlpha, 1);
            }
            else
            {
                c.a = Mathf.Clamp(1 - percentageComplete, 0, startingAlpha);
            }
            BGRenderer.color = c;

            if(percentageComplete >= 1) 
            { 
                break; 
            }

            yield return null;
        }
    }

    private void Fade(BGSettings BG, bool inOut)
    {
        StartCoroutine(Fade(BG.BGRenderer, inOut));
        StartCoroutine(Fade(BG.BlurredBGRenderer, BGBlurred));
    }

    private void SnapTo(BGSettings BG, bool SnapBlur = true)
    {
        StopAllCoroutines();
        foreach (BGSettings background in backgrounds)
        {
            Color c = background.BGRenderer.color;
            if (background.BGRenderer == BG.BGRenderer)
            {
                c.a = 1;
            }
            else
            {
                c.a = 0;
            }
            background.BGRenderer.color = c;
            if(SnapBlur) background.BlurredBGRenderer.color = c;

        }
    }


}
