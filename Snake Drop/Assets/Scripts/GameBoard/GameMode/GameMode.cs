using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameModes/GameMode")]
public class GameMode : ScriptableObject
{
    public EndGameCondition EndGameCondition;
    public TypeBank TypeBank;
    public GridAction OnGameStart;
    public Rule OnCrash;

    [System.Serializable]
    public class Level
    {
        //public enum StatTypes
        //{
        //    minBaseLength,
        //    maxBaseLength,
        //    minBaseEntropy,
        //    maxBaseEntropy,
        //    LengthScoreMod,
        //    LengthSnakeNumberMod,
        //    EntropyScoreMod,
        //    EntropySnakeNumberMod,
        //    minTotalLength,
        //    maxTotalLength,
        //    minTotalEntropy,
        //    maxTotalEntropy
        //}

        public int MinScore;
        public int MaxScore;
        public bool UseMinScore;
        public bool UseMaxScore;

        public int MinSnakeNumber;
        public int MaxSnakeNumber;
        public bool UseMinSnakeNumber;
        public bool UseMaxSnakeNumber;

        public Stat<BlockType>[] possibleTypes;
        public Stat<BlockColor>[] possibleColors;
        public Stat<float>[] Stats;

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
    public Stat<BlockType>[] GetTypes(int score, int snakeNumber)
    {
        List<Stat<BlockType>> results = new List<Stat<BlockType>>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber))
            {
                foreach (Stat<BlockType> type in level.possibleTypes)
                {
                    results.Add(type);
                }
            }
        }
        if (results.Count > 0) return results.ToArray();

        return null;
        //throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public Stat<BlockColor>[] GetColors(int score, int snakeNumber)
    {
        List<Stat<BlockColor>> results = new List<Stat<BlockColor>>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber))
            {
                foreach (Stat<BlockColor> color in level.possibleColors)
                {
                    if (!results.Contains(color)) results.Add(color);
                }
            }
        }
        if (results.Count > 0) return results.ToArray();

        return null;
        //throw new System.Exception("No Acceptable Difficulty Settings");
    }

    public Stat<BlockColor>[] GetAllPossibleColorStats()
    {
        List<Stat<BlockColor>> results = new List<Stat<BlockColor>>();
        foreach (Level level in Levels)
        {
            foreach (Stat<BlockColor> color in level.possibleColors)
            {
                if (!results.Contains(color)) results.Add(color);
            }
        }
        if (results.Count > 0) return results.ToArray();

        return null;
        //throw new System.Exception("No Acceptable Difficulty Settings");
    }
    public BlockColor[] GetAllPossibleColors()
    {
        List<BlockColor> results = new List<BlockColor>();
        foreach (Level level in Levels)
        {
            foreach (Stat<BlockColor> color in level.possibleColors)
            {
                if (!results.Contains(color.value)) results.Add(color.value);
            }
        }
        if (results.Count > 0) return results.ToArray();
        
        return null;
        //throw new System.Exception("No Acceptable Difficulty Settings");
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


    public float GetRandomStat(SnakeStatType statType, int score, int snakeNumber)
    {
        List<Stat<float>> possibleResults = new List<Stat<float>>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, snakeNumber))
            {
                foreach(Stat<float> stat in level.Stats)
                {
                    if(stat.StatType == statType) possibleResults.Add(stat);
                }
            }
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)].value;

        //Debug.Log("No Acceptable Difficulty Settings, Returning default:" + statType + ": " + statType.defaultValue);
        return statType.defaultValue;
    }

    public float GetRandomStatRange(SnakeStatType minStatType, SnakeStatType maxStatType, int score, int snakeNumber)
    {
        return Random.Range(GetRandomStat(minStatType, score, snakeNumber), GetRandomStat(maxStatType, score, snakeNumber));
    }

    private float GetStatModifier(SnakeStatType ScoreMod, SnakeStatType SnakeNumberMod, int score, int snakeNumber)
    {
        float result = 0;

        result += (score * GetRandomStat(ScoreMod, score, snakeNumber));
        result += (snakeNumber * GetRandomStat(SnakeNumberMod, score, snakeNumber));

        return result;
    }

    public float GetModifiedStat(SnakeStatType minBase, SnakeStatType maxBase, SnakeStatType scoreMod, SnakeStatType snakeNumberMod, SnakeStatType minTotal, SnakeStatType maxTotal, int score, int snakeNumber)
    {
        float Base = GetRandomStatRange(minBase, maxBase, score, snakeNumber);
        float Modifier = GetStatModifier(scoreMod, snakeNumberMod, score, snakeNumber);
        float min = GetRandomStat(minTotal, score, snakeNumber);
        float max = GetRandomStat(maxTotal, score, snakeNumber);

        return Mathf.Clamp(Base + Modifier, min, max);
    }




}
