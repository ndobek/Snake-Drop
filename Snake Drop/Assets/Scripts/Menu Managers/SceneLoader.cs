using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private bool Additive;
    [SerializeField]
    private string GameObjectToMove;
    [SerializeField]
    private string sceneToUnload;
    private int waitingFramesCount;


    private void LateUpdate()
    {
        CheckDelay();
    } 
    private void waitForFrames(int i) { waitingFramesCount = i + 1; }
    private void DoDelayedTasks()
    {
        if (Additive)
        {
            MoveLoadedScene();

        }
    }
    private void CheckDelay()
    {
        if (waitingFramesCount > 0)
        {
            waitingFramesCount -= 1;
            if(waitingFramesCount == 0)
            {
                DoDelayedTasks();
            }
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, Additive?LoadSceneMode.Additive : LoadSceneMode.Single);
        waitForFrames(1);
        Invoke("UnloadScene", 1);
    }

    private void UnloadScene()
    {
        //GameObject.DontDestroyOnLoad(this);
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }

    private void MoveLoadedScene()
    {
        GameObject obj = GameObject.Find(GameObjectToMove);
        if (obj != null) obj.transform.position = transform.position;
    }
}
