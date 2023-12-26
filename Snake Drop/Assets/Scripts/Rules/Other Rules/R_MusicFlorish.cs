using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/General/Music Florish")]
public class R_MusicFlorish : Rule
{
    protected override void Action(Block block, PlayerManager player = null)
    {
        GameObject.FindFirstObjectByType<MusicManager>().Florish(block);
    }

}
