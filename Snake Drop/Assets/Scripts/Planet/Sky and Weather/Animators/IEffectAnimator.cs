using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectAnimator
{
    List<IAnimatableEffect> Effects { get; }
    IPresetData PresetData { get; } 
    void UpdateAnimations();
}
