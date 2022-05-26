using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ArrowHandler : MonoBehaviour
{
    public float blockedDelayTime;
    private float timeBlocked;
    public float inputDelayTime;
    private float timeWithNoInput;

    public Color OpenColor;
    public Color BlockedColor;

    private bool showBlockedSprites;
    private bool showNoInputSprites;
    private PlayerManager player;

    public GameObject left;
    private SpriteRenderer leftSprite;
    public GameObject right;
    private SpriteRenderer rightSprite;
    public GameObject up;
    private SpriteRenderer upSprite;
    public GameObject down;
    private SpriteRenderer downSprite;


    private void Start()
    {
        if (player == null) player = GameManager.instance.playerManagers[0];
        leftSprite = left.GetComponent<SpriteRenderer>();
        rightSprite = right.GetComponent<SpriteRenderer>();
        upSprite = up.GetComponent<SpriteRenderer>();
        downSprite = down.GetComponent<SpriteRenderer>();

        timeBlocked = 0;
        timeWithNoInput = 0;
    }
    private void Update()
    {
        TimeStep();
        Block snakeHead = player.SnakeHead;
        transform.position = snakeHead.transform.position;
        if (player.RoundInProgress)
        {
            if (leftSprite && snakeHead.CanMoveToWithoutCrashing(snakeHead.Neighbor(Directions.Direction.LEFT), player)) leftSprite.color = OpenColor; else leftSprite.color = BlockedColor;
            if (rightSprite && snakeHead.CanMoveToWithoutCrashing(snakeHead.Neighbor(Directions.Direction.RIGHT), player)) rightSprite.color = OpenColor; else rightSprite.color = BlockedColor;
            if (upSprite && snakeHead.CanMoveToWithoutCrashing(snakeHead.Neighbor(Directions.Direction.UP), player)) upSprite.color = OpenColor; else upSprite.color = BlockedColor;
            if (downSprite && snakeHead.CanMoveToWithoutCrashing(snakeHead.Neighbor(Directions.Direction.DOWN), player)) downSprite.color = OpenColor; else downSprite.color = BlockedColor;
        }
        else
        {
            leftSprite.color = OpenColor;
            rightSprite.color = OpenColor;
            upSprite.color = OpenColor;
            downSprite.color = OpenColor;
        }

        if (showBlockedSprites)
        {
            left.SetActive(true);
            right.SetActive(true);
            up.SetActive(false);
            down.SetActive(false);
        } 
        
        else if (showNoInputSprites)
        {
            if (player.RoundInProgress)
            {
                left.SetActive(!snakeHead.blockType.OverrideMove(snakeHead, snakeHead.Neighbor(Directions.Direction.LEFT), player));
                right.SetActive(!snakeHead.blockType.OverrideMove(snakeHead, snakeHead.Neighbor(Directions.Direction.RIGHT), player));
                up.SetActive(!snakeHead.blockType.OverrideMove(snakeHead, snakeHead.Neighbor(Directions.Direction.UP), player));
                down.SetActive(!snakeHead.blockType.OverrideMove(snakeHead, snakeHead.Neighbor(Directions.Direction.DOWN), player));
                
            }
            else
            {
                Directions.Direction dir = Directions.TranslateDirection(Directions.Direction.UP, player.boardRotator.currentDirection);
                left.SetActive(dir != Directions.Direction.LEFT);
                right.SetActive(dir != Directions.Direction.RIGHT);
                up.SetActive(dir != Directions.Direction.UP);
                down.SetActive(dir != Directions.Direction.DOWN);
            }
        }

        else
        {
            left.SetActive(false);
            right.SetActive(false);
            up.SetActive(false);
            down.SetActive(false);
        }


    }

    private void TimeStep()
    {
        if (player.GameInProgress)
        {
            if (!player.RoundInProgress && !player.enterSlot.CheckIfEntranceValid(player, player.playGrid))
            {
                timeBlocked += Time.deltaTime;
                if (timeBlocked > blockedDelayTime)
                {
                    showBlockedSprites = true;
                }
            }
            else
            {
                timeBlocked = 0;
                showBlockedSprites = false;
            }

            timeWithNoInput += Time.deltaTime;
            showNoInputSprites = timeWithNoInput > inputDelayTime;
        }

    }

    public void OnInputGiven()
    {
        timeWithNoInput = 0;
    }
}
