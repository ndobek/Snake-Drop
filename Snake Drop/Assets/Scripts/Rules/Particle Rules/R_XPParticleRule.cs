﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Particles/XP")]
public class R_XPParticleRule : Rule
{
    public float particleMultiplier = .5f;
    public int minParticles = 3;
    public int maxParticles = 5;
    public bool particlesReflectScore = false;
    public bool particlesOnBlockBreak = false;
    protected override void Action(Block block, PlayerManager player = null)
    {

        BlockCollection collection = block.BlockCollection;
        Color color = block.blockColor.GetDeathParticleColor();
         int numParticles()
        {
            if (particlesReflectScore)
            {
                return((int)(particleMultiplier * player.Score.Multiplier));
            }
            else
            {
                return((int)Random.Range(minParticles, maxParticles));
            }
        }
            if (collection != null)
            {
                Vector3 centre = collection.WorldPosition();
                player.particleManager.TriggerXPParticles(numParticles(), centre, color);
            
            }
            else if (particlesOnBlockBreak)
            {
            player.particleManager.TriggerXPParticles(numParticles(), block.Slot.transform.position, color);
            }

         
    }
}
