using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/DirectionalSpriteAnimator")]
public class DirectionalSpriteController : BlockSpriteAnimator
{
    public Sprite UpSprite;
    public Sprite DownSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;
    public bool HighlightTail;

    public Color color;
    public int sortingOrder;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        Block block = blockAnimation.block;
        Directions.Direction dir = Directions.Direction.DOWN;
        if (block && block.Owner && block.Owner.playerController && block.Owner.RoundInProgress) dir = block.Owner.playerController.mostRecentDirection;
        switch (dir) 
        {
            case Directions.Direction.UP:
                SetSprite(block, UpSprite, color, sortingOrder);
                break;
            case Directions.Direction.DOWN:
                SetSprite(block, DownSprite, color, sortingOrder);
                break;
            case Directions.Direction.LEFT:
                SetSprite(block, LeftSprite, color, sortingOrder);
                break;
            case Directions.Direction.RIGHT:
                SetSprite(block, RightSprite, color, sortingOrder);
                break;
        }
    }


}
