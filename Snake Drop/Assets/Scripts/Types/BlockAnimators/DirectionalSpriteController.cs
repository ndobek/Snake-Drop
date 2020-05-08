using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpriteControllers/DirectionalSpriteController")]
public class DirectionalSpriteController : BlockSpriteAnimator
{
    public Sprite UpSprite;
    public Sprite DownSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;
    public bool HighlightTail;

    public override void AnimationStep(BlockAnimation blockAnimation)
    {
        Block block = blockAnimation.block;
        GameManager.Direction dir = GameManager.Direction.DOWN;
        if (block && block.Owner && block.Owner.playerController && block.Owner.RoundInProgress) dir = block.Owner.playerController.mostRecentDirection;
        switch (dir) 
        {
            case GameManager.Direction.UP:
                SetSprite(block, UpSprite);
                break;
            case GameManager.Direction.DOWN:
                SetSprite(block, DownSprite);
                break;
            case GameManager.Direction.LEFT:
                SetSprite(block, LeftSprite);
                break;
            case GameManager.Direction.RIGHT:
                SetSprite(block, RightSprite);
                break;
        }
    }


}
