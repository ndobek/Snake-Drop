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

    private Block block;
    public Block Block
    {
        get { return block; }
    }

    //public enum Neighbor
    //{
    //    Right,
    //    Left,
    //    Up,
    //    Down
    //}
    public BlockSlot GetNeighbor(GameManager.Direction neighbor)
    {
        switch (neighbor)
        {
            case GameManager.Direction.Right:
                if (customRightNeighbor) return customRightNeighbor;
                else return playGrid.GetSlot(x + 1, y);
            case GameManager.Direction.Left:
                if (customLeftNeighbor) return customLeftNeighbor;
                else return playGrid.GetSlot(x - 1, y);
            case GameManager.Direction.Up:
                if (customUpNeighbor) return customUpNeighbor;
                else return playGrid.GetSlot(x, y + 1);
            case GameManager.Direction.Down:
                if (customDownNeighbor) return customDownNeighbor;
                else return playGrid.GetSlot(x, y - 1);
        }
        return null;
    }

    public virtual void OnAssignment(Block obj)
    {
        block = obj;
    }

    public void MoveBlockHere(Block obj)
    {
        obj.MoveTo(this);
    }
    public void SetBlock(BlockType type)
    {
        DeleteBlock();
        CreateBlock(type);
    }
    protected void CreateBlock(BlockType type)
    {
        Block newBlock = Instantiate(playGrid.blockObj, playGrid.position(x, y), Quaternion.identity, this.transform);
        newBlock.SetBlockType(type);
        MoveBlockHere(newBlock);
    }
    public void DeleteBlock()
    {
        if (block) GameObject.Destroy(block.gameObject);
        RemoveBlock();
    }
    public void RemoveBlock()
    {
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
