using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherAnimator : MonoBehaviour, IEffectAnimator<Weather, WeatherState>
{



    public void Animate(WeatherState keyframe1, WeatherState keyframe2, float t)
    {
        //Transitions the weather settings from the old preset to the new preset
    }
    
    public void UpdateEffect(Weather effect, WeatherState nextFrame)
    {

    }
}
