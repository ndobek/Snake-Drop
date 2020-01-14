using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollection : IComparable
{
    public int LeftCoord;
    public int RightCoord;
    public int TopCoord;
    public int BottomCoord;


    public Block[] Blocks;
    public BlockSlot[] Slots;

    public int XSize() { return RightCoord - (LeftCoord - 1); }
    public int YSize() { return TopCoord - (BottomCoord - 1); }

    public int Area()
    {
        return XSize() * YSize();
    }

    public int CompareTo(object obj)
    {
        BlockCollection rect2 = obj as BlockCollection;
        if (Area() == rect2.Area()) return 0;
        else return Area() < rect2.Area() ? 1 : -1;
    }

    private int FlattenedIndex(int x, int y)
    {
        return y * XSize() + x;
    }
    private Vector2 IndexToCoords(int i)
    {
        return new Vector2(i % XSize(), i / XSize());
    }
    private int GridCoordsToIndex(int GridX, int GridY)
    {
        Vector2 Coords = GridToCollectionCoords(GridX, GridY);
        return FlattenedIndex((int)Coords.x, (int)Coords.y);
    }
    public Vector2 GridToCollectionCoords(int GridX, int GridY)
    {
        return new Vector2(GridX - LeftCoord, GridY - BottomCoord);
    }
    public Vector2 GridToCollectionCoords(Block block)
    {
        return GridToCollectionCoords(block.X, block.Y);
    }
    public Vector2 GridToCollectionCoords(BlockSlot slot)
    {
        return GridToCollectionCoords(slot.x, slot.y);
    }


    public void Build(PlayGrid grid)
    {
        Blocks = new Block[XSize() * YSize()];
        Slots = new BlockSlot[XSize() * YSize()];

        for (int x = LeftCoord; x <= RightCoord; x++)
        {
            for (int y = BottomCoord; y <= TopCoord; y++)
            {
                Add(grid.GetSlot(x, y));
            }
        }
    }
    public void UpdateCoords()
    {
        LeftCoord = Blocks[0].X;
        RightCoord = Blocks[0].X;
        TopCoord = Blocks[0].Y;
        BottomCoord = Blocks[0].Y;

        foreach (Block block in Blocks)
        {
            LeftCoord = Mathf.Min(block.X, LeftCoord);
            RightCoord = Mathf.Max(block.X, RightCoord);
            TopCoord = Mathf.Max(block.Y, TopCoord);
            BottomCoord = Mathf.Min(block.Y, BottomCoord);
        }
    }


    public void Add(BlockSlot slot)
    {
        Block block = slot.Block;

        Slots[GridCoordsToIndex(block.X, block.Y)] = slot;

        block.SetBlockType(block.blockColor, GameManager.instance.collectionType);
        Blocks[GridCoordsToIndex(block.X, block.Y)] = block;
        block.BlockCollection = this;
    }

    //private bool CheckArea(BlockSlot origin, System.Func<BlockSlot, bool> action, bool defaultResult = true)
    //{
    //    bool result = defaultResult;

    //    BlockSlot temp = origin;
    //    for (int x = 0; x < xSize; x++)
    //    {
    //        for (int y = 0; y < ySize; y++)
    //        {
    //            bool tempResult = action.Invoke(temp);
    //            if (tempResult != defaultResult) result = tempResult;
    //            temp = temp.GetNeighbor(GameManager.Direction.DOWN);
    //        }
    //        temp = temp.GetNeighbor(GameManager.Direction.RIGHT);
    //    }

    //    return result;
    //}

    //public override void RawMoveTo(BlockSlot obj)
    //{
    //    base.RawMoveTo(obj);
    //}

    //public override void BasicMoveTo(BlockSlot obj)
    //{
    //    bool BasicCheck(BlockSlot slot)
    //    {
    //        if (slot && slot.Block && slot.Block == this) return true;
    //        return blockType.moveRules[0].CanMoveTo(this, obj);
    //    }


    //    if (CheckArea(obj, BasicCheck))
    //    {
    //        base.BasicMoveTo(obj);
    //    }
    //}

    //public override void ActionMoveTo(BlockSlot obj)
    //{
    //    base.ActionMoveTo(obj);
    //}
}
