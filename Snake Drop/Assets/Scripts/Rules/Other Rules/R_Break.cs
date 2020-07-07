using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Block Functions/Break")]
public class R_Break : Rule
{
    protected override void Action(Block block, PlayerManager player)
    {
        block.Break(player);
    }
}
