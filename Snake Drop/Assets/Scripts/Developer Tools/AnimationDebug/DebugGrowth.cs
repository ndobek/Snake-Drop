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
        plants = pm.GetAllPlants();
        display = gameObject.GetComponent<Text>();
        displayContents = new StringBuilder();
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

    // Start is called before the first frame update
    void Start()
    {
        if (plants == null) Debug.LogError("Plants is null");
        Debug.Log("Number of plants: " + plants.Count);
        foreach (Plant p in plants) 
        {
            Debug.Log (p.growable.GrowthStage.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
