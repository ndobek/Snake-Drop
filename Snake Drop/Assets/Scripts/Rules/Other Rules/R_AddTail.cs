using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/AddTail")]
public class R_AddTail : Rule
{

    protected override void Action(Block block, PlayerManager player = null)
    {
        Block tail = block.GetLastTail();

        tail.SetTail(tail.Slot.CreateBlock(tail.blockColor, tail.blockType));
    }
}
