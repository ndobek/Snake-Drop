using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;

    public virtual void OnMove(Block block, BlockSlot slot)
    {
        block.Slot = slot;
    }


    public virtual void OnKill(Block block)
    {
        if (block.Tail != null)
        {
            if (block.Tail.Slot.playGrid == GameManager.instance.playGrid) block.Tail.Kill();
            block.SetTail(null);
        }
        block.isPartOfSnake = false;
        block.UpdateBlock();
        GameManager.instance.OnBlockDeath(block);
    }
}
