using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanavasCamaraToMain : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }
}
