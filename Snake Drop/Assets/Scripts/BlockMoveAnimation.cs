using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveAnimation : IBlockAnimation
{
    public Block block;
    public BlockSlot destination;

    public float ArrivalTolerance;
    public float LerpSpeed;

    public BlockMoveAnimation(Block _block, BlockSlot _destination, float _ArrivalTolerance, float _LerpSpeed)
    {
        block = _block;
        destination = _destination;
        ArrivalTolerance = _ArrivalTolerance;
        LerpSpeed = _LerpSpeed;
    }

    public bool AnimationIsComplete()
    {
        return Vector3.Distance(block.transform.localPosition, Vector3.zero) < ArrivalTolerance;
    }
    public void AnimationStep()
    {
        block.transform.SetParent(destination.transform);
        block.transform.localRotation = Quaternion.identity;
        block.transform.localScale = Vector3.one;
        block.transform.localPosition = /*Vector3.zero; *//*Vector3.MoveTowards(this.transform.localPosition, Vector3.zero, .01f);*/  Vector3.Lerp(block.transform.localPosition, Vector3.zero, LerpSpeed);
        if (AnimationIsComplete()) block.transform.localPosition = Vector3.zero;
    }
}
