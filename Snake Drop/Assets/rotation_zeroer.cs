using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_zeroer : MonoBehaviour
{
    private BoardRotator boardRotator;
    private BlockSlot slot;
    private void Start() {
        boardRotator = GameManager.instance.boardRotator;
        slot = GetComponent<BlockSlot>();
    }
    void Update()
    {
        if (slot.Block.blockType != GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType)
        {
            transform.rotation = boardRotator.transform.rotation;
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }

    }
}
