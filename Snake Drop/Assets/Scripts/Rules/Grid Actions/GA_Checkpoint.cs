using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/Checkpoint")]
public class GA_Checkpoint : GridAction
{

    protected override void Action(PlayGrid grid)
    {
        CheckpointManager cm = GameManager.instance.playerManagers[0].Score.checkpointManager;
        if (cm) cm.TryCheckpoint();
    }
}
