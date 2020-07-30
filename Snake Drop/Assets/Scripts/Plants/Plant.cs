using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public IGrowable growable;

    [HideInInspector]
    public int xp = 0;
    public int xpPerGrow = 5;

    private void Awake()
    {
        growable = gameObject.GetComponent<IGrowable>();
    }

    public bool ShouldGrow()
    {
        return xp >= xpPerGrow;
    }
    public void Grow()
    {
        xp = 0;
        growable.Grow();
    }
}
