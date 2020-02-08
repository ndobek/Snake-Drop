using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/DoIfInPreviewGrid")]
public class DoIfInPreviewGrid : Rule
{
    public Rule Rule;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if(player && block.Slot.playGrid == player.previewGrid)
        {
            Rule.Invoke(block, player);
        }
    }
}
