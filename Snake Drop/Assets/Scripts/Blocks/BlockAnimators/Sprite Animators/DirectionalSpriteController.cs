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
    public Material snakeMaterial;//Added by Alyssa
    public Material notSnakeMaterial;
    private Material currentMaterial;
    public Color color;
    public int sortingOrder;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        Block block = blockAnimation.block;
        Directions.Direction dir = Directions.Direction.DOWN;
        if (block.blockType.isPartOfSnake == true)//Alyssa added this if
        {
            currentMaterial = snakeMaterial;
        }
        else
        {
            currentMaterial = notSnakeMaterial;
        }
        if (block && block.Owner && block.Owner.playerController && block.Owner.RoundInProgress) dir = block.Owner.playerController.MostRecentDirectionMoved;
        switch (dir) 
        {
            case Directions.Direction.UP:
                SetSprite(block, UpSprite, color, sortingOrder, currentMaterial);//Alyssa added materials to these
                break;
            case Directions.Direction.DOWN:
                SetSprite(block, DownSprite, color, sortingOrder, currentMaterial);
                break;
            case Directions.Direction.LEFT:
                SetSprite(block, LeftSprite, color, sortingOrder, currentMaterial);
                break;
            case Directions.Direction.RIGHT:
                SetSprite(block, RightSprite, color, sortingOrder, currentMaterial);
                break;
        }
    }


}
