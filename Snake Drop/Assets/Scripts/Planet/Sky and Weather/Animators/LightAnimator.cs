using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightAnimator : MonoBehaviour, IEffectAnimator
{
    [SerializeField]
    private List<IAnimatableEffect> effects;
    public List<IAnimatableEffect> Effects { get; }

    private IPresetData presetData;
    public IPresetData PresetData { get; }


    public void Start()
    {

    }
    public void UpdateAnimations()
    {
    }
}
