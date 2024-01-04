using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpcomingBlock : Block
{
    [HideInInspector]
    public int quantity;
    public Image blockDisplay;
    public Image headDisplay;
    public TMP_Text quantityDisplay;

    public void Display(BlockColor color, int quantity, bool isHead)
    {
        if (isHead)
        {
            DirectionalSpriteController head = (DirectionalSpriteController)(color.GetAnimator(GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType));
            headDisplay.sprite = head.DownSprite;

            headDisplay.gameObject.SetActive(true);
            blockDisplay.gameObject.SetActive(false);
        }
        else
        {
            BasicSpriteAnimator block = (BasicSpriteAnimator)(color.GetAnimator(GameManager.instance.GameModeManager.GameMode.TypeBank.basicType));
            blockDisplay.sprite = block.sprite;

            headDisplay.gameObject.SetActive(false);
            blockDisplay.gameObject.SetActive(true);
        }
    }

    public void ReduceQuantity(int qty = 1)
    {
        quantity -= qty;
        CheckBreak();
    }

    public void CheckBreak()
    {
        if (quantity <= 0)
        {
            PlayerManager p = GameManager.instance.playerManagers[0];
            Color color = blockColor.GetDeathParticleColor();
            p.particleManager.TriggerBlockBreakParticles(color, Slot.transform.position);
            RawBreak();
        }
    }
}
