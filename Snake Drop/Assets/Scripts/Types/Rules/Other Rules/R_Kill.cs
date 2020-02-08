using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Block Functions/Kill")]
public class R_Kill : Rule
{
    protected override void Action(Block block, PlayerManager player)
    {
        block.Kill(player);
    }
}
