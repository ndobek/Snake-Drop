using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region old version
//public interface IEffectAnimator
//{
//List<IAnimatableEffect> Effects { get; }
//List<IPresetData> PresetData { get; } 
//void UpdateAnimations();


//}
#endregion

public interface IEffectAnimator<Effect, State, InitialStatePreset>
{
    InitialStatePreset InitialState { get; }
    State CurrentState { get; set; }
    State PreviousState { get; set; }
    void TransitionComplete(State stateTransitionedTo, State stateTransitionedFrom);
    void UpdateEffect(Effect effect, State nextFrame);
    void Animate(State keyframe1, State keyframe2, float t);
}

//This interface is for stuff like LightAnimator, which now goes on a light. it takes a keyframe, makes new interpolated frames, and changes the light based on the new frame.