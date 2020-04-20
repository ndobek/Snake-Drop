using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Block Functions/Special Action")]
public class R_InvokeSpecialAction : Rule
{
    protected override void Action(Block block, PlayerManager player = null)
    {
        block.SpecialAction(player);
    }
}
