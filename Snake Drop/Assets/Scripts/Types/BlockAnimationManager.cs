using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimationManager : MonoBehaviour
{

    private Queue<BlockAnimation> Animations = new Queue<BlockAnimation>();

    public void AddAnimation(BlockAnimation animation)
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
