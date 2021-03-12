using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/SnakeSpriteController")]
public class SnakeSpriteController : BlockSpriteAnimator
{
    public Sprite horizontal;
    public Sprite vertical;
    public Sprite topLeft;
    public Sprite topRight;
    public Sprite bottomLeft;
    public Sprite bottomRight;
    public Color color;
    public int sortingOrder;
    public Material snakeMaterial;
    public Material notSnakeMaterial;


    public override void OnComplete(BlockAnimation blockAnimation)
    {
        Block block = blockAnimation.block;
        Directions.Direction headDirection = Directions.Direction.DOWN;
        Directions.Direction tailDirection = Directions.Direction.DOWN;
        if (block.Tail != null)
        {
            tailDirection = Directions.TailDirection(block);
        }
        else if (block && block.Owner && block.Owner.playerController && block.Owner.RoundInProgress)
        {
            tailDirection = Directions.GetOppositeDirection(block.Owner.playerController.MostRecentDirectionMoved);
        }
        if (block.Head != null)
        {
            headDirection = Directions.HeadDirection(block);
        }
        else if (block && block.Owner && block.Owner.playerController && block.Owner.RoundInProgress)
        {
            headDirection = block.Owner.playerController.MostRecentDirectionMoved;
        }




        if ((headDirection == Directions.GetOppositeDirection(tailDirection) || headDirection == tailDirection))
        {
            if (headDirection == Directions.Direction.UP || headDirection == Directions.Direction.DOWN)
            {
                SetSprite(block, vertical, color, sortingOrder, snakeMaterial);
            }
            else
            {
                SetSprite(block, horizontal, color, sortingOrder, snakeMaterial);
            }
        }
        else if (headDirection == Directions.Direction.UP || tailDirection == Directions.Direction.UP)
        {

            if (headDirection == Directions.Direction.LEFT || tailDirection == Directions.Direction.LEFT)
            {
                SetSprite(block, topLeft, color, sortingOrder, snakeMaterial);
            }
            else
            {
                SetSprite(block, topRight, color, sortingOrder, snakeMaterial);
            }
        }
        else if (headDirection == Directions.Direction.LEFT || tailDirection == Directions.Direction.LEFT)
        {
            SetSprite(block, bottomLeft, color, sortingOrder, snakeMaterial);
        }
        else
        {
            SetSprite(block, bottomRight, color, sortingOrder, snakeMaterial);
        }
    }

}
