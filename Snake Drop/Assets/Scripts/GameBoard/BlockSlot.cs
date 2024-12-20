﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : MonoBehaviour
{
    #region Variables

    #region Grid and Coordinates

    public PlayGrid playGrid;
    public SlotType slotType;

    public int x;
    public int y;
    public Vector2 Coords
    {
        get { return new Vector2(x, y); }
    }


    public class EdgeInfo
    {
        public bool Top;
        public bool Bottom;
        public bool Left;
        public bool Right;

        public Directions.Direction direction()
        {
            Directions.Direction result = Directions.Direction.DOWN;

            if (Top)
            {
                result = Directions.Direction.UP;
            }
            if (Bottom)
            {
                result = Directions.Direction.DOWN;
            }
            if (Right)
            {
                result = Directions.Direction.RIGHT;
            }
            if (Left)
            {
                result = Directions.Direction.LEFT;
            }
            return result;
        }

    }
    public EdgeInfo GetEdgeInfo()
    {
        int TopY = playGrid.YSize - 1;
        int BottomY = 0;

        int RightX = playGrid.XSize - 1;
        int LeftX = 0;

        return new EdgeInfo()
        {
            Top = y == TopY,
            Bottom = y == BottomY,
            Right = x == RightX,
            Left = x == LeftX
        };
    
            
    }

    public bool IsOnEdge(Directions.Direction direction)
    {
        EdgeInfo info = GetEdgeInfo();
        switch (direction)
        {
            case Directions.Direction.UP:
                return info.Top;
            case Directions.Direction.DOWN:
                return info.Bottom;
            case Directions.Direction.LEFT:
                return info.Left;
            case Directions.Direction.RIGHT:
                return info.Right;
            default:
                return false;
            
        }

    }

    #endregion

    #region Neighbors

    public BlockSlot customRightNeighbor;
    public BlockSlot customLeftNeighbor;
    public BlockSlot customUpNeighbor;
    public BlockSlot customDownNeighbor;

    public BlockSlot GetNeighbor(int x, int y)
    {
        BlockSlot destination = this;
        destination = destination.GetNeighbor(Directions.Direction.RIGHT, x);
        if(destination != null) destination = destination.GetNeighbor(Directions.Direction.UP, y);
        return destination;
    }
    public BlockSlot GetNeighbor(Directions.Direction neighbor, int distance = 1)
    {
        switch (neighbor)
        {
            case Directions.Direction.RIGHT:
                if (customRightNeighbor && distance > 0) return customRightNeighbor;
                else return playGrid.GetSlot(x + distance, y);
            case Directions.Direction.LEFT:
                if (customLeftNeighbor && distance > 0) return customLeftNeighbor;
                else return playGrid.GetSlot(x - distance, y);
            case Directions.Direction.UP:
                if (customUpNeighbor && distance > 0) return customUpNeighbor;
                else return playGrid.GetSlot(x, y + distance);
            case Directions.Direction.DOWN:
                if (customDownNeighbor && distance > 0) return customDownNeighbor;
                else return playGrid.GetSlot(x, y - distance);
        }
        return null;
    }

    public bool HasNeighbor(BlockSlot slot)
    {
        if(GetNeighbor(Directions.Direction.UP) == slot) return true;
        if(GetNeighbor(Directions.Direction.DOWN) == slot) return true;
        if (GetNeighbor(Directions.Direction.LEFT) == slot) return true;
        if (GetNeighbor(Directions.Direction.RIGHT) == slot) return true;

        return false;
    }

    #endregion

    [HideInInspector]
    public List<Block> Blocks;
    public Block Block
    {
        get
        {
            if (Blocks.Count > 0) return Blocks[0];
            else return null;
        }
    }

    #endregion

    #region Methods to Assign and Unassign Blocks

    public virtual void OnAssignment(Block obj, PlayerManager player = null)
    {
        Blocks.Add(obj);
        if (slotType) slotType.OnAssignment(obj, player);
    }
    public virtual void OnUnassignment(Block obj, PlayerManager player = null)
    {
        if (slotType) slotType.OnUnassignment(obj, player);
        if (Blocks.Contains(obj)) Blocks.Remove(obj);
    }

    #endregion

    #region Methods to Create and Delete blocks

    public virtual void DeleteBlock()
    {
        for(int i = Blocks.Count; i > 0; i--)
        {
            Blocks[i-1].RawBreak();
        }
    }
    public Block CreateBlock(BlockColor color, BlockType type)
    {
        Block newBlock = Instantiate(GameManager.instance.GameModeManager.GameMode.TypeBank.blockObj, playGrid.CoordsPosition(x, y), Quaternion.identity, this.transform);
        newBlock.SetBlockType(color, type);
        MoveBlockHere(newBlock);
        return newBlock;
    }
    public void SetBlock(BlockColor color, BlockType type)
    {
        DeleteBlock();
        CreateBlock(color, type);
    }
    public void SetOwner(PlayerManager player)
    {
        foreach(Block block in Blocks)
        {
            block.SetOwner(player);
        }
    }
    public bool CheckIsClear()
    {
        return Blocks.Count == 0;
    }
    public void RefreshBlocklocation()
    {
        foreach(Block block in Blocks)
        {
            block.transform.localPosition = Vector3.zero;
        }
    }

    #endregion

    #region Methods to update blocks

    public void MoveBlockHere(Block obj)
    {
        //Don't Change this, add to OnAssignment instead
        obj.RawMoveTo(this);
    }
    #endregion

    //public BlockSlotSaveData Save(SaveData save)
    //{
    //    List<BlockSaveData> BlockSaveData = new List<BlockSaveData>();
    //    for(int i = 0; i < Blocks.Count; i++)
    //    {
    //        BlockSaveData.Add(Blocks[i].Save(save, i));
    //    }
    //    return new BlockSlotSaveData()
    //    {
    //        BlockData = BlockSaveData,
    //        x = x,
    //        y = y
    //    };
    //}

    //public void Load(BlockSlotSaveData save)
    //{
    //    DeleteBlock();
    //    for(int i = 0; i < save.BlockData.Count; i++)
    //    {
    //        Blocks.Add(CreateBlock(GameManager.instance.Colors.getObject(save.BlockData[i].blockColor) as BlockColor, GameManager.instance.Types.getObject(save.BlockData[i].blockType) as BlockType));
    //    }
    //}

}
