using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMover : MonoBehaviour
{
    private Queue<Directions.Direction> upcomingDirections = new Queue<Directions.Direction>();
    [SerializeField]
    private int MaxQueuedDirections;
    public PlayerController playerController;
    public float TimePerMove;
    public bool MoveBetweenRounds = false;
    private float currentTime = 0;
    private Directions.Direction prevQueuedDirection;
    [HideInInspector]
    public Directions.Direction prevMovedDirection = Directions.Direction.DOWN;

    public void Queue(Directions.Direction direction)
    {
        if (upcomingDirections.Count < MaxQueuedDirections && direction != prevQueuedDirection)
        {
            prevQueuedDirection = direction;
            upcomingDirections.Enqueue(direction);
        }

    }

    private void Update()
    {
        if (TimePerMove != 0)
        {
            currentTime += Time.deltaTime;

            if (currentTime > TimePerMove)
            {
                currentTime -= TimePerMove;
                if (playerController.player.RoundInProgress || MoveBetweenRounds) MovePlayer();
                else prevMovedDirection = playerController.MostRecentDirectionMoved;
            }
        }

    }

    public void resetTime()
    {
        currentTime = 0;
        upcomingDirections.Clear();
    }

    private void MovePlayer()
    {
        Directions.Direction directionToMove = playerController.MostRecentDirectionMoved;
        Debug.Log(upcomingDirections.ToString());
        if (upcomingDirections.Count > 0) directionToMove = upcomingDirections.Dequeue();
        if (directionToMove == Directions.GetOppositeDirection(prevMovedDirection)) directionToMove = prevMovedDirection;
        else prevMovedDirection = directionToMove;
        playerController.MoveSnake(directionToMove);
    }
}
