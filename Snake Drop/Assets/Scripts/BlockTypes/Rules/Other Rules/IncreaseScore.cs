using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/Rules/IncreaseScore")]
public class IncreaseScore : Rule
{
    public int ScoreIncrease;
    protected override void Action(Block block)
    {
        GameManager.instance.difficultyManager.Score += ScoreIncrease;
    }
}
