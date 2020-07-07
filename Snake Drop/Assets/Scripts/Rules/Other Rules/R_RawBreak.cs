using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Block Functions/Raw Break")]
public class R_RawBreak : Rule
{
    protected override void Action(Block block, PlayerManager player)
    {
        //block.Kill(player);
        block.RawBreak();
    }
}
