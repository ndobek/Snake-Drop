using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerAnimationManager : EntranceAnimationManager
{
    public Sprite Open;
    public Sprite Closed;

    public BlockSlot SlotToCheck;

    public override void UpdateSprite()
    {
        if (SlotToCheck.Block != null)
        {
            spriteRenderer.sprite = Closed;
        }
        else
        {
            spriteRenderer.sprite = Open;
        }
    }
}
