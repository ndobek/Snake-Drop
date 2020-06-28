using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlayerController : MonoBehaviour
{
    public PlayerManager player;
    [SerializeField]
    private float autoMoveInterval;
    [SerializeField]
    private float intervalToWaitBeforeHold;
    private float timeSinceLastMove;
    private float timePressed;
    public Direction mostRecentDirection;

    public void Awake()
    {
        TouchManager.OnSwipe += MoveSnakeOnSwipe;
        TouchManager.OnHold += MoveSnakeOnHold;
        TouchManager.OnTap += MoveSnakeOnTap;
    }

    //Temp to add keyboard controls
    public void Update()
    {
        if (Input.GetKeyDown("w") | Input.GetKeyDown("up")) { FirstPress(Direction.UP); }
        if (Input.GetKeyDown("s") | Input.GetKeyDown("down")) { FirstPress(Direction.DOWN); }
        if (Input.GetKeyDown("a") | Input.GetKeyDown("left")) { FirstPress(Direction.LEFT); }
        if (Input.GetKeyDown("d") | Input.GetKeyDown("right")) { FirstPress(Direction.RIGHT); }

        if (Input.GetKey("w") | Input.GetKey("up")) { Hold(Direction.UP); }
        if (Input.GetKey("s") | Input.GetKey("down")) { Hold(Direction.DOWN); }
        if (Input.GetKey("a") | Input.GetKey("left")) { Hold(Direction.LEFT); }
        if (Input.GetKey("d") | Input.GetKey("right")) { Hold(Direction.RIGHT); }
    }

    private void MoveSnakeOnSwipe(TouchManager.TouchData Swipe)
    {
        Press(Swipe.direction); 
    }

    private void MoveSnakeOnHold(TouchManager.TouchData HoldData)
    {
        Hold(mostRecentDirection);
    }
    private void MoveSnakeOnTap(TouchManager.TouchData Tap)
    {
        Press();
    }

    private void Press(Direction direction)
    {
        timeSinceLastMove = 0;
        MoveSnake(direction);
    }
    private void Press()
    {
        Press(mostRecentDirection);
    }
    private void FirstPress(Direction direction)
    {
        timePressed = Time.time;
        Press(direction);
    }
    private void FirstPress()
    {
        FirstPress(mostRecentDirection);
    }

    private void Hold(Direction direction)
    {
        if (timeSinceLastMove > autoMoveInterval && (Time.time - timePressed) >= intervalToWaitBeforeHold)
        {
            Press(direction);
        }
        else
        {
            timeSinceLastMove += Time.deltaTime;
        }
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
