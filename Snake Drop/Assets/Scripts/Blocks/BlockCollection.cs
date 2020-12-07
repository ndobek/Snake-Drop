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

    public bool CheckEdge(Directions.Direction edge, int GridX, int GridY)
    {
        switch (edge)
        {
            case Directions.Direction.UP: return GridY == TopCoord;
            case Directions.Direction.DOWN: return GridY == BottomCoord;
            case Directions.Direction.LEFT: return GridX == LeftCoord;
            case Directions.Direction.RIGHT: return GridX == RightCoord;
        }
        return false;
    }
    public bool CheckEdge(Directions.Direction edge, Block block)
    {
        return CheckEdge(edge, block.X, block.Y);
    }
    public bool CheckEdge(Directions.Direction edge, BlockSlot slot)
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
        return FillPercentage() >= 1;
    }
    public float FillPercentage()
    {
        return (float)FillAmount/(float)Area();
    }
    private void Rebuild()
    {
       if(Blocks[0] != null) Build(Blocks[0].Slot.playGrid, Blocks[0].blockType);
    }

    public void Build(PlayGrid grid, BlockType type = null)
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
        UpdateFillSprites();
        if (type) SetType(type);
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

        Rebuild();
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
    public void IncreaseFillAmmount(PlayerManager player)
    {
        FillAmount += 1;
        player.Score.Multiplier += 1;
        UpdateFillSprites();
    }
    public void UpdateFillSprites()
    {
        foreach(Block block in Blocks)
        {
            if (block)
            {
                block.AnimationManager.AddAnimation(new BlockAnimation(block, block.blockType.BlockFillAnimator));
            }
        }
    }

    //public BlockCollectionSaveData Save()
    //{
    //    return new BlockCollectionSaveData
    //    {
    //        LeftCoord = LeftCoord,
    //        RightCoord = RightCoord,
    //        TopCoord = TopCoord,
    //        BottomCoord = BottomCoord,
    //        FillAmount = FillAmount
    //    };
    //}

}
