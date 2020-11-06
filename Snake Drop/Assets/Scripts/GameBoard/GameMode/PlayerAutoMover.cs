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
    private Directions.Direction prevDirection = Directions.Direction.DOWN;

    public void Queue(Directions.Direction direction)
    {
        if (upcomingDirections.Count < MaxQueuedDirections) upcomingDirections.Enqueue(direction);
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
                else prevDirection = playerController.mostRecentDirectionMoved;
            }
        }

    }

    private void MovePlayer()
    {
        Directions.Direction directionToMove = playerController.mostRecentDirectionMoved;
        Debug.Log(upcomingDirections.ToString());
        if (upcomingDirections.Count > 0) directionToMove = upcomingDirections.Dequeue();
        if (directionToMove == Directions.GetOppositeDirection(prevDirection)) directionToMove = prevDirection;
        else prevDirection = directionToMove;
        playerController.MoveSnake(directionToMove);
    }
}
