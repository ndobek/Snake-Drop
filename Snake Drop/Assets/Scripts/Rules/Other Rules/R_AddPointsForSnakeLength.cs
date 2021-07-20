using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Add Points For Snake Length")]
public class R_AddPointsForSnakeLength : Rule
{
    public int ScoreIncreasePerBlockInPlaygrid;
    [SerializeField]
    private bool ApplyMultiplier;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player)
        {
            int OriginalSnakeLength = player.mostRecentSnakeLength;
            int CurrentSnakeLength = block != null? block.SnakeLength() : 0;
            int CurrentInPlaygrid = block != null ? block.SnakeLengthInPlayGrid() : 0;

            int TotalInPlaygrid = OriginalSnakeLength - (CurrentSnakeLength - CurrentInPlaygrid);
            int ScoreIncrease = TotalInPlaygrid * ScoreIncreasePerBlockInPlaygrid;

            if (ApplyMultiplier) player.Score.IncreaseScoreUsingMultiplier(ScoreIncrease);
            else player.Score.Score += ScoreIncrease;
        }
    }
}
