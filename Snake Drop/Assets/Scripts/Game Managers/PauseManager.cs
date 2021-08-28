using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private UIFade GameUIFader;
    [SerializeField]
    private UIFade MenuUIFader;

    [SerializeField]
    private GameObject continueButtonFader;
    [SerializeField]
    private GameManager gameManager;
    private PlayerManager playerManager;

    [HideInInspector]
    public bool paused;

    void Awake()
    {
        GetGameManager();
    }

    private void GetGameManager()
    {
        if (gameManager == null) gameManager = GetComponent<GameManager>();
        if (playerManager == null) playerManager = gameManager.playerManagers[0];
    }

    void Start()
    {
        SaveData autoSave = SaveManager.LoadAutoSave();    
        if(autoSave != null) GameManager.instance.LoadGame(autoSave);
        Pause();
    }

    public void Pause(bool onOff)
    {
        if (onOff) Pause();
        else UnPause();
    }
    public void Pause()
    {
        GetGameManager();
        playerManager.GameInProgress = false;
        continueButtonFader.SetActive(SaveManager.SaveExists(SaveManager.AutoSaveSaveName));
        GameUIFader.FadeOut();
        MenuUIFader.FadeIn();
        paused = true;
    }
    public void UnPause()
    {
        GetGameManager();
        playerManager.GameInProgress = true;
        GameUIFader.FadeIn();
        MenuUIFader.FadeOut();;
        paused = false;
    }

    public void TogglePause()
    {
        Pause(!paused);
    }
}
