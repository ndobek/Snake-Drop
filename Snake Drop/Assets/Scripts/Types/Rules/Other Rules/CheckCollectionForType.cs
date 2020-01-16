using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/CheckCollectionForSnake")]
public class CheckCollectionForType : Rule
{
    public Rule OnNoneOfType;
    public BlockType Type;

    protected override void Action(Block block)
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

        if (!CollectionContainsType) OnNoneOfType.Invoke(block);
    }
}
