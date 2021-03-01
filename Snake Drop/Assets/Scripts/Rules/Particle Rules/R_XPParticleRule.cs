using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Particles/XP")]
public class R_XPParticleRule : Rule
{
    
    protected override void Action(Block block, PlayerManager player = null)
    {

        BlockCollection collection = block.BlockCollection;
        int numParticles = player.Score.Multiplier;
        if (collection != null)
        {
            Vector3 centre = collection.WorldPosition();
            player.particleManager.TriggerXPParticles(numParticles, centre);           
        }

        player.particleManager.TriggerXPParticles(numParticles, block.transform.position);
     
    }
}
