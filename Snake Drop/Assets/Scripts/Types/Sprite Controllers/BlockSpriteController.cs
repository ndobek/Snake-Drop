using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpriteControllers/BasicSpriteController")]
public class BlockSpriteController : ScriptableObject
{
    public Sprite sprite;
    public virtual void UpdateSprite(Block block)
    {
        SetSprite(block, sprite);
    }
    public void SetSprite(Block block, Sprite sprite)
    {
        block.animator.AddAnimation(new BlockSpriteChangeAnimation(block, sprite));
    }
}
