using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAnimations : MonoBehaviour
{
    public AnimationCurve colourCurveHueSat = new AnimationCurve();
    public Keyframe key = new Keyframe();
    public float value;
    public float time;
    public void InitializeKeys()
    {
        key.time = time;
        key.value = value;
        colourCurveHueSat.AddKey(key);
    }

    public void UpdateKeys()
    {
        for (int i = 0; i > colourCurveHueSat.keys.Length; i++)
        {
            colourCurveHueSat.MoveKey(i, key);
        }
        
    }
 
}
