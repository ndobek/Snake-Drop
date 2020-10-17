using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLerper : MonoBehaviour
{
    public float speed;
    public Transform target;

    private void Update()
    {
        transform.Lerp(target, speed);
    }


}

public static class TransformExtention
{
    public static void Lerp(this Transform transform, Transform target, float speed)
    {
        transform.Lerp(target.position, speed);
    }

    public static void Lerp(this Transform transform, Vector3 target, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, target, speed);
    }
}
