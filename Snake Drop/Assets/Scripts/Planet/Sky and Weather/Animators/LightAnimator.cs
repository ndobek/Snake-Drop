using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightAnimator : MonoBehaviour, IEffectAnimator
{
    [SerializeField]
    private List<IAnimatableEffect> effects;
    public List<IAnimatableEffect> Effects { get; }

    [SerializeField]
    private List<IPresetData> presetData;
    public List<IPresetData> PresetData { get { return presetData; } }


    public void Start()
    {

    }
    public void UpdateAnimations()
    {
    }
}
