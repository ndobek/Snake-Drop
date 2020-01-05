using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables

    #region Block Info

    //[HideInInspector]
    public BlockType blockType;
    [HideInInspector]
    public BlockColor blockColor;
    private BlockSlot slot;
    public BlockSlot Slot
    {
        get { return slot; }
    }

    #endregion

    #region Coordinates

    public Vector2 Coords()
    {
        return new Vector2(Slot.x, Slot.y);
    }
    public int X
    {
        get { return Slot.x; }
    }
    public int Y
    {
        get { return Slot.y; }
    }
    public BlockSlot Neighbor(GameManager.Direction direction)
    {
        return Slot.GetNeighbor(direction);
    }

    #endregion

    #region GameObject Components

    public SpriteRenderer BlockSprite;
    public SpriteRenderer Highlight;

    #endregion

    #region Snake Data

    [HideInInspector]
    public bool isPartOfSnake;

    private Block tail;
    public Block Tail
    {
        get { return tail; }
        set { tail = value; }
    }

    #endregion

    #endregion

    #region Methods to Update Block Status

    public void SetBlockType(BlockColor color, BlockType type)
    {
        blockType = type;
        blockColor = color;
        UpdateBlock();
    }
    private void UpdateSprite()
    {
        if (blockColor != null && blockType != null)
        {
            BlockSprite.sprite = blockType.sprite;
            BlockSprite.color = blockColor.color;
        }

        Highlight.enabled = isPartOfSnake;
    }
    private void UpdateLocation()
    {
        this.transform.SetParent(Slot.transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.transform.localScale = Vector3.one;
    }
    public void UpdateBlock()
    {
        if(Slot) UpdateLocation();
        UpdateSprite();
    }

    //Tells the grid that it needs to check for fall movement and update
    private void SetGridDirty()
    {
        if (Slot && Slot.playGrid) Slot.playGrid.SetDirty();
    }

    #endregion

    #region Movement Methods

    //Base for all movement code, doesn't follow game rules.
    #region Raw Movement

    public void RawMove(GameManager.Direction neighbor)
    {
        RawMoveTo(Neighbor(neighbor));
    }
    public void RawMoveTo(BlockSlot obj)
    {
        SetGridDirty();
        BlockSlot Old = Slot;
        if (Old) Old.OnUnassignment(this);
        if (obj)
        {
            obj.OnAssignment(this);
            slot = obj;
            if (Tail != null) Tail.RawMoveTo(Old);
        }
        UpdateBlock();
    }
    public void Break()
    {
        SetGridDirty();
        Slot.OnUnassignment(this);
        GameObject.Destroy(this.gameObject);
    }

    #endregion

    //Basic movement that follows game rules but doesn't do any special block actions
    #region Basic Movement


    public void BasicMove(GameManager.Direction neighbor)
    {
        BasicMoveTo(Neighbor(neighbor));
    }
    public void BasicMoveTo(BlockSlot obj)
    {
        blockType.OnMove(this, obj, 0);
    }
    public void BasicFall()
    {
        blockType.OnBasicFall(this);
    }

    #endregion

    //Movement that does special actions based on block type.
    #region Action Movement


    public void ActionMove(GameManager.Direction neighbor)
    {
        ActionMoveTo(Neighbor(neighbor));
    }
    public void ActionMoveTo(BlockSlot obj)
    {
        blockType.OnMove(this, obj);
        GameManager.instance.CheckForRoundEnd();
    }
    public void ActionFall()
    {
        blockType.OnActionFall(this);
    }
    public void ActionBreak()
    {
        blockType.OnActionBreak(this);
        Kill();
        Break();
    }

    #endregion


    #endregion

    #region Snake Controls

    public void SetTail(GameManager.Direction neighbor)
    {
        SetTail(Neighbor(neighbor).Block);
    }
    public void SetTail(Block obj)
    {
        tail = obj;
    }

    public void ActivateSnake()
    {
        if (tail != null)
        {
            tail.ActivateSnake();
        }
        isPartOfSnake = true;
        UpdateBlock();
    }
    public void Kill()
    {
        isPartOfSnake = false;
        UpdateBlock();
    }
    public void KillSnake()
    {

        if (Tail != null)
        {
            if (Tail.Slot.playGrid == GameManager.instance.playGrid) Tail.KillSnake();
            SetTail(null);
        }
        Kill();
    }

    public int FindSnakeMaxY()
    {
        int obj1 = (int)Coords().y;
        int obj2 = 0;
        if (tail != null)
        {
            obj2 = tail.FindSnakeMaxY();
        }
        int result = (obj1 > obj2) ? obj1 : obj2;
        return result;
    }

    #endregion
}
