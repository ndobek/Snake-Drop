using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpriteControllers/DirectionalSpriteController")]
public class DirectionalSpriteController : BlockSpriteController
{
    public Sprite UpSprite;
    public Sprite DownSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;

    public override void UpdateSprite(Block block)
    {
        base.UpdateSprite(block);
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
        CreateSnakeLine(block);
    }

    public void CreateSnakeLine(Block block)
    {
        block.Highlight.enabled = true;
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
