using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMaker : BlockSlot
{
    [HideInInspector]
    public Block mostRecentSnake;

    public BlockType SnakeHeadType;
    public BlockType[] possibleTypes;
    public BlockColor[] possibleColors;


    public void MakeSnake(int length, float entropy)
    {
        if (CheckIsClear())
        {
            int color = Random.Range(0, possibleColors.Length);
            int type = Random.Range(0, possibleTypes.Length);
            CreateBlock(possibleColors[color], SnakeHeadType);
            for (int i = 1; i < length; i++)
            {
                if (Random.value < entropy) color = Random.Range(0, possibleColors.Length);
                CreateBlock(possibleColors[color], possibleTypes[type]);

                if (i > 0) Blocks[i - 1].SetTail(Blocks[i]);
            }
            mostRecentSnake = Blocks[0];
        }
    }
}
