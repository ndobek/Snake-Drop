using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLerper : MonoBehaviour, IReact
{
    public BoardRotator target;
    [SerializeField]
    private AnimationCurve rotationCurve;
    [SerializeField]
    private float rotationDuration = 1;

    public void Awake()
    {
        target.reactToSpin.Add(this);
    }

    private IEnumerator RotationRoutine()
    {
        float startTime = Time.time;
        float percentageComplete = (Time.time - startTime) / rotationDuration;

        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, target.targetRotation);

        while (true)
        {
            percentageComplete = (Time.time - startTime) / rotationDuration;
            transform.rotation = Quaternion.LerpUnclamped(startRot, targetRot, rotationCurve.Evaluate(percentageComplete));

            yield return new WaitForEndOfFrame();
            if (percentageComplete >= 1) break;
        }

        transform.rotation = targetRot;
    }

    public void React()
    {
        StartCoroutine(RotationRoutine());
    }
}
