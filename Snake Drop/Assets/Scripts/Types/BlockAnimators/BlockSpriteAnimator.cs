using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockSpriteAnimator")]
public class BlockSpriteAnimator : BlockAnimator
{
    public Sprite sprite;
    public Color color;
    public int sortingOrder;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        SetSprite(blockAnimation.block, sprite, color, sortingOrder);
    }

}
