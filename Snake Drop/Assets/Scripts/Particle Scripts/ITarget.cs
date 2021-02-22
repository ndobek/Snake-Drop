using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    void LooseTarget(ParticleSystem pSystem, Transform target);
    void MagnetActivate(ParticleSystem pSystem, Transform target);

}
