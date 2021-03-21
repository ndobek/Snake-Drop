using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plants/Species")]
public class PlantSpecies : ScriptableObject, IMovementStage<GrowthStage>
{
    private string label;
    public string Label { get => label; set { label = value; } }
    private List<GrowthStage> states;
    public List<GrowthStage> States { get => states; set { states = value; } }
}
