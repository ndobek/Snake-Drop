using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayGrid gameBoard;
    public SnakeMaker snakeMaker;

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return Direction.DOWN;
            case Direction.DOWN: return Direction.UP;
            case Direction.LEFT: return Direction.RIGHT;
            case Direction.RIGHT: return Direction.LEFT;
            default: throw new System.Exception("Big OOPsy Doopsy" + this + "is not a real direction dumbass");
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

    private void Awake()
    {
        if(!instance) instance = this;
        TouchManager.OnSwipe += MoveSnakeOnSwipe;
        TouchManager.OnHold += MoveSnakeOnHold;
        TouchManager.OnTap += MoveSnakeOnTap;
        ContinueGame();
    }

    private void MoveSnakeOnSwipe(TouchManager.TouchData Swipe)
    {
        if (snakeHead && Swipe.direction != GetOppositeDirection(mostRecentDirection))
        {
            snakeHead.Eat(Swipe.direction);
            mostRecentDirection = Swipe.direction;
        }
    }

    private void MoveSnakeOnHold(TouchManager.TouchData Hold)
    {
        if (timeSinceLastAutoMove > autoMoveInterval)
        {
            SnakeHead.Eat(mostRecentDirection);
            timeSinceLastAutoMove = 0;
        }
        else
        {
            timeSinceLastAutoMove += Time.deltaTime;
        }
    }

    private void MoveSnakeOnTap(TouchManager.TouchData Tap)
    {
        SnakeHead.Eat(mostRecentDirection);
    }

    public void OnBlockDeath(Block obj)
    {
        if (obj == snakeHead) OnSnakeDeath(obj);
    }
    public void OnSnakeDeath(Block obj)
    {
        snakeHead = null;
        //CheckGameState
        ContinueGame();
        //EndGame
    }

    private void ContinueGame()
    {
        snakeHead = snakeMaker.MakeSnake(7, .3f, this);
    }
}
