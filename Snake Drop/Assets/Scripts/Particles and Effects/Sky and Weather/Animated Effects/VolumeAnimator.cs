using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeAnimator : MonoBehaviour, IEffectAnimator<ColorCurves, VolumeState, VolumePreset>
{

    public TextureCurve hueVsSat;
    public TextureCurveParameter hueVsSatParameter;
    public Keyframe key;
    public Volume volume;
    public ColorCurves colorCurves;
    public VolumeProfile profile;
    private float time;
    [SerializeField]
    private VolumePreset initialState;
    public VolumePreset InitialState { get => initialState; set { initialState = value; } }

    private VolumeState currentState;
    public VolumeState CurrentState { get => currentState; set {currentState = value; } }

    public void InitializeCurves()
    {
        currentState = initialState.volumeState;
        profile.TryGet<ColorCurves>(out colorCurves);
        hueVsSatParameter = new TextureCurveParameter(initialState.volumeState.hueVsSat, true);
        colorCurves.hueVsSat = hueVsSatParameter;
        
        volume.profile = profile;
        
    }
    private void Start()
    {
        InitializeCurves();
    }
    public void Animate(VolumeState keyframe1, VolumeState keyframe2, float t)
    {
        CurrentState.hueVsSat = keyframe1.hueVsSat;
        UpdateEffect(colorCurves, keyframe2);
        time = t;
    }

    public void UpdateEffect(ColorCurves effect, VolumeState nextFrame)
    {
        effect.hueVsSat.Interp(CurrentState.hueVsSat, nextFrame.hueVsSat, time);
        volume.profile = profile;
    }
}
