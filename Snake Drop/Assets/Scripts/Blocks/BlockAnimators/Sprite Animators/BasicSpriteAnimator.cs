using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAnimators/BasicSpriteAnimator")]
public class BasicSpriteAnimator : BlockSpriteAnimator
{
    public Sprite sprite;
    public Color color;
    public int sortingOrder;

    //Alyssa Interferes
    public Material materialInSnake;
    public Material materialOutOfSnake;

    public override void OnComplete(BlockAnimation blockAnimation)
    {
        if (blockAnimation.block.blockType.isPartOfSnake == true)//Alyssa made this if statement no scare
        {
            SetSprite(blockAnimation.block, sprite, color, sortingOrder, materialInSnake);//I added material dont b mad
        }
        else //Alyssa added else :(
        {
            SetSprite(blockAnimation.block, sprite, color, sortingOrder, materialOutOfSnake);
        }
        
    }
}
