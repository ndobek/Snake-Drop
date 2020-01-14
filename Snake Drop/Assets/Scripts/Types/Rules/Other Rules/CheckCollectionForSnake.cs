using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/CheckCollectionForSnake")]
public class CheckCollectionForSnake : Rule
{
    public Rule OnSnakeLeave;
    protected override void Action(Block block)
    {
        if (block.BlockCollection == null) throw new System.ArgumentException("No Collection on this block");

        BlockSlot[] CollectionSlots = block.BlockCollection.Slots;

        bool CollectionContainsSnake = false;
        foreach(BlockSlot slot in CollectionSlots)
        {
            foreach(Block obj in slot.Blocks)
            {
                if(obj.isPartOfSnake) CollectionContainsSnake = true;
            }

        }

        if (!CollectionContainsSnake) OnSnakeLeave.Invoke(block);
    }
}
