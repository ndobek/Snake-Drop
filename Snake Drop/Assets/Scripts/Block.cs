using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //[SerializeField]
    protected BlockSlot CurrentLocation;

    public bool isPartOfSnake;
    public Block tail;


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

    public void UpdateLocation(BlockSlot obj)
    {
        CurrentLocation = obj;
        UpdateLocation();
    }
    private void UpdateLocation()
    {
        this.transform.SetParent(CurrentLocation.transform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.transform.localScale = Vector3.one;
    }

    public void MoveLeft()
    {
        BlockSlot destination = CurrentLocation.GetNeighbor(BlockSlot.Neighbor.Left);
        if(destination) MoveTo(destination);
    }
    public void MoveRight()
    {
        BlockSlot destination = CurrentLocation.GetNeighbor(BlockSlot.Neighbor.Right);
        if (destination) MoveTo(destination);
    }
    public void MoveUp()
    {
        BlockSlot destination = CurrentLocation.GetNeighbor(BlockSlot.Neighbor.Up);
        if (destination) MoveTo(destination);
    }
    public void MoveDown()
    {
        BlockSlot destination = CurrentLocation.GetNeighbor(BlockSlot.Neighbor.Down);
        if(destination) MoveTo(destination);
    }

    public virtual void MoveTo(BlockSlot obj)
    {
        BlockSlot Old = CurrentLocation;
        CurrentLocation = obj;
        UpdateLocation();

        if (tail != null) tail.MoveTo(Old);
    }

}
