using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimator : MonoBehaviour, IEffectAnimator<Animator, GrowthStage, PlantSpecies>
{
    public PlantSpecies InitialState => throw new System.NotImplementedException();

    public GrowthStage CurrentState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public GrowthStage PreviousState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Animate(GrowthStage keyframe1, GrowthStage keyframe2, float t)
    {
        throw new System.NotImplementedException();
    }

    public void TransitionComplete(GrowthStage stateTransitionedTo, GrowthStage stateTransitionedFrom)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateEffect(Animator effect, GrowthStage nextFrame)
    {
        throw new System.NotImplementedException();
    }
}
