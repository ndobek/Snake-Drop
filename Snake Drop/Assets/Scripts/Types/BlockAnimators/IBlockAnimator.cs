using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockAnimator
{
    void AnimationStep(BlockAnimation blockAnimation);
    bool AnimationIsComplete(BlockAnimation blockAnimation);
    bool AnimationConcurrent { get; }
}
