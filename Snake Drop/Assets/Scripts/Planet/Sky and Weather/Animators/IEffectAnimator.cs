using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectAnimator
{
    List<IAnimatableEffect> Effects { get; }
    List<IPresetData> PresetData { get; } 
    void UpdateAnimations();
}
