using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Do To Every Block in Slot")]
public class R_DoToEveryBlockInSlot : Rule
{
    public Rule Rule;
    protected override void Action(Block block, PlayerManager player)
    {
        foreach(Block obj in block.Slot.Blocks)
        {
            if (obj) Rule.Invoke(obj, player);
        }
    }
}
