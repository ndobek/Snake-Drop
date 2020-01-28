using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    private Block snakeHead;
    public Block SnakeHead
    {
        get { return snakeHead; }
        set { ActivateSnake(value); }
    }


    [HideInInspector]
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            ScoreText.text = "Score: " + score.ToString();
        }
    }
    public Text ScoreText;

    public PlayerController playerController;

    public PlayGrid playGrid;
    public PlayGrid previewGrid;
    public HeightLimitIndicator HeightLimitIndicator;

    public BlockSlot startSlot;
    public BlockSlot waitSlot;

    [HideInInspector]
    public bool RoundInProgress;
    [HideInInspector]
    public bool GameInProgress;

    public void OnCrash()
    {
        RoundInProgress = false;
    }

    public void MidRound()
    {
        if (!RoundInProgress)
        {
            EndRound();
        }
        else
        {
            DifficultyManager difficulty = GameManager.instance.difficultyManager;
            playGrid.Fall();
            FillPreviewBar();
            if (difficulty.HeightLimit) HeightLimitIndicator.LowerHeightLimit(SnakeHead.FindSnakeMaxY() + difficulty.HeightLimitModifier);
        }
    }

    private void EndRound()
    {
        if (SnakeHead)
        {
            SnakeHead.KillSnake(this);
        }
        playGrid.Fall();

        if (waitSlot.GetNeighbor(GameManager.Direction.DOWN).Block != null)
        {
            EndGame();
        }
        else
        {
            StartNewRound();
        }
    }

    private void ShufflePreviewBar()
    {
        previewGrid.Fall(GameManager.instance.BasicFall);
        MakeSnake();
    }
    public void FillPreviewBar()
    {
        ShufflePreviewBar();
        while (startSlot.Blocks.Count == 0 | waitSlot.Block == null)
        {
            ShufflePreviewBar();
        }
    }

    public void MakeSnake()
    {
        SnakeMaker.MakeSnake(startSlot, GameManager.instance.difficultyManager.GetSnakeInfo(score));
    }

    public void ResetGame()
    {
        playerController.ResetGame();
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        GameInProgress = true;
    }
    public void StartNewRound()
    {
        FillPreviewBar();
        ResetMoveRestrictions();
        SnakeHead = waitSlot.Block;
        SnakeHead.RawMove(GameManager.Direction.DOWN);
        FillPreviewBar();
        RoundInProgress = true;
    }
    private void EndGame()
    {
        GameInProgress = false;
    }


    public void ResetMoveRestrictions()
    {
        HeightLimitIndicator.ResetHeightLimit();
        playerController.ResetGame();
    }

    private void ActivateSnake(Block newSnakeHead)
    {
        if (snakeHead) snakeHead.Kill(this); /*snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.defaultType);*/
        if (newSnakeHead)
        {
            snakeHead = newSnakeHead;
            snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.snakeHeadType);
            //snakeHead.ActivateSnake();
        }
        else
        {
            snakeHead = null;
        }
    }

    public void MoveSnake(GameManager.Direction direction)
    {

        if (GameInProgress)
        {
            SnakeHead.Move(direction, this);
            MidRound();
        }
    }
}
