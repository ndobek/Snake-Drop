using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotator : MonoBehaviour
{
    [SerializeField]
    public float targetRotation = 0;
    public int EnterSlotMoveDistance = 11;
    public bool KeepEntranceSlotOnOneSide;
    public Directions.Direction EntranceSide;

    //for board spin animations
    public AnimationCurve rotationCurve; 
    public float rotationTime; 
    public float ClampedTime 
    { 
        get 
        { 
            return Mathf.Clamp(rotationTime, 0, rotationDuration); 
        } 
    }
    //TODO: make some of these private when testing is done
    public float rotationDuration;
    public bool rotating = false;
    public float savedTargetRotation = 0f;
    public float totalRotationDegrees = 0f;
    public bool targetAquired = false;
    public float rotationThreshold;
    public float currentStartingPoint;
    public bool RotationDirection 
    { 
        get 
        { 
            return (totalRotationDegrees > 0f);
        } 
    }
    //for particle reactions
    public float reactionThreshold;
    public bool reacted = false;
    public List<IReact> reactToSpin = new List<IReact>();

    
   
    [HideInInspector]
    public Directions.Direction currentDirection = Directions.Direction.UP;
    private bool EntranceMismatched = false;
    private void Start()
    {
        rotationTime = rotationDuration;
        
    }
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
        rotationTime += Time.deltaTime;
        if (!Mathf.Approximately(transform.rotation.eulerAngles.z, targetRotation)  && rotating == false)
        {
            NewTarget();
        }
        if (rotating == true)
        {
            if(targetRotation != savedTargetRotation)
            {
                
                NewTarget(FindStartingPoint(), true);
            }
            
            AnimateRotation();
        }
        
        
    }
    #region Alyssa Methods
    private float FindStartingPoint()
    {

        float rotationDegrees = transform.rotation.eulerAngles.z;
        if (RotationDirection)
        {

            return (Mathf.Ceil(rotationDegrees / 90f))*90f;
        }
      
        else
        {
            return (Mathf.Floor(rotationDegrees / 90f)) * 90f;
        }
    }
    private float FindNearestTarget(float rotationDegrees)
    {
        float quadrant = rotationDegrees / 90f;
        if (Mathf.Approximately(quadrant, 0f))
        {
            return rotationDegrees;
        }
       
        if (Mathf.Abs(rotationDegrees % 90) > .5f)
        {
            return Mathf.Ceil(quadrant) * 90f;
        }
        else
        {
            return Mathf.Floor(quadrant) * 90f;
        }
        
        
    }

    private void SetNewTrajectory(float newStartingPoint)
    {
        
        totalRotationDegrees = Mathf.DeltaAngle(newStartingPoint, targetRotation);

    }
    private void NewTarget()
    {
        NewTarget(FindNearestTarget(transform.rotation.eulerAngles.z), false);
        
    }
    private void NewTarget(float startingPoint, bool reverseMidRotation)
    {

        if (reverseMidRotation == true)
        {
            rotationTime = rotationDuration - rotationTime;
        }
        else
        {
            rotationTime = 0;
        }


        SetNewTrajectory(startingPoint);
        currentStartingPoint = startingPoint;
        savedTargetRotation = targetRotation; 

        rotating = true;
        targetAquired = true;

    }
    private float RotationPercent()
    {
     
        return ClampedTime / rotationDuration;
    }

 
    
    private void AnimateRotation()
    {
        if (targetAquired == true)
        {

            if (reacted == false && RotationPercent() >= reactionThreshold)
            {
                foreach (IReact react in reactToSpin)
                {
                    react.React();
                }
                reacted = true;
            }
        
            float rotationStep = rotationCurve.Evaluate(RotationPercent()) * 90f;
            transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0, 0, currentStartingPoint), Quaternion.Euler(0, 0, targetRotation), rotationStep);

        }
        if (Mathf.Approximately(transform.rotation.eulerAngles.z, targetRotation))
        {

            rotating = false;
            reacted = false;
            targetAquired = false;
            
        }
       
    }
#endregion



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
