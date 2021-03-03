using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkyState
{
    [SerializeField]
    private float saturation;
    public float Saturation
    {
        get => saturation;
        set => saturation = value;
    }
    [SerializeField]
    private float hue;
    public float Hue
    {
        get => hue;
        set => hue = value;
    }
    [SerializeField]
    private float contrast;
    public float Contrast
    {
        get => contrast;
        set => contrast = value;

    }
    [SerializeField]
    private float overwrite;
    public float Overwrite
    {
        get => overwrite;
        set => overwrite = value;
    }
    [SerializeField]
    private float overlay;
    public float Overlay
    {
        get => overlay;
        set => overlay = value;
    }
    [SerializeField]
    private float burn;
    public float Burn
    {
        get => burn;
        set => burn = value;
    }

    [SerializeField]
    private float dodge;
    public float Dodge
    {
        get => dodge;
        set => dodge = value;
    }
    [SerializeField]
    private float screen;
    public float Screen
    {
        get => screen;
        set => screen = value;
    }
    [SerializeField]
    private float multiply;
    public float Multiply
    {
        get => multiply;
        set => multiply = value;
    }
    [SerializeField]
    private float darken;
    public float Darken
    {
        get => darken;
        set => darken = value;
    }
}
