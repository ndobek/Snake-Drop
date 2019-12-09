using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMaker : BlockSlot
{
    [HideInInspector]
    public List<Block> Blocks;

    public BlockType[] possibleTypes;

    public override void OnAssignment(Block obj)
    {
        base.OnAssignment(obj);
        Blocks.Add(obj);
    }
    public override void OnUnassignment(Block obj)
    {
        base.OnUnassignment(obj);
        if (Blocks.Contains(obj)) Blocks.Remove(obj);
    }
    public override void DeleteBlock()
    {
        base.DeleteBlock();
        Blocks.Clear();
    }

    public bool CheckIsClear()
    {
        return Blocks.Count == 0;
    }

    public Block MakeSnake(int length, float entropy, GameManager obj)
    {
        //Blocks.Clear();
        if (CheckIsClear())
        {
            int type = Random.Range(0, possibleTypes.Length);
            for (int i = 0; i < length; i++)
            {
                if (Random.value < entropy) type = Random.Range(0, possibleTypes.Length);
                CreateBlock(possibleTypes[type]);
                //Blocks[i].isPartOfSnake = true;
                if (i > 0) Blocks[i - 1].SetTail(Blocks[i]);
            }
            return Blocks[0];
        }
        return null;
    }
}
