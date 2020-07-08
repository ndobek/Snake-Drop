using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior_Walk : LilGuyMovementBehavior
{
    public float speed;
    public override void DoTheThing(LilGuy guy, Vector3 destination)
    {
        guy.transform.position = Vector3.MoveTowards(guy.transform.position, destination, speed);
    }
}
