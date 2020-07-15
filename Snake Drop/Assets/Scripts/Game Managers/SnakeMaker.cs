﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SnakeMaker
{
    public static void MakeSnake(BlockSlot slot, SnakeInfo info)
    {
        BlockType currentType = Stat<BlockType>.GetRandomFromStats(info.possibleTypes);
        BlockColor currentColor = Stat<BlockColor>.GetRandomFromStats(info.possibleColors);
        slot.CreateBlock(currentColor, GameManager.instance.snakeHeadType);
        for (int i = 1; i < info.length; i++)
        {
            if (Random.value < info.entropy) currentColor = Stat<BlockColor>.GetRandomFromStats(info.possibleColors);

            currentType = Stat<BlockType>.GetRandomFromStats(info.possibleTypes);
            slot.CreateBlock(currentColor, currentType);

            if (i > 0) slot.Blocks[i - 1].SetTail(slot.Blocks[i]);
        }
    }

}