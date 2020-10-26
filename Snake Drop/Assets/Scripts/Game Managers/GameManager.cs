using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager[] playerManagers;
    public GameModeManager difficultyManager;
    public PlantManager plantManager;


    public bool GameInProgress()
    {
        foreach(PlayerManager player in playerManagers)
        {
            if (player.GameInProgress) return true;
        }
        return false;
    }

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
        UpdateScore();
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
        UpdateScore();
        gameOverScreen.SetActive(true);
    }

    private void UpdateScore()
    {
        foreach(PlayerManager player in playerManagers)
        {
            player.Score.UpdateScore();
        }
    }

    public SaveData GetSaveData()
    {
        SaveData result = new SaveData();

        result.score = playerManagers[0].Score.Score;
        result.planetData = plantManager.SavePlanet();

        return result;
    }

    public void SaveGame()
    {
        SaveManager.SaveGame();
    }
}
