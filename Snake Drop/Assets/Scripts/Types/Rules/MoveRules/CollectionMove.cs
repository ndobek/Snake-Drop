using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/MoveRule/Collection")]
public class CollectionMove : MoveRule
{
    protected override bool MoveCondition(Block block, BlockSlot slot)
    {
        if(block && slot && block.BlockCollection != null)
        {
            bool result = true;
            Block[] blocks = block.BlockCollection.Blocks;
            int xDistance = slot.x - block.X;
            int yDistance = slot.y - block.Y;

            foreach(Block obj in blocks)
            {
                BlockSlot destination = obj.Slot.GetNeighbor(xDistance, yDistance);
                if (!CanBasicMove(obj, destination) && (!destination || !blocks.Contains(destination.Block))) result = false;
            }

            return result;
        }
        return false;
    }
    private bool CanBasicMove(Block block, BlockSlot slot)
    {
        return slot && slot.Block == null;
    }

    protected override void MoveAction(Block block, BlockSlot slot)
    {
        Block[] blocks = block.BlockCollection.Blocks;
        int xDistance = slot.x - block.X;
        int yDistance = slot.y - block.Y;

        foreach (Block obj in blocks)
        {
            BlockSlot destination = obj.Slot.GetNeighbor(xDistance, yDistance);
            obj.RawMoveTo(destination);
        }

        block.BlockCollection.UpdateCoords();
    }

}
