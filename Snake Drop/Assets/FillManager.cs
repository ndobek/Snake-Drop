using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FillManager
{
    public static void UpdateBlockCollectionFill(BlockCollection blockCollection)
    {
        foreach (Block obj in blockCollection.Blocks)
        {
            SetFill(obj.FillMaskTransform, SingleBlockFillAmount(blockCollection, obj));
        }
    }
    public static void UpdateBlockCollectionFill(Block block)
    {
        UpdateBlockCollectionFill(block.BlockCollection);
    }

    public static void SetFill(Transform maskTransform, float amount)
    {
        maskTransform.localScale = new Vector3(maskTransform.localScale.x, amount, maskTransform.localScale.z);
    }
    public static float SingleBlockFillAmount(BlockCollection collection, Block block)
    {
        float singleColumnFill = (float)collection.FillAmount / (float)collection.XSize();
        float result = (float)singleColumnFill - collection.GridToCollectionCoords(block).y;

        return Mathf.Clamp(result ,0, 1);
    }
}
