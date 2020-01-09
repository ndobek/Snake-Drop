using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;
    public DifficultyManager difficultyManager;
    [HideInInspector]
    public bool GameInProgress;
    [HideInInspector]
    public bool RoundInProgress;

    public PlayGrid playGrid;
    public PlayGrid previewGrid;
    public HeightLimitIndicator HeightLimitIndicator;
    public SnakeMaker snakeMaker;
    public BlockSlot waitSlot;

    public Block blockObj;
    public BlockType defaultType;
    public BlockType snakeHeadType;

    public GameObject gameOverScreen;

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public static Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return Direction.DOWN;
            case Direction.DOWN: return Direction.UP;
            case Direction.LEFT: return Direction.RIGHT;
            case Direction.RIGHT: return Direction.LEFT;
            default: throw new System.Exception("Big OOPsy Doopsy that is not a real direction dumbass");
        }
    }

    private void Awake()
    {
        if(!instance) instance = this;
        StartGame();
    }

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
            playGrid.Fall(false);
            BlockMelder.Meld(playGrid, snakeMaker.possibleColors);
            FillPreviewBar();
            //HeightLimitIndicator.LowerHeightLimit(playerController.SnakeHead.FindSnakeMaxY() + 2);
        }
    }

    private void EndRound()
    {
        if (playerController.SnakeHead)
        {
            playerController.SnakeHead.KillSnake();
        }
        playGrid.Fall(true);

        if (waitSlot.GetNeighbor(Direction.DOWN).Block != null)
        {
            EndGame();
        }
        else
        {
            ContinueGame();
        }
    }

    private void ShufflePreviewBar()
    {
        previewGrid.Fall(false);
        MakeSnake();
    }
    public void FillPreviewBar()
    {
        ShufflePreviewBar();
        while (snakeMaker.Blocks.Count == 0 | waitSlot.Block == null)
        {
            ShufflePreviewBar();
        }
    }

    private void MakeSnake()
    {
        snakeMaker.MakeSnake(25, .1f);
    }

    public void StartGame()
    {
        difficultyManager.ResetGame();
        playerController.ResetGame();
        gameOverScreen.SetActive(false);
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        GameInProgress = true;
        ContinueGame();
    }
    private void ContinueGame()
    {
        FillPreviewBar();
        ResetMoveRestrictions();
        playerController.SnakeHead = waitSlot.Block;
        playerController.SnakeHead.RawMove(Direction.DOWN);
        FillPreviewBar();
        RoundInProgress = true;
    }
    private void EndGame()
    {
        GameInProgress = false;
        gameOverScreen.SetActive(true);
    }


    public void ResetMoveRestrictions()
    {
        HeightLimitIndicator.ResetHeightLimit();
        playerController.ResetGame();
    }

}
