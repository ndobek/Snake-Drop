using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    //unneeded?
    private Material skyMaterial;

    private void Start()
    {
        skyMaterial = GetComponent<SpriteRenderer>().material;
    }

    public float Saturation
    {

        get
        {
            return skyMaterial.GetFloat("_Saturation");
        }
        set
        {
            skyMaterial.SetFloat("_Saturation", value);
        }

    }
    public float Hue
    {

        get
        {
            return skyMaterial.GetFloat("_Hue");
        }
        set
        {
            skyMaterial.SetFloat("_Hue", value);
        }

    }
    public float Contrast
    {

        get
        {
            return skyMaterial.GetFloat("_Contrast");
        }
        set
        {
            skyMaterial.SetFloat("_Contrast", value);
        }

    }
}
