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
            UpdateEffect(startTransition, currentState);
        } 
    }
    public float blendDistance;
    private float startTransition;
    private float endTransition;
    private float triggerPos;
    public GameObject volumeTrigger;
    public Volume volumeStart;
    public Volume volumeDestination;
    

    
    private void InitializeVolumes()
    {
        startTransition = volumeStart.transform.position.x;
        endTransition = startTransition + (blendDistance*2);
        volumeStart.transform.position = new Vector3(startTransition, 0f, 0f);
        volumeDestination.transform.position = new Vector3(endTransition, 0f, 0f);
        volumeTrigger.transform.position = volumeStart.transform.position;
        volumeStart.profile = initialState.volumeState;
        triggerPos = startTransition;
    }
    private void Start()
    {

        InitializeVolumes();
    }
    public void Animate(VolumeProfile keyframe1, VolumeProfile keyframe2, float t)
    {
        volumeDestination.profile = keyframe2;
        triggerPos = Mathf.Lerp(startTransition, endTransition, t);
        UpdateEffect(triggerPos, keyframe2);        
    
    }

    public void UpdateEffect(float effect, VolumeProfile nextFrame)
    {
        Vector3 triggerTransform = volumeTrigger.transform.position;
        triggerPos = effect;
        triggerTransform.x = triggerPos;
        volumeTrigger.transform.position = triggerTransform;

    } 
}
