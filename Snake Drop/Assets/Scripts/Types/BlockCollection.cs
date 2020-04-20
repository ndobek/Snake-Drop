using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollection : IComparable
{
    public int LeftCoord;
    public int RightCoord;
    public int TopCoord;
    public int BottomCoord;

    //Used in gameplay for when a snake is in a collection. Starts at one to account for when the collection first turns into a ghost
    public int FillAmount = 1;

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

    public bool CheckEdge(GameManager.Direction edge, int GridX, int GridY)
    {
        switch (edge)
        {
            case GameManager.Direction.UP: return GridY == TopCoord;
            case GameManager.Direction.DOWN: return GridY == BottomCoord;
            case GameManager.Direction.LEFT: return GridX == LeftCoord;
            case GameManager.Direction.RIGHT: return GridX == RightCoord;
        }
        return false;
    }
    public bool CheckEdge(GameManager.Direction edge, Block block)
    {
        return CheckEdge(edge, block.X, block.Y);
    }
    public bool CheckEdge(GameManager.Direction edge, BlockSlot slot)
    {
        return CheckEdge(edge, slot.x, slot.y);
    }

    public bool CoordsAreInCollection(int x, int y)
    {
        bool xIn = x >= LeftCoord && x <= RightCoord;
        bool yIn = y >= BottomCoord && y <= TopCoord;

        return xIn && yIn;
    }

    public bool isFull()
    {
        return FillAmount >= Area();
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


    private void Add(BlockSlot slot)
    {
        Slots[GridCoordsToIndex(slot.x, slot.y)] = slot;
    }

    public void SetType(BlockType type)
    {
        foreach(BlockSlot slot in Slots)
        {
            Block block = slot.Block;
            if (block)
            {
                block.SetBlockType(block.blockColor, type);
                Blocks[GridCoordsToIndex(block.X, block.Y)] = block;
                block.BlockCollection = this;
            }
            else
            {
                throw new SystemException("Trying to set the type of an incomplete block collection");
            }
        }
    }
}
