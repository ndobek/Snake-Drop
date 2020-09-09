using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public string gameSceneName;
    public void LoadNewGame()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
