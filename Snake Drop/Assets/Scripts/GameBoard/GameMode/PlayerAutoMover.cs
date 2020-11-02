using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMover : MonoBehaviour
{
    public PlayerController playerController;
    public float TimePerMove;
    private float currentTime = 0;

    private void Update()
    {
        if (TimePerMove != 0)
        {
            currentTime += Time.deltaTime;

            if (currentTime > TimePerMove)
            {
                currentTime -= TimePerMove;
                MovePlayer();
            }
        }

    }

    private void MovePlayer()
    {
        playerController.MoveSnake(playerController.mostRecentDirectionMoved);
    }
}
