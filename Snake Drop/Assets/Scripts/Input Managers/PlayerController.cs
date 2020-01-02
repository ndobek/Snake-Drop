using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    private Block snakeHead;
    public Block SnakeHead
    {
        get { return snakeHead; }
        set { ActivateSnake(value); }
    }

    private Direction mostRecentDirection;
    [SerializeField]
    private float autoMoveInterval;
    private float timeSinceLastAutoMove;

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
        if (snakeHead)
        {
            MoveSnake(Swipe.direction);
        }
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

        if (GameManager.instance.GameInProgress)
        {
            mostRecentDirection = direction;
            SnakeHead.ActionMove(direction);
            GameManager.instance.FillPreviewBar();
            GameManager.instance.HeightLimitIndicator.LowerHeightLimit(SnakeHead.FindSnakeMaxY() + 2);
        }
    }

    private void ActivateSnake(Block newSnakeHead)
    {
        if(snakeHead) snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.defaultType);
        snakeHead = newSnakeHead;
        snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.snakeHeadType);
        snakeHead.ActivateSnake();
    }

    public void ResetGame()
    {
        mostRecentDirection = Direction.DOWN;
    }

}
