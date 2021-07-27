using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve rotationCurve;
    [SerializeField]
    private float rotationDuration = 1;
    [HideInInspector]
    public float targetRotation = 0;
    public int EnterSlotMoveDistance = 11;
    public bool KeepEntranceSlotOnOneSide;
    public Directions.Direction EntranceSide;

    [HideInInspector]
    public Directions.Direction currentDirection = Directions.Direction.UP;

    public List<IReact> reactToSpin = new List<IReact>();

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
    }

    public void SetRotation(Directions.Direction UpDirection)
    {

        if (currentDirection != UpDirection)
        {
            PlayerManager player = GameManager.instance.playerManagers[0];
            Directions.Direction mostRecentDirectionMoved = Directions.TranslateDirection(player.playerController.MostRecentDirectionMoved, currentDirection);
            currentDirection = UpDirection;
            switch (currentDirection)
            {
                case Directions.Direction.UP:
                    targetRotation = 0;
                    break;
                case Directions.Direction.DOWN:
                    targetRotation = 180;
                    break;
                case Directions.Direction.LEFT:
                    targetRotation = 270;
                    break;
                case Directions.Direction.RIGHT:
                    targetRotation = 90;
                    break;
            }
            if(!player.RoundInProgress) player.playerController.MostRecentDirectionMoved = Directions.TranslateDirection(mostRecentDirectionMoved, currentDirection);
            foreach (IReact obj in reactToSpin) obj.React();
            StartCoroutine(RotationRoutine());
            GameManager.instance.playerManagers[0].playGrid.InvokeGridAction();
        }
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
        PlayerManager player = GameManager.instance.playerManagers[0];
        Directions.Direction newDirection = Directions.TranslateDirection(player.enterSlot.GetEdgeInfo().direction(), EntranceSide);
        SetRotation(newDirection);


    }

    private IEnumerator RotationRoutine()
    {
        float startTime = Time.time;
        float percentageComplete = (Time.time - startTime) / rotationDuration;

        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, targetRotation);

        while (true)
        {
            percentageComplete = (Time.time - startTime) / rotationDuration;
            transform.rotation = Quaternion.Lerp(startRot, targetRot, rotationCurve.Evaluate(percentageComplete));

            yield return new WaitForEndOfFrame();
            if (percentageComplete >= 1) break;
        }

        transform.rotation = targetRot;
    }
}
