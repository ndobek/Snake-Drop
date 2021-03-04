using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeInfo
{
    public int length;
    public float entropy;
    public Stat<BlockColor>[] possibleColors;
    public Stat<BlockType>[] possibleTypes;
}