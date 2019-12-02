using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        if(!instance) instance = this;
        SwipeManager.OnSwipe += MoveSnakeOnSwipe;
        ContinueGame();
    }

    private void MoveSnakeOnSwipe(SwipeManager.SwipeData swipe)
    {
        if (snakeHead)
        {
            switch (swipe.direction)
            {
                case SwipeManager.SwipeDirection.Up:
                    snakeHead.Eat(BlockSlot.Neighbor.Up);
                    break;
                case SwipeManager.SwipeDirection.Down:
                    snakeHead.Eat(BlockSlot.Neighbor.Down);
                    break;
                case SwipeManager.SwipeDirection.Left:
                    snakeHead.Eat(BlockSlot.Neighbor.Left);
                    break;
                case SwipeManager.SwipeDirection.Right:
                    snakeHead.Eat(BlockSlot.Neighbor.Right);
                    break;
            }
        }
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
