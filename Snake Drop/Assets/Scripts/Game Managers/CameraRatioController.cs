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
    private bool roundAspectToPixelSize;

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
        Set(height / 2);
    }

    private void Set(float size)
    {
        float newSize = size;

        if (roundAspectToPixelSize)
        {
            float roundToPPU(float input) { return input - (input % (ControlledCamera.orthographicSize * 2 / PPU)); }

            Vector2 currentRes = new Vector2(Screen.width, Screen.height);
            Vector2 targetRes = new Vector2(roundToPPU(currentRes.x), roundToPPU(currentRes.y));
            //Vector2 targetRes = new Vector2(1920f, 1080f);

            Vector2 normalizedRes = targetRes / currentRes;
            Vector2 rectSize = normalizedRes / Mathf.Max(normalizedRes.x, normalizedRes.y);

            ControlledCamera.rect = new Rect(default, rectSize) { center = new Vector2(0.5f, 0.5f) };

            float sizeAdj = size % (targetRes.y / PPU / 2);
            if (sizeAdj < size) size -= sizeAdj;

        }

        //These are the relationships between Camera Size and in game Units
        //var height = 2*Camera.main.orthographicSize;
        //var width = height * Camera.main.aspect;

        //This is the ratio to keep to avoid pixel stretching
        //k* (Camera.main.orthographicSize * 2)*ppu = vertical resolution


        ControlledCamera.orthographicSize = newSize;
    }
}
