using System;
using System.Collections.Generic;
using UnityEngine;

public static class BlockMelder
{
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

    public static void Meld(PlayGrid grid, BlockColor[] colors)
    {
        Debug.Log("ROUND");
        foreach (BlockColor color in colors)
        {
            Debug.Log(color.name);
            BlockMelder.GetBlockCollections(grid, C => Temp(C, color));
        }
    }

    private static bool Temp(BlockSlot obj, BlockColor color)
    {
        return obj && obj.Block && obj.Block.blockColor == color;
    }

    private static BlockCollection[] GetBlockCollections(PlayGrid grid, System.Func<BlockSlot, bool> _condition)
    {
        List<BlockCollection> result = new List<BlockCollection>();
        List<BlockSlot> addedSlots = new List<BlockSlot>();

        bool condition(BlockSlot slot)
        {
            return !addedSlots.Contains(slot) && _condition.Invoke(slot);
        }

        int resultLength = -1;
        while (resultLength != addedSlots.Count)
        {
            resultLength = addedSlots.Count;
            RectInfo[] AllRectangles = GetRectInArray(grid, condition);

            System.Array.Sort(AllRectangles);

            foreach (RectInfo rect in AllRectangles)
            {
                BlockSlot[] SlotsInRect = RectToBlockSlots(grid, rect);

                bool AddRect = true;
                foreach (BlockSlot slot in SlotsInRect)
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
                    Debug.Log(rect.TopLeft + " and " + rect.BottomRight + "Area: " + rect.Area);
                }
            }
        }
        return result.ToArray();

    }

    private static BlockSlot[] RectToBlockSlots(PlayGrid grid, RectInfo obj)
    {
        List<BlockSlot> result = new List<BlockSlot>();
        for (int x = (int)obj.TopLeft.x; x <= (int)obj.BottomRight.x; x++)
        {
            for (int y = (int)obj.BottomRight.y; y <= (int)obj.TopLeft.y; y++)
            {
                result.Add(grid.GetSlot(x, y));
            }
        }
        return result.ToArray();
    }

    private static RectInfo[] GetRectInArray(PlayGrid _grid, System.Func<BlockSlot, bool> condition)
    {
        int[][] grid = _grid.GridAndBoolToIntArray(condition);

        List<RectInfo> rectList = new List<RectInfo>(GetRectInsideHist(grid[0]));

        for (int x = 1; x < _grid.XSize; x++)
        {
            for (int y = 0; y < _grid.YSize; y++)
            {
                if (grid[x][y] == 1) grid[x][y] += grid[x - 1][y];
            }

            rectList.AddRange(GetRectInsideHist(grid[x], x));
        }

        return rectList.ToArray();
    }



    private static RectInfo[] GetRectInsideHist(int[] column, int xMod = 0)
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
