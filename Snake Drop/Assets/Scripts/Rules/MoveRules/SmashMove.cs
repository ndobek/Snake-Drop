﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/MoveRule/Smash")]
public class SmashMove : MoveRule
{
    public Rule OnSmash;
    protected override bool MoveCondition(Block block, BlockSlot slot, PlayerManager player = null)
    {

        if (slot && slot.Block)
        {
            if (slot.Block && !slot.Blocks.TrueForAll(Block.isNotPartOfSnake)) return false;
            return slot.Block.blockColor == block.blockColor;
        }
        return false;
    }
    protected override void MoveAction(Block block, BlockSlot slot, PlayerManager player = null)
    {
        //Sound
        MusicManager m = FindFirstObjectByType<MusicManager>();
        m.ParseBlock(block);
        m.ParseBlock(slot.Block);

        Block tail = null;
        if (block.Tail) { tail = block.Tail; }
        //if (slot.Block && slot.Block.isPartOfSnake) slot.Block.KillSnake();
        slot.Block.Break(player);
        block.RawMoveTo(slot);

        if (OnSmash) OnSmash.Invoke(block, player);
        block.Break(player);
        if (tail)
        {
            tail.RawMoveTo(slot);
        }
        //else
        //{
        //    player.OnCrash(true);
        //}

    }
}
