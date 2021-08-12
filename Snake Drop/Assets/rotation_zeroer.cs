using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_zeroer : MonoBehaviour
{
    public Vector3 targetRotation = Vector3.zero;
    void Update()
    {
        transform.rotation = Quaternion.Euler(targetRotation);
    }
}
