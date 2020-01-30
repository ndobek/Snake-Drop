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

    [SerializeField]
    private ScoreManager scoreManager;
    public int Score
    {
        get { return scoreManager.Score; }
        set { scoreManager.Score = value; }
    }
    private int NumberOfSnakes;

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
            playGrid.Fall();
            FillPreviewBar();

            DifficultyManager difficulty = GameManager.instance.difficultyManager;
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
        if (startSlot.CheckIsClear())
        {
            NumberOfSnakes += 1;
            SnakeMaker.MakeSnake(startSlot, GameManager.instance.difficultyManager.GetSnakeInfo(Score, NumberOfSnakes));
        }
    }

    public void ResetGame()
    {
        Score = 0;
        NumberOfSnakes = 0;
        ResetMoveRestrictions();
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
        GameManager.instance.CheckForGameOver();
    }


    public void ResetMoveRestrictions()
    {
        HeightLimitIndicator.ResetHeightLimit();
        playerController.ResetGame();
    }

    private void ActivateSnake(Block newSnakeHead)
    {
        if (snakeHead) snakeHead.Kill(this);
        if (newSnakeHead)
        {
            snakeHead = newSnakeHead;
            snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.snakeHeadType);
            snakeHead.SetOwner(this);
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
