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
        foreach (BGSettings bg in backgrounds) bg.BGObject.SetActive(false);
        ActiveBG.BGObject.SetActive(true);
    }

    private int activeBGIndex = 0;
    public int ActiveBGIndex
    {
        get { return activeBGIndex; }
        set { ActiveBG.BGObject.SetActive(false);
            activeBGIndex = value;
            ActiveBG.BGObject.SetActive(true);
        }
    }

    public BGSettings[] backgrounds;
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
        ActiveBGIndex = p.Powerup.numOfPowerupsObtained;
        p.playGrid.UpdateAllSprites();
        p.previewGrid.UpdateAllSprites();
    }
}
