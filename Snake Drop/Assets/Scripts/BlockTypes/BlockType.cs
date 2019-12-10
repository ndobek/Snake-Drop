using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;

    public virtual void OnEat(Block block, BlockSlot slot)
    {
        Block blockObj = null;
        if (slot) blockObj = slot.Block;
        //if (blockObj) Debug.Log(blockObj.blockType);
        //else Debug.Log("null");

        if (!slot | (blockObj != null && blockObj.blockColor != block.blockColor))
        {
            block.Kill();
        }
        else
        {
            if (blockObj)
            {

                GameManager.instance.difficultyManager.Score += 1;
                blockObj.Kill();
                slot.DeleteBlock();
            }
            block.MoveTo(slot);
        }
        block.MoveTo(slot);
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
