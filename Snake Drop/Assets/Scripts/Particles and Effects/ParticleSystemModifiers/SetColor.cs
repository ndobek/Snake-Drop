using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour, INeedBlockInfo
{
    [SerializeField]
    private ParticleSystem ps;
    
    public void SetBlock(Block block)
    {
        var main = ps.main;
        main.startColor = block.blockColor.GetDeathParticleColor(); 
    }
}
