using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockMoveAnimator")]
public class BlockMoveAnimator : BlockAnimator
{
    public AnimationCurve Curve;
    public override void AnimationStep(BlockAnimation blockAnimation)
    {
        Transform obj = blockAnimation.block.transform;
        obj.SetParent(blockAnimation.destination.transform);
        obj.localRotation = Quaternion.identity;
        obj.localScale = Vector3.one;
        obj.position = Evaluate(blockAnimation.origin.position, blockAnimation.destination.position, percentageComplete(blockAnimation));
    }

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        blockAnimation.block.transform.localPosition = Vector3.zero;
    }

    public Vector3 Evaluate(Vector3 origin, Vector3 destination, float time)
    {
        //Vector3 result = origin + ((destination - origin) * time);
        //Debug.Log("Origin: " + origin + " Destination: " + destination + "Time: " + time + " Result: " + result);
        //Debug.Log(time);
        return origin + ((destination - origin) * Curve.Evaluate(time));
    }
}
