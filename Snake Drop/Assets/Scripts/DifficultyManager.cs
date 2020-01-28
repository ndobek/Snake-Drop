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

    public BlockType[] GetRandomTypes(int score)
    {
        return Difficulty.GetRandomBlockTypes(score);
    }
    public BlockColor[] GetRandomColors(int score)
    {
        return Difficulty.GetRandomBlockColors(score);
    }
    public int GetRandomSnakeLength(int score)
    {
        return Difficulty.GetRandomLength(score);
    }
    public float GetRandomSnakeEntropy(int score)
    {
        return Difficulty.GetRandomEntropy(score);
    }

    public SnakeInfo GetDetachedSnakeInfo(int score)
    {
        return new SnakeInfo()
        {
            possibleTypes = GetRandomTypes(score),
            possibleColors = GetRandomColors(score),
            length = GetRandomSnakeLength(score),
            entropy = GetRandomSnakeEntropy(score)
        };
    }
    public SnakeInfo GetSnakeInfo(int score)
    {
        return Difficulty.GetRandomSnakeInfo(score);
    }

}

