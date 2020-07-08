using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilGuy : MonoBehaviour
{
    public ILilGuyThinkerBox Brain;
    public ILilGuySpriteController SpriteController;
    public ILilGuyMovementController MovementController;

    public void Awake()
    {
        Brain = GetComponent<ILilGuyThinkerBox>();
        SpriteController = GetComponent<ILilGuySpriteController>();
        MovementController = GetComponent<ILilGuyMovementController>();
    }

    public void MoveTo(Vector3 destination)
    {
        MovementController.MoveTo(this, destination);
    }
}
