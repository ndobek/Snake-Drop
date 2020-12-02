using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int score;
    public PlanetSaveData planetData;
    public GridSaveData playGrid;
    public GridSaveData loadGrid;

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

public class GridSaveData
{
    public List<BlockSlotSaveData> BlockSlotData = new List<BlockSlotSaveData>();
}

public class BlockSlotSaveData
{
    public List<BlockSaveData> BlockData = new List<BlockSaveData>();
    public int x;
    public int y;
}

public class BlockSaveData
{
    public string blockType;
    public string blockColor;
    public int tailX;
    public int tailY;
    public int tailI;
}

public class BlockCollectionSaveData
{
    public int LeftCoord;
    public int RightCoord;
    public int TopCoord;
    public int BottomCoord;

    public int FillAmount;
}