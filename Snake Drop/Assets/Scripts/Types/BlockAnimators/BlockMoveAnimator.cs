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
            blockAnimation.block.transform.localPosition = Evaluate(blockAnimation.block.transform.InverseTransformPoint(blockAnimation.origin.position), Vector3.zero, percentageComplete(blockAnimation));
    }

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        blockAnimation.block.transform.localPosition = Vector3.zero;
    }

    public Vector3 Evaluate(Vector3 origin, Vector3 destination, float time)
    {
        Vector3 result = origin + ((destination - origin) * time);
        //Debug.Log("Origin: " + origin + " Destination: " + destination + "Time: " + time + " Result: " + result);
        Debug.Log(time);
        return origin + ((destination - origin) * time);
    }
}
