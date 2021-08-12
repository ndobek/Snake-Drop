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
        return (int)GameMode.GetModifiedStat("minBaseLength", "maxBaseLength", "lengthScoreMod", "lengthSnakeNumberMod", "minTotalLength", "maxTotalLength", score, snakeNumber);
    }
    public float GetRandomEntropy(int score, int snakeNumber)
    {
        return GameMode.GetModifiedStat("minBaseEntropy", "maxBaseEntropy", "entropyScoreMod", "entropySnakeNumberMod", "minTotalEntropy", "maxTotalEntropy", score, snakeNumber);
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

    public float GetPowerupInfo(int score, int powerupNumber)
    {
        return GameMode.GetModifiedStat("minBasePowerup", "maxBasePowerup", "powerupScoreMod", "powerupSequenceMod", "minTotalPowerup", "maxTotalPowerup", score, powerupNumber);
    }
}

