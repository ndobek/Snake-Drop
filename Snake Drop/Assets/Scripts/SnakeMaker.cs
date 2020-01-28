using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SnakeMaker
{
    public static void MakeSnake(BlockSlot slot, SnakeInfo info)
    {
        if (slot.CheckIsClear())
        {
            int color = Random.Range(0, info.possibleColors.Length);
            int type = Random.Range(0, info.possibleTypes.Length);
            slot.CreateBlock(info.possibleColors[color], GameManager.instance.snakeHeadType);
            for (int i = 1; i < info.length; i++)
            {
                if (Random.value < info.entropy) color = Random.Range(0, info.possibleColors.Length);
                slot.CreateBlock(info.possibleColors[color], info.possibleTypes[type]);

                if (i > 0) slot.Blocks[i - 1].SetTail(slot.Blocks[i]);
            }
        }
    }
}
