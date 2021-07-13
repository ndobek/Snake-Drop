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
    private GameObject continueButton;

    private GameManager gameManager;
    private PlayerManager playerManager;

    [HideInInspector]
    public bool paused;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        playerManager = gameManager.playerManagers[0];
    }

    void Start()
    {
        GameManager.instance.LoadGame(SaveManager.LoadAutoSave());
        Pause();
    }

    public void Pause(bool onOff)
    {
        if (onOff) Pause();
        else UnPause();
    }
    public void Pause()
    {
        playerManager.GameInProgress = false;
        continueButton.SetActive(SaveManager.SaveExists(SaveManager.AutoSaveSaveName));
        GameUIFader.FadeOut();
        MenuUIFader.FadeIn();
    }
    public void UnPause()
    {
        playerManager.GameInProgress = true;
        GameUIFader.FadeIn();
        MenuUIFader.FadeOut();
    }
    public void TogglePause()
    {
        Pause(!paused);
    }
}
