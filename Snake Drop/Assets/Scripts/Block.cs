using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected PlayGrid playGrid;
    protected int currentX;
    protected int currentY;
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
    public void UpdateBlock(PlayGrid obj, int x, int y)
    {
        UpdateLocation(obj, x, y);
        UpdateSprite();
    }

    public void UpdateLocation(PlayGrid obj, int x, int y)
    {
        currentX = x;
        currentY = y;
        UpdateLocation(obj);
    }
    public void UpdateLocation(PlayGrid obj)
    {
        this.playGrid = obj;
        UpdateLocation(this.playGrid.position(currentX, currentY));
    }
    public void UpdateLocation(Vector3 location)
    {
        this.transform.position = location;
    }

    public void MoveLeft()
    {
        MoveTo(currentX - 1, currentY);
    }
    public void MoveRight()
    {
        MoveTo(currentX + 1, currentY);
    }
    public void MoveUp()
    {
        MoveTo(currentX, currentY + 1);
    }
    public void MoveDown()
    {
        MoveTo(currentX, currentY - 1);
    }

    public virtual void MoveTo(int x, int y)
    {
        playGrid.SwapBlocks(currentX, currentY, x, y);
    }

}
