using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BoolRule : ScriptableObject
{
    public abstract bool Invoke(PlayerManager player);
}
