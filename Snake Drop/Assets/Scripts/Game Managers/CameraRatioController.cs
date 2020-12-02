using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRatioController : MonoBehaviour
{
    //These are the relationships between Camera Size and in game Units
    //var height = 2*Camera.main.orthographicSize;
    //var width = height * Camera.main.aspect;
    public Vector3 targetPosition;

    [HideInInspector]
    private Camera ControlledCamera;
    private Transform CameraTransform;
    [SerializeField]
    private float targetWidth;
    [SerializeField]
    private float targetHeight;
    [SerializeField]
    private bool portraitUseHeight;
    [SerializeField]
    private bool landscapeUseHeight;

    private void Awake()
    {
        ControlledCamera = GetComponent<Camera>();
        CameraTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (ControlledCamera.aspect < 1)
        {
            SetPortrait();
        }
        else
        {
            SetLandscape();
        }
        PositionCamera();
    }

    private void SetPortrait()
    {
        if (portraitUseHeight) SetByHeight(targetHeight);
        else SetByWidth(targetWidth);
    }

    private void SetLandscape()
    {
        if (landscapeUseHeight) SetByHeight(targetHeight);
        else SetByWidth(targetWidth);
    }

    private void SetByWidth(float width)
    {
        ControlledCamera.orthographicSize = width / ControlledCamera.aspect / 2;
    }

    private void SetByHeight(float height)
    {
        ControlledCamera.orthographicSize = height / 2;
    }

    private void PositionCamera()
    {
        Debug.Log(ControlledCamera.WorldToScreenPoint(targetPosition));
        //Vector3 adjPosition = targetPosition;

        //CameraTransform.position = adjPosition;
    }


}
