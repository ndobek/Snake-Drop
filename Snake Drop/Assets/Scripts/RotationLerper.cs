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
        thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, target.rotation, lerpSpeed);
    }
}
