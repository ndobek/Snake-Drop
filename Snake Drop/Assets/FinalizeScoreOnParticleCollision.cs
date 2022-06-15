using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalizeScoreOnParticleCollision : MonoBehaviour
{
    public PowerupsDisplayer displayer;
    public void OnParticleCollision(GameObject other)
    {
        displayer.FinalizeScore(other);
    }
}
