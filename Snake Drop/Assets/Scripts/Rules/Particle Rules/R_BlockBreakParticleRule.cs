using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Particles/Block Break Particle")]
public class R_BlockBreakParticleRule : Rule
{
    public bool particlesOnCollectionBreak = false;

    protected override void Action(Block block, PlayerManager player = null)
    {
        BlockCollection collection = block.BlockCollection;
        Color color = block.blockColor.GetDeathParticleColor();
        if (collection != null && particlesOnCollectionBreak)
        {
            Vector3 pos = collection.WorldPosition();
            player.particleManager.TriggerBlockBreakParticles(color, pos);

        }
        player.particleManager.TriggerBlockBreakParticles(color, block.Slot.transform.position);


    }

}
