using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Increase Score")]
public class R_IncreaseScore : Rule
{
    public int ScoreIncrease;
    [SerializeField]
    private bool ApplyMultiplier;
    [SerializeField]
    private bool WaitForParticles;
    protected override void Action(Block block, PlayerManager player = null)
    {
        if (player)
        {
            GameObject refObj = null;
            if (WaitForParticles) refObj = player.particleManager.PeekNextXPSystem().gameObject;

            player.Score.IncreaseScore(ScoreIncrease, ApplyMultiplier, refObj);
        }
    }
}
