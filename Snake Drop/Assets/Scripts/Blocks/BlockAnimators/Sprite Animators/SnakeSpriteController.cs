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
    public float positionThreshold = .1f;

    
    public override void OnComplete(BlockAnimation blockAnimation)
    {
       
        
        Block block = blockAnimation.block;
        if ((block.transform.position - block.Slot.transform.position).magnitude <= positionThreshold)
        {
            Directions.Direction headDirection = Directions.Direction.DOWN;
            Directions.Direction tailDirection = Directions.Direction.DOWN;

            if (block.Head != null && block.Head.Slot.playGrid == block.Slot.playGrid)
            {
                headDirection = Directions.HeadDirection(block);
            }
            else
            {
                headDirection = block.mostRecentDirectionMoved;
            }

            if (block.Tail != null && block.Tail.Slot.playGrid == block.Slot.playGrid)
            {
                tailDirection = Directions.TailDirection(block);
            }
            else
            {
                tailDirection = Directions.GetOppositeDirection(block.mostRecentDirectionMoved);
            }

            bool up = headDirection == Directions.Direction.UP || tailDirection == Directions.Direction.UP;
            bool down = headDirection == Directions.Direction.DOWN || tailDirection == Directions.Direction.DOWN;
            bool left = headDirection == Directions.Direction.LEFT || tailDirection == Directions.Direction.LEFT;
            bool right = headDirection == Directions.Direction.RIGHT || tailDirection == Directions.Direction.RIGHT;

            if (up)
            {
                if (down) { SetSprite(block, vertical, color, sortingOrder); }
                if (left) { SetSprite(block, topLeft, color, sortingOrder); }
                if (right) { SetSprite(block, topRight, color, sortingOrder); }

            }
            else if (down)
            {
                if (left) { SetSprite(block, bottomLeft, color, sortingOrder); }
                if (right) { SetSprite(block, bottomRight, color, sortingOrder); }
            }
            else
            {
                SetSprite(block, horizontal, color, sortingOrder, snakeMaterial);
            }
        }



        //if ((headDirection == Directions.GetOppositeDirection(tailDirection) || headDirection == tailDirection))
        //{
        //    if (headDirection == Directions.Direction.UP || headDirection == Directions.Direction.DOWN)
        //    {
        //        SetSprite(block, vertical, color, sortingOrder, snakeMaterial);
        //    }
        //    else
        //    {
        //        SetSprite(block, horizontal, color, sortingOrder, snakeMaterial);
        //    }
        //}
        //else if (headDirection == Directions.Direction.UP || tailDirection == Directions.Direction.UP)
        //{

        //    if (headDirection == Directions.Direction.LEFT || tailDirection == Directions.Direction.LEFT)
        //    {
        //        SetSprite(block, topLeft, color, sortingOrder, snakeMaterial);
        //    }
        //    else
        //    {
        //        SetSprite(block, topRight, color, sortingOrder, snakeMaterial);
        //    }
        //}
        //else if (headDirection == Directions.Direction.LEFT || tailDirection == Directions.Direction.LEFT)
        //{
        //    SetSprite(block, bottomLeft, color, sortingOrder, snakeMaterial);
        //}
        //else
        //{
        //    SetSprite(block, bottomRight, color, sortingOrder, snakeMaterial);
        //}
    }

}
