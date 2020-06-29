using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockSpriteAnimator")]
public class BlockSpriteAnimator : BlockAnimator
{
    public Sprite newSprite;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        SetSprite(blockAnimation.block, newSprite);
    }

    protected void SetSprite(Block block, Sprite sprite)
    {
        if (block.blockColor != null && block.blockType != null)
        {
            if (block.blockColor.useSprite && block.blockColor.sprite != null) block.BlockSprite.sprite = block.blockColor.sprite;
            else block.BlockSprite.sprite = sprite;

            if (block.blockColor.useColor) block.BlockSprite.color = block.blockColor.color;
            else block.BlockSprite.color = Color.white;

            block.BlockSprite.sortingOrder = block.blockType.sortingOrder;

            block.Highlight.enabled = block.blockType.HighlightTail;

        }


    }

}
