using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/Rules/RawBreak")]
public class Break : Rule
{
    protected override void Action(Block block)
    {
        block.RawBreak();
    }
}
