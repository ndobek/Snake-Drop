using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation
{
    public Block block;
    public BlockSlot destination;
    public BlockAnimator Animator;
    public float elapsedTime;
    public bool begun = false;
    public bool complete;
    public bool Concurrent { get { return Animator.AnimationConcurrent; } }


    public BlockAnimation(Block _block, BlockAnimator _Animator, BlockSlot _destination)
    {
        block = _block;
        Animator = _Animator;
        destination = _destination;
        elapsedTime = 0;
    }

    public bool AnimationIsComplete()
    {
        return Animator.AnimationIsComplete(this);
    }

    public IEnumerator Animate()
    {
        if (!begun)
        {
            begun = true;
            return Animator.Animate(this);
        }
        return null;
    }





}
