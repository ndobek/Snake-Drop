using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    #region Variables

    #region Grid and Coordinates

    public PlayGrid playGrid;

    public int x;
    public int y;
    public Vector2 Coords
    {
        get { return new Vector2(x, y); }
    }

    #endregion

    #region Neighbors

    public BlockSlot customRightNeighbor;
    public BlockSlot customLeftNeighbor;
    public BlockSlot customUpNeighbor;
    public BlockSlot customDownNeighbor;

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

    #endregion

    protected Block block;
    public virtual Block Block
    {
        get { return block; }
    }

    #endregion

    #region Methods to Assign and Unassign Blocks

    public virtual void OnAssignment(Block obj)
    {
        block = obj;
    }
    public virtual void OnUnassignment(Block obj)
    {
        if (block == obj) block = null;
    }

    #endregion

    #region Methods to Create and Delete blocks

    public virtual void DeleteBlock()
    {
        if (block)
        {
            block.Break();
        }
    }
    protected void CreateBlock(BlockColor color, BlockType type)
    {
        Block newBlock = Instantiate(GameManager.instance.blockObj, playGrid.CoordsPosition(x, y), Quaternion.identity, this.transform);
        newBlock.SetBlockType(color, type);
        MoveBlockHere(newBlock);
    }
    public void SetBlock(BlockColor color, BlockType type)
    {
        DeleteBlock();
        CreateBlock(color, type);
    }

    #endregion

    #region Methods to update blocks

    public void MoveBlockHere(Block obj)
    {
        //Don't Change this, add to OnAssignment instead
        obj.RawMoveTo(this);
    }
    public void UpdateBlock()
    {
        if (block) block.UpdateBlock();
    }

    #endregion

}
