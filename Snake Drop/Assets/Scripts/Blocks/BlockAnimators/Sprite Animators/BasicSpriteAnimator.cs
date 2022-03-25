using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BasicSpriteAnimator")]
public class BasicSpriteAnimator : BlockSpriteAnimator
{
    public Sprite sprite;
    public Color color;
    public int sortingOrder;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        SetSprite(blockAnimation.block, sprite, color, sortingOrder);
    }
}
