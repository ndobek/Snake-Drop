using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Types/BlockColor")]
public class BlockColor : ScriptableObject, ISaveable
{
    [SerializeField]
    private string ColorName;
    public string Name { get { return ColorName; } set { ColorName = value; } }
    public BlockSpriteAnimator defaultAnimator;
    public Color deathParticleColor;

    [System.Serializable]
    public struct AnimatorInfo
    {
        public BlockType BlockType;
        public BlockSpriteAnimator Animator;
    }

    public AnimatorInfo[] animatorInfos;

    public BlockSpriteAnimator GetAnimator(BlockType blockType)
    {
        foreach(AnimatorInfo obj in animatorInfos)
        {
            if (obj.BlockType == blockType) return obj.Animator;
        }
        return defaultAnimator;
    }

}
