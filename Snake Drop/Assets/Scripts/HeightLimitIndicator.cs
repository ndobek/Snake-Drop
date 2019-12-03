using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightLimitIndicator : MonoBehaviour
{
    public float offset;
    void Update()
    {
        Vector2 obj = GameManager.instance.gameBoard.position(0, GameManager.instance.HeightLimit + offset);
        this.transform.position = new Vector2(0, obj.y);
    }
}
