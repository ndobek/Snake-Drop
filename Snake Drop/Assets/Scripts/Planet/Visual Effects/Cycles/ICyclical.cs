using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICyclical
{
    float CycleLength {get;}
    float CyclePoint { get;}
    void CycleUpdate();
}
