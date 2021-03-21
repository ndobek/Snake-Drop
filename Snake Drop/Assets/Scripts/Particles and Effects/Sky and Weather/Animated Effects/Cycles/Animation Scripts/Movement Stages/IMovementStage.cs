using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStage<states>
{
    string Label { get;}
    List<states> States { get; set; }
}
