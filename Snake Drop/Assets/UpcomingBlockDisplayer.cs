using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpcomingBlockDisplayer : MonoBehaviour
{
    public List<Image> blockDisplays;
    public List<Image> headDisplays;
    public List<TMP_Text> qtyDisplays;

    public void Display()
    {
        PlayerManager p = GameManager.instance.playerManagers[0];

        List<Block> snakeHeads = p.GetAllSpawnedSnakeHeads();
        int currentHead = 0;
        Block current= null;
        List<BlockColor> upcomingColors = null;
        int startingNumberOfSnakes = p.Score.numberOfSnakes;
        Random.state = p.randStateForSnake;

        for (int i = 0; i < blockDisplays.Count; i++)
        {
            bool isHead = false;
            if (current == null && currentHead < snakeHeads.Count)
            {
                isHead = true;
                current = snakeHeads[currentHead++];
            }
            if (current != null)
            {
                SetDisplay(i, current.blockColor, isHead);
                Block comparision = current;
                int qty = 0;
                while (current != null && comparision.blockColor == current.blockColor)
                {
                    current = current.Tail;
                    ++qty;
                }
                qtyDisplays[i].text = qty.ToString();
            }

            else
            {
                if (upcomingColors == null || upcomingColors.Count == 0)
                {
                    p.Score.NumberOfSnakes += 1;
                    SnakeInfo info = GameManager.instance.GameModeManager.GetSnakeInfo();
                    upcomingColors = SnakeMaker.GetSnakeDetails(info);
                    isHead = true;
                }

                BlockColor color = upcomingColors[0];
                int qty = 0;
                while (upcomingColors.Count > 0 && upcomingColors[0] == color)
                {
                    ++qty;
                    upcomingColors.RemoveAt(0);
                }

                SetDisplay(i, color, isHead);
                qtyDisplays[i].text = qty.ToString();

            }
        }

        p.Score.numberOfSnakes = startingNumberOfSnakes;
    }

    public void SetDisplay(int i, BlockColor color, bool isHead)
    {
        if (isHead)
        {
            DirectionalSpriteController head = (DirectionalSpriteController)(color.GetAnimator(GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType));
            headDisplays[i].sprite = head.DownSprite;

            headDisplays[i].gameObject.SetActive(true);
            blockDisplays[i].gameObject.SetActive(false);
        }
        else
        {
            BasicSpriteAnimator block = (BasicSpriteAnimator)(color.GetAnimator(GameManager.instance.GameModeManager.GameMode.TypeBank.basicType));
            blockDisplays[i].sprite = block.sprite;

            headDisplays[i].gameObject.SetActive(false);
            blockDisplays[i].gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        Display();
    }
}
