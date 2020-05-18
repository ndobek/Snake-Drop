using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation
{
    public Block block;
    public Transform origin;
    public Transform destination;
    public BlockAnimator Animator;
    public float elapsedTime = 0;
    public bool begun = false;
    public bool Concurrent { get { return Animator.AnimationConcurrent; } }


    public BlockAnimation(Block _block, BlockAnimator _Animator)
    {
        block = _block;
        Animator = _Animator;
        elapsedTime = 0;
    }
    public BlockAnimation(Block _block, BlockAnimator _Animator, Transform _origin, Transform _destination)
    {
        block = _block;
        Animator = _Animator;
        origin = _origin;
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
