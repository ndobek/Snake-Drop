using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/SnakeType")]
public class SnakeType : ScriptableObject
{
    public BlockType[] possibleTypes;
    public BlockColor[] possibleColors;

    public int minSnakeLength;
    public int maxSnakeLength;

    public float minSnakeEntropy;
    public float maxSnakeEntropy;

    public int GetRandomLength()
    {
        return Random.Range(minSnakeLength, maxSnakeLength);
    }
    public float GetRandomEntropy()
    {
        return Random.Range(minSnakeEntropy, maxSnakeEntropy);
    }

    public SnakeInfo GetRandomSnakeInfo()
    {
        return new SnakeInfo()
        {
            entropy = GetRandomEntropy(),
            length = GetRandomLength(),
            possibleColors = possibleColors,
            possibleTypes = possibleTypes
        };
    }
}
