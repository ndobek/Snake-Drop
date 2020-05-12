using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockMoveAnimator")]
public class BlockMoveAnimator : BlockAnimator
{

    public override void AnimationStep(BlockAnimation blockAnimation)
    {
            blockAnimation.block.transform.SetParent(blockAnimation.destination.transform);
            blockAnimation.block.transform.localRotation = Quaternion.identity;
            blockAnimation.block.transform.localScale = Vector3.one;
            blockAnimation.block.transform.localPosition = Evaluate(blockAnimation.block.transform.localPosition, Vector3.zero, percentageComplete(blockAnimation));/* Vector3.Lerp(blockAnimation.block.transform.localPosition, Vector3.zero, LerpSpeed);*/
    }


    public override void OnComplete(BlockAnimation blockAnimation)
    {
        blockAnimation.block.transform.localPosition = Vector3.zero;
        base.OnComplete(blockAnimation);
    }

    public Vector3 Evaluate(Vector3 origin, Vector3 destination, float time)
    {
        return origin + ((destination - origin) * time);
    }
}
