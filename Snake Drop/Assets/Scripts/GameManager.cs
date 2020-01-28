using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager[] playerManagers;
    public DifficultyManager difficultyManager;

    public bool GameInProgress()
    {
        foreach(PlayerManager player in playerManagers)
        {
            if (player.GameInProgress) return true;
        }
        return false;
    }

    
    public Rule BasicFall;
    public Block blockObj;
    public BlockType defaultType;
    public BlockType snakeHeadType;
    public BlockType snakeType;
    public BlockType collectionType;
    public BlockType collectionGhostType;

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
        foreach(PlayerManager player in playerManagers)
        {
            OnCrash(player);
        }
    }
    public void OnCrash(PlayerManager player = null)
    {
        player.OnCrash();
    }

    //public void MidRound()
    //{
    //    if (!RoundInProgress)
    //    {
    //        EndRound();
    //    }
    //    else
    //    {
    //        playGrid.Fall();
    //        FillPreviewBar();
    //        if(difficultyManager.HeightLimit) HeightLimitIndicator.LowerHeightLimit(playerController.SnakeHead.FindSnakeMaxY() + difficultyManager.HeightLimitModifier);
    //    }
    //}

    //private void EndRound()
    //{
    //    if (playerController.SnakeHead)
    //    {
    //        playerController.SnakeHead.KillSnake();
    //    }
    //    playGrid.Fall();

    //    if (waitSlot.GetNeighbor(Direction.DOWN).Block != null)
    //    {
    //        EndGame();
    //    }
    //    else
    //    {
    //        ContinueGame();
    //    }
    //}

    //private void ShufflePreviewBar()
    //{
    //    previewGrid.Fall(BasicFall);
    //    MakeSnake();
    //}
    //public void FillPreviewBar()
    //{
    //    ShufflePreviewBar();
    //    while (snakeMaker.Blocks.Count == 0 | waitSlot.Block == null)
    //    {
    //        ShufflePreviewBar();
    //    }
    //}

    //private void MakeSnake()
    //{
    //    difficultyManager.MakeSnake(difficultyManager.SnakeLength, difficultyManager.SnakeEntropy);
    //}

    public void StartGame()
    {
        gameOverScreen.SetActive(false);
        foreach (PlayerManager player in playerManagers)
        {
            player.ResetGame();
        }
        foreach (PlayerManager player in playerManagers)
        {
            StartNewRound(player);
        }
    }
    private void StartNewRound(PlayerManager player)
    {
        player.StartNewRound();
    }

    private void EndGame()
    {
        gameOverScreen.SetActive(true);
    }


    //public void ResetMoveRestrictions()
    //{
    //    HeightLimitIndicator.ResetHeightLimit();
    //    playerController.ResetGame();
    //}

}
