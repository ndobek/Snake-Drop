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

    private List<Block> PreviewSnakes = new List<Block>();

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
    private void ShiftPreviewBar()
    {
        foreach(Block snake in PreviewSnakes)
        {
            snake.Move(Direction.LEFT);
        }
        MakeSnake();
    }

    public void FillPreviewBar()
    {
        MakeSnake();
        while (PreviewSnakes.Count > 0 && PreviewSnakes[0].Slot != waitSlot && PreviewSnakes[0].Neighbor(Direction.LEFT).Block == null)
        {
            ShiftPreviewBar();
        }
    }

    private void MakeSnake()
    {
        if (snakeMaker.CheckIsClear()) PreviewSnakes.Add(snakeMaker.MakeSnake(25, .1f, this));
    }

    public void StartGame()
    {
        difficultyManager.Score = 0;
        gameOverScreen.SetActive(false);
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        PreviewSnakes.Clear();
        MakeSnake();
        FillPreviewBar();
        playerController.ResetMoveRestrictions();
        GameInProgress = true;
        ContinueGame();
    }
    private void ContinueGame()
    {
        playerController.SnakeHead = waitSlot.Block;
        playerController.SnakeHead.ActivateSnake();
        playerController.SnakeHead.Move(Direction.DOWN);
        playerController.ResetMoveRestrictions();
        if (PreviewSnakes.Count > 0 && playerController.SnakeHead == PreviewSnakes[0]) PreviewSnakes.RemoveAt(0);
        FillPreviewBar();
    }
    private void EndGame()
    {
        GameInProgress = false;
        gameOverScreen.SetActive(true);
    }
}
