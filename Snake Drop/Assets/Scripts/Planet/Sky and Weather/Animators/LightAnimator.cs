using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

#region old stuff
//public class LightAnimator : MonoBehaviour, IEffectAnimator
//{
//    [SerializeField]
//    private List<IAnimatableEffect> effects;
//    public List<IAnimatableEffect> Effects { get { return effects; } }


//    [SerializeField]
//    private List<IPresetData> presetData;
//    public List<IPresetData> PresetData { get { return presetData; } }




//    public void UpdateAnimations()
//    {
//    }
//}
#endregion
//This class:
//Goes on light
//Interpolates data from keyframe
//Instantiates a frame
//Updates the light using data from the new frame
public class LightAnimator : MonoBehaviour, IEffectAnimator<Light2D, LightState>
{
    //renamed light to animatableLight because unity had an issue with the name light, probably because it's already a thing
    public Light2D animatableLight;

    private void Awake()
    {
        if (animatableLight == null) animatableLight = GetComponent<Light2D>();
    }

    public void UpdateEffect(Light2D effect, LightState nextFrame)
    {
        effect.pointLightInnerAngle = nextFrame.InnerAngle;
    }

    public void Animate(LightState keyframe1, LightState keyframe2, float t)
    {
        LightState result = new LightState();
        result.InnerAngle = Mathf.Lerp(keyframe1.InnerAngle, keyframe2.InnerAngle, t);
        UpdateEffect(animatableLight, result);
    }
}
