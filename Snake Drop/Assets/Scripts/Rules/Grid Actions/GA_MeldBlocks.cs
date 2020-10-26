using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Rules/Grid Actions/MeldBlocks")]
public class GA_MeldBlocks : GridAction
{
    protected override void Action(PlayGrid grid)
    {
        Meld(grid, GameManager.instance.difficultyManager.PossibleColors);
    }

    public static void Meld(PlayGrid grid, BlockColor[] colors)
    {
        //Debug.Log("ROUND");
        foreach (BlockColor color in colors)
        {
            //Debug.Log(color.name);
            List<BlockCollection> Melded = GetBlockCollections(grid, C => Condition(C, color));


            foreach (BlockCollection obj in Melded)
            {
                obj.Build(grid, GameManager.instance.difficultyManager.Difficulty.TypeBank.collectionType);
            }
        }
    }

    private static bool Condition(BlockSlot obj, BlockColor color)
    {
        return obj && obj.Block && obj.Block.blockColor == color && obj.Block.isPartOfSnake() == false && obj.Block.blockType != GameManager.instance.difficultyManager.Difficulty.TypeBank.collectionGhostType;
    }

    private static BlockCollection MeldCollections(List<BlockCollection> obj)
    {
        BlockCollection result = obj[0];
        foreach (BlockCollection collection in obj)
        {
            result = MeldCollections(result, collection);
        }
        return result;
    }
    private static BlockCollection MeldCollections(BlockCollection obj1, BlockCollection obj2)
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

    private static List<BlockCollection> CheckForOldCollections(List<BlockCollection> obj, PlayGrid grid)
    {
        List<BlockCollection> result = new List<BlockCollection>();
        obj.Sort();
        foreach (BlockCollection collection in obj)
        {
            BlockCollection temp = CheckForOldCollection(collection, grid);
            if (temp != null) result.Add(temp);
        }
        return result;
    }
    private static BlockCollection CheckForOldCollection(BlockCollection obj, PlayGrid grid)
    {
        if (obj == null) return obj;
        obj.Build(grid);
        List<BlockCollection> OldCollections = new List<BlockCollection>();
        bool rectValid = true;

        foreach (BlockSlot slot in obj.Slots)
        {

            if (slot.Block != null && slot.Block.BlockCollection != null && !OldCollections.Contains(slot.Block.BlockCollection))
            {
                BlockCollection obj2 = slot.Block.BlockCollection;

                foreach(BlockSlot slot2 in obj2.Slots)
                {
                    if(!(Array.IndexOf(obj.Slots, slot2) > -1))
                    {
                        rectValid = false;
                        break;
                    }
                    else
                    {
                        OldCollections.Add(obj2);
                    }
                }
            }
            if (!rectValid) break;

        }
        OldCollections.Add(obj);
        if (rectValid) return MeldCollections(OldCollections);
        return null;
    }

    private static List<BlockCollection> GetBlockCollections(PlayGrid grid, System.Func<BlockSlot, bool> _condition)
    {
        List<BlockCollection> result = new List<BlockCollection>();
        List<BlockSlot> addedSlots = new List<BlockSlot>();

        bool LocalCondition(BlockSlot slot)
        {
            return !addedSlots.Contains(slot) && _condition.Invoke(slot);
        }

        int resultLength = -1;
        while (resultLength != addedSlots.Count)
        {
            resultLength = addedSlots.Count;

            List<BlockCollection> AllRectangles = CheckForOldCollections(GetRectInArray(grid.GridAndBoolToIntArray(LocalCondition)), grid);
            AllRectangles.Sort();

            foreach (BlockCollection rect in AllRectangles)
            {
                bool AddRect = true;

                BlockCollection rectToAdd = rect;
                BlockSlot[] SlotsInRect = RectToBlockSlots(grid, rect);


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
                    result.Add(rectToAdd);
                    addedSlots.AddRange(SlotsInRect);
                    //Debug.Log("Top: " + rect.TopCoord + " Bottom: " + rect.BottomCoord + " Left: " + rect.LeftCoord + " Right: " + rect.RightCoord + " Area: " + rect.Area());
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

    //private static List<BlockCollection> GetRectInArray(int[][] grid)
    //{

    //    List<BlockCollection> result = new List<BlockCollection>();

    //    for(int x = 0; x < grid.Length; x++)
    //    {
    //        for (int y = 0; y < grid[x].Length; y++)
    //        {
    //            void RegisterRect()
    //            {
    //                if (xSize > 2 && ySize > 2)
    //                {
    //                    result.Add(new BlockCollection
    //                    {
    //                        TopCoord = y,
    //                        BottomCoord = y + ySize,
    //                        LeftCoord = x,
    //                        RightCoord = x + xSize
    //                    });
    //                }
    //            }
    //            int xSize = 0;
    //            int ySize = 0;

    //            while (x + xSize < grid.Length && grid[x + xSize][y] == 1)
    //            {
    //                xSize += 1;
    //                while (y + ySize < grid[x].Length && grid[x + xSize][y + ySize] == 1) { ySize += 1; }
    //            }
    //        }
    //    }

    //    return result;
    //}


    private static List<BlockCollection> GetRectInArray(int[][] grid)
    {
        List<BlockCollection> rectList = new List<BlockCollection>(GetRectInsideHist(grid[0]));

        for (int x = 1; x < grid.Length; x++)
        {
            for (int y = 0; y < grid[x].Length; y++)
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
        Stack<int> pStack = new Stack<int>();
        Stack<int> wStack = new Stack<int>();

        int ySize = column.Length;
        int pos = 0;
        int tempPos = 0;

        void registerRect()
        {
            tempPos = pStack.Pop();


            int x1 = xMod - (wStack.Pop() - 1);
            int x2 = xMod;
            int y1 = pos - 1;
            int y2 = tempPos;

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

        for (int i = 0; i <= ySize; i++)
        {
            pos = i;
            int width = i < ySize ? column[i] : 0;
            if (wStack.Count == 0 || width > wStack.Peek())
            {
                pStack.Push(i);
                wStack.Push(width);
            }
            else if (width < wStack.Peek())
            {
                while (wStack.Count > 0 && width < wStack.Peek()) { registerRect(); }
                wStack.Push(width);
                pStack.Push(tempPos);
            }
        }

        while (wStack.Count > 0) { registerRect(); }

        return result;

    }



    //private static List<BlockCollection> GetRectInsideHist(int[] column, int xMod = 0)
    //{
    //    List<BlockCollection> result = new List<BlockCollection>();
    //    Stack<int> rectangleIndex = new Stack<int>();

    //    int ySize = column.Length;
    //    int i = 0;

    //    void registerRect()
    //    {

    //        int x1 = xMod - (column[rectangleIndex.Peek()] - 1);
    //        int x2 = xMod;
    //        int y1 = i - 1;
    //        int y2 = rectangleIndex.Count > 0 ? rectangleIndex.Peek(): 0;

    //        BlockCollection temp = new BlockCollection
    //        {
    //            TopCoord = y1,
    //            BottomCoord = y2,
    //            LeftCoord = x1,
    //            RightCoord = x2
    //        };

    //        if (temp.XSize() >= 2 && temp.YSize() >= 2)
    //        {
    //            result.Add(temp);

    //        }
    //        rectangleIndex.Pop();
    //    }

    //    while (i < ySize)
    //    {
    //        if (rectangleIndex.Count == 0 || column[rectangleIndex.Peek()] <= column[i])
    //        {
    //            rectangleIndex.Push(i);
    //            i++;
    //        }
    //        else
    //        {
    //            registerRect();
    //        }
    //    }

    //    while (rectangleIndex.Count > 0)
    //    {
    //        registerRect();
    //    }
    //    return result;
    //}
}
