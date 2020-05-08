using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveAnimator : ScriptableObject, IBlockAnimator
{

    public float ArrivalTolerance;
    public float LerpSpeed;

    public bool AnimationIsComplete(BlockAnimation blockAnimation)
    {
        if (Vector3.Distance(blockAnimation.block.transform.localPosition, Vector3.zero) < ArrivalTolerance)
        {
            blockAnimation.block.transform.localPosition = Vector3.zero;
            return true;
        }
        return false;
    }
    public void AnimationStep(BlockAnimation blockAnimation)
    {
        blockAnimation.block.transform.SetParent(blockAnimation.destination.transform);
        blockAnimation.block.transform.localRotation = Quaternion.identity;
        blockAnimation.block.transform.localScale = Vector3.one;
        blockAnimation.block.transform.localPosition = Vector3.Lerp(blockAnimation.block.transform.localPosition, Vector3.zero, LerpSpeed); /*Vector3.zero; *//*Vector3.MoveTowards(this.transform.localPosition, Vector3.zero, .01f);*/

    }
}
