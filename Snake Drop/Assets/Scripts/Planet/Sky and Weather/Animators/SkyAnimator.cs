using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyAnimator : MonoBehaviour, IEffectAnimator<Material, SkyState, SkyPreset>
{
    public Material skyMaterial;
    [SerializeField]
    private SkyPreset initialState;
    public SkyPreset InitialState { get => initialState; }
    private SkyState currentState;
    public SkyState CurrentState { get => currentState; set { currentState = value; } }

    private void Start()
    {
        if (skyMaterial == null) skyMaterial = GetComponent<SpriteRenderer>().material;
        currentState = initialState.skyState;
    }
    public void Animate(SkyState keyframe1, SkyState keyframe2, float t)
    {
        SkyState result = new SkyState();
        result.Saturation = Mathf.Lerp(keyframe1.Saturation, keyframe2.Saturation, t);
        result.Hue = Mathf.Lerp(keyframe1.Hue, keyframe2.Hue, t);
        result.Contrast = Mathf.Lerp(keyframe1.Contrast, keyframe2.Contrast, t);
        result.Overwrite = Mathf.Lerp(keyframe1.Overwrite, keyframe2.Overwrite, t);
        result.Overlay = Mathf.Lerp(keyframe1.Overlay, keyframe2.Overlay, t);
        result.Burn = Mathf.Lerp(keyframe1.Burn, keyframe2.Burn, t);
        result.Dodge = Mathf.Lerp(keyframe1.Dodge, keyframe2.Dodge, t);
        result.Screen = Mathf.Lerp(keyframe1.Screen, keyframe2.Screen, t);
        result.Multiply = Mathf.Lerp(keyframe1.Multiply, keyframe2.Multiply, t);
        result.Darken = Mathf.Lerp(keyframe1.Darken, keyframe2.Darken, t);
        UpdateEffect(skyMaterial, result);
        currentState = result;
    }

    public void UpdateEffect(Material effect, SkyState nextFrame)
    {
        effect.SetFloat("_Saturation", nextFrame.Saturation);
        effect.SetFloat("_Hue", nextFrame.Hue);
        effect.SetFloat("_Contrast", nextFrame.Contrast);
        effect.SetFloat("_Adjusted_Overwrite", nextFrame.Overwrite);
        effect.SetFloat("_Overlay", nextFrame.Overlay);
        effect.SetFloat("_Burn", nextFrame.Burn);
        effect.SetFloat("_Dodge", nextFrame.Dodge);
        effect.SetFloat("_Screen", nextFrame.Screen);
        effect.SetFloat("_Multiply", nextFrame.Multiply);
        effect.SetFloat("_Darken", nextFrame.Darken);
    }
}
