using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Directions;

public class PlayerController : MonoBehaviour
{
    public PlayerManager player;
    public PlayerAutoMover autoMover;
    public BoardRotator cameraRotator;
    public bool MoveOnCommandDuringRound = true;
    public bool MoveOnCommandBetweenRounds = true;
    [SerializeField]
    private float autoMoveInterval;
    [SerializeField]
    private float intervalToWaitBeforeHold;
    private float timeSinceLastMove;
    private float timePressed;

    [HideInInspector]
    private Direction mostRecentDirectionMoved;
    public Direction MostRecentDirectionMoved
    {
        get { return mostRecentDirectionMoved; }
        set
        {
            SecondMostRecentDirectionMoved = mostRecentDirectionMoved;
            mostRecentDirectionMoved = value;
        }
    }

    [HideInInspector]
    public Direction SecondMostRecentDirectionMoved;

    private Vector2 DistanceMovedThisTouch;
    public float DistanceNeededToDragBeforeSnakeMoves = 1;

    public void Awake()
    {
        TouchManager.OnTouchBegin += ResetDistanceMovedThisTouch;
        TouchManager.OnSwipe += MoveSnakeOnSwipe;
        //TouchManager.OnDrag += MoveSnakeOnDrag;
        TouchManager.OnHold += MoveSnakeOnHold;
        TouchManager.OnTap += MoveSnakeOnTap;
    }

    //Temp to add keyboard controls
    public void Update()
    {
        //if (Input.GetButtonDown("Up")) { FirstPress(CameraDirectionTranslate(Direction.UP)); }
        //if (Input.GetButtonDown("Down")) { FirstPress(CameraDirectionTranslate(Direction.DOWN)); }
        //if (Input.GetButtonDown("Left")) { FirstPress(CameraDirectionTranslate(Direction.LEFT)); }
        //if (Input.GetButtonDown("Right")) { FirstPress(CameraDirectionTranslate(Direction.RIGHT)); }



        //if (Input.GetButton("Up")) { Hold(CameraDirectionTranslate(Direction.UP)); }
        //if (Input.GetButton("Down")) { Hold(CameraDirectionTranslate(Direction.DOWN)); }
        //if (Input.GetButton("Left")) { Hold(CameraDirectionTranslate(Direction.LEFT)); }
        //if (Input.GetButton("Right")) { Hold(CameraDirectionTranslate(Direction.RIGHT)); }


        if (Input.GetKeyDown("w") || Input.GetKeyDown("up")) { FirstPress(CameraDirectionTranslate(Direction.UP)); }
        if (Input.GetKeyDown("s") || Input.GetKeyDown("down")) { FirstPress(CameraDirectionTranslate(Direction.DOWN)); }
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left")) { FirstPress(CameraDirectionTranslate(Direction.LEFT)); }
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right")) { FirstPress(CameraDirectionTranslate(Direction.RIGHT)); }

        if (Input.GetKeyDown("z") || Input.GetKeyDown("backspace")) { player.Undoer.Undo(); }

        if (Input.GetKeyDown("e") || Input.GetKeyDown("right ctrl")) { cameraRotator.RotateCounterClockwise(); }
        if (Input.GetKeyDown("q") || Input.GetKeyDown("[0]")) { cameraRotator.RotateClockwise(); }

        if (Input.GetKey("w") || Input.GetKey("up")) { Hold(CameraDirectionTranslate(Direction.UP)); }
        if (Input.GetKey("s") || Input.GetKey("down")) { Hold(CameraDirectionTranslate(Direction.DOWN)); }
        if (Input.GetKey("a") || Input.GetKey("left")) { Hold(CameraDirectionTranslate(Direction.LEFT)); }
        if (Input.GetKey("d") || Input.GetKey("right")) { Hold(CameraDirectionTranslate(Direction.RIGHT)); }
    }

    private Direction CameraDirectionTranslate(Direction direction)
    {
        return TranslateDirection(direction, cameraRotator.currentDirection);
    }

    private void ResetDistanceMovedThisTouch(TouchManager.TouchData unused)
    {
        DistanceMovedThisTouch = Vector2.zero;
    }

    private void MoveSnakeOnDrag(TouchManager.TouchData Drag)
    {
        Vector2 DragDistance = Camera.main.ScreenToWorldPoint(Drag.endPos) - Camera.main.ScreenToWorldPoint(Drag.startPos);
        Vector2 UnmovedDistance = DragDistance - DistanceMovedThisTouch;
        if (IntervalToWaitBeforeHoldElapsed())
        {
            if (Mathf.Abs(UnmovedDistance.y) > DistanceNeededToDragBeforeSnakeMoves)
            {
                if (UnmovedDistance.y > 0) Press(Direction.UP);
                else Press(Direction.DOWN);
            }
            if (Mathf.Abs(UnmovedDistance.x) > DistanceNeededToDragBeforeSnakeMoves)
            {
                if (UnmovedDistance.x > 0) Press(Directions.Direction.RIGHT);
                else Press(Directions.Direction.LEFT);
            }
        }
    }

    private void MoveSnakeOnSwipe(TouchManager.TouchData Swipe)
    {
        Press(CameraDirectionTranslate(Swipe.direction)); 
    }

    private void MoveSnakeOnHold(TouchManager.TouchData HoldData)
    {
        Hold(CameraDirectionTranslate(MostRecentDirectionMoved));
    }
    private void MoveSnakeOnTap(TouchManager.TouchData Tap)
    {
        Press();
    }

    private void Press(Direction direction)
    {
        MostRecentDirectionMoved = direction;
        if ((MoveOnCommandDuringRound && player.RoundInProgress) || (MoveOnCommandBetweenRounds && !player.RoundInProgress))
        {
            switch (direction)
            {
                case Direction.UP:
                    DistanceMovedThisTouch.y += DistanceNeededToDragBeforeSnakeMoves;
                    break;
                case Direction.DOWN:
                    DistanceMovedThisTouch.y -= DistanceNeededToDragBeforeSnakeMoves;
                    break;
                case Direction.LEFT:
                    DistanceMovedThisTouch.x -= DistanceNeededToDragBeforeSnakeMoves;
                    break;
                case Direction.RIGHT:
                    DistanceMovedThisTouch.x += DistanceNeededToDragBeforeSnakeMoves;
                    break;
            }
            timeSinceLastMove = 0;
            MoveSnake(direction);
        }
    }
    private void Press()
    {
        Press(MostRecentDirectionMoved);
    }
    private void FirstPress(Direction direction)
    {
        timePressed = Time.time;
        if (autoMover != null && player.RoundInProgress) autoMover.Queue(direction);
        Press(direction);
    }
    private void FirstPress()
    {
        FirstPress(MostRecentDirectionMoved);
    }

    private void Hold(Direction direction)
    {
        if (timeSinceLastMove > autoMoveInterval && IntervalToWaitBeforeHoldElapsed())
        {
            Press(direction);
        }
        else
        {
            timeSinceLastMove += Time.deltaTime;
        }
    }

    public bool IntervalToWaitBeforeHoldElapsed()
    {
        return (Time.time - timePressed) >= intervalToWaitBeforeHold;
    }
    public void MoveSnake(Direction direction)
    {

        if (player) player.MoveSnake(direction);
    }

    public void ResetGame()
    {
        MostRecentDirectionMoved = Direction.DOWN;
    }

}
