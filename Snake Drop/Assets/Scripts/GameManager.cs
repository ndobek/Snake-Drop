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

    public PlayGrid playGrid;
    public PlayGrid previewGrid;
    public SnakeMaker snakeMaker;
    public BlockSlot waitSlot;

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

    public void OnBlockDeath(Block obj)
    {
        if (obj == playerController.SnakeHead) OnSnakeDeath(obj);
    }
    private void OnSnakeDeath(Block obj)
    {
        playGrid.Fall();
        if (waitSlot.GetNeighbor(Direction.DOWN).Block != null)
        {
            EndGame();
        }
        else
        {
            playerController.ResetMoveRestrictions();
            ContinueGame();
        }
    }
    private void ShufflePreviewBar()
    {
        previewGrid.Fall();
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
        playerController.SnakeHead = waitSlot.Block;
        playerController.SnakeHead.ActivateSnake();
        playerController.SnakeHead.BasicMove(Direction.DOWN);
        playerController.ResetMoveRestrictions();
        FillPreviewBar();
    }
    private void EndGame()
    {
        GameInProgress = false;
        gameOverScreen.SetActive(true);
    }
}
