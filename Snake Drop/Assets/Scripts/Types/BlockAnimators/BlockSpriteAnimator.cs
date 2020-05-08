using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpriteAnimator : ScriptableObject, IBlockAnimator
{
    public Sprite newSprite;

    public bool AnimationIsComplete(BlockAnimation blockAnimation)
    {
        return true;
    }
    public virtual void AnimationStep(BlockAnimation blockAnimation)
    {
        SetSprite(blockAnimation.block, newSprite);
    }

    public void SetSprite(Block block, Sprite sprite)
    {
        if (block.blockColor != null && block.blockType != null)
        {
            block.BlockSprite.sprite = newSprite;
            block.BlockSprite.color = block.blockColor.color;
            block.BlockSprite.sortingOrder = block.blockType.sortingOrder;

            if (block.blockType.HighlightTail) CreateSnakeLine(block);

            block.Highlight.enabled = block.blockType.HighlightTail;
        }


    }

    public void CreateSnakeLine(Block block)
    {
        block.Highlight.enabled = true;
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
