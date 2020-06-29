using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockSpriteAnimator")]
public class BlockSpriteAnimator : BlockAnimator
{
    public Sprite newSprite;
    public bool useBlockColorSprite;
    public bool useBlockColorColor;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        SetSprite(blockAnimation.block, newSprite);
    }

    protected void SetSprite(Block block, Sprite sprite)
    {
        if (block.blockColor != null && block.blockType != null)
        {
            if (useBlockColorSprite && block.blockColor.sprite != null) block.BlockSprite.sprite = block.blockColor.sprite;
            else block.BlockSprite.sprite = sprite;

            if (useBlockColorColor) block.BlockSprite.color = block.blockColor.color;

            block.BlockSprite.sortingOrder = block.blockType.sortingOrder;

            block.Highlight.enabled = block.blockType.HighlightTail;

        }


    }

}
