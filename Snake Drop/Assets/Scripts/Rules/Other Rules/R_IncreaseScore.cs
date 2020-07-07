using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Increase Score")]
public class R_IncreaseScore : Rule
{
    public int ScoreIncrease;
    [SerializeField]
    private bool ApplyMultiplier;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player)
        {
            if (ApplyMultiplier) player.Score.IncreaseScoreUsingMultiplier(ScoreIncrease);
            else player.Score.Score += ScoreIncrease;
        }
    }
}
