using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
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

    public Block blockObj;
    public Snake snakeObj;

    public BlockType defaultType;
    public BlockType snakeType;

    public Block[,] blocks;

    public Vector3 position(int x, int y)
    {
        Vector3 result = new Vector3(x * gridSpace, y * gridSpace);
        result = this.transform.TransformPoint(result);
        return result;
    }

    private void Awake()
    {
        CreateGrid();
    }

    private void Update()
    {
        UpdateGrid();
    }

    public void SetBlock( int x, int y, BlockType type = null, Block baseBlock = null)
    {
        DestroyBlock(x, y);
        CreateBlock(x, y, type, baseBlock);
    }

    private void CreateBlock(int x, int y)
    {
        CreateBlock(x, y, defaultType, blockObj);
    }
    private void CreateBlock(int x, int y, Block baseBlock)
    {
        CreateBlock(x, y, defaultType, baseBlock);
    }
    private void CreateBlock(int x, int y, BlockType blockType)
    {
        CreateBlock(x, y, blockType, blockObj);
    }
    private void CreateBlock(int x, int y, BlockType blockType, Block baseBlock)
    {
        if (blockType == null) blockType = defaultType;
        if (baseBlock == null) baseBlock = blockObj;

        if (InGrid(x, y) && blocks[x, y] == null)
        {
            blocks[x, y] = Instantiate(baseBlock, position(x, y), Quaternion.identity, this.transform);
            blocks[x, y].blockType = blockType;
            blocks[x, y].UpdateLocation(this, x, y);
            blocks[x, y].UpdateSprite();
        }
    }

    private void DestroyBlock(int x, int y)
    {
        if (InGrid(x, y))
        {
            Destroy(blocks[x, y].gameObject);
            blocks[x, y] = null;
        }
    }

    private void CreateGrid()
    {
        blocks = new Block[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                CreateBlock(x, y);
            }
        }

        SetBlock(0, 0, snakeType, snakeObj);
    }

    private void UpdateGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                blocks[x, y].UpdateBlock(this, x, y);
            }
        }
    }

    public void SwapBlocks(int x1, int y1, int x2, int y2)
    {
        if (InGrid(x1, y1) && InGrid(x2, y2))
        {
            Block swap = blocks[x1, y1];
            blocks[x1, y1] = blocks[x2, y2];
            blocks[x2, y2] = swap;

            blocks[x1, y1].UpdateLocation(this, x1, y1);
            blocks[x2, y2].UpdateLocation(this, x2, y2);
        }
    }

    public bool InGrid(int x, int y)
    {
        if (
            x >= 0 &&
            x < xSize &&

            y >= 0 &&
            y < ySize
            ) { return true; }
        else return false;
    }
}