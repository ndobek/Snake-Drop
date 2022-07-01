using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Snake Type")]
public class SnakeType : ScriptableObject
{
    public BlockColor[] possibleBlockColors;
    public BlockType[] possibleBlockTypes;
    public FloatRule lengthRule;
    public FloatRule entropyRule;

    public SnakeInfo GetSnakeInfo()
    {
        PlayerManager player = GameManager.instance.playerManagers[0];
        return new SnakeInfo()
        {

            entropy = entropyRule.Invoke(player),
            length = (int)lengthRule.Invoke(player),
            possibleColors = possibleBlockColors,
            possibleTypes = possibleBlockTypes
        };

    }
}
