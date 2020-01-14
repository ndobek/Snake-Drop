using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/RawBreak")]
public class Break : Rule
{
    protected override void Action(Block block)
    {
        block.RawBreak();
    }
}
