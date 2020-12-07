using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayGrid : MonoBehaviour
{
    #region Grid Information

    public BlockSlot slotObj;
    public GridAction[] gridActions;

    [SerializeField]
    private int xSize;
    public int XSize
    {
        get { return xSize; }
        set { xSize = value; }
    }

    [SerializeField]
    private int ySize;
    public int YSize
    {
        get { return ySize; }
        set { ySize = value; }
    }

    public bool CheckInGrid(int x, int y)
    {
        if (
            x >= 0 &&
            x < xSize &&

            y >= 0 &&
            y < ySize
            ) { return true; }
        else return false;
    }

    [SerializeField]
    private float gridSpace;
    public float GridSpace
    {
        get { return gridSpace; }
        set { gridSpace = value; }
    }

    public Vector3 CoordsPosition(float x, float y)
    {
        float xAdj = ((XSize - 1) * gridSpace) / 2;
        float yAdj = ((YSize - 1) * gridSpace);

        Vector3 result = new Vector3((x * gridSpace) - xAdj, (y * gridSpace) - yAdj);
        result = this.transform.TransformPoint(result);
        return result;
    }

    #endregion

    #region Slots and relevant methods

    [SerializeField]
    public BlockSlot[] slots;
    protected int FlattenedIndex(int x, int y)
    {
        return y * xSize + x;
    }
    private Vector2 Coords(int i)
    {
        return new Vector2(i % xSize, i / xSize);
    }

    public BlockSlot GetSlot(int x, int y)
    {
        if (CheckInGrid(x, y)) return slots[FlattenedIndex(x, y)];
        else return null;
    }


    public void SetBlock(int x, int y, BlockColor color, BlockType type)
    {
        GetSlot(x, y).SetBlock(color, type);
    }
    public Block GetBlock(int x, int y)
    {
        return GetSlot(x, y).Block;
    }
    public void DeleteBlock(int x, int y)
    {
        GetSlot(x, y).DeleteBlock();
    }

    #endregion

    #region Grid initialization

    public virtual void CreateGrid()
    {
        slots = new BlockSlot[xSize * ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                CreateSlot(x, y);
            }
        }
    }
    protected void CreateSlot(int x, int y, BlockColor color, BlockType type)
    {
        CreateSlot(x, y);
        SetBlock(x, y, color, type);
    }
    protected virtual void CreateSlot(int x, int y)
    {
        if (CheckInGrid(x, y) && slots[FlattenedIndex(x, y)] == null)
        {
            int i = FlattenedIndex(x, y);
            slots[i] = Instantiate(slotObj, CoordsPosition(x, y), Quaternion.identity, this.transform);
            slots[i].playGrid = this;
            slots[i].x = x;
            slots[i].y = y;
        }
    }

    #endregion

    #region Methods for updating and maintenance.


    private bool dirty = false;
    public bool Dirty
    {
        get { return dirty; }
        set { dirty = value; }
    }
    public void SetDirty()
    {
        dirty = true;
    }

    public void InvokeGridAction()
    {
        foreach(GridAction action in gridActions)
        {
            action.Invoke(this);
        }
    }

    public void ClearGrid()
    {
        foreach (BlockSlot slot in slots)
        {
            slot.DeleteBlock();
        }
    }

    #endregion

    public int[][] GridAndBoolToIntArray(System.Func<BlockSlot, bool> condition)
    {
        int[][] result = new int[xSize][];

        for (int i = 0; i < xSize; i++)
        {
            result[i] = new int[ySize];
            for (int j = 0; j < ySize; j++)
            {
                result[i][j] = condition(GetSlot(i, j)) ? 1 : 0;
            }
        }

        return result;
    }

    public List<BlockSlot> EmptyBlockSlots(int minX, int maxX, int minY, int maxY)
    {
        List<BlockSlot> result = new List<BlockSlot>();
        foreach (BlockSlot slot in slots)
        {
            if (slot.Block == null &&
                slot.x >= minX &&
                slot.x < maxX &&
                slot.y >= minY &&
                slot.y < maxY)

                result.Add(slot);
        }
        return result;
    }

    public List<BlockSlot> EmptyBlockSlots()
    {
        return EmptyBlockSlots(0, XSize, 0, ySize);
    }

    //public GridSaveData Save(SaveData save)
    //{
    //    List<BlockSlotSaveData> BlockSlotSaveData = new List<BlockSlotSaveData>();
    //    foreach (BlockSlot slot in slots) BlockSlotSaveData.Add(slot.Save(save));

    //    return new GridSaveData()
    //    {
    //        BlockSlotData = BlockSlotSaveData
    //    };
        
    //}

    //public void Load(GridSaveData save)
    //{
    //    ClearGrid();
    //    foreach(BlockSlotSaveData slotData in save.BlockSlotData)
    //    {
    //        GetSlot(slotData.x, slotData.y).Load(slotData);
    //    }

    //    foreach (BlockSlotSaveData slotData in save.BlockSlotData)
    //    {
    //        foreach(BlockSaveData blockData in slotData.BlockData)
    //        {
    //            if (blockData.tail)
    //            {
    //                PlayGrid tailGrid = blockData.tailOnLoadGrid ? GameManager.instance.playerManagers[0].previewGrid : GameManager.instance.playerManagers[0].playGrid;
    //                GetSlot(slotData.x, slotData.y).Blocks[blockData.index].Tail = tailGrid.GetSlot(blockData.tailX, blockData.tailY).Blocks[blockData.tailI];
    //            }
    //        }
    //    }

    //}

}