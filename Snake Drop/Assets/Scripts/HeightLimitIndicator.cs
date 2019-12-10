﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightLimitIndicator : MonoBehaviour
{
    public float offset;
    void Update()
    {
        Vector2 obj = GameManager.instance.playGrid.position(0, GameManager.instance.playerController.HeightLimit + offset);
        this.transform.position = new Vector2(0, obj.y);
    }
}
