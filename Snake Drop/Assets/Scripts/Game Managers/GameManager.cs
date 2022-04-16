using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager[] playerManagers;
    public GameModeManager GameModeManager;
    public PlantManager plantManager;
    public BoardRotator boardRotator;
    public PauseManager pauseManager;



    public SaveableObjects Colors;
    public SaveableObjects Types;
    public SaveableObjects Powerups;

    public bool GameInProgress()
    {
        foreach (PlayerManager player in playerManagers)
        {
            if (player.GameInProgress) return true;
        }
        return false;
    }

    public UIFade gameOverScreen;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void OnCrash()
    {
        foreach (PlayerManager player in playerManagers)
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
        gameOverScreen.Fade(false);
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
            if (!player.GameOver) return false;
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
    }

    private void UpdateScore()
    {
        foreach (PlayerManager player in playerManagers)
        {
            player.Score.UpdateScore();
        }
    }

    public void SaveGame()
    {
        SaveManager.SaveGame();
    }

    public void LoadGame(SaveData save)
    {
        save.LoadTo(this);
        boardRotator.RotateBoardToMatchEntrance();
    }

    private void Update()
    {
        gameOverScreen.Fade(GameIsOver());
    }
}
