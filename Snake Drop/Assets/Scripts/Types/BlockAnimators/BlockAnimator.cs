using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockAnimator: ScriptableObject
{

    public bool AnimationConcurrent;
    public bool PreventPlayerInput;
    public float AnimationTotalTime;

    public virtual void AnimationStep(BlockAnimation blockAnimation) { }
    public void TimeStep(BlockAnimation blockAnimation)
    {
        blockAnimation.elapsedTime += Time.deltaTime;
        //Debug.Log(blockAnimation.elapsedTime);
    }
    public virtual void OnComplete(BlockAnimation blockAnimation) { }

    public IEnumerator Animate(BlockAnimation blockAnimation)
    {
        if (PreventPlayerInput && blockAnimation.block.Owner) blockAnimation.block.Owner.PreventInput(blockAnimation);
        while (!blockAnimation.AnimationIsComplete())
        {
            AnimationStep(blockAnimation);
            TimeStep(blockAnimation);
            yield return null;
        }

        OnComplete(blockAnimation);
        if (PreventPlayerInput && blockAnimation.block.Owner) blockAnimation.block.Owner.AllowInput(blockAnimation);
    }

    public float percentageComplete(float elapsedTime)
    {
        if (AnimationTotalTime != 0)
        {
            return elapsedTime / AnimationTotalTime;
        }
        else return 1;
    }
    public float percentageComplete(BlockAnimation blockAnimation)
    {
        return percentageComplete(blockAnimation.elapsedTime);
    }

    public virtual bool AnimationIsComplete(BlockAnimation blockAnimation)
    {
        return percentageComplete(blockAnimation) >= 1.0f;
    }
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
