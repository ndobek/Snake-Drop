using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Effect Presets/Environmental Preset")]
public class EnvironmentalState : ScriptableObject
{  
    public List<Intensity> intensities;
}
