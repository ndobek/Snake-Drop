using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //[SerializeField]
    protected BlockSlot CurrentLocation;

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
    public GameObject Highlight;


    public void UpdateSprite()
    {
        if (blockType != null)
        {
            BlockSprite.sprite = blockType.sprite;
            BlockSprite.color = blockType.color;
        }

        Highlight.SetActive(isPartOfSnake);
    }
    public void UpdateBlock()
    {
        if(CurrentLocation) UpdateLocation();
        UpdateSprite();
    }
    public void SetBlockType(BlockType type)
    {
        blockType = type;
        UpdateBlock();
    }
    public void SetTail(BlockSlot.Neighbor neighbor)
    {
        SetTail(Neighbor(neighbor).Block);
    }
    public void SetTail(Block obj)
    {
        if (obj)
        {
            tail = obj;
            isPartOfSnake = true;
            tail.isPartOfSnake = true;
        } else
        {
            tail = null;
        }
    }
    public BlockSlot Neighbor(BlockSlot.Neighbor direction)
    {
        return CurrentLocation.GetNeighbor(direction);
    }

    private void UpdateLocation()
    {
        this.transform.SetParent(CurrentLocation.transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.transform.localScale = Vector3.one;
    }

    public void MoveTo(BlockSlot obj)
    {
        BlockSlot Old = CurrentLocation;
        if(Old) Old.RemoveBlock();
        CurrentLocation = obj;
        obj.OnAssignment(this);
        UpdateBlock();

        if (tail != null) tail.MoveTo(Old);
    }
    public void Move(BlockSlot.Neighbor neighbor)
    {
        BlockSlot destination = Neighbor(neighbor);
        if (destination) MoveTo(destination);
    }

    public void Eat(BlockSlot.Neighbor neighbor)
    {
        BlockSlot slotOjb = Neighbor(neighbor);

        Block blockObj = null;
        if (slotOjb) blockObj = slotOjb.Block;
        if (blockObj) Debug.Log(blockObj.blockType);
        else Debug.Log("null");

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
        isPartOfSnake = false;
        if (tail != null)
        {
            tail.Kill();
            SetTail(null);
        }
        UpdateBlock();
        GameManager.instance.OnBlockDeath(this);
    }

}
