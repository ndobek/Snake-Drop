using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/ChangeSnakeHead")]
public class ChangeSnakeHead : Rule
{
    protected override void Action(Block block)
    {
        if (block == GameManager.instance.playerController.SnakeHead)
        {
            if (block.Tail)
            {
                GameManager.instance.playerController.SnakeHead = block.Tail;
            }
            else
            {
                GameManager.instance.playerController.SnakeHead = null;
                GameManager.instance.OnCrash();
            }
        }
    }
}
