using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationLerpSpeed = 1;
    private float targetRotation = 0;
    public int EnterSlotMoveDistance = 11;
    public bool KeepEntranceSlotOnOneSide;
    public Directions.Direction EntranceSide;

    [HideInInspector]
    public Directions.Direction currentDirection = Directions.Direction.UP;

    public void Update()
    {
        if (!GameManager.instance.playerManagers[0].RoundInProgress && KeepEntranceSlotOnOneSide) SetRotation(Directions.TranslateDirection(GameManager.instance.playerManagers[0].enterSlot.GetEdgeInfo().direction(), EntranceSide));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), rotationLerpSpeed);
    }

    public void SetRotation(Directions.Direction UpDirection)
    {
        currentDirection = UpDirection;
        OnRotate();
    }

    public void RotateClockwise()
    {
        DoRotation(true);
    }
    public void RotateCounterClockwise()
    {
        DoRotation(false);
    }

    private void DoRotation(bool clockwise)
    {

        bool roundInProgress = GameManager.instance.playerManagers[0].RoundInProgress;
        Directions.Direction newDirection = clockwise ? Directions.GetClockwiseNeighborDirection(currentDirection) : Directions.GetCounterClockwiseNeighborDirection(currentDirection);

        if (!(KeepEntranceSlotOnOneSide && !roundInProgress)) SetRotation(newDirection);
        if (KeepEntranceSlotOnOneSide && !roundInProgress)
        {
            int i = 0;
            while (i < EnterSlotMoveDistance)
            {
                i += 1;
                GameManager.instance.playerManagers[0].MoveWaitSlot(clockwise);
            }

        }

    }

    private void OnRotate()
    {
        UpdateRotation();
        GameManager.instance.playerManagers[0].playGrid.InvokeGridAction();
    }


    public void UpdateRotation()
    {
        switch (currentDirection)
        {
            case Directions.Direction.UP:
                targetRotation = 0;
                return;
            case Directions.Direction.DOWN:
                targetRotation = 180;
                return;
            case Directions.Direction.LEFT:
                targetRotation = 270;
                return;
            case Directions.Direction.RIGHT:
                targetRotation = 90;
                return;
        }
    }
}
