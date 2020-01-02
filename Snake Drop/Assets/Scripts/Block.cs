using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //[HideInInspector]
    public BlockType blockType;
    [HideInInspector]
    public BlockColor blockColor;

    //[SerializeField]
    public BlockSlot Slot;

    [HideInInspector]
    public bool isPartOfSnake;
    [SerializeField, HideInInspector]
    private Block tail;
    public Block Tail
    {
        get { return tail; }
        set { tail = value; }
    }

    public SpriteRenderer BlockSprite;
    public SpriteRenderer Highlight;


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

    public void SetBlockType(BlockColor color, BlockType type)
    {
        blockType = type;
        blockColor = color;
        UpdateBlock();
    }
    public void SetTail(GameManager.Direction neighbor)
    {
        SetTail(Neighbor(neighbor).Block);
    }
    public void SetTail(Block obj)
    {
        if (obj)
        {
            tail = obj;
        } else
        {
            tail = null;
        }
    }

    public Vector2 Coords()
    {
        return new Vector2(Slot.x, Slot.y);
    }
    public BlockSlot Neighbor(GameManager.Direction direction)
    {
        return Slot.GetNeighbor(direction);
    }

    public void BasicMoveTo(BlockSlot obj)
    {
        SetGridDirty();
        BlockSlot Old = Slot;
        if (Old) Old.OnUnassignment(this);
        if (obj)
        {
            obj.OnAssignment(this);
            Slot = obj;
            if (Tail != null) Tail.BasicMoveTo(Old);
        }
        UpdateBlock();
    }

    public void BasicMove(GameManager.Direction neighbor)
    {
        BlockSlot destination = Neighbor(neighbor);
        if (destination) BasicMoveTo(destination);
    }

    public void BasicFall(bool StayOnSameGrid = true)
    {
        BlockSlot destination = Neighbor(GameManager.Direction.DOWN);
        while (
            destination != null &&
            destination.Block == null &&
            (!StayOnSameGrid | destination.playGrid == this.Slot.playGrid)
            )
        {
            BasicMove(GameManager.Direction.DOWN);
            destination = Neighbor(GameManager.Direction.DOWN);
        }
    }

    public void ActionFall()
    {
        blockType.OnActionFall(this);
    }

    public void ActionMove(GameManager.Direction neighbor)
    {
        SetGridDirty();
        blockType.OnActionMove(this, Neighbor(neighbor));
    }

    public void Kill()
    {
        isPartOfSnake = false;
        UpdateBlock();
    }
    public void ActionBreak()
    {
        blockType.OnBreak(this);
        Kill();
        Slot.DeleteBlock();
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

    public void ActivateSnake()
    {
        if(tail != null)
        {
            tail.ActivateSnake();
        }
        isPartOfSnake = true;
        UpdateBlock();
    }
    private void SetGridDirty()
    {
        if(Slot && Slot.playGrid) Slot.playGrid.SetDirty();
    }
    //public bool SnakeIsInSlot(BlockSlot obj)
    //{
    //    bool isFurtherDownSnake = false;
    //    if (tail != null)
    //    {
    //        isFurtherDownSnake = SnakeIsInSlot(obj);
    //    }
    //    return (Slot == obj | isFurtherDownSnake);
    //}
}
