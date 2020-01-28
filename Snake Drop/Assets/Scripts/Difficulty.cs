using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/Difficulties")]
public class Difficulty : ScriptableObject
{
    public Level[] Levels;


    public SnakeInfo GetRandomSnakeInfo(int score)
    {
        List<SnakeInfo> possibleResults = new List<SnakeInfo>();
        foreach (Level level in Levels)
        {
            if (level.ScoreInRange(score)) possibleResults.Add(level.GetRandomSnakeInfo());
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }

    public BlockColor[] GetAllPossibleColors()
    {
        List<BlockColor> results = new List<BlockColor>();
        foreach (Level level in Levels)
        {
            foreach (BlockColor color in level.possibleColors)
            {
                if (!results.Contains(color)) results.Add(color);
            }
        }
        if (results.Count > 0) return results.ToArray();

        throw new System.Exception("No Acceptable Difficulty Settings");
    }

    public BlockType[] GetRandomBlockTypes(int score)
    {
        List<BlockType[]> possibleResults = new List<BlockType[]>();
        foreach (Level level in Levels)
        {
            if (level.ScoreInRange(score)) possibleResults.Add(level.possibleTypes);
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public BlockColor[] GetRandomBlockColors(int score)
    {
        List<BlockColor[]> possibleResults = new List<BlockColor[]>();
        foreach (Level level in Levels)
        {
            if (level.ScoreInRange(score)) possibleResults.Add(level.possibleColors);
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public int GetRandomLength(int score)
    {
        List<int> possibleResults = new List<int>();
        foreach (Level level in Levels)
        {
            if (level.ScoreInRange(score)) possibleResults.Add(level.GetRandomLength());
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public float GetRandomEntropy(int score)
    {
        List<float> possibleResults = new List<float>();
        foreach (Level level in Levels)
        {
            if (level.ScoreInRange(score)) possibleResults.Add(level.GetRandomEntropy());
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
}
