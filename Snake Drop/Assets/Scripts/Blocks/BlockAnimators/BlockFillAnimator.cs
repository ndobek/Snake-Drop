using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BlockFillAnimator")]
public class BlockFillAnimator : BlockAnimator
{
    public Sprite MaskSprite;
    public Sprite FillSprite;

    public AnimationCurve[] PossibleAnimationCurves;

    public override void AnimationStep(BlockAnimation blockAnimation)
    {
        blockAnimation.block.FillMask.sprite = MaskSprite;
        blockAnimation.block.FillSpriteRenderer.sprite = FillSprite;
        UpdateBlockFill(blockAnimation.block, percentageComplete(blockAnimation));
    }

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        UpdateBlockCollectionFill(blockAnimation.block, 1);
    }
    public void UpdateBlockCollectionFill(BlockCollection blockCollection, float speed = 1)
    {
        foreach (Block obj in blockCollection.Blocks)
        {
            UpdateBlockFill(obj, speed);
        }
    }
    public void UpdateBlockCollectionFill(Block block, float speed = 1)
    {
        UpdateBlockCollectionFill(block.BlockCollection, speed);
    }

    public  void UpdateBlockFill(Block block, float speed = 1)
    {
        SetFill(block.FillMaskTransform, EvaluateCurve(block.BlockCollection.FillPercentage(), block), speed);
        //SetFill(block.FillMaskTransform, SingleBlockFillAmount(block.BlockCollection, block), speed);
    }
    public void SetFill(Transform maskTransform, float amount, float speed)
    {
        Vector3 target = new Vector3(amount, amount, maskTransform.localScale.z);
        maskTransform.localScale = Vector3.Lerp(maskTransform.localScale, target, speed);
    }

    //public static void SetFill(Transform maskTransform, float amount, float speed)
    //{
    //    Vector3 target = new Vector3(maskTransform.localScale.x, amount, maskTransform.localScale.z);
    //    maskTransform.localScale = Vector3.Lerp(maskTransform.localScale, target, speed);
    //}
    public float SingleBlockFillAmount(BlockCollection collection, Block block)
    {
        float singleColumnFill = (float)collection.FillAmount / (float)collection.XSize();
        float result = (float)singleColumnFill - collection.GridToCollectionCoords(block).y;

        return Mathf.Clamp(result, 0, 1);
    }

    public float EvaluateCurve(float time, Block block)
    {
        return GetCurve(block).Evaluate(time);
    }

    public AnimationCurve GetCurve(Block block)
    {
        Random.InitState(block.X * block.Y);
        return PossibleAnimationCurves[Random.Range(0, PossibleAnimationCurves.Length)];
    }
}
