using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayGrid gameBoard;
    public SnakeMaker snakeMaker;

    [HideInInspector]
    private Block snakeHead;
    public Block SnakeHead
    {
        get { return snakeHead; }
        set { snakeHead = value; }
    }

    private void Awake()
    {
        SwipeManager.OnSwipe += MoveSnakeOnSwipe;
        snakeHead = snakeMaker.MakeSnake(55, .3f, this);
    }

    private void MoveSnakeOnSwipe(SwipeManager.SwipeData swipe)
    {
        switch (swipe.direction)
        {
            case SwipeManager.SwipeDirection.Up:
                snakeHead.MoveUp();
                break;
            case SwipeManager.SwipeDirection.Down:
                snakeHead.MoveDown();
                break;
            case SwipeManager.SwipeDirection.Left:
                snakeHead.MoveLeft();
                break;
            case SwipeManager.SwipeDirection.Right:
                snakeHead.MoveRight();
                break;
        }
    }
}
