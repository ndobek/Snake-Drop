using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LilGuyMovementBehavior : ScriptableObject
{
    public abstract void DoTheThing(LilGuy guy, Vector3 destination);
}
