using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugWind : MonoBehaviour, IWindReactive 
{
    public WindData wind;
    public Text display;

    public void Awake()
    {
        display = gameObject.GetComponent<Text>();
    }
    public void UpdateWindReaction()
    {
        display.text = wind.Gust(0).ToString() + wind.GustStrength().ToString();
    }


}
