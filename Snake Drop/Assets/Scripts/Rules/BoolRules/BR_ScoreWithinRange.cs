using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/BoolRules/ScoreWithinRange")]
public class BR_ScoreWithinRange : BoolRule
{
    public int scoreMin;
    public int scoreMax;

    public bool useScoreMin;
    public bool useScoreMax;

    public int snakeNumberMin;
    public int snakeNumberMax;

    public bool useSnakeNumberMin;
    public bool useSnakeNumberMax;

    public override bool Invoke(PlayerManager player)
    {
        int s = player.Score.Score;
        int sn = player.Score.NumberOfSnakes;

        if (useScoreMin && s < scoreMin) return false;
        if (useScoreMax && s > scoreMax) return false;

        if (useSnakeNumberMin && sn < snakeNumberMin) return false;
        if (useSnakeNumberMax && sn > snakeNumberMax) return false;

        return true;
    }
}
