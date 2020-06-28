using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Types/BlockColor")]
public class BlockColor : ScriptableObject
{
    //public Sprite sprite;

    [System.Serializable]
    public struct BlockTypeInfo
    {
        public BlockType blockType;
        public BlockSpriteAnimator blockSpriteAnimator;
    }

    public BlockTypeInfo[] BlockSpriteOverrides;

    public BlockSpriteAnimator GetBlockSpriteAnimator(BlockType blockType)
    {
        foreach(BlockTypeInfo obj in BlockSpriteOverrides)
        {
            if(obj.blockType == blockType)
            {
                return obj.blockSpriteAnimator;
            }
        }
        return (BlockSpriteAnimator)blockType.defaultSpriteAnimator;
    }
}
