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

    public List<IReact> reactToSpin;

    private bool EntranceMismatched = false;

    public void Update()
    {
        if (!GameManager.instance.playerManagers[0].RoundInProgress && KeepEntranceSlotOnOneSide)
        {
            if (EntranceMismatched)
            {
                EntranceMismatched = false;
                RotateEntranceToMatchBoard();
            }
            RotateBoardToMatchEntrance();
            
        }
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
        if (KeepEntranceSlotOnOneSide)
        {
            if (roundInProgress) EntranceMismatched = true;
            else RotateEntranceSlot(clockwise);
        }

    }

    public void RotateEntranceSlot(bool clockwise)
    {
        int i = 0;
        while (i < EnterSlotMoveDistance)
        {
            i += 1;
            GameManager.instance.playerManagers[0].MoveWaitSlot(clockwise);
        }
    }

    public void RotateEntranceSlotTo(Directions.Direction side)
    {
        Directions.Direction currentSide = GameManager.instance.playerManagers[0].enterSlot.GetEdgeInfo().direction();
        if (currentSide == side) return;
        else if (Directions.GetClockwiseNeighborDirection(currentSide) == side) RotateEntranceSlot(true);
        else if (Directions.GetCounterClockwiseNeighborDirection(currentSide) == side) RotateEntranceSlot(false);
        else
        {
            RotateEntranceSlot(true);
            RotateEntranceSlot(true);
        }
    }

    public void RotateEntranceToMatchBoard()
    {
        RotateEntranceSlotTo(currentDirection);
    }
    public void RotateBoardToMatchEntrance()
    {
        SetRotation(Directions.TranslateDirection(GameManager.instance.playerManagers[0].enterSlot.GetEdgeInfo().direction(), EntranceSide));
    }

    private void OnRotate()
    {
        UpdateRotation();
        foreach(IReact obj in reactToSpin) obj.React();
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
