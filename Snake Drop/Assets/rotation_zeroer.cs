using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_zeroer : MonoBehaviour
{
    private BoardRotator boardRotator;

    private void Start() {
        boardRotator = GameManager.instance.boardRotator;
    }
    void Update()
    {
        transform.rotation = boardRotator.transform.rotation;
    }
}
