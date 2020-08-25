﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DisplayDebugInfo : MonoBehaviour
{
    public Text waveDataDisplay;
    public Text plantDataDisplay;
    public WaveData waveData;
    float sun;
    float windGustFrequency;
    float windSpeed;
    public PlantManager pM;


    List<Plant> plantsList;

    // Update is called once per frame
    void Update()
    {
        StringBuilder plantString = new StringBuilder("Plant Stages: ");
        StringBuilder offsetString = new StringBuilder("Offset: ");
       //can be in void start// plantsList = pM.AllPlants();

        //foreach (Plant p in plantsList)
        //{
        //    plantString.Append(p.growable.GrowthStage);
        //   // don't do this because too much in 1 loop getcomponent is slow going to void start if I use it // offsetString.Append(p.gameObject.GetComponent<PlantAnimator>().offset);
        //}
        
        sun = MovementWave.AccessMovementWave(waveData.sunRate);
        windGustFrequency = MovementWave.AccessMovementWave(waveData.windRate);
        windSpeed = MovementWave.AccessMovementWave(waveData.windSpeedRate);
        plantDataDisplay.text = plantString + "\n" + offsetString;
        waveDataDisplay.text = "Sun: " + sun + "\nWind Gust Frequency: " + windGustFrequency + "\nWind Speed: " + windSpeed;
    }
}