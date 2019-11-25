using System.Collections;
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

    public BlockSlot forwardNeighbor;
    public BlockSlot backwardNeighbor;

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
        Down,
        Forward,
        Backward
    }

    public BlockSlot GetNeighbor(Neighbor neighbor)
    {
        switch (neighbor)
        {
            case Neighbor.Right:
                return playGrid.GetSlot(x + 1, y);
            case Neighbor.Left:
                return playGrid.GetSlot(x - 1, y);
            case Neighbor.Up:
                return playGrid.GetSlot(x, y + 1);
            case Neighbor.Down:
                return playGrid.GetSlot(x, y - 1);
            case Neighbor.Forward:
                return forwardNeighbor;
            case Neighbor.Backward:
                return backwardNeighbor;
        }
        return null;
    }

    public void MoveBlockHere(Block obj)
    {
        block = obj;
        block.UpdateLocation(this);
    }
    public void SetBlock(BlockType type)
    {
        DeleteBlock();
        block = Instantiate(playGrid.blockObj, playGrid.position(x, y), Quaternion.identity, this.transform);
        block.SetBlockType(type);
        block.UpdateLocation(this);
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
