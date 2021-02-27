using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetic
{
    void Magnetize();
    bool InRange {get; set;}
}
