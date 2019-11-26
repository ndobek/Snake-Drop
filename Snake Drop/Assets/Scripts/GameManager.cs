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


    public BlockType test;
    public BlockType test2;
    private void Update()
    {
        if(snakeHead == null)
        {
            snakeMaker.MakeSnake(55, .9f, this);
            //gameBoard.SetBlock(0, 1, test);
            //gameBoard.SetBlock(1, 1, test2);
            //gameBoard.SetBlock(2, 1, test);
            //gameBoard.GetBlock(2, 1).SetTail(BlockSlot.Neighbor.Left);
            //gameBoard.GetBlock(1, 1).SetTail(BlockSlot.Neighbor.Left);

            //snakeHead = gameBoard.GetBlock(2, 1);
        }
    }
}
