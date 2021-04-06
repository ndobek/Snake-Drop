using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plants/PlantClip")]
public class PlantClip: ScriptableObject
{
    public string stateName;
    public int intensityLevel;
    public List<TransitionClip> transitions;
}

