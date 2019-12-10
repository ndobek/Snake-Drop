using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMaker : BlockSlot
{
    [HideInInspector]
    public List<Block> Blocks;

    public BlockColor[] possibleColors;
    public BlockType defaultType;
    public BlockType snakeHeadType;

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
            int color = Random.Range(0, possibleColors.Length);
            CreateBlock(possibleColors[color], snakeHeadType);
            for (int i = 1; i < length; i++)
            {
                if (Random.value < entropy) color = Random.Range(0, possibleColors.Length);
                //Blocks[i].isPartOfSnake = true;
                Blocks[i - 1].SetTail(Blocks[i]);
            }
            return Blocks[0];
        }
        return null;
    }
}
