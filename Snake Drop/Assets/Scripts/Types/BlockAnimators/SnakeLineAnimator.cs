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

        while (current.Tail != null)
        {
            result.Add(current.Highlight.transform.position);
            current = current.Tail;
        }
        result.Add(current.Highlight.transform.position);
        block.Highlight.positionCount = result.Count;
        block.Highlight.SetPositions(result.ToArray());

    }
}
