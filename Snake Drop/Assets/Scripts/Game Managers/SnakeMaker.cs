using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SnakeMaker
{
    public static void MakeSnake(BlockSlot slot, SnakeInfo info)
    {
        BlockType currentType = info.GetRandomType();
        BlockColor currentColor = info.GetRandomColor();

        if (currentType == null || currentColor == null) return;

        slot.CreateBlock(currentColor, GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType);
        for (int i = 1; i < info.length; i++)
        {
            if (Random.value < info.entropy) currentColor = info.GetRandomColor();

            currentType = info.GetRandomType();
            slot.CreateBlock(currentColor, currentType);

            if (i > 0) slot.Blocks[i - 1].SetTail(slot.Blocks[i]);
        }
    }

}
