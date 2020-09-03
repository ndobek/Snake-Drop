using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LatitudeData: MonoBehaviour
{
    
    public GameObject plantAnimMarker0;
    public GameObject plantAnimMarker1;
    [HideInInspector]
    public Vector2 plantAnimMarkersVector;

    //This function will return the vector that the values of GetLatitude() will be relative to 
    public Vector2 GetPlantAnimMarkersVector()
    {
        Vector2 result;
        result = plantAnimMarker1.transform.position - plantAnimMarker0.transform.position;
        Debug.Log(result);
        return result;
    }
    //This function is supposed to give you the offset of the plant, starting with 0 in the bottom left 
    //going to 2 * pi in the top right.
    //Based on transform of PlantAnimMarker gameobjects
    public float GetLatitude(Vector2 pos, float frequency = 1)
    {

        frequency = frequency * 2 * (float)Math.PI;
        float offsetX = frequency * (pos.x / plantAnimMarkersVector.x);
        float offsetY = frequency * (pos.y / plantAnimMarkersVector.y);
        Vector2 offsetVector = new Vector2(offsetX, offsetY);
        float latitude = Vector2.Distance(offsetVector, Vector2.zero);
        return latitude;

    }
    void Awake()
    {      
        plantAnimMarkersVector = GetPlantAnimMarkersVector();
    }

}
