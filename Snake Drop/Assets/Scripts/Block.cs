using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //[SerializeField]
    protected BlockSlot CurrentLocation;

    public bool isPartOfSnake;

    [SerializeField]
    private Block tail;
    public Block Tail
    {
        get { return tail; }
        set { tail = value; }
    }


    public BlockType blockType;
    public SpriteRenderer spriteRenderer;


    public void UpdateSprite()
    {
        if (blockType != null)
        {
            spriteRenderer.sprite = blockType.sprite;
            spriteRenderer.color = blockType.color;
        }
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
        tail = obj;
        isPartOfSnake = true;
        tail.isPartOfSnake = true;
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
    private void OnMoveTo(BlockSlot obj)
    {
        CurrentLocation = obj;
        UpdateBlock();
    }

    public void MoveTo(BlockSlot obj)
    {
        BlockSlot Old = CurrentLocation;
        CurrentLocation = obj;
        OnMoveTo(obj);

        if (tail != null) tail.MoveTo(Old);
    }
    public void MoveTo(BlockSlot.Neighbor neighbor)
    {
        BlockSlot destination = Neighbor(neighbor);
        if (destination) MoveTo(destination);
    }
    public void MoveLeft()
    {
        MoveTo(BlockSlot.Neighbor.Left);
    }
    public void MoveRight()
    {
        MoveTo(BlockSlot.Neighbor.Right);
    }
    public void MoveUp()
    {
        MoveTo(BlockSlot.Neighbor.Up);
    }
    public void MoveDown()
    {
        MoveTo(BlockSlot.Neighbor.Down);
    }



}
