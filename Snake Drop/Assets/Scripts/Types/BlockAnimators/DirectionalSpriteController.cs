using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/DirectionalSpriteAnimator")]
public class DirectionalSpriteController : BlockAnimator
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
        GameManager.Direction dir = GameManager.Direction.DOWN;
        if (block && block.Owner && block.Owner.playerController && block.Owner.RoundInProgress) dir = block.Owner.playerController.mostRecentDirection;
        switch (dir) 
        {
            case GameManager.Direction.UP:
                SetSprite(block, UpSprite, color, sortingOrder);
                break;
            case GameManager.Direction.DOWN:
                SetSprite(block, DownSprite, color, sortingOrder);
                break;
            case GameManager.Direction.LEFT:
                SetSprite(block, LeftSprite, color, sortingOrder);
                break;
            case GameManager.Direction.RIGHT:
                SetSprite(block, RightSprite, color, sortingOrder);
                break;
        }
    }


}
