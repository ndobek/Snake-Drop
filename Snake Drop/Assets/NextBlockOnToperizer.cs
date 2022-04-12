using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBlockOnToperizer : MonoBehaviour
{
    [HideInInspector]
    public BlockSlot thisSlot;
    private void Awake()
    {
        thisSlot = GetComponent<BlockSlot>();
    }

    private void Update()
    {

        for(int i = 0; i < thisSlot.Blocks.Count; i++)
        {
            thisSlot.Blocks[i].BlockSprite.sortingOrder = thisSlot.Blocks.Count - i;
        }
    }
}
