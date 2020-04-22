using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpriteControllers/CollectionSpriteController")]
public class BlockCollectionSpriteController : BlockSpriteController
{
    public Sprite fullBlockSprite;

    public Sprite topLeftBlockSprite;
    public Sprite topBlockSprite;
    public Sprite topRightBlockSprite;
    public Sprite leftBlockSprite;
    public Sprite centerBlockSprite;
    public Sprite rightBlockSprite;
    public Sprite bottomLeftBlockSprite;
    public Sprite bottomBlockSprite;
    public Sprite bottomRightBlockSprite;

    public override void UpdateSprite(Block block)
    {
        if (block.blockColor != null && block.blockType != null)
        {
            SetEdge(block);
            block.BlockSprite.color = block.blockColor.color;
            block.BlockSprite.sortingOrder = block.blockType.sortingOrder;
        }
    }

    public void SetEdge(Block block)
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
            SetEdge(block, top, bottom, left, right);
        }
        else SetSprite(block, fullBlockSprite);

    }

    private void SetEdge(Block block, bool top, bool bottom, bool left, bool right)
    {
        if (top)
        {
            if (left) SetSprite(block, topLeftBlockSprite);
            else if (right) SetSprite(block, topRightBlockSprite);
            else SetSprite(block, topBlockSprite);
        }
        else if (bottom)
        {
            if (left) SetSprite(block, bottomLeftBlockSprite);
            else if (right) SetSprite(block, bottomRightBlockSprite);
            else SetSprite(block, bottomBlockSprite);
        }
        else if (left)
        {
            SetSprite(block, leftBlockSprite);
        }
        else if (right)
        {
            SetSprite(block, rightBlockSprite);
        }
        else
        {
            SetSprite(block, centerBlockSprite);
        }
    }
}
