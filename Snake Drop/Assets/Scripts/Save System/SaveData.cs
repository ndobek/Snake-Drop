﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public PlayerSaveData playerData;
    public PlanetSaveData planetData;

    public SaveData(GameManager gameManager)
    {
        playerData = new PlayerSaveData(gameManager.playerManagers[0]);
        planetData = gameManager.plantManager.SavePlanet();
    }

    public void LoadTo(GameManager LoadObj)
    {
        playerData.LoadTo(LoadObj.playerManagers[0], LoadObj);
    }
}
[System.Serializable]
public class PlayerSaveData
{
    public BlockLocationData snakeHead;
    public ScoreSaveData score;
    public PowerupSaveData powerup;
    public GridSaveData playGrid;
    public GridSaveData previewGrid;
    public EntranceManagerSaveData entranceManager;
    public bool RoundInProgress;
    public bool GameInProgress;

    public PlayerSaveData(PlayerManager SaveObj)
    {
        snakeHead = new BlockLocationData(SaveObj.SnakeHead);
        score = new ScoreSaveData(SaveObj.Score);
        powerup = new PowerupSaveData(SaveObj.Powerup);
        playGrid = new GridSaveData(SaveObj.playGrid);
        previewGrid = new GridSaveData(SaveObj.previewGrid);
        entranceManager = new EntranceManagerSaveData(SaveObj.entranceManager);
        RoundInProgress = SaveObj.RoundInProgress;
        GameInProgress = SaveObj.GameInProgress;
    }
    
    public void LoadTo(PlayerManager LoadObj, GameManager gameManager)
    {
        LoadObj.SnakeHead = snakeHead.GetBlock(LoadObj);
        score.LoadTo(LoadObj.Score);
        powerup.LoadTo(LoadObj.Powerup, gameManager);
        playGrid.LoadTo(LoadObj.playGrid, gameManager);
        previewGrid.LoadTo(LoadObj.previewGrid, gameManager);
        entranceManager.LoadTo(LoadObj.entranceManager, gameManager);
        LoadObj.RoundInProgress = RoundInProgress;
        LoadObj.GameInProgress = GameInProgress;
    }
}

[System.Serializable]
public class PowerupSaveData
{
    public int currentProgress;
    public string currentPowerup;

    public PowerupSaveData(PowerupManager SaveObj)
    {
        currentProgress = SaveObj.currentProgress;
        currentPowerup = SaveObj.currentPowerup.Name;
    }

    public void LoadTo(PowerupManager LoadObj, GameManager gameManager)
    {
        LoadObj.currentProgress = currentProgress;
        LoadObj.currentPowerup = gameManager.Powerups.getObject(currentPowerup) as Powerup;
    }
}

[System.Serializable]
public class ScoreSaveData
{
    public int score;
    public int multiplier;
    public int numberOfSnakes;

    public ScoreSaveData(ScoreManager SaveObj)
    {
        score = SaveObj.Score;
        multiplier = SaveObj.Multiplier;
        numberOfSnakes = SaveObj.numberOfSnakes;
    }

    public void LoadTo(ScoreManager LoadObj)
    {
        LoadObj.Score = score;
        LoadObj.Multiplier = multiplier;
        LoadObj.numberOfSnakes = numberOfSnakes;
    }
}


[System.Serializable]
public class GridSaveData
{
    public List<BlockSlotSaveData> BlockSlotData;
    public List<BlockCollectionSaveData> BlockCollectionData;

    public GridSaveData(PlayGrid SaveObj)
    {
        BlockSlotData = new List<BlockSlotSaveData>();
        BlockCollectionData = new List<BlockCollectionSaveData>();
        List<BlockCollection> addedCollections = new List<BlockCollection>();

        foreach (BlockSlot slot in SaveObj.slots)
        {
            foreach (Block block in slot.Blocks)
            {
                if (block.BlockCollection != null && !addedCollections.Contains(block.BlockCollection))
                {
                    addedCollections.Add(block.BlockCollection);
                    BlockCollectionData.Add(new BlockCollectionSaveData(block.BlockCollection));
                }
            }

            BlockSlotData.Add(new BlockSlotSaveData(slot));
        }

    }

    public void LoadTo(PlayGrid LoadObj, GameManager gameManager)
    {
        foreach (BlockSlotSaveData slotData in BlockSlotData) slotData.LoadTo(LoadObj.GetSlot(slotData.x, slotData.y), gameManager);
        foreach (BlockSlotSaveData slotData in BlockSlotData)
        {
            foreach (BlockSaveData blockData in slotData.BlockData)
            {
                if(blockData.tail) blockData.location.GetBlock(gameManager.playerManagers[0]).SetTail(blockData.tailLocation.GetBlock(gameManager.playerManagers[0]));
            }
        }

        foreach (BlockCollectionSaveData collectionData in BlockCollectionData)
        {
            BlockCollection newCollection = new BlockCollection(); 
            collectionData.LoadTo(newCollection, gameManager);
            newCollection.Build(LoadObj);
        }
    }
}
[System.Serializable]
public class BlockSlotSaveData
{
    public List<BlockSaveData> BlockData = new List<BlockSaveData>();
    public int x;
    public int y;

    public BlockSlotSaveData(BlockSlot SaveObj)
    {
        BlockData = new List<BlockSaveData>();
        for (int i = 0; i < SaveObj.Blocks.Count; i++)
        {
            BlockData.Add(new BlockSaveData(SaveObj.Blocks[i]));
        }
        x = SaveObj.x;
        y = SaveObj.y;
    }
    public void LoadTo(BlockSlot LoadObj, GameManager gameManager)
    {
        LoadObj.DeleteBlock();
        foreach (BlockSaveData block in BlockData) LoadObj.CreateBlock(gameManager.Colors.getObject(block.blockColor) as BlockColor, gameManager.Types.getObject(block.blockType) as BlockType);
    }
}
[System.Serializable]
public class BlockLocationData
{
    public int x;
    public int y;
    public int i;

    public bool locatedOnLoadGrid;

    public BlockLocationData(Block SaveObj, int index)
    {
        x = SaveObj.X;
        y = SaveObj.Y;
        i = index;
        locatedOnLoadGrid = SaveObj.Slot.playGrid == GameManager.instance.playerManagers[0].previewGrid;
    }
    public BlockLocationData(Block SaveObj) : this(SaveObj, SaveObj.Slot.Blocks.IndexOf(SaveObj)) { }
    public Block GetBlock(PlayerManager player)
    {
        PlayGrid grid = locatedOnLoadGrid ? player.previewGrid : player.playGrid;
        return grid.GetSlot(x, y).Blocks[i];
    }

}
[System.Serializable]
public class BlockSaveData
{
    public string blockType;
    public string blockColor;
    public BlockLocationData location;
    public bool tail;
    public BlockLocationData tailLocation;

    public BlockSaveData(Block SaveObj)
    {
        blockType = SaveObj.blockType.Name;
        blockColor = SaveObj.blockColor.Name;
        location = new BlockLocationData(SaveObj);
        tail = SaveObj.Tail != null;

        if (tail) tailLocation = new BlockLocationData(SaveObj.Tail);
    }
    public void LoadTo(Block LoadObj, GameManager gameManager)
    {
        //LoadObj.SetBlockType(gameManager.Colors.getObject(blockColor) as BlockColor, gameManager.Types.getObject(blockType) as BlockType);
        if (tail) LoadObj.SetTail(tailLocation.GetBlock(gameManager.playerManagers[0]));
    }
}

[System.Serializable]
public class EntranceManagerSaveData 
{
    public List<EntranceSlotSaveData> EntranceSlots = new List<EntranceSlotSaveData>();

    public EntranceManagerSaveData(EntranceManager SaveObj)
    {
        foreach(EntranceSlot slot in SaveObj.slots) EntranceSlots.Add(new EntranceSlotSaveData(slot));
    }

    public void LoadTo(EntranceManager LoadObj, GameManager gameManager)
    {
        foreach (EntranceSlotSaveData entranceSlot in EntranceSlots) entranceSlot.LoadTo(LoadObj.GetSlot(entranceSlot.x, entranceSlot.y) as EntranceSlot, gameManager);
    }
}

[System.Serializable]
public class EntranceSlotSaveData
{
    public int x;
    public int y;
    public bool active;
    public EntranceSlotSaveData(EntranceSlot SaveObj)
    {
        active = SaveObj.Active;
        x = SaveObj.x;
        y = SaveObj.y;
    }

    public void LoadTo(EntranceSlot LoadObj, GameManager gameManager)
    {
        LoadObj.Active = active;
    }
}


[System.Serializable]
public class BlockCollectionSaveData
{
    public int LeftCoord;
    public int RightCoord;
    public int TopCoord;
    public int BottomCoord;

    public int FillAmount;

    public BlockCollectionSaveData(BlockCollection SaveObj)
    {
        LeftCoord = SaveObj.LeftCoord;
        RightCoord = SaveObj.RightCoord;
        TopCoord = SaveObj.TopCoord;
        BottomCoord = SaveObj.BottomCoord;
        FillAmount = SaveObj.FillAmount;
    }
    
    public void LoadTo(BlockCollection LoadObj, GameManager gameManager)
    {
        LoadObj.LeftCoord = LeftCoord;
        LoadObj.RightCoord = RightCoord;
        LoadObj.TopCoord = TopCoord;
        LoadObj.BottomCoord = BottomCoord;
        LoadObj.FillAmount = FillAmount;
    }
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