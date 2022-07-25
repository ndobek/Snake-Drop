using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Types/BlockColor")]
public class BlockColor : ScriptableObject, ISaveable
{
    [SerializeField]
    private string ColorName;
    public string Name { get { return ColorName; } set { ColorName = value; } }
    public bool CheckForBGOverride;
    public BlockSpriteAnimator defaultAnimator;
    public Color deathParticleColor;

    public Color GetDeathParticleColor()
    {
            if (CheckForBGOverride)
            {
            return BGManager.inst.getDeathParticleColor(this);
            }
            
            return deathParticleColor;
    }

    [System.Serializable]
    public struct AnimatorInfo
    {
        public BlockType BlockType;
        public BlockSpriteAnimator Animator;
    }

    public AnimatorInfo[] animatorInfos;

    public BlockSpriteAnimator GetAnimator(BlockType blockType, bool allowOverride = true)
    {

        if(CheckForBGOverride && allowOverride)
        {
            BlockSpriteAnimator result = null;
            result = BGManager.inst.GetAnimator(this, blockType);
            if (result != null) return result;
        }


        foreach(AnimatorInfo obj in animatorInfos)
        {
            if (obj.BlockType == blockType) return obj.Animator;
        }
        return defaultAnimator;
    }

}
