using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public bool HeightLimit;
    public int HeightLimitModifier;

    public Difficulty Difficulty;
    [HideInInspector]
    public BlockColor[] PossibleColors;

    private void Awake()
    {
        PossibleColors = Difficulty.GetAllPossibleColors();
    }

    public BlockType[] GetRandomTypes(int score, int snakeNumber)
    {
        return Difficulty.GetTypes(score, snakeNumber);
    }
    public BlockColor[] GetRandomColors(int score, int snakeNumber)
    {
        return Difficulty.GetColors(score, snakeNumber);
    }
    public int GetRandomSnakeLength(int score, int snakeNumber)
    {
        return Difficulty.GetRandomLength(score, snakeNumber);
    }
    public float GetRandomSnakeEntropy(int score, int snakeNumber)
    {
        return Difficulty.GetRandomEntropy(score, snakeNumber);
    }

    public SnakeInfo GetSnakeInfo(int score, int snakeNumber)
    {
        return Difficulty.GetSnakeInfo(score, snakeNumber);
    }
    //public SnakeInfo GetSnakeInfo(int score, int snakeNumber)
    //{
    //    return Difficulty.GetRandomSnakeInfo(score, snakeNumber);
    //}

}

