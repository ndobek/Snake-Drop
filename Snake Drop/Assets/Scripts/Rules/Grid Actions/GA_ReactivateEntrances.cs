using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Grid Actions/ReactivateEntrances")]
public class GA_ReactivateEntrances : GridAction
{
    protected override void Action(PlayGrid grid)
    {
        GameManager.instance.playerManagers[0].entranceManager.ReactivateEntrances();
    }
}

