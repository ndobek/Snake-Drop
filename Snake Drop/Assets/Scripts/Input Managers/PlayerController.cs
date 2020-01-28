using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlayerController : MonoBehaviour
{
    public PlayerManager player;
    [SerializeField]
    private float autoMoveInterval;
    private float timeSinceLastAutoMove;
    private Direction mostRecentDirection;

    public void Awake()
    {
        TouchManager.OnSwipe += MoveSnakeOnSwipe;
        TouchManager.OnHold += MoveSnakeOnHold;
        TouchManager.OnTap += MoveSnakeOnTap;
    }

    //Temp to add keyboard controls
    public void Update()
    {
        if (Input.GetKeyDown("w")) { MoveSnake(Direction.UP); }
        if (Input.GetKeyDown("s")) { MoveSnake(Direction.DOWN); }
        if (Input.GetKeyDown("a")) { MoveSnake(Direction.LEFT); }
        if (Input.GetKeyDown("d")) { MoveSnake(Direction.RIGHT); }
    }

    private void MoveSnakeOnSwipe(TouchManager.TouchData Swipe)
    {
        MoveSnake(Swipe.direction);
    }

    private void MoveSnakeOnHold(TouchManager.TouchData Hold)
    {
        if (timeSinceLastAutoMove > autoMoveInterval)
        {
            timeSinceLastAutoMove = 0;
            MoveSnake(mostRecentDirection);
        }
        else
        {
            timeSinceLastAutoMove += Time.deltaTime;
        }
    }
    private void MoveSnakeOnTap(TouchManager.TouchData Tap)
    {
        MoveSnake(mostRecentDirection);
    }
    public void MoveSnake(Direction direction)
    {
        mostRecentDirection = direction;
        if (player) player.MoveSnake(direction);
    }

    public void ResetGame()
    {
        mostRecentDirection = Direction.DOWN;
    }

}
