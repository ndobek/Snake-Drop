using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockAnimation
{
    void AnimationStep();
    bool AnimationIsComplete();
}
