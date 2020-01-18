using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMaker : BlockSlot
{
    public Block mostRecentSnake;
    public BlockColor[] possibleColors;

    public void MakeSnake(int length, float entropy)
    {
        //Blocks.Clear();
        if (CheckIsClear())
        {
            int color = Random.Range(0, possibleColors.Length);
            CreateBlock(possibleColors[color], GameManager.instance.snakeHeadType);
            for (int i = 1; i < length; i++)
            {
                if (Random.value < entropy) color = Random.Range(0, possibleColors.Length);
                CreateBlock(possibleColors[color], GameManager.instance.snakeType);
                //Blocks[i].isPartOfSnake = true;
                if (i > 0) Blocks[i - 1].SetTail(Blocks[i]);
            }
            mostRecentSnake = Blocks[0];
        }
    }
}
