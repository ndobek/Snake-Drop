using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBlock : BlockType
{
    public int xSize;
    public int ySize;

    private BlockSlot[] BlockSlots;

    private BlockSlot[] Build(Block block)
    {
        List<BlockSlot> result = new List<BlockSlot>();

        BlockSlot temp = block.Slot;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                result.Add(temp);
                temp = temp.GetNeighbor(GameManager.Direction.DOWN);
            }
            temp = temp.GetNeighbor(GameManager.Direction.RIGHT);
        }

        return result.ToArray();
    }

    private void SizeCount(BlockSlot origin)
    {
        int newXSize = DimensionSizeCount(origin, GameManager.Direction.RIGHT);

        for(int x = 0; x < newXSize; x++)
        {
            int tempY = DimensionSizeCount(origin.GetNeighbor(GameManager.Direction.RIGHT, x), GameManager.Direction.DOWN);
            
        }
    }
    private int DimensionSizeCount(BlockSlot origin, GameManager.Direction direction)
    {
        int result = 0;
        BlockSlot current = origin;
        while(current.Block.blockColor == origin.Block.blockColor)
        {
            result += 1;
            current = current.GetNeighbor(direction);
        }
        return result;
    }

}
