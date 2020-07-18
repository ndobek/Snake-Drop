using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using static Directions;

public class PlayerController : MonoBehaviour
{
    public PlayerManager player;
    [SerializeField]
    private float autoMoveInterval;
    [SerializeField]
    private float intervalToWaitBeforeHold;
    private float timeSinceLastMove;
    private float timePressed;
    [HideInInspector]
    public Direction mostRecentDirection;

    [SerializeField]
    private float AxisDeadzone;
    private bool FirstPressComplete;

    public void Awake()
    {
        //TouchManager.OnSwipe += MoveSnakeOnSwipe;
        //TouchManager.OnHold += MoveSnakeOnHold;
        //TouchManager.OnTap += MoveSnakeOnTap;
    }

    //Temp to add keyboard controls
    public void Update()
    {
        //if (Input.GetKeyDown("w") || Input.GetKeyDown("up")) { FirstPress(Direction.UP); }
        //if (Input.GetKeyDown("s") || Input.GetKeyDown("down")) { FirstPress(Direction.DOWN); }
        //if (Input.GetKeyDown("a") || Input.GetKeyDown("left")) { FirstPress(Direction.LEFT); }
        //if (Input.GetKeyDown("d") || Input.GetKeyDown("right")) { FirstPress(Direction.RIGHT); }

        //if (Input.GetKey("w") || Input.GetKey("up")) { Hold(Direction.UP); }
        //if (Input.GetKey("s") || Input.GetKey("down")) { Hold(Direction.DOWN); }
        //if (Input.GetKey("a") || Input.GetKey("left")) { Hold(Direction.LEFT); }
        //if (Input.GetKey("d") || Input.GetKey("right")) { Hold(Direction.RIGHT); }

        //if (CrossPlatformInputManager.GetAxis("Horizontal") >= JoystickSensitivity) { Hold(Direction.RIGHT); }
        //if (CrossPlatformInputManager.GetAxis("Horizontal") <= -JoystickSensitivity) { Hold(Direction.LEFT); }
        //if (CrossPlatformInputManager.GetAxis("Vertical") >= JoystickSensitivity) { Hold(Direction.UP); }
        //if (CrossPlatformInputManager.GetAxis("Vertical") <= -JoystickSensitivity) { Hold(Direction.DOWN); }
        if (BothAxisBelowDeadzone()) ResetTime();
        GetInput(Direction.RIGHT, CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        GetInput(Direction.LEFT, -CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        GetInput(Direction.UP, CrossPlatformInputManager.GetAxisRaw("Vertical"));
        GetInput(Direction.DOWN, -CrossPlatformInputManager.GetAxisRaw("Vertical"));
    }
    private void GetInput(Directions.Direction direction, float Axis)
    {

        if (!FirstPressComplete && Axis >= AxisDeadzone)
        {
            FirstPress(direction);
        }
        else if(HoldTimeElapsed())
        {
            Hold(direction, Axis);
        }

    }

    private bool BothAxisBelowDeadzone()
    {
        return Math.Abs(CrossPlatformInputManager.GetAxisRaw("Horizontal")) <= AxisDeadzone && Math.Abs(CrossPlatformInputManager.GetAxisRaw("Vertical")) <= AxisDeadzone;
    }

    private void ResetTime()
    {
        timePressed = Time.time;
        FirstPressComplete = false;
    }

    private void MoveSnakeOnSwipe(TouchManager.TouchData Swipe)
    {
        Press(Swipe.direction); 
    }

    private void MoveSnakeOnHold(TouchManager.TouchData HoldData)
    {
        Hold(HoldData.direction, HoldData.distance);
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
        ResetTime();
        FirstPressComplete = true;
        Press(direction);
    }
    private void FirstPress()
    {
        FirstPress(mostRecentDirection);
    }

    private void Hold(Directions.Direction direction, float autoMoveMultiplier = 1)
    {
        if (timeSinceLastMove * autoMoveMultiplier > autoMoveInterval && HoldTimeElapsed())
        {
            Press(direction);
        }
        else
        {
            timeSinceLastMove += Time.deltaTime;
        }
    }

    private bool HoldTimeElapsed()
    {
        return (Time.time - timePressed) >= intervalToWaitBeforeHold;
    }
    public void MoveSnake(Direction direction)
    {
        mostRecentDirection = direction;
        if (player)
        {
            player.mostRecentDirection = direction;
            player.MoveSnake(direction);
        }
    }
}
