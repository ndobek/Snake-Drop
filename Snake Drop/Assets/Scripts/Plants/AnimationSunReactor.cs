using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSunReactor : MonoBehaviour, ISunReactive
{
    public GameObject Sun;

    public void Awake()
    {
        Sun = GameObject.Find("Sun");
    }

    public void UpdateSun()
    {
        Vector3 targetDir = Sun.transform.position - gameObject.transform.position;

        gameObject.transform.rotation = Quaternion.LookRotation(transform.forward, targetDir);
    }
}
