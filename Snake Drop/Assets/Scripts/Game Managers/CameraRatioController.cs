using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRatioController : MonoBehaviour
{
    //These are the relationships between Camera Size and in game Units
    //var height = 2*Camera.main.orthographicSize;
    //var width = height * Camera.main.aspect;

    [HideInInspector]
    private Camera ControlledCamera;
    [SerializeField]
    private Transform CameraTransform;
    [SerializeField]
    private float targetWidth;
    [SerializeField]
    private float targetHeight;
    [SerializeField]
    private bool portraitUseHeight;
    [SerializeField]
    private bool landscapeUseHeight;
    [SerializeField]
    private bool roundPosToPixelSize;
    [SerializeField]
    private float PPU;

    private void Awake()
    {
        if (ControlledCamera == null) ControlledCamera = GetComponent<Camera>();
        if (CameraTransform == null) CameraTransform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (ControlledCamera.aspect < 1)
        {
            SetPortrait();
        }
        else
        {
            SetLandscape();
        }
        if(roundPosToPixelSize) PositionCamera();
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
        Vector3 pos = CameraTransform.position;
        float pixelSize = 1 / PPU;

        float xAdj = pos.x % pixelSize;
        float yAdj = pos.y % pixelSize;
        float zAdj = pos.z % pixelSize;

        CameraTransform.position = new Vector3(pos.x - xAdj, pos.y - yAdj, pos.z - zAdj);
    }


}
