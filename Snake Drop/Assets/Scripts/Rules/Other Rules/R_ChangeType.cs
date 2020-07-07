using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Change Type")]
public class R_ChangeType : Rule
{
    public BlockType ChangeTypeTo;

    protected override void Action(Block block, PlayerManager player = null)
    {
        if (ChangeTypeTo) block.SetBlockType(block.blockColor, ChangeTypeTo);
    }
}
