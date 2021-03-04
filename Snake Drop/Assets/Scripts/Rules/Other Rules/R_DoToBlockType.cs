using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Containers And Conditions/Do If Of BlockType")]
public class R_DoToBlockType : Rule
{
    public BlockType blockType;
    public Rule DoIfBlockType;
    public Rule DoIfNotBlockType;

    protected override void Action(Block block, PlayerManager player = null)
    {
        if (block.blockType == blockType && DoIfBlockType != null) DoIfBlockType.Invoke(block, player);
        if (block.blockType != blockType && DoIfNotBlockType != null) DoIfNotBlockType.Invoke(block, player);
    }
}
