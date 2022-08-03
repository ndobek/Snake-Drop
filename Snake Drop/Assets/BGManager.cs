using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{

    public static BGManager inst;

    [System.Serializable]
    public struct BGSettings
    {
        public GameObject BGObject;
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
            SnapTo(ActiveBG);
            StartCoroutine(Fade(ActiveBG, false));
            activeBGIndex = value;
            StartCoroutine(Fade(ActiveBG, true));
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

    private IEnumerator Fade(BGSettings BG, bool inOut)
    {
        SpriteRenderer[] BGRenderers = BG.BGObject.GetComponentsInChildren<SpriteRenderer>();
        float startTime = Time.time;
        float percentageComplete;

        while (true)
        {
            percentageComplete = (Time.time - startTime) / fadeTime;

            foreach(SpriteRenderer renderer in BGRenderers)
            {
                Color c = renderer.color;
                if (inOut)
                {
                    c.a = Mathf.Clamp(percentageComplete, 0, 1);
                }
                else
                {
                    c.a = Mathf.Clamp(1 - percentageComplete, 0, 1);
                }
                renderer.color = c;

            }

            if(percentageComplete >= 1) 
            { 
                break; 
            }

            yield return null;
        }
    }

    private void SnapTo(BGSettings BG)
    {
        foreach(BGSettings background in backgrounds)
        {
            SpriteRenderer[] BGRenderers = background.BGObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer renderer in BGRenderers)
            {
                Color c = renderer.color;
                if (background.BGObject == BG.BGObject)
                {
                    c.a = 1;
                }
                else
                {
                    c.a = 0;
                }
                renderer.color = c;

            }

        }
    }
}
