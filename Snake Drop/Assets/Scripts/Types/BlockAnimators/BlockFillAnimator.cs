using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockFillAnimator")]
public class BlockFillAnimator : BlockAnimator
{
    public Sprite FillSprite;
    public override void AnimationStep(BlockAnimation blockAnimation)
    {
        blockAnimation.block.FillSpriteRenderer.sprite = FillSprite;
        UpdateBlockCollectionFill(blockAnimation.block, percentageComplete(blockAnimation));
    }

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        UpdateBlockCollectionFill(blockAnimation.block, 1);
    }
    public static void UpdateBlockCollectionFill(BlockCollection blockCollection, float speed = 1)
    {
        foreach (Block obj in blockCollection.Blocks)
        {
            SetFill(obj.FillMaskTransform, SingleBlockFillAmount(blockCollection, obj), speed);
        }
    }
    public static void UpdateBlockCollectionFill(Block block, float speed = 1)
    {
        UpdateBlockCollectionFill(block.BlockCollection, speed);
    }

    public static void SetFill(Transform maskTransform, float amount, float speed)
    {
        Vector3 target = new Vector3(maskTransform.localScale.x, amount, maskTransform.localScale.z);
        maskTransform.localScale = Vector3.Lerp(maskTransform.localScale, target, speed);
    }
    public static float SingleBlockFillAmount(BlockCollection collection, Block block)
    {
        float singleColumnFill = (float)collection.FillAmount / (float)collection.XSize();
        float result = (float)singleColumnFill - collection.GridToCollectionCoords(block).y;

        return Mathf.Clamp(result, 0, 1);
    }
}
