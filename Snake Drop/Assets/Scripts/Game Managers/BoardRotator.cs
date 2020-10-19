using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationLerpSpeed = 1;
    private float targetRotation = 0;

    [HideInInspector]
    public Directions.Direction currentDirection = Directions.Direction.UP;

    public void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, GetRotation()), rotationLerpSpeed);
    }

    public void RotateClockwise()
    {
        currentDirection = Directions.GetCounterClockwiseNeighborDirection(currentDirection);
        targetRotation += 90;
        OnRotate();
    }
    public void RotateCounterClockwise()
    {
        currentDirection = Directions.GetClockwiseNeighborDirection(currentDirection);
        targetRotation -= 90;
        OnRotate();
    }

    private void OnRotate()
    {
        GameManager.instance.playerManagers[0].playGrid.InvokeGridAction();
    }


    public float GetRotation()
    {
        return targetRotation;
        //switch (currentDirection)
        //{
        //    case Directions.Direction.UP:
        //        return 0;
        //    case Directions.Direction.DOWN:
        //        return 180;
        //    case Directions.Direction.LEFT:
        //        return 270;
        //    case Directions.Direction.RIGHT:
        //        return 90;
        //}
        //return 0;
    }
}
