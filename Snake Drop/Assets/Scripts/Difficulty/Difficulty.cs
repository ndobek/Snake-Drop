using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/Difficulties")]
public class Difficulty : ScriptableObject
{
    [System.Serializable]
    public class Stat
    {
        public Difficulty.Level.StatTypes StatType;
        public float value;
    }
    [System.Serializable]
    public class Level
    {
        public enum StatTypes
        {
            minBaseLength,
            maxBaseLength,
            minBaseEntropy,
            maxBaseEntropy,
            LengthScoreMod,
            LengthSnakeNumberMod,
            EntropyScoreMod,
            EntropySnakeNumberMod,
            minTotalLength,
            maxTotalLength,
            minTotalEntropy,
            maxTotalEntropy
        }

        public int MinScore;
        public int MaxScore;
        public bool UseMinScore;
        public bool UseMaxScore;

        public int MinSnakeNumber;
        public int MaxSnakeNumber;
        public bool UseMinSnakeNumber;
        public bool UseMaxSnakeNumber;

        public BlockType[] possibleTypes;
        public BlockColor[] possibleColors;
        public Stat[] Stats;
        //public SnakeType SnakeType;


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


    //public SnakeInfo GetRandomSnakeInfo(int score, int snakeNumber)
    //{
    //    List<SnakeInfo> possibleResults = new List<SnakeInfo>();
    //    foreach (Level level in Levels)
    //    {
    //        if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.SnakeType.GetRandomSnakeInfo(score, snakeNumber));
    //    }
    //    if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

    //    throw new System.Exception("No Acceptable Difficulty Settings");
    //}
    public BlockType[] GetTypes(int score, int snakeNumber)
    {
        List<BlockType> results = new List<BlockType>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber))
            {
                foreach (BlockType type in level.possibleTypes)
                {
                    results.Add(type);
                }
            }
        }
        if (results.Count > 0) return results.ToArray();

        throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public BlockColor[] GetColors(int score, int snakeNumber)
    {
        List<BlockColor> results = new List<BlockColor>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber))
            {
                foreach (BlockColor color in level.possibleColors)
                {
                    if (!results.Contains(color)) results.Add(color);
                }
            }
        }
        if (results.Count > 0) return results.ToArray();

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

    //public BlockType[] GetRandomTypes(int score, int snakeNumber)
    //{
    //    List<BlockType[]> possibleResults = new List<BlockType[]>();
    //    foreach (Level level in Levels)
    //    {
    //        if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.possibleTypes);
    //    }
    //    if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

    //    throw new System.Exception("No Acceptable Difficulty Settings");
    //}
    //public BlockColor[] GetRandomColors(int score, int snakeNumber)
    //{
    //    List<BlockColor[]> possibleResults = new List<BlockColor[]>();
    //    foreach (Level level in Levels)
    //    {
    //        if (level.LevelInRange(score, snakeNumber)) possibleResults.Add(level.possibleColors);
    //    }
    //    if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)];

    //    throw new System.Exception("No Acceptable Difficulty Settings");
    //}


    public float GetRandomStat(Level.StatTypes statType, int score, int snakeNumber)
    {
        List<Stat> possibleResults = new List<Stat>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber))
            {
                foreach(Stat stat in level.Stats)
                {
                    if(stat.StatType == statType) possibleResults.Add(stat);
                }
            }
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)].value;

        throw new System.Exception("No Acceptable Difficulty Settings");
    }

    public float GetRandomStatRange(Level.StatTypes minStatType, Level.StatTypes maxStatType, int score, int snakeNumber)
    {
        return Random.Range(GetRandomStat(minStatType, score, snakeNumber), GetRandomStat(maxStatType, score, snakeNumber));
    }

    private int GetStatModifier(Level.StatTypes ScoreMod, Level.StatTypes SnakeNumberMod, int score, int snakeNumber)
    {
        int result = 0;

        result += (int)(score * GetRandomStat(ScoreMod, score, snakeNumber));
        result += (int)(snakeNumber * GetRandomStat(SnakeNumberMod, score, snakeNumber));

        return result;
    }

    public float GetModifiedStat(Level.StatTypes minBase, Level.StatTypes maxBase, Level.StatTypes scoreMod, Level.StatTypes snakeNumberMod, Level.StatTypes minTotal, Level.StatTypes maxTotal, int score, int snakeNumber)
    {
        float Base = GetRandomStatRange(minBase, maxBase, score, snakeNumber);
        float Modifier = GetStatModifier(scoreMod, snakeNumberMod, score, snakeNumber);
        float min = GetRandomStat(minTotal, score, snakeNumber);
        float max = GetRandomStat(maxTotal, score, snakeNumber);

        return Mathf.Clamp(Base + Modifier, min, max);
    }

    public int GetRandomLength(int score, int snakeNumber)
    {
        return (int)GetModifiedStat(Level.StatTypes.minBaseLength, Level.StatTypes.maxBaseLength, Level.StatTypes.LengthScoreMod, Level.StatTypes.LengthSnakeNumberMod, Level.StatTypes.minTotalLength, Level.StatTypes.maxTotalLength, score, snakeNumber);
    }
    public float GetRandomEntropy(int score, int snakeNumber)
    {
        return GetModifiedStat(Level.StatTypes.minBaseEntropy, Level.StatTypes.maxBaseEntropy, Level.StatTypes.EntropyScoreMod, Level.StatTypes.EntropySnakeNumberMod, Level.StatTypes.minTotalEntropy, Level.StatTypes.maxTotalEntropy, score, snakeNumber);
    }

    public SnakeInfo GetSnakeInfo(int score, int snakeNumber)
    {
        return new SnakeInfo()
        {
            possibleTypes = GetTypes(score, snakeNumber),
            possibleColors = GetColors(score, snakeNumber),
            length = GetRandomLength(score, snakeNumber),
            entropy = GetRandomEntropy(score, snakeNumber)
        };
    }
}
