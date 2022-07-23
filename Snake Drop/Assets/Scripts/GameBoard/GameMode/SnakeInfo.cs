using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeInfo
{
    public int length;
    public float entropy;
    public BlockColor[] possibleColors;
    public BlockType[] possibleTypes;

    public BlockColor GetRandomColor()
    {
        return(possibleColors[Random.Range(0,possibleColors.Length)]);
    }

    public BlockType GetRandomType()
    {
        return (possibleTypes[Random.Range(0, possibleTypes.Length)]);
    }
}
