using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager[] playerManagers;
    public DifficultyManager difficultyManager;
    public PlantManager plantManager;

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
        SaveScore();
        gameOverScreen.SetActive(true);
    }

    private void SaveScore()
    {
        foreach(PlayerManager player in playerManagers)
        {
            player.Score.SaveScore();
        }
    }
}
