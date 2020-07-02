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
    public MoveRule BasicMove;
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
    public static Direction GetClockwiseNeighborDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return Direction.RIGHT;
            case Direction.DOWN: return Direction.LEFT;
            case Direction.LEFT: return Direction.UP;
            case Direction.RIGHT: return Direction.DOWN;
            default: throw new System.Exception("Big OOPsy Doopsy that is not a real direction dumbass");
        }
    }
    public static Direction GetCounterClockwiseNeighborDirection(Direction direction)
    {
        return GetOppositeDirection(GetClockwiseNeighborDirection(direction));
    }

    private void Awake()
    {
        if(!instance) instance = this;
    }
    private void Start()
    {
        StartGame();
    }

    public void OnCrash()
    {
        foreach(PlayerManager player in playerManagers)
        {
            OnCrash(player);
        }
    }
    public void OnCrash(PlayerManager player)
    {
        player.OnCrash();
    }

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
        player.PrepareNewRound();
    }

    private bool GameIsOver()
    {
        foreach (PlayerManager player in playerManagers)
        {
            if (player.GameInProgress) return false;
        }
        return true;
    }

    public void CheckForGameOver()
    {
        if (GameIsOver())
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameOverScreen.SetActive(true);
    }

}
