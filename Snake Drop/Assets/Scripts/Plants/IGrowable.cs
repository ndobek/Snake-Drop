using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrowable
{
    void Grow();
    int GrowthStage { get; set; }
    void ResetGrowth();
    void UpdateGrowable();
    bool ShouldGrow();
    void AddXP(int XP);
}
