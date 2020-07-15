using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Activate Entrance Slot")]
public class R_ActivateEntranceSlot : Rule
{
    public bool active;
    protected override void Action(Block block, PlayerManager player = null)
    {
        GameManager.instance.playerManagers[0].enterSlot.Active = active;
    }
}
