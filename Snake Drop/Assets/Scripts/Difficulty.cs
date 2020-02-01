using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/Difficulties")]
public class Difficulty : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public int MinScore;
        public int MaxScore;
        public bool UseMinScore;
        public bool UseMaxScore;

        public int MinSnakeNumber;
        public int MaxSnakeNumber;
        public bool UseMinSnakeNumber;
        public bool UseMaxSnakeNumber;

        public SnakeType SnakeType;

        private bool ScoreInRange(int score)
        {
            return (!UseMinScore || score >= MinScore) && (!UseMaxScore || score <= MaxScore);
        }

        private bool SnakeNumberInRange(int snakeNumber)
        {
            return (!UseMinSnakeNumber || snakeNumber >= MinSnakeNumber) && (!UseMaxSnakeNumber || snakeNumber <= MaxSnakeNumber);
        }

        public bool LevelInRange(int score, int snakeNumber)
        {
            return ScoreInRange(score) && SnakeNumberInRange(snakeNumber);
        }

    }

    [SerializeField]
    public Level[] Levels;


    public SnakeInfo GetRandomSnakeInfo(int score, int snakeNumber)
    {
        List<SnakeInfo> possibleResults = new List<SnakeInfo>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.SnakeType.GetRandomSnakeInfo(score, snakeNumber));
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }

    public BlockColor[] GetAllPossibleColors()
    {
        List<BlockColor> results = new List<BlockColor>();
        foreach (Level level in Levels)
        {
            foreach (BlockColor color in level.SnakeType.possibleColors)
            {
                if (!results.Contains(color)) results.Add(color);
            }
        }
        if (results.Count > 0) return results.ToArray();

        throw new System.Exception("No Acceptable Difficulty Settings");
    }

    public BlockType[] GetRandomBlockTypes(int score, int snakeNumber)
    {
        List<BlockType[]> possibleResults = new List<BlockType[]>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.SnakeType.possibleTypes);
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public BlockColor[] GetRandomBlockColors(int score, int snakeNumber)
    {
        List<BlockColor[]> possibleResults = new List<BlockColor[]>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.SnakeType.possibleColors);
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public int GetRandomLength(int score, int snakeNumber)
    {
        List<int> possibleResults = new List<int>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.SnakeType.GetRandomLength());
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public float GetRandomEntropy(int score, int snakeNumber)
    {
        List<float> possibleResults = new List<float>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.SnakeType.GetRandomEntropy());
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
}
