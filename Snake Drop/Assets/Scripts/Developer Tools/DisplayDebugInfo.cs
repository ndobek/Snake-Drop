using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DisplayDebugInfo : MonoBehaviour

{
    ISunReactive debugSun;
    IWindReactive debugWind;
    DebugGrowth debugGrowth;
    void Awake()
    {
        debugSun = GameObject.Find("Sun Display").GetComponent<ISunReactive>();
        debugWind = GameObject.Find("Wind Display").GetComponent<IWindReactive>();
        debugGrowth = GameObject.Find("Growth Display").GetComponent<DebugGrowth>();
    }
    void DebugUpdate()
    {
        debugSun.UpdateSunReaction();
        debugWind.UpdateWindReaction();
        debugGrowth.UpdateGrowth();
    }

    private void Update()
    {
        DebugUpdate();
    }
    //public Text waveDataDisplay;
    //public Text plantDataDisplay;
    //public Sun sun;
    //public Wind wind;


    //public PlantManager pM;


    //List<Plant> plantsList;

    //// Update is called once per frame
    //void Update()
    //{
    //    StringBuilder plantString = new StringBuilder("Plant Stages: ");
    //    StringBuilder offsetString = new StringBuilder("Offset: ");
    //   //can be in void start// plantsList = pM.AllPlants();

    //    //foreach (Plant p in plantsList)
    //    //{
    //    //    plantString.Append(p.growable.GrowthStage);
    //    //   // don't do this because too much in 1 loop getcomponent is slow going to void start if I use it // offsetString.Append(p.gameObject.GetComponent<PlantAnimator>().offset);
    //    //}

    //    sun = MovementWave.AccessMovementWave(waveData.sunRate);
    //    windGustFrequency = MovementWave.AccessMovementWave(waveData.windRate);
    //    windSpeed = MovementWave.AccessMovementWave(waveData.windSpeedRate);
    //    plantDataDisplay.text = plantString + "\n" + offsetString;
    //    waveDataDisplay.text = "Sun: " + sun + "\nWind Gust Frequency: " + windGustFrequency + "\nWind Speed: " + windSpeed;
    //}
}
