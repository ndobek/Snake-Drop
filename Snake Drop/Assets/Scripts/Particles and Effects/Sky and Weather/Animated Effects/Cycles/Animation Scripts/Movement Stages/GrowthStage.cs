using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Plants/GrowthStage")]
[System.Serializable]
public class GrowthStage : ScriptableObject, IMovementStage<EnviroReactStage>
{
    private string label;
    public string Label { get => label; set { label = value; } }
    private List<EnviroReactStage> states;
    public List<EnviroReactStage> States { get => states; set { states = value; } }

}
