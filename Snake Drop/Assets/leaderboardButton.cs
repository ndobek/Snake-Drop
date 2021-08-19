using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;

public class leaderboardButton : MonoBehaviour
{
    public void showLeaderboard()
    {
        Cloud.Leaderboards.ShowOverlay();
        Debug.Log("Leaderboard!");
    }
}
