﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/RawKill")]
public class RawKill : Rule
{
    protected override void Action(Block block, PlayerManager player)
    {
        block.Kill(player);
    }
}
