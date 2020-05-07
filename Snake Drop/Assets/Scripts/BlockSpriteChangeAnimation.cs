using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpriteChangeAnimation : IBlockAnimation
{
    public Block block;
    public Sprite newSprite;

    public BlockSpriteChangeAnimation(Block _block, Sprite _newSprite)
    {
        block = _block;
        newSprite = _newSprite;
    }

    public bool AnimationIsComplete()
    {
        return true;
    }
    public void AnimationStep()
    {
        if (block.blockColor != null && block.blockType != null)
        {
            block.BlockSprite.sprite = newSprite;
            block.BlockSprite.color = block.blockColor.color;
            block.BlockSprite.sortingOrder = block.blockType.sortingOrder;

            block.Highlight.enabled = block.blockType.HighlightTail;
        }


    }
}
