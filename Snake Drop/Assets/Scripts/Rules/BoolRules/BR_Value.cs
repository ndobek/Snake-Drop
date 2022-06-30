using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/BoolRules/Value")]
public class BR_Value : BoolRule
{
    public bool value;
    public override bool Invoke(PlayerManager player)
    {
        return value;
    }
}
