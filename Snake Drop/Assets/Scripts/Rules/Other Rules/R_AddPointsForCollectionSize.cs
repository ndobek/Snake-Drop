using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Add Points For CollectionSize")]
public class R_AddPointsForCollectionSize : Rule
{
    public int ScoreIncreasePerBlockInCollection;
    [SerializeField]
    private bool ApplyMultiplier;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player)
        {
            int CollectionSize = block.BlockCollection == null ? 1 : block.BlockCollection.Area();
            int ScoreIncrease = CollectionSize * ScoreIncreasePerBlockInCollection;

            if (ApplyMultiplier) player.Score.IncreaseScoreUsingMultiplier(ScoreIncrease);
            else player.Score.Score += ScoreIncrease;
        }
    }
}
