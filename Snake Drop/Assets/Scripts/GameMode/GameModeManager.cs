using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    public bool HeightLimit;
    public int HeightLimitModifier;

    public GameMode GameMode;
    [HideInInspector]
    public BlockColor[] PossibleColors;

    public SnakeStatType minBaseLength;
    public SnakeStatType maxBaseLength;
    public SnakeStatType minBaseEntropy;
    public SnakeStatType maxBaseEntropy;
    public SnakeStatType LengthScoreMod;
    public SnakeStatType LengthSnakeNumberMod;
    public SnakeStatType EntropyScoreMod;
    public SnakeStatType EntropySnakeNumberMod;
    public SnakeStatType minTotalLength;
    public SnakeStatType maxTotalLength;
    public SnakeStatType minTotalEntropy;
    public SnakeStatType maxTotalEntropy;

    private void Awake()
    {
        PossibleColors = GameMode.GetAllPossibleColors();
    }

    public Stat<BlockType>[] GetTypes(int score, int snakeNumber)
    {
        return GameMode.GetTypes(score, snakeNumber);
    }
    public Stat<BlockColor>[] GetColors(int score, int snakeNumber)
    {
        return GameMode.GetColors(score, snakeNumber);
    }
    public int GetRandomLength(int score, int snakeNumber)
    {
        return (int)GameMode.GetModifiedStat(minBaseLength, maxBaseLength, LengthScoreMod, LengthSnakeNumberMod, minTotalLength, maxTotalLength, score, snakeNumber);
    }
    public float GetRandomEntropy(int score, int snakeNumber)
    {
        return GameMode.GetModifiedStat(minBaseEntropy, maxBaseEntropy, EntropyScoreMod, EntropySnakeNumberMod, minTotalEntropy, maxTotalEntropy, score, snakeNumber);
    }

    public SnakeInfo GetSnakeInfo(int score, int snakeNumber)
    {
        SnakeInfo result = new SnakeInfo()
        {
            possibleTypes = GetTypes(score, snakeNumber),
            possibleColors = GetColors(score, snakeNumber),
            length = GetRandomLength(score, snakeNumber),
            entropy = GetRandomEntropy(score, snakeNumber)
        };

        if (result.possibleColors == null
            || result.possibleTypes == null) return null;

        return result;
    }
}

