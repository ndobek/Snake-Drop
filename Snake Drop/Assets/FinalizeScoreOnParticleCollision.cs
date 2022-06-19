using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalizeScoreOnParticleCollision : MonoBehaviour
{
    private ScoreManager scoreManager;
    public void Start()
    {
        scoreManager = GameManager.instance.playerManagers[0].Score;
    }
    public void OnParticleCollision(GameObject other)
    {
        scoreManager.FinalizeScore(other);
    }
}
