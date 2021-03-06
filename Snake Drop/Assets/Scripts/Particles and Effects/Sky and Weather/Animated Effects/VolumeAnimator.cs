using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeAnimator : MonoBehaviour, IEffectAnimator<float, VolumeProfile, VolumePreset>
{
    [SerializeField]
    private VolumePreset initialState;
    public VolumePreset InitialState { get => initialState; set { initialState = value; } }

    private VolumeProfile currentState;
    public VolumeProfile CurrentState
    
    { 
        get => currentState; 
        set 
        {
            currentState = value;
            volumeStart.profile = currentState;
            UpdateEffect(startTransitionValue, currentState);
        } 
    }
    private VolumeProfile previousState;
    public VolumeProfile PreviousState { get => previousState; set { previousState = value; } }

    public float blendDistance;
    private Vector3 startPos;
    private float startTransitionValue;
    private Vector3 endPos;
    private float endTransitionValue;
    private float triggerPosX;
    public GameObject volumeTrigger;
    public Volume volumeStart;
    public Volume volumeDestination;


    public void TransitionComplete(VolumeProfile transitionedTo, VolumeProfile transitionedFrom)
    {
        previousState = transitionedFrom;
        currentState = transitionedTo;
    }
    private void InitializeVolumes()
    {
        startPos = volumeStart.transform.position;
        endPos = startPos;
        endPos.x = startPos.x + (blendDistance*2);
        volumeDestination.transform.position = endPos;

        startTransitionValue = startPos.x;
        endTransitionValue = endPos.x;
        
        volumeTrigger.transform.position = startPos;
        volumeStart.profile = initialState.volumeState;
        triggerPosX = startTransitionValue;
    }
    private void Start()
    {

        InitializeVolumes();
    }
    public void Animate(VolumeProfile keyframe1, VolumeProfile keyframe2, float t)
    {
        volumeDestination.profile = keyframe2;
        triggerPosX = Mathf.Lerp(startTransitionValue, endTransitionValue, t);
        UpdateEffect(triggerPosX, keyframe2);        
    
    }

    public void UpdateEffect(float effect, VolumeProfile nextFrame)
    {
        Vector3 triggerTransform = volumeTrigger.transform.position;
        triggerPosX = effect;
        triggerTransform.x = triggerPosX;
        volumeTrigger.transform.position = triggerTransform;

    } 
}
