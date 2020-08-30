using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DebugGrowth : MonoBehaviour //trying to narrow down the weird no plants showing up thing
{
    public PlantManager pm;
    List<Plant> plants;
    StringBuilder displayContents;
    public Text display;
    string str;
    int growthStage;
    string growthStageStr;
    private void Awake()
    {
        
        display = gameObject.GetComponent<Text>();
        displayContents = new StringBuilder();
    }
    void Start()
    {
        plants = pm.AllPlants();
    }
    public void UpdateGrowth()
    {
        foreach(Plant p in plants)
        {
            growthStage = p.growable.GrowthStage;
            growthStageStr = growthStage.ToString();
            displayContents.Append(growthStageStr);
        }
        str = displayContents.ToString();
        display.text = str;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
