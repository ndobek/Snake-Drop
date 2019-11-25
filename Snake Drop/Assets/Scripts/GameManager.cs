using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayGrid gameBoard;

    [HideInInspector]
    private Block snakeHead;
    public Block SnakeHead
    {
        get { return snakeHead; }
        set { snakeHead = value; }
    }

    public BlockType test;

    private void Awake()
    {
        SwipeManager.OnSwipe += MoveSnakeOnSwipe;
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

    private void Update()
    {
        if(snakeHead == null)
        {
            gameBoard.SetBlock(0, 0, test);
            snakeHead = gameBoard.blocks[0, 0];
        }
    }
}
