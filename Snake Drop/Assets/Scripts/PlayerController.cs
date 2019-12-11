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
        set { snakeHead = value; }
    }

    [HideInInspector]
    public int HeightLimit;

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

    private void MoveSnakeOnSwipe(TouchManager.TouchData Swipe)
    {
        if (snakeHead && Swipe.direction != GetOppositeDirection(mostRecentDirection))
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

        if (GameManager.instance.GameInProgress && direction != Direction.UP | SnakeHead.Coords().y < HeightLimit)
        {
            mostRecentDirection = direction;
            SnakeHead.ActionMove(direction);
            GameManager.instance.FillPreviewBar();
            if (GameManager.instance.GameInProgress) LowerHeightLimit();
        }
    }

    private void ActivateSnake(Block newSnakeHead)
    {
        snakeHead = newSnakeHead;
        snakeHead.ActivateSnake();
    }

    private void LowerHeightLimit()
    {
        int temp = SnakeHead.FindSnakeMaxY() + 2;
        if (temp < HeightLimit) HeightLimit = temp;
    }

    public void ResetMoveRestrictions()
    {
        HeightLimit = GameManager.instance.playGrid.YSize + 1;
        mostRecentDirection = Direction.DOWN;
    }

    public void ResetGame()
    {
        ResetMoveRestrictions();
    }

}
