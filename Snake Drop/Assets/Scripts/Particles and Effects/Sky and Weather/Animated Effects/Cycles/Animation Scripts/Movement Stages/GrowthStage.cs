using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Plants/GrowthStage")]
[System.Serializable]
public class GrowthStage : ScriptableObject
{
    public int growthStageNumber;
    public List<PlantClip> clips;

}
