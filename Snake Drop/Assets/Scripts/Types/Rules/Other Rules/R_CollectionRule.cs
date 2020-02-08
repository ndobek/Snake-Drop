using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Collection Rules/Collection Rule")]
public class R_CollectionRule : Rule
{
    public Rule Rule;

    protected override void Action(Block block, PlayerManager player = null)
    {
        Block[] blocks = block.BlockCollection.Blocks;

        foreach (Block obj in blocks)
        {
            obj.Slot.playGrid.SetDirty();
            Rule.Invoke(obj, player);
        }
    }
}
