using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimator : MonoBehaviour
{

    private Queue<IBlockAnimation> Animations = new Queue<IBlockAnimation>();

    public void AddAnimation(IBlockAnimation animation)
    {
        Animations.Enqueue(animation);
    }

    public void AnimateStep()
    {
        if (Animations.Count > 0)
        {
            Animations.Peek().AnimationStep();
            if (Animations.Peek().AnimationIsComplete() && Animations.Count > 1) Animations.Dequeue();
        }
    }

    public void Update()
    {
        AnimateStep();
    }
}
