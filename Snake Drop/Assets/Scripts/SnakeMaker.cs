using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMaker : BlockSlot
{
    [HideInInspector]
    public List<Block> Blocks;
    public override Block Block
    {
        get {
            if (Blocks.Count > 0) return Blocks[0];
            else return null;
        }
    }
    public Block mostRecentSnake;

    public BlockColor[] possibleColors;


    public override void OnAssignment(Block obj)
    {
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

    public void MakeSnake(int length, float entropy)
    {
        //Blocks.Clear();
        if (CheckIsClear())
        {
            int color = Random.Range(0, possibleColors.Length);
            CreateBlock(possibleColors[color], GameManager.instance.snakeHeadType);
            for (int i = 1; i < length; i++)
            {
                if (Random.value < entropy) color = Random.Range(0, possibleColors.Length);
                CreateBlock(possibleColors[color], GameManager.instance.defaultType);
                //Blocks[i].isPartOfSnake = true;
                if (i > 0) Blocks[i - 1].SetTail(Blocks[i]);
            }
            mostRecentSnake = Blocks[0];
        }
    }
}
