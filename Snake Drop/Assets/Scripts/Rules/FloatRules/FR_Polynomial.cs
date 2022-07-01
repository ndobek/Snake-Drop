using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/FloatRules/Polynomial")]
public class FR_Polynomial : FloatRule
{
    [System.Serializable]
    public class Term 
    {
        public FloatRule[] floats;
        public float Multiply(PlayerManager player)
        {
            float result = floats[0].Invoke(player);

            for (int i = 1; i < floats.Length; i++)
            {
                result *= floats[i].Invoke(player);
            }
            return result;
        }

    }
    public Term[] terms;

    protected override float Action(PlayerManager player)
    {
        float result = 0;

        foreach (var term in terms)
        {
            result += term.Multiply(player);
        }
        return result;
    }
}
