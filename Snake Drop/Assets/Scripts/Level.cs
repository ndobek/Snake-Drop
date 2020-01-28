using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/Levels")]
public class Level : ScriptableObject
{

    public int MinScore;
    public int MaxScore;
    public bool HasMaxScore;

    public BlockType[] possibleTypes;
    public BlockColor[] possibleColors;

    public int minSnakeLength;
    public int maxSnakeLength;

    public float minSnakeEntropy;
    public float maxSnakeEntropy;

    public bool ScoreInRange(int score)
    {
        return score >= MinScore && (!HasMaxScore || score <= MaxScore);
    }

    public int GetRandomLength()
    {
        return Random.Range(minSnakeLength, maxSnakeLength);
    }
    public float GetRandomEntropy()
    {
        return Random.Range(minSnakeEntropy, maxSnakeEntropy);
    }
}
