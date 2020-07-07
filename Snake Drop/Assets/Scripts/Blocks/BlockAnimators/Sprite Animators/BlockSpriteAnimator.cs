using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BlockSpriteAnimator : BlockAnimator
{
    //protected void SetSprite(Block block, Sprite sprite)
    //{
    //    SetSprite(block, sprite, Color.white, 0);
    //}

    protected void SetSprite(Block block, Sprite sprite, Color color, int sortingOrder)
    {
        if (block.blockColor != null && block.blockType != null)
        {
            block.BlockSprite.sprite = sprite;
            block.BlockSprite.color = color;
            block.BlockSprite.sortingOrder = sortingOrder;

            block.Highlight.enabled = block.blockType.HighlightTail;

        }


    }

}
