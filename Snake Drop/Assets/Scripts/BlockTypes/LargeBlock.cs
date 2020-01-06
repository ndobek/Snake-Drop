using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBlock : Block
{
    public int xSize;
    public int ySize;

    private BlockSlot[] BlockSlots;
    private int FlattenedIndex(int x, int y)
    {
        return y * xSize + x;
    }

    private void Build(Block block, int _xSize, int _ySize)
    {
        BlockSlot[] result = new BlockSlot[xSize*ySize];
        xSize = _xSize;
        ySize = _ySize;

        BlockSlot temp = block.Slot;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                result[FlattenedIndex(x,y)] = temp;
                temp = temp.GetNeighbor(GameManager.Direction.DOWN);
            }
            temp = temp.GetNeighbor(GameManager.Direction.RIGHT);
        }

        BlockSlots = result;
    }

    private bool CheckArea(BlockSlot origin, System.Func<BlockSlot, bool> action, bool defaultResult = true)
    {
        bool result = defaultResult;

        BlockSlot temp = origin;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                bool tempResult = action.Invoke(temp);
                if (tempResult != defaultResult) result = tempResult;
                temp = temp.GetNeighbor(GameManager.Direction.DOWN);
            }
            temp = temp.GetNeighbor(GameManager.Direction.RIGHT);
        }

        return result;
    }

    public override void RawMoveTo(BlockSlot obj)
    {
        base.RawMoveTo(obj);
    }

    public override void BasicMoveTo(BlockSlot obj)
    {
        bool BasicCheck(BlockSlot slot)
        {
            if (slot && slot.Block && slot.Block == this) return true;
            return blockType.moveRules[0].CanMoveTo(this, obj);
        }
        if (CheckArea(obj, BasicCheck))
        {
            base.BasicMoveTo(obj);
        }
    }

    public override void ActionMoveTo(BlockSlot obj)
    {
        base.ActionMoveTo(obj);
    }
}
