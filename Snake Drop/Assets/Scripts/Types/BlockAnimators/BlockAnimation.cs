using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation
{
    public Block block;
    public BlockSlot destination;
    public IBlockAnimator Animator;
    public bool complete;

    public BlockAnimation(Block _block, IBlockAnimator _Animator, BlockSlot _destination)
    {
        block = _block;
        Animator = _Animator;
        destination = _destination;
    }

    public void AnimationStep()
    {
        Animator.AnimationStep(this);
        complete = Animator.AnimationIsComplete(this);
    }

    public bool AnimationIsComplete()
    {
        return Animator.AnimationIsComplete(this);
    }


}
