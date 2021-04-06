using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plants/Species")]
public class PlantSpecies : ScriptableObject
{

    public List<GrowthStage> stages; 
}
