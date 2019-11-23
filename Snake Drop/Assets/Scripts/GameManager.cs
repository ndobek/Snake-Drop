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

    private void Update()
    {
        if(snakeHead == null)
        {
            gameBoard.SetBlock(0, 0, test);
            snakeHead = gameBoard.blocks[0, 0];
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;
            float centeredX = (touchPos.x / Camera.main.scaledPixelWidth) - .5f;
            float centeredY = (touchPos.y / Camera.main.scaledPixelHeight) - .5f;
            if (touch.phase == TouchPhase.Began && snakeHead != null)
            {
                if(Mathf.Abs(centeredX) > Mathf.Abs(centeredY))
                {
                    if (centeredX > 0) snakeHead.MoveRight();
                    else snakeHead.MoveLeft();
                }
                else
                {
                    if (centeredY > 0) snakeHead.MoveUp();
                    else snakeHead.MoveDown();
                }
            }

        }
    }
}
