using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpriteController : MonoBehaviour
{
    public Block block;
    public SpriteRenderer BlockSprite;
    public SpriteRenderer Highlight;

    public void UpdateSprite()
    {
        if (block.blockColor != null && block.blockType != null)
        {
            //BlockSprite.sprite = block.blockType.fullBlockSprite;
            SetEdge();
            BlockSprite.color = block.blockColor.color;
            BlockSprite.sortingOrder = block.blockType.sortingOrder;
        }

        Highlight.enabled = block.isPartOfSnake();
    }
    public void SetEdge()
    {
        bool top = false;
        bool bottom = false;
        bool left = false;
        bool right = false;

        if (block.BlockCollection != null)
        {
            top = block.BlockCollection.CheckEdge(GameManager.Direction.UP, block);
            bottom = block.BlockCollection.CheckEdge(GameManager.Direction.DOWN, block);
            left = block.BlockCollection.CheckEdge(GameManager.Direction.LEFT, block);
            right = block.BlockCollection.CheckEdge(GameManager.Direction.RIGHT, block);
            SetEdge(top, bottom, left, right);
        }
        else BlockSprite.sprite = block.blockType.fullBlockSprite;

    }

    private void SetEdge(bool top, bool bottom, bool left, bool right)
    {
        if (top)
        {
            if (left) BlockSprite.sprite = block.blockType.topLeftBlockSprite;
            else if (right) BlockSprite.sprite = block.blockType.topRightBlockSprite;
            else BlockSprite.sprite = block.blockType.topBlockSprite;
        }
        else if (bottom)
        {
            if (left) BlockSprite.sprite = block.blockType.bottomLeftBlockSprite;
            else if (right) BlockSprite.sprite = block.blockType.bottomRightBlockSprite;
            else BlockSprite.sprite = block.blockType.bottomBlockSprite;
        }
        else if (left)
        {
            BlockSprite.sprite = block.blockType.leftBlockSprite;
        }
        else if (right)
        {
            BlockSprite.sprite = block.blockType.rightBlockSprite;
        }
        else
        {
            BlockSprite.sprite = block.blockType.centerBlockSprite;
        }
    }
}
