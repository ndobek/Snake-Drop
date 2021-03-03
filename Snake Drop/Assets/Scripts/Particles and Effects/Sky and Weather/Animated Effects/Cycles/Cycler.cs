using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycler : MonoBehaviour
{
    public List<ICyclical> cyclicalBehaviours = new List<ICyclical>();
    

    public void CycleThings()
    {
        if (cyclicalBehaviours != null)
        {
            foreach (ICyclical c in cyclicalBehaviours)
            {
                c.CycleUpdate();
            }
        }
    }

    private void Update()
    {
        CycleThings();
    }

}
