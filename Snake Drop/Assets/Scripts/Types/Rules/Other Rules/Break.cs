﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/CommandToBreak")]
public class Break : Rule
{
    protected override void Action(Block block, PlayerManager player)
    {
        block.Break(player);
    }
}