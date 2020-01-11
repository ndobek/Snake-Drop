using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BreakRules/ChangeSnakeHeadRule")]
public class ChangeSnakeHeadRule : BreakRule
{
    protected override void BreakAction(Block block)
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
