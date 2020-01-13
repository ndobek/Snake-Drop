using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/Rules/ChangeType")]
public class ChangeType : Rule
{
    public BlockType ChangeTypeTo;

    protected override void Action(Block block)
    {
        if (ChangeTypeTo) block.SetBlockType(block.blockColor, ChangeTypeTo);
    }
}
