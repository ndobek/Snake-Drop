using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayGrid : MonoBehaviour
{
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

    [SerializeField]
    private float gridSpace;
    public float GridSpace
    {
        get { return gridSpace; }
        set { gridSpace = value; }
    }

    public BlockSlot slotObj;
    public Block blockObj;

    [SerializeField]
    private BlockSlot[] slots;

    private int FlattenedIndex(int x, int y)
    {
        return y * xSize + x;
    }

    public Vector3 position(float x, float y)
    {
        float xAdj = ((XSize - 1) * gridSpace) / 2;
        float yAdj = ((YSize - 1) * gridSpace);

        Vector3 result = new Vector3((x * gridSpace) - xAdj, (y * gridSpace) - yAdj);
        result = this.transform.TransformPoint(result);
        return result;
    }

    private void Awake()
    {
        //CreateGrid();
    }
    private void Update()
    {
        UpdateGrid();
    }

    public void CreateGrid()
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
    private void UpdateGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GetSlot(x,y).UpdateBlock();
            }
        }
    }
    public void ClearGrid()
    {
        foreach(BlockSlot slot in slots)
        {
            slot.DeleteBlock();
        }
    }
    public void Fall()
    {
        for (int x = 0; x < xSize; x++)
        {
            Fall(x, 0, ySize);
        }
        UpdateGrid();
    }
    private void Fall(int x, int startingY, int maxY)
    {
        for (int y = startingY; y < maxY; y++)
        {
            if(GetSlot(x,y).Block == null)
            {
                for (int y2 = y; y2 < maxY; y2++)
                {
                    Block block = GetSlot(x, y2).Block;
                    if (block != null)
                    {
                        block.Fall();
                    }
                }
            }
        }
        //BlockSlot lowestEmptySlot = null;
        //for (int y = startingY; y < maxY; y++)
        //{
        //    BlockSlot obj = GetSlot(x, y);
        //    if (lowestEmptySlot == null && obj.Block == null)
        //    {
        //        lowestEmptySlot = obj;
        //    }
        //    if (lowestEmptySlot != null && obj.Block != null)
        //    {
        //        ////Moving Block one at a time so it can move snakes properly
        //        //Block blockGettingShuffled = obj.Block;
        //        //while(blockGettingShuffled.Slot != lowestEmptySlot)
        //        //{
        //        //    blockGettingShuffled.Move(GameManager.Direction.DOWN);
        //        //}

        //        obj.Block.MoveTo(lowestEmptySlot);

        //        Fall(x, lowestEmptySlot.y + 1, maxY);
        //        break;
        //    }
        //}
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


    private void CreateSlot(int x, int y, BlockColor color, BlockType type)
    {
        CreateSlot(x, y);
        SetBlock(x, y, color, type);
    }
    private void CreateSlot(int x, int y)
    {
        if (CheckInGrid(x, y) && slots[FlattenedIndex(x,y)] == null)
        {
            int i = FlattenedIndex(x, y);
            slots[i] = Instantiate(slotObj, position(x, y), Quaternion.identity, this.transform);
            slots[i].playGrid = this;
            slots[i].x = x;
            slots[i].y = y;
        }
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
    public void SwapBlocks(int x1, int y1, int x2, int y2)
    {
        if (CheckInGrid(x1, y1) && CheckInGrid(x2, y2))
        {
            BlockSlot.SwapBlocks(GetSlot(x1, y1), GetSlot(x2, y2));
        }
    }
}