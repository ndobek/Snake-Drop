using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticParticles : MonoBehaviour, IMagnetic
{
    private bool inRange;
    public bool InRange
    {
        get { return inRange; }
        set { inRange = value; }
    }
    public void Magnetize()
    {

    }
}
