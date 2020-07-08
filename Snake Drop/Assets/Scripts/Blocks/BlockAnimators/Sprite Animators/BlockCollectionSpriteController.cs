using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/CollectionSpriteAnimator")]
public class BlockCollectionSpriteController : BlockSpriteAnimator
{
    //public Sprite fullBlockSprite;

    public Sprite topLeftBlockSprite;
    public Sprite topBlockSprite;
    public Sprite topRightBlockSprite;
    public Sprite leftBlockSprite;
    public Sprite centerBlockSprite;
    public Sprite rightBlockSprite;
    public Sprite bottomLeftBlockSprite;
    public Sprite bottomBlockSprite;
    public Sprite bottomRightBlockSprite;

    public Color color;
    public int sortingOrder;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        SetEdge(blockAnimation.block);
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
        //else SetSprite(block, fullBlockSprite);

    }

    private void SetEdge(Block block, bool top, bool bottom, bool left, bool right)
    {
        if (top)
        {
            if (left) SetSprite(block, topLeftBlockSprite, color, sortingOrder);
            else if (right) SetSprite(block, topRightBlockSprite, color, sortingOrder);
            else SetSprite(block, topBlockSprite, color, sortingOrder);
        }
        else if (bottom)
        {
            if (left) SetSprite(block, bottomLeftBlockSprite, color, sortingOrder);
            else if (right) SetSprite(block, bottomRightBlockSprite, color, sortingOrder);
            else SetSprite(block, bottomBlockSprite, color, sortingOrder);
        }
        else if (left)
        {
            SetSprite(block, leftBlockSprite, color, sortingOrder);
        }
        else if (right)
        {
            SetSprite(block, rightBlockSprite, color, sortingOrder);
        }
        else
        {
            SetSprite(block, centerBlockSprite, color, sortingOrder);
        }

        //Temp Jank Solution
        if (top & right) block.BlockSprite.sortingOrder = -1;
    }


}
