using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/SpawnPrefab")]
public class R_SpawnPrefab : Rule
{
    public GameObject prefab;
    protected override void Action(Block block, PlayerManager player = null)
    {
        GameObject spawned = GameObject.Instantiate(prefab, block.Slot.transform);
        INeedBlockInfo needBlockInfo = spawned.GetComponent<INeedBlockInfo>();
        if (needBlockInfo != null) needBlockInfo.SetBlock(block);
    }

}
