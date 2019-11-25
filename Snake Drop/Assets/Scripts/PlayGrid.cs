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

    [SerializeField, HideInInspector]
    private BlockSlot[,] slots;

    public Vector3 position(int x, int y)
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
        slots = new BlockSlot[xSize, ySize];
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
                slots[x, y].UpdateBlock();
            }
        }
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


    private void CreateSlot(int x, int y, BlockType type)
    {
        CreateSlot(x, y);
        SetBlock(x, y, type);
    }
    private void CreateSlot(int x, int y)
    {
        if (CheckInGrid(x, y) && slots[x, y] == null)
        {
            slots[x, y] = Instantiate(slotObj, position(x, y), Quaternion.identity, this.transform);
            slots[x, y].playGrid = this;
            slots[x, y].x = x;
            slots[x, y].y = y;
        }
    }
    public BlockSlot GetSlot(int x, int y)
    {
        if (CheckInGrid(x, y)) return slots[x, y];
        else return null;
    }



    public void SetBlock(int x, int y, BlockType type)
    {
        GetSlot(x, y).SetBlock(type);
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
            BlockSlot.SwapBlocks(slots[x1, y1], slots[x2, y2]);
        }
    }
}