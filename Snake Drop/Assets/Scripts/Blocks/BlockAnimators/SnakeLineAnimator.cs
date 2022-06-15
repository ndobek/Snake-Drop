using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/SnakeLineAnimator")]
public class SnakeLineAnimator : BlockAnimator
{
    public override void OnComplete(BlockAnimation blockAnimation)
    {
        
        CreateSnakeLine(blockAnimation.block);
    }
    
    protected void CreateSnakeLine(Block block)
    {
        if (block.Tail != null)
        {
            //block.Highlight.enabled = true;
            Block current = block;
            List<Vector3> result = new List<Vector3>();

            BlockAnimation[] ActiveAnimations = current.AnimationManager.ActiveAnimations.ToArray();
            Vector3 snakeHeadNextPos = current.Slot.transform.position;
            if (ActiveAnimations.Length > 0)
            {
                for (int i = (ActiveAnimations.Length - 1); i >= 0; i--)
                {
                    BlockAnimation animation = ActiveAnimations[i];
                    if (animation != null && animation.destination != null)
                    {
                        snakeHeadNextPos = animation.destination.position;
                        break;
                    }
                }
            }

            result.Add(current.transform.position);

            bool SnakeReached = false;
            while (current.Tail != null)
            {
                Vector3 currentPos = current.Slot.transform.position;
                if (SnakeReached) result.Add(currentPos);
                if (!SnakeReached && Vector3.Distance(currentPos, snakeHeadNextPos) < .1f)
                {
                    SnakeReached = true;
                }


                current = current.Tail;
            }
            result.Add(current.Slot.transform.position);
            BlockAnimation[] tailActiveAnimations = current.AnimationManager.ActiveAnimations.ToArray();
            BlockAnimation[] tailUpcomingAnimations = current.AnimationManager.UpcomingAnimations.ToArray();
            if (tailUpcomingAnimations.Length > 0)
            {
                for (int i = (tailUpcomingAnimations.Length - 1); i >= 0; i--)
                {
                    BlockAnimation animation = tailUpcomingAnimations[i];
                    if (animation != null && animation.destination != null)
                    {
                        result.Add(animation.destination.position);
                    }
                }
            }
            if (tailActiveAnimations.Length > 0)
            {
                for (int i = (tailActiveAnimations.Length - 1); i >= 0; i--)
                {
                    BlockAnimation animation = tailActiveAnimations[i];
                    if (animation != null && animation.destination != null)
                    {
                        result.Add(animation.destination.position);
                    }
                }
            }
            result.Add(current.transform.position);


            block.Highlight.positionCount = result.Count;
            block.Highlight.SetPositions(result.ToArray());
        }
    }
}
