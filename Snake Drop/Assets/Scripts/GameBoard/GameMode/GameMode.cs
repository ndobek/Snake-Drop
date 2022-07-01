using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameModes/GameMode")]
public class GameMode : ScriptableObject
{
    public BoolRule EndGameCondition;
    public TypeBank TypeBank;
    public GridAction OnGameStart;
    public Rule OnCrash;

    [System.Serializable]
    public class Level
    {

        public BoolRule LevelValid;
        public SnakeType snakeType;
        public FloatRule NextPowerupRule;
        public float probability;

    }

    [SerializeField]
    public Level[] Levels;


    public Level GetLevel()
    {
        PlayerManager player = GameManager.instance.playerManagers[0];
        float denominator = 0;
        foreach (Level level in Levels)
        {
            if(level.LevelValid.Invoke(player)) denominator += 0;
        }
        float result = Random.Range(0, denominator);
        foreach (Level level in Levels)
        {
            if (level.LevelValid.Invoke(player))
            {
                result -= level.probability;
                if (result <= 0) return level;
            }
        }
        return null;
    }
    public SnakeInfo GetSnakeInfo()
    {
        return GetLevel().snakeType.GetSnakeInfo();
    }

    public BlockColor[] GetAllPossibleColors()
    {
        List<BlockColor> result = new List<BlockColor>();
        foreach(Level level in Levels)
        {
            foreach(BlockColor color in level.snakeType.possibleBlockColors)
            {
                if (!result.Contains(color)) result.Add(color);
            }
        }
        return result.ToArray();
    }

    public float GetPowerupInfo(PlayerManager player)
    {
        return GetLevel().NextPowerupRule.Invoke(player);
    }

}
