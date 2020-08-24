﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Plants get data from this class
[CreateAssetMenu]
public class WaveData : ScriptableObject
{
    //Default movement rates to use for sun and wind animations
    public float sunRate;
    public float windRate;
    public float windSpeedRate;


    public GameObject plantAnimMarker0;
    public GameObject plantAnimMarker1;


    [HideInInspector]
    public Vector2 plantAnimMarkersVector;

    //This function will return the vector that the values of GetMovementOffset() will be relative to 
    public Vector2 GetPlantAnimMarkersVector()
    {
        Vector2 result;
        result = plantAnimMarker1.transform.position - plantAnimMarker0.transform.position;
        return result;
    }

    //This function is supposed to give you the offset of the plant, starting with 0 in the bottom left 
    //going to 2 * pi in the top right.
    //Based on transform of PlantAnimMarker gameobjects
    public float GetMovementOffset(Vector2 pos, float frequency = 1)
    {

        frequency = frequency * 2 * (float)Math.PI;
        float offsetX = frequency * (pos.x / plantAnimMarkersVector.x);
        float offsetY = frequency * (pos.y / plantAnimMarkersVector.y);
        Vector2 offsetVector = new Vector2(offsetX, offsetY);
        float offset = Vector2.Distance(offsetVector, Vector2.zero);
        return offset;

    }
    private void Awake()
    {
        plantAnimMarkersVector = GetPlantAnimMarkersVector();
    }
}
