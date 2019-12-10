using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BlockType")]
public class BlockType : ScriptableObject
{
    public Sprite sprite;
    public BlockType ChangeTypeToOnDeath;

    public virtual void OnEat(Block block, BlockSlot slot)
    {
        Block eatenBlock = null;
        if (slot) eatenBlock = slot.Block;
        //if (blockObj) Debug.Log(blockObj.blockType);
        //else Debug.Log("null");

        if (!slot | (eatenBlock != null && eatenBlock.blockColor != block.blockColor))
        {
            block.Kill();
        }
        else
        {
            if (eatenBlock)
            {

                GameManager.instance.difficultyManager.Score += 1;
                eatenBlock.Kill();
                slot.DeleteBlock();
            }
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
        if(ChangeTypeToOnDeath) block.SetBlockType(block.blockColor, ChangeTypeToOnDeath);
        block.UpdateBlock();
        GameManager.instance.OnBlockDeath(block);
    }
}
