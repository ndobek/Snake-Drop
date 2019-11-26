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
        Blocks.Add(obj);
    }

    public Block MakeSnake(int length, float entropy, GameManager obj)
    {
        if (Blocks.Count == 0)
        {
            int type = Random.Range(0, possibleTypes.Length);
            for (int i = 0; i < length; i++)
            {
                if (Random.value < entropy) type = Random.Range(0, possibleTypes.Length);
                CreateBlock(possibleTypes[type]);
                Blocks[i].isPartOfSnake = true;
                if (i > 0) Blocks[i - 1].SetTail(Blocks[i]);
            }
            
        }
        return Blocks[0];
    }
}
