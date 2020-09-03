using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class DebugLatitude : MonoBehaviour
{
    public float lineSpacing = .75f;
    public PlantManager pm;
    List<Plant> plants;
    List<float> latitudes;
    public Text display;
    StringBuilder displayContents;


    private void Awake()
    {
        display = gameObject.GetComponent<Text>();
        display.lineSpacing = lineSpacing;
        latitudes = new List<float>();
    }
    void Start()
    {
        plants = pm.AllPlants();
        foreach (Plant p in plants)
        {
            latitudes.Add(p.gameObject.GetComponent<Latitude>().ReturnLatitude());
        }
        latitudes.Sort();
    }
    public void UpdateLatitude()
    {
        displayContents = new StringBuilder();
        foreach (float l in latitudes)
        {
            displayContents.Append(l.ToString() + " ");
        }
        display.text = "Latitudes:\n" + displayContents.ToString();
    }
}
