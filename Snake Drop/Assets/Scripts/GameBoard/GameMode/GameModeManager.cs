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


    public SnakeInfo GetSnakeInfo()
    {
        SnakeInfo result = GameMode.GetSnakeInfo();

        if (result.possibleColors == null
            || result.possibleTypes == null) return null;

        return result;
    }

    public float GetPowerupInfo(PlayerManager player)
    {
        return GameMode.GetPowerupInfo(player);
    }
}

