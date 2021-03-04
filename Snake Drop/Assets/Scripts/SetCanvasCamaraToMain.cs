using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasCamaraToMain : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
}
