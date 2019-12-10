using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    public int x;
    public int y;
    //[HideInInspector]
    public PlayGrid playGrid;

    public BlockSlot customRightNeighbor;
    public BlockSlot customLeftNeighbor;
    public BlockSlot customUpNeighbor;
    public BlockSlot customDownNeighbor;

    protected Block block;
    public virtual Block Block
    {
        get { return block; }
    }

    public BlockSlot GetNeighbor(GameManager.Direction neighbor)
    {
        switch (neighbor)
        {
            case GameManager.Direction.RIGHT:
                if (customRightNeighbor) return customRightNeighbor;
                else return playGrid.GetSlot(x + 1, y);
            case GameManager.Direction.LEFT:
                if (customLeftNeighbor) return customLeftNeighbor;
                else return playGrid.GetSlot(x - 1, y);
            case GameManager.Direction.UP:
                if (customUpNeighbor) return customUpNeighbor;
                else return playGrid.GetSlot(x, y + 1);
            case GameManager.Direction.DOWN:
                if (customDownNeighbor) return customDownNeighbor;
                else return playGrid.GetSlot(x, y - 1);
        }
        return null;
    }

    public virtual void OnAssignment(Block obj)
    {
        block = obj;
    }
    public virtual void OnUnassignment(Block obj)
    {
        if (block == obj) block = null;
    }

    public void MoveBlockHere(Block obj)
    {
        //Don't Change this, add to OnAssignment instead
        obj.MoveTo(this);
    }
    public void SetBlock(BlockColor color, BlockType type)
    {
        DeleteBlock();
        CreateBlock(color, type);
    }

    protected void CreateBlock(BlockColor color, BlockType type)
    {
        Block newBlock = Instantiate(playGrid.blockObj, playGrid.position(x, y), Quaternion.identity, this.transform);
        newBlock.SetBlockType(color, type);
        MoveBlockHere(newBlock);
    }
    public virtual void DeleteBlock()
    {
        if (block) GameObject.Destroy(block.gameObject);
        OnUnassignment(block);
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
