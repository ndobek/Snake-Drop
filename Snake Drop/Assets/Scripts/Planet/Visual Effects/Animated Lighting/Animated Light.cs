using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedLight : MonoBehaviour, IAnimatableLight
{

    private List<LightPreset> presets;
    public List<LightPreset> Presets
    { get { return presets; } }
  
}
