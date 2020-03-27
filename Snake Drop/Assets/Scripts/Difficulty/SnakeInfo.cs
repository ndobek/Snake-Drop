using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SnakeInfo
{
    public int length;
    public float entropy;
    public BlockColor[] possibleColors;
    public BlockType[] possibleTypes;
}
