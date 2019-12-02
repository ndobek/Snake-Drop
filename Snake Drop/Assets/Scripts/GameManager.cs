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
        Up,
        Down,
        Left,
        Right
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
        ContinueGame();
    }

    private void MoveSnakeOnSwipe(TouchManager.SwipeData swipe)
    {
        if (snakeHead)
        {
            snakeHead.Eat(swipe.direction);
            mostRecentDirection = swipe.direction;
        }
    }

    private void MoveSnakeOnHold(TouchManager.HoldData Hold)
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
        Debug.Log(Hold.TimeHeld);
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
