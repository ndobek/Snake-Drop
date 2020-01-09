using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayGrid : MonoBehaviour
{
    #region Grid Information

    public BlockSlot slotObj;

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
    private BlockSlot[] slots;
    private int FlattenedIndex(int x, int y)
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
    private void CreateSlot(int x, int y, BlockColor color, BlockType type)
    {
        CreateSlot(x, y);
        SetBlock(x, y, color, type);
    }
    private void CreateSlot(int x, int y)
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

    private void Update()
    {
        UpdateGrid();
    }
    private void UpdateGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GetSlot(x, y).UpdateBlock();
            }
        }
    }

    private bool dirty = false;
    public void SetDirty()
    {
        dirty = true;
    }
    public void Fall(bool doTypeAction = false)
    {
        bool dirty = true;

        while (dirty)
        {
            dirty = false;
            for (int x = 0; x < xSize; x++)
            {
                Fall(x, 0, ySize, doTypeAction);
            }
            UpdateGrid();
        }
    }
    private void Fall(int x, int startingY, int maxY, bool doTypeAction = false)
    {
        for (int y = startingY; y < maxY; y++)
        {
            Block block = GetBlock(x, y);
            if (block && block.Slot.playGrid == this && !block.isPartOfSnake)
            {
                if (doTypeAction)
                {
                    block.ActionFall();
                }
                else
                {
                    block.BasicFall();
                }
            }
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

    private int[][] GridAndBoolToIntArray(System.Func<BlockSlot, bool> condition)
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

    public class RectInfo : IComparable
    {
        public Vector2 TopLeft;
        public Vector2 BottomRight;
        public int Area;

        public int CompareTo(object obj)
        {
            RectInfo rect2 = obj as RectInfo;
            if (Area == rect2.Area) return 0;
            else return Area < rect2.Area ? 1 : -1;
        }
    }

    public BlockCollection[] GetBlockCollections(System.Func<BlockSlot, bool> condition)
    {
        RectInfo[] AllRectangles = GetRectInArray(condition);

        System.Array.Sort(AllRectangles);

        List<BlockCollection> result = new List<BlockCollection>();
        List<BlockSlot> addedSlots = new List<BlockSlot>();
        Debug.Log("UGH");
        foreach (RectInfo rect in AllRectangles)
        {
            BlockSlot[] SlotsInRect = RectToBlockSlots(rect);

            bool AddRect = true;
            foreach(BlockSlot slot in SlotsInRect)
            {
                if (addedSlots.Contains(slot))
                {
                    AddRect = false;
                    break;
                }
            }

            if (AddRect)
            {
                //NEED TO IMPLEMENT: Create new BlockCollection and Add it
                addedSlots.AddRange(SlotsInRect);
                Debug.Log(rect.TopLeft + " and " + rect.BottomRight + "Area: " + rect.Area + " AddedSlots: " + addedSlots.Count);
            }
        }
        return result.ToArray();

    }

    private BlockSlot[] RectToBlockSlots(RectInfo obj)
    {
        List<BlockSlot> result = new List<BlockSlot>();
        for (int x = (int)obj.TopLeft.x; x <= (int)obj.BottomRight.x; x++)
        {
            for (int y = (int)obj.BottomRight.y; y <= (int)obj.TopLeft.y; y++)
            {
                result.Add(GetSlot(x, y));
            }
        }
        return result.ToArray();
    }

    private RectInfo[] GetRectInArray(System.Func<BlockSlot, bool> condition)
    {
        int[][] grid = GridAndBoolToIntArray(condition);

        List<RectInfo> rectList = new List<RectInfo>(GetRectInsideHist(grid[0]));

        for(int x = 1; x < xSize; x++)
        {
            for(int y = 0; y < ySize; y++)
            {
                if (grid[x][y] == 1) grid[x][y] += grid[x - 1][y];
            }

            rectList.AddRange(GetRectInsideHist(grid[x], x));
        }

        return rectList.ToArray();
    }



    private RectInfo[] GetRectInsideHist(int[] column, int xMod = 0)
    {
        List<RectInfo> result = new List<RectInfo>();
        Stack<int> rectangleIndex = new Stack<int>();

        int ySize = column.Length;
        int i = 0;

        void registerRect()
        {

            int x1 = xMod - (column[rectangleIndex.Pop()] - 1);
            int x2 = xMod;
            int y1 = i - 1;
            int y2 = rectangleIndex.Count > 0 ? rectangleIndex.Peek() + 1 : 0;

            int areaX = x2 - (x1 - 1);
            int areaY = y1 - (y2 - 1);

            RectInfo temp = new RectInfo
            {
                TopLeft = new Vector2(x1, y1),
                BottomRight = new Vector2(x2, y2),
                Area = areaX * areaY
            };

            if (areaX >= 2 && areaY >= 2)
            {
                result.Add(temp);

            }
        }

        while (i < ySize)
        {
            if (rectangleIndex.Count == 0 || column[rectangleIndex.Peek()] <= column[i])
            {
                rectangleIndex.Push(i++);
            }
            else
            {
                registerRect();
            }
        }

        while (rectangleIndex.Count > 0)
        {
            registerRect();
        }
        return result.ToArray();
    }
}