using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector]
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

    public void MoveTo(BlockSlot obj)
    {
        BlockSlot Old = Slot;
        if (Old) Old.OnUnassignment(this);
        if (obj)
        {
            obj.OnAssignment(this);
            Slot = obj;
            if (Tail != null) Tail.MoveTo(Old);
        }
        UpdateBlock();
    }
    public void Move(GameManager.Direction neighbor)
    {
        BlockSlot destination = Neighbor(neighbor);
        if (destination) MoveTo(destination);
    }

    public void Eat(GameManager.Direction neighbor)
    {
        blockType.OnEat(this, Neighbor(neighbor));
    }

    public void Kill()
    {
        blockType.OnKill(this);
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
