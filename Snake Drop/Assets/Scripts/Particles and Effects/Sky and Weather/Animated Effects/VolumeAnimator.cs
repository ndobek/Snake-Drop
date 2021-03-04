using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeAnimator : MonoBehaviour, IEffectAnimator<Transform, VolumeProfile, VolumePreset>
{

    [SerializeField]
    private VolumePreset initialState;
    public VolumePreset InitialState { get => initialState; set { initialState = value; } }

    private VolumeProfile currentState;
    public VolumeProfile CurrentState { get => currentState; set {currentState = value; } }

    
    private void Start()
    {
    }
    public void Animate(VolumeProfile keyframe1, VolumeProfile keyframe2, float t)
    {
    
    }

    public void UpdateEffect(Transform effect, VolumeProfile nextFrame)
    {
    } 
}
