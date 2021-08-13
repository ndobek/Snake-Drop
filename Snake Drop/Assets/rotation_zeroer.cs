using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_zeroer : MonoBehaviour
{
    private BoardRotator boardRotator;
    private void Awake() {
        boardRotator = GameManager.instance.boardRotator;
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0,0,boardRotator.targetRotation));
    }
}
