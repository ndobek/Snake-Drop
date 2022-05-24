using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ArrowHandler : MonoBehaviour
{
    public float delayTime;
    private float timeBlocked;
    private PlayerManager player;
    private Animator ArrowAnimator;

    private void Start()
    {
        if (player == null) player = GameManager.instance.playerManagers[0];
        if (ArrowAnimator == null) ArrowAnimator = GetComponent<Animator>();
        timeBlocked = 0;
    }
    private void Update()
    {
        if (!player.RoundInProgress && !player.enterSlot.CheckIfEntranceValid(player, player.playGrid))
        {
            timeBlocked += Time.deltaTime;
            if(timeBlocked > delayTime)
            {
                ArrowAnimator.SetBool("Bounce", true);
            }
        }
        else
        {
            timeBlocked = 0;
            ArrowAnimator.SetBool("Bounce", false);
        }

    }
}
