using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpriteControllers/BasicSpriteController")]
public class BlockSpriteController : ScriptableObject
{
    public Sprite sprite;
    public virtual void UpdateSprite(Block block)
    {
        if (block.blockColor != null && block.blockType != null)
        {
            block.BlockSprite.sprite = sprite;
            block.BlockSprite.color = block.blockColor.color;
            block.BlockSprite.sortingOrder = block.blockType.sortingOrder;
        }
    }
    public void SetSprite(Block block, Sprite sprite)
    {
        block.BlockSprite.sprite = sprite;
    }
}
