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

    public float GetRandomStat(string statType, int score, int sequenceNumber)
    {
        List<Stat<float>> possibleResults = new List<Stat<float>>();
        foreach (Level level in Levels)
        {
            if (level.LevelInRange(score, sequenceNumber))
            {
                foreach(Stat<float> stat in level.Stats)
                {
                    if(stat.StatType == statType) possibleResults.Add(stat);
                }
            }
        }
        if (possibleResults.Count > 0) return possibleResults[Random.Range(0, possibleResults.Count)].value;

        Debug.Log("No Acceptable Difficulty Settings, Returning default:" + statType + ": " + "0");
        return 0;
    }

    public float GetRandomStatRange(string minStatType, string maxStatType, int score, int sequenceNumber)
    {
        return Random.Range(GetRandomStat(minStatType, score, sequenceNumber), GetRandomStat(maxStatType, score, sequenceNumber));
    }

    private float GetStatModifier(string ScoreMod, string SequenceNumberMod, int score, int sequenceNumber)
    {
        float result = 0;

        result += (score * GetRandomStat(ScoreMod, score, sequenceNumber));
        result += (sequenceNumber * GetRandomStat(SequenceNumberMod, score, sequenceNumber));

        return result;
    }

    public float GetModifiedStat(string minBase, string maxBase, string scoreMod, string sequenceNumberMod, string minTotal, string maxTotal, int score, int sequenceNumber)
    {
        float Base = GetRandomStatRange(minBase, maxBase, score, sequenceNumber);
        float Modifier = GetStatModifier(scoreMod, sequenceNumberMod, score, sequenceNumber);
        float min = GetRandomStat(minTotal, score, sequenceNumber);
        float max = GetRandomStat(maxTotal, score, sequenceNumber);

        return Mathf.Clamp(Base + Modifier, min, max);
    }




}
