using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Add Points For CollectionFill")]
public class R_AddPointsForCollectionFill : Rule
{
    public int ScoreIncreasePerFillAmount;
    [SerializeField]
    public int MultiplierIncreasePerFillAmount;
    [SerializeField]
    private bool ApplyMultiplier;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player)
        {
            int FillAmount = block.BlockCollection == null ? 1 : block.BlockCollection.FillAmount;
            int ScoreIncrease = FillAmount * ScoreIncreasePerFillAmount;
            int MIncrease = FillAmount * MultiplierIncreasePerFillAmount;

            player.Score.Multiplier += MIncrease;
            if (ApplyMultiplier) player.Score.IncreaseScoreUsingMultiplier(ScoreIncrease);
            else player.Score.Score += ScoreIncrease;
        }
    }
}
