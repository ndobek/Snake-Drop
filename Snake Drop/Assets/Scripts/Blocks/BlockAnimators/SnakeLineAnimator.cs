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
        //block.Highlight.enabled = true;
        Block current = block;
        List<Vector3> result = new List<Vector3>();
        result.Add(current.transform.position);
        while (current.Tail != null)
        {
            current = current.Tail;

            BlockAnimation[] UpcomingAnimations = current.AnimationManager.UpcomingAnimations.ToArray();
            BlockAnimation[] ActiveAnimations = current.AnimationManager.ActiveAnimations.ToArray();
            if (ActiveAnimations.Length > 0)
            {
                for (int i = ActiveAnimations.Length - 1; i >= 0; i--)
                {
                    BlockAnimation animation = ActiveAnimations[i];
                    if (animation != null && animation.destination != null) result.Add(animation.destination.position);
                }
            }
            else
            {
                result.Add(current.transform.position);
            }

        }
        result.Add(current.transform.position);
        block.Highlight.positionCount = result.Count;
        block.Highlight.SetPositions(result.ToArray());

    }
}
