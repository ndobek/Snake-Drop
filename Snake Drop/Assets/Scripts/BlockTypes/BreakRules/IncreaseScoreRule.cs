using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/BlockTypes/BreakRules/IncreaseScoreRule")]
public class IncreaseScoreRule : BreakRule
{
    public int ScoreIncrease;
    protected override void BreakAction(Block block)
    {
        GameManager.instance.difficultyManager.Score += ScoreIncrease;
    }
}
