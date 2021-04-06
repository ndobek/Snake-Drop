using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimator : MonoBehaviour, IEffectAnimator<EnvironmentalEffects, EnvironmentalState, EnvironmentalState>
{
    public Weather weather;

    private EnvironmentalState initialState;
    public EnvironmentalState InitialState { get => initialState; }

    private EnvironmentalState currentState;
    public EnvironmentalState CurrentState { get => currentState; set { currentState = value; } }
    private EnvironmentalState previousState;
    public EnvironmentalState PreviousState { get => previousState; set { previousState = value; } }
    [HideInInspector]
    public List<EnvironmentalEffects> effects = new List<EnvironmentalEffects>();

    private void Start()
    {
        initialState = weather.startingPreset.environmentalState;
        TransitionComplete(initialState, initialState);
    }
    public void Animate(EnvironmentalState keyframe1, EnvironmentalState keyframe2, float t)
    {
        TransitionComplete(keyframe2, keyframe1);

    }

    public void TransitionComplete(EnvironmentalState stateTransitionedTo, EnvironmentalState stateTransitionedFrom)
    {
        PreviousState = stateTransitionedFrom;
        currentState = stateTransitionedTo;
        foreach (EnvironmentalEffects effect in effects)
        {
            UpdateEffect(effect, currentState);
        }
    }

    public void UpdateEffect(EnvironmentalEffects effect, EnvironmentalState nextFrame)
    {
        effect.SetState(nextFrame);
    }

  
}
