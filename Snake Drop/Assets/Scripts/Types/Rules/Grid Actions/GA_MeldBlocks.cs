using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/MeldBlocks")]
public class GA_MeldBlocks : GridAction
{
    protected override void Action(PlayGrid grid)
    {
        BlockMelder.Meld(grid, GameManager.instance.difficultyManager.PossibleColors);
    }
}
