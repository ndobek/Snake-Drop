﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/RawBreak")]
public class RawBreak : Rule
{
    protected override void Action(Block block, PlayerManager player)
    {
        block.Kill(player);
        block.RawBreak();
    }
}
