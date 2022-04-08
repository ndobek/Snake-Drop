using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAtUIElement : MonoBehaviour
{
    public RectTransform UITransform;

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(UITransform.transform.position);
    }
}
