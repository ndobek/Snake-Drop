using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroReactStage : ScriptableObject, IMovementStage<AnimationState>
{
    private string label;
    public string Label { get => label; }
    private List<AnimationState> states;
    public List<AnimationState> States { get => states; set { states = value; } }
}
