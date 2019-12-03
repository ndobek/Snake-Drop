using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //[SerializeField]
    public BlockSlot Slot;

    [HideInInspector]
    public bool isPartOfSnake;

    [SerializeField, HideInInspector]
    private Block tail;
    public Block Tail
    {
        get { return tail; }
        //set { tail = value; }
    }

    [HideInInspector]
    public BlockType blockType;

    public SpriteRenderer BlockSprite;
    public SpriteRenderer Highlight;


    public void UpdateSprite()
    {
        if (blockType != null)
        {
            BlockSprite.sprite = blockType.sprite;
            BlockSprite.color = blockType.color;
        }

        Highlight.enabled = isPartOfSnake;
    }
    public void UpdateBlock()
    {
        if(Slot) UpdateLocation();
        UpdateSprite();
    }
    public void SetBlockType(BlockType type)
    {
        blockType = type;
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
    public BlockSlot Neighbor(GameManager.Direction direction)
    {
        return Slot.GetNeighbor(direction);
    }

    private void UpdateLocation()
    {
        this.transform.SetParent(Slot.transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.transform.localScale = Vector3.one;
    }

    public void MoveTo(BlockSlot obj)
    {
        BlockSlot Old = Slot;
        if(Old) Old.RemoveBlock(this);
        Slot = obj;
        obj.OnAssignment(this);
        UpdateBlock();
        if (tail != null) tail.MoveTo(Old);
    }
    public void Move(GameManager.Direction neighbor)
    {
        BlockSlot destination = Neighbor(neighbor);
        if (destination) MoveTo(destination);
    }

    public void Eat(GameManager.Direction neighbor)
    {
        BlockSlot slotOjb = Neighbor(neighbor);

        Block blockObj = null;
        if (slotOjb) blockObj = slotOjb.Block;
        //if (blockObj) Debug.Log(blockObj.blockType);
        //else Debug.Log("null");

        if (!slotOjb | (blockObj != null && blockObj.blockType != blockType))
        {
            Kill();
        } else
        {
            if (blockObj)
            {
                blockObj.Kill();
                slotOjb.DeleteBlock();
            }
            MoveTo(slotOjb);
        }

    }

    public void Kill()
    {
        if (tail != null)
        {
            tail.Kill();
            SetTail(null);
        }
        isPartOfSnake = false;
        UpdateBlock();
        GameManager.instance.OnBlockDeath(this);
    }

    public Vector2 Coords()
    {
        return new Vector2(Slot.x, Slot.y);
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
}
