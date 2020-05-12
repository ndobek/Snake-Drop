﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockSpriteAnimator")]
public class BlockSpriteAnimator : BlockAnimator
{
    public Sprite newSprite;

    public override void AnimationStep(BlockAnimation blockAnimation)
    {
        SetSprite(blockAnimation.block, newSprite);
    }

    protected void SetSprite(Block block, Sprite sprite)
    {
        if (block.blockColor != null && block.blockType != null)
        {
            block.BlockSprite.sprite = sprite;
            block.BlockSprite.color = block.blockColor.color;
            block.BlockSprite.sortingOrder = block.blockType.sortingOrder;

            if (block.blockType.HighlightTail) CreateSnakeLine(block);

            block.Highlight.enabled = block.blockType.HighlightTail;
        }


    }

    protected void CreateSnakeLine(Block block)
    {
        //block.Highlight.enabled = true;
        Block current = block;
        List<Vector3> result = new List<Vector3>();

        while (current.Tail != null)
        {
            result.Add(current.Highlight.transform.position);
            current = current.Tail;
        }
        result.Add(current.Highlight.transform.position);
        block.Highlight.positionCount = result.Count;
        block.Highlight.SetPositions(result.ToArray());

    }
}
