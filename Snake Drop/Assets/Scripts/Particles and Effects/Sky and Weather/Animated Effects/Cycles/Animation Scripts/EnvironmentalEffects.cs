using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour, ICyclical, IEffectAnimator<PlantAnimator, EnvironmentalState, EnvironmentalPreset>
{
    public Weather weather;
    public float CycleLength => throw new System.NotImplementedException();

    public float CyclePoint => throw new System.NotImplementedException();

    private EnvironmentalPreset initialState;
    public EnvironmentalPreset InitialState { get => initialState; set { initialState = value; } }

    private EnvironmentalState currentState;
    public EnvironmentalState CurrentState { get => currentState; set { currentState = value; }}
    private EnvironmentalState previousState;
    public EnvironmentalState PreviousState { get => previousState; set { previousState = value; } }

    public void Animate(EnvironmentalState keyframe1, EnvironmentalState keyframe2, float t)
    {
        throw new System.NotImplementedException();
    }

    public void CycleUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void TransitionComplete(EnvironmentalState stateTransitionedTo, EnvironmentalState stateTransitionedFrom)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateEffect(PlantAnimator effect, EnvironmentalState nextFrame)
    {
        throw new System.NotImplementedException();
    }
}
