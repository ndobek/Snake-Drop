using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Particles/Block Break Particle")]
public class R_BlockBreakParticleRule : Rule
{
    protected override void Action(Block block, PlayerManager player = null)
    {
        BlockCollection collection = block.BlockCollection;
        if (collection != null)
        {
            Vector3 pos = collection.WorldPosition();
            player.particleManager.TriggerBlockBreakParticles(block.blockColor.name, pos);

        }
        player.particleManager.TriggerBlockBreakParticles(block.blockColor.name, block.Slot.transform.position);

    }

}
