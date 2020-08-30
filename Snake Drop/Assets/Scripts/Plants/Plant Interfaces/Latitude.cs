using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latitude : MonoBehaviour, ILatitudeDependent
{
    [HideInInspector]
    public LatitudeData lat;

    void Awake()
    {
        lat = GameObject.Find("LatitudeData").GetComponent<LatitudeData>();
    }
    public float ReturnLatitude()
    {
        if (lat != null)
        {
            float result = lat.GetLatitude(this.gameObject.transform.position);
            return result;
        }
        return 0;
    }
}
