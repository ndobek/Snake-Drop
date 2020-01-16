using System;
using System.Collections.Generic;
using UnityEngine;

public static class BlockMelder
{
    public static void Meld(PlayGrid grid, BlockColor[] colors)
    {
        Debug.Log("ROUND");
        foreach (BlockColor color in colors)
        {
            Debug.Log(color.name);
            List<BlockCollection> Melded = BlockMelder.GetBlockCollections(grid, C => Condition(C, color));


            foreach(BlockCollection obj in Melded)
            {
                obj.Build(grid);
            }
        }
    }

    private static bool Condition(BlockSlot obj, BlockColor color)
    {
        return obj && obj.Block && obj.Block.blockColor == color && obj.Block.isPartOfSnake == false && obj.Block.blockType ==  GameManager.instance.defaultType;
    }

    private static List<BlockCollection> MeldCollections(List<BlockCollection> obj1, List<BlockCollection> obj2)
    {
        List<BlockCollection> result = new List<BlockCollection>();

        foreach(BlockCollection collection1 in obj1)
        {
            foreach (BlockCollection collection2 in obj2)
            {
                if (
                    (collection1.TopCoord == collection2.TopCoord && collection1.BottomCoord == collection2.BottomCoord) |
                    (collection1.LeftCoord == collection2.LeftCoord && collection1.RightCoord == collection2.RightCoord)
                    )
                {
                    result.Add(MeldTwoCollections(collection1, collection2));
                }
            }
        }
        return result;


    }

    private static BlockCollection MeldTwoCollections(BlockCollection obj1, BlockCollection obj2)
    {
        BlockCollection temp = new BlockCollection
        {
            TopCoord = Mathf.Max(obj1.TopCoord, obj2.TopCoord),
            BottomCoord = Mathf.Min(obj1.BottomCoord, obj2.BottomCoord),
            LeftCoord = Mathf.Min(obj1.LeftCoord, obj2.LeftCoord),
            RightCoord = Mathf.Max(obj1.RightCoord, obj2.RightCoord)
        };
        return temp;
    }

    private static List<BlockCollection> GetOldBlockCollections(PlayGrid grid, System.Func<BlockSlot, bool> _condition)
    {
        List<BlockCollection> result = new List<BlockCollection>();
        foreach(BlockSlot slot in grid.slots)
        {
            foreach(Block block in slot.Blocks)
            {
                if (block.BlockCollection != null && !result.Contains(block.BlockCollection) && _condition(block.Slot)) result.Add(block.BlockCollection);
            }
        }
        return result;
    }
    private static List<BlockCollection> GetBlockCollections(PlayGrid grid, System.Func<BlockSlot, bool> _condition)
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
            //List<BlockCollection> NewRects = GetRectInArray(grid, condition);
            //List<BlockCollection> OldRects = GetOldBlockCollections(grid, condition);
            //List<BlockCollection> MeldedRects = MeldCollections(NewRects, OldRects);
            List<BlockCollection> AllRectangles = GetRectInArray(grid, condition);

            AllRectangles.Sort();

            foreach (BlockCollection rect in AllRectangles)
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
                    result.Add(rect);
                    addedSlots.AddRange(SlotsInRect);
                    Debug.Log("Top: " + rect.TopCoord + " Bottom: " + rect.BottomCoord + " Left: " + rect.LeftCoord + " Right: " + rect.RightCoord + " Area: " + rect.Area());
                }
            }
        }
        return result;

    }

    private static BlockSlot[] RectToBlockSlots(PlayGrid grid, BlockCollection obj)
    {
        List<BlockSlot> result = new List<BlockSlot>();
        for (int x = obj.LeftCoord; x <= obj.RightCoord; x++)
        {
            for (int y = obj.BottomCoord; y <= obj.TopCoord; y++)
            {
                result.Add(grid.GetSlot(x, y));
            }
        }
        return result.ToArray();
    }

    private static List<BlockCollection> GetRectInArray(PlayGrid _grid, System.Func<BlockSlot, bool> condition)
    {
        int[][] grid = _grid.GridAndBoolToIntArray(condition);

        List<BlockCollection> rectList = new List<BlockCollection>(GetRectInsideHist(grid[0]));

        for (int x = 1; x < _grid.XSize; x++)
        {
            for (int y = 0; y < _grid.YSize; y++)
            {
                if (grid[x][y] == 1) grid[x][y] += grid[x - 1][y];
            }

            rectList.AddRange(GetRectInsideHist(grid[x], x));
        }

        return rectList;
    }



    private static List<BlockCollection> GetRectInsideHist(int[] column, int xMod = 0)
    {
        List<BlockCollection> result = new List<BlockCollection>();
        Stack<int> rectangleIndex = new Stack<int>();

        int ySize = column.Length;
        int i = 0;

        void registerRect()
        {

            int x1 = xMod - (column[rectangleIndex.Pop()] - 1);
            int x2 = xMod;
            int y1 = i - 1;
            int y2 = rectangleIndex.Count > 0 ? rectangleIndex.Peek() + 1 : 0;

            BlockCollection temp = new BlockCollection
            {
                TopCoord = y1,
                BottomCoord = y2,
                LeftCoord = x1,
                RightCoord = x2
            };

            if (temp.XSize() >= 2 && temp.YSize() >= 2)
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
        return result;
    }
}
