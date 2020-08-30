using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DebugGrowth : MonoBehaviour 
{
    public PlantManager pm;
    List<Plant> plants;
    StringBuilder displayContents;
    public Text display;

    private void Awake()
    {      
        display = gameObject.GetComponent<Text>();
    }
    void Start()
    {
        plants = pm.AllPlants();
    }
    public void UpdateGrowth()
    {
        displayContents = new StringBuilder();
        foreach (Plant p in plants)
        {
            displayContents.Append(p.growable.GrowthStage.ToString());
        }
        display.text = "Growth Stages:\n" + displayContents.ToString();
        
    }

}
