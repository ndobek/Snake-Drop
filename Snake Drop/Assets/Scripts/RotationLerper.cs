using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLerper : MonoBehaviour
{
    public Transform target;
    private Transform thisTransform;
    [SerializeField]
    private float lerpSpeed;
    private void Awake()
    {
        if (thisTransform == null) thisTransform = GetComponent<Transform>();
    }
    void Update()
    {
        Vector3 currentEuler = thisTransform.rotation.eulerAngles;
        Vector3 targetEuler = thisTransform.rotation.eulerAngles;

        if (targetEuler.z < currentEuler.z) targetEuler.z += 360;

        thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, Quaternion.Euler(targetEuler), lerpSpeed);
    }
}
