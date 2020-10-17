using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int score;
    public PlanetSaveData planetData;

}

[System.Serializable]
public class PlanetSaveData
{
    public List<PlantSaveData> plantData = new List<PlantSaveData>();
}

[System.Serializable]
public class PlantSaveData
{
    public Vector3 position;
    public Quaternion rotation;
    public int growthStage;

    public string plantName;
}