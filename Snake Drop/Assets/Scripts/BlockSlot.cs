﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    [HideInInspector]
    public int x;
    [HideInInspector]
    public int y;
    [HideInInspector]
    public PlayGrid playGrid;

    public BlockSlot customRightNeighbor;
    public BlockSlot customLeftNeighbor;
    public BlockSlot customUpNeighbor;
    public BlockSlot customDownNeighbor;

    private Block block;
    public Block Block
    {
        get { return block; }
    }

    public enum Neighbor
    {
        Right,
        Left,
        Up,
        Down
    }
    public BlockSlot GetNeighbor(Neighbor neighbor)
    {
        switch (neighbor)
        {
            case Neighbor.Right:
                if (customRightNeighbor) return customRightNeighbor;
                else return playGrid.GetSlot(x + 1, y);
            case Neighbor.Left:
                if (customLeftNeighbor) return customLeftNeighbor;
                else return playGrid.GetSlot(x - 1, y);
            case Neighbor.Up:
                if (customUpNeighbor) return customUpNeighbor;
                else return playGrid.GetSlot(x, y + 1);
            case Neighbor.Down:
                if (customDownNeighbor) return customDownNeighbor;
                else return playGrid.GetSlot(x, y - 1);
        }
        return null;
    }

    public void MoveBlockHere(Block obj)
    {
        block = obj;
        block.MoveTo(this);
    }
    public void SetBlock(BlockType type)
    {
        DeleteBlock();
        block = Instantiate(playGrid.blockObj, playGrid.position(x, y), Quaternion.identity, this.transform);
        block.SetBlockType(type);
        block.MoveTo(this);
    }
    public void DeleteBlock()
    {
        if(block) GameObject.Destroy(block);
        block = null;
    }
    public void UpdateBlock()
    {
        if(block) block.UpdateBlock();
    }
    public static void SwapBlocks(BlockSlot obj1, BlockSlot obj2)
    {
        Block swap = obj1.Block;
        obj1.MoveBlockHere(obj2.Block);
        obj2.MoveBlockHere(swap);

        obj1.UpdateBlock();
        obj2.UpdateBlock();
    }
}
