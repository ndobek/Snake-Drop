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
    [SerializeField]
    private bool OncePerCollection;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player)
        {
            int FillAmount = block.BlockCollection == null ? 1 : block.BlockCollection.FillAmount;
            int ScoreIncrease = FillAmount * ScoreIncreasePerFillAmount;
            int MIncrease = FillAmount * MultiplierIncreasePerFillAmount;

            if (OncePerCollection)
            {
                player.Score.IncreaseMultiplierPartially((float)MIncrease / (float)block.BlockCollection.Area());
                player.Score.IncreaseScorePartially( (float)ScoreIncrease /  (float)block.BlockCollection.Area(), ApplyMultiplier);
            }
            else
            {
                player.Score.Multiplier += MIncrease;
                player.Score.IncreaseScore(ScoreIncrease, ApplyMultiplier);
            }


        }
    }
}
