using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/SnakeType")]
public class SnakeType : ScriptableObject
{
    public BlockType[] possibleTypes;
    public BlockColor[] possibleColors;

    public int minBaseLength;
    public int maxBaseLength;

    public float minBaseEntropy;
    public float maxBaseEntropy;

    public float LengthScoreMod;
    public float LengthSnakeNumberMod;

    public float EntropyScoreMod;
    public float EntropySnakeNumberMod;

    public int minTotalLength;
    public int maxTotalLength;

    public float minTotalEntropy;
    public float maxTotalEntropy;

    public int GetRandomLength()
    {
        return Random.Range(minTotalLength, maxTotalLength);
    }
    public float GetRandomEntropy()
    {
        return Random.Range(minTotalEntropy, maxTotalEntropy);
    }
    private int GetRandomBaseLength()
    {
        return Random.Range(minBaseLength, maxBaseLength);
    }
    private float GetRandomBaseEntropy()
    {
        return Random.Range(minBaseEntropy, maxBaseEntropy);
    }

    private int GetLengthModifier(int score, int snakeNumber)
    {
        int result = 0;

        result += (int)(score * LengthScoreMod);
        result += (int)(snakeNumber * LengthSnakeNumberMod);

        return result;
    }
    private float GetEntropyModifier(int score, int snakeNumber)
    {
        int result = 0;

        result += (int)(score * EntropyScoreMod);
        result += (int)(snakeNumber * EntropySnakeNumberMod);

        return result;
    }

    public int GetLength(int score, int snakeNumber)
    {
        return Mathf.Clamp(GetRandomBaseLength() + GetLengthModifier(score, snakeNumber), minTotalLength, maxTotalLength);
    }
    public float GetEntropy(int score, int snakeNumber)
    {
        return Mathf.Clamp(GetRandomBaseEntropy() + GetEntropyModifier(score, snakeNumber), minTotalEntropy, maxTotalEntropy);
    }

    public SnakeInfo GetRandomSnakeInfo(int score, int snakeNumber)
    {
        return new SnakeInfo()
        {
            entropy = GetEntropy(score, snakeNumber),
            length = GetLength(score, snakeNumber),
            possibleColors = possibleColors,
            possibleTypes = possibleTypes
        };
    }
}
