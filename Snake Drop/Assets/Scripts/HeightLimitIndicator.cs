using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightLimitIndicator : MonoBehaviour
{
    public float offset;

    [HideInInspector]
    public int HeightLimit;

    public void LowerHeightLimit(int temp)
    {
        if (temp < HeightLimit) HeightLimit = temp;
    }

    public void ResetHeightLimit()
    {
        HeightLimit = GameManager.instance.playGrid.YSize + 1;
    }
    void Update()
    {
        Vector2 obj = GameManager.instance.playGrid.position(0, HeightLimit + offset);
        this.transform.position = new Vector2(0, obj.y);
    }
}
