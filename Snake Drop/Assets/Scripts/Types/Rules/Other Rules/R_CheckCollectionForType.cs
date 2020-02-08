using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Collection Rules/Check Collection For Type")]
public class R_CheckCollectionForType : Rule
{
    public Rule OnNoneOfType;
    public Rule OnType;
    public BlockType Type;

    protected override void Action(Block block, PlayerManager player = null)
    {
        if (block.BlockCollection == null) throw new System.ArgumentException("No Collection on this block");

        BlockSlot[] CollectionSlots = block.BlockCollection.Slots;

        bool CollectionContainsType = false;
        foreach(BlockSlot slot in CollectionSlots)
        {
            foreach(Block obj in slot.Blocks)
            {
                if(obj.blockType == Type) CollectionContainsType = true;
            }

        }

        if (!CollectionContainsType && OnNoneOfType != null) OnNoneOfType.Invoke(block, player);
        if (CollectionContainsType && OnType != null) OnType.Invoke(block, player);
    }
}
