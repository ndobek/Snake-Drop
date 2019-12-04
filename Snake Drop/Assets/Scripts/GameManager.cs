using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayGrid gameBoard;
    public SnakeMaker snakeMaker;
    public BlockSlot waitSlot;

    public int HeightLimit;

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public static Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return Direction.DOWN;
            case Direction.DOWN: return Direction.UP;
            case Direction.LEFT: return Direction.RIGHT;
            case Direction.RIGHT: return Direction.LEFT;
            default: throw new System.Exception("Big OOPsy Doopsy that is not a real direction dumbass");
        }
    }

    private Direction mostRecentDirection;
    [SerializeField]
    private float autoMoveInterval;
    private float timeSinceLastAutoMove;

    [HideInInspector]
    private Block snakeHead;
    public Block SnakeHead
    {
        get { return snakeHead; }
        set { snakeHead = value; }
    }

    private List<Block> PreviewSnakes = new List<Block>();

    private void Awake()
    {
        if(!instance) instance = this;
        TouchManager.OnSwipe += MoveSnakeOnSwipe;
        TouchManager.OnHold += MoveSnakeOnHold;
        TouchManager.OnTap += MoveSnakeOnTap;
        StartGame();
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

        if (direction != Direction.UP | SnakeHead.Coords().y < HeightLimit)
        {
            SnakeHead.Eat(direction);
            mostRecentDirection = direction;
            FillPreviewBar();
            LowerHeightLimit();
        }
    }

    public void OnBlockDeath(Block obj)
    {
        if (obj == snakeHead) OnSnakeDeath(obj);
    }
    public void OnSnakeDeath(Block obj)
    {
        gameBoard.Fall();
        if (waitSlot.GetNeighbor(Direction.DOWN).Block != null)
        {
            EndGame();
        }
        else
        {
            ContinueGame();
        }
    }
    private void ShiftPreviewBar()
    {
        foreach(Block snake in PreviewSnakes)
        {
            snake.Move(Direction.LEFT);
        }
        MakeSnake();
    }

    private void FillPreviewBar()
    {
        MakeSnake();
        while (PreviewSnakes.Count > 0 && PreviewSnakes[0].Slot != waitSlot && PreviewSnakes[0].Neighbor(Direction.LEFT).Block == null)
        {
            ShiftPreviewBar();
        }
    }

    private void LowerHeightLimit()
    {
        int temp = SnakeHead.FindSnakeMaxY() + 2;
        if (temp < HeightLimit) HeightLimit = temp;
    }

    private void ResetMoveRestrictions()
    {
        HeightLimit = gameBoard.YSize + 1;
        mostRecentDirection = Direction.DOWN;
    }

    private void MakeSnake()
    {
        if (snakeMaker.CheckIsClear()) PreviewSnakes.Add(snakeMaker.MakeSnake(25, .1f, this));
    }

    private void ActivateSnake(Block newSnakeHead)
    {
        snakeHead = newSnakeHead;
        snakeHead.ActivateSnake();
    }

    private void StartGame()
    {
        MakeSnake();
        FillPreviewBar();
        ResetMoveRestrictions();
        ContinueGame();
    }
    private void ContinueGame()
    {
        snakeHead = waitSlot.Block;
        snakeHead.ActivateSnake();
        snakeHead.Move(Direction.DOWN);
        ResetMoveRestrictions();
        if (PreviewSnakes.Count > 0 && snakeHead == PreviewSnakes[0]) PreviewSnakes.RemoveAt(0);
        FillPreviewBar();
    }
    private void EndGame()
    {
        Debug.Log("Game Over");
    }
}
