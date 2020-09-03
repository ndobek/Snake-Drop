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
    //DebugLatitude debugLatitude;
    void Awake()
    {
        debugSun = GameObject.Find("Sun Display").GetComponent<ISunReactive>();
        debugWind = GameObject.Find("Wind Display").GetComponent<IWindReactive>();
        debugGrowth = GameObject.Find("Growth Display").GetComponent<DebugGrowth>();
        //debugLatitude = GameObject.Find("Latitude Display").GetComponent<DebugLatitude>();
    }
    void DebugUpdate()
    {
        debugSun.UpdateSunReaction();
        debugWind.UpdateWindReaction();
        debugGrowth.UpdateGrowth();

    }
    private void Start()
    {
        //debugLatitude.UpdateLatitude();
    }
    private void Update()
    {
        DebugUpdate();
    }
}
