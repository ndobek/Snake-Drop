using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public PlayerSaveData playerData;
    public PlanetSaveData planetData;
    public Random.State randState;

    public SaveData(GameManager gameManager)
    {
        playerData = new PlayerSaveData(gameManager.playerManagers[0]);
        randState = Random.state;
    }

    public void LoadTo(GameManager LoadObj)
    {
        Random.state = randState;
        playerData.LoadTo(LoadObj.playerManagers[0], LoadObj);
        LoadObj.boardRotator.RotateBoardToMatchEntrance();
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
    public EntranceSlotSaveData EnterSlot;
    public int mostRecentDirectionMoved;
    public int mostRecentSnakeLength;
    public Random.State randStateForSnake;

    public PlayerSaveData(PlayerManager SaveObj)
    {
        snakeHead = new BlockLocationData(SaveObj.SnakeHead);
        powerup = new PowerupSaveData(SaveObj.Powerup);
        score = new ScoreSaveData(SaveObj.Score);
        playGrid = new GridSaveData(SaveObj.playGrid);
        previewGrid = new GridSaveData(SaveObj.previewGrid);
        entranceManager = new EntranceManagerSaveData(SaveObj.entranceManager);
        RoundInProgress = SaveObj.RoundInProgress;
        GameInProgress = SaveObj.GameInProgress;
        EnterSlot = new EntranceSlotSaveData(SaveObj.enterSlot);
        mostRecentDirectionMoved = (int)SaveObj.playerController.SecondMostRecentDirectionMoved;
        mostRecentSnakeLength = SaveObj.mostRecentSnakeLength;
        randStateForSnake = SaveObj.randStateForSnake;

    }
    
    public void LoadTo(PlayerManager LoadObj, GameManager gameManager)
    {

        playGrid.LoadTo(LoadObj.playGrid, gameManager);
        previewGrid.LoadTo(LoadObj.previewGrid, gameManager);
        playGrid.LoadReferences(gameManager);
        previewGrid.LoadReferences(gameManager);
        entranceManager.LoadTo(LoadObj.entranceManager, gameManager);
        LoadObj.SnakeHead = snakeHead.GetBlock(LoadObj);
        LoadObj.PositionWaitSlot(LoadObj.entranceManager.GetSlot(EnterSlot.x, EnterSlot.y));
        LoadObj.playerController.MostRecentDirectionMoved = (Directions.Direction)mostRecentDirectionMoved;
        LoadObj.mostRecentSnakeLength = mostRecentSnakeLength;
        score.LoadTo(LoadObj.Score);
        powerup.LoadTo(LoadObj.Powerup, gameManager);
        LoadObj.RoundInProgress = RoundInProgress;
        LoadObj.GameInProgress = GameInProgress;
        LoadObj.randStateForSnake = randStateForSnake;
        LoadObj.previewGrid.RefreshBlockLocations();
        LoadObj.playGrid.RefreshBlockLocations();
    }
}

[System.Serializable]
public class PowerupSaveData
{
    public int currentProgress;
    public string currentPowerup;
    public int extraPowerups;
    public int numberOfObtainedPowerups;
    public int nextPowerupScoreDiff;
    public int numberOfUsedPowerups;

    public PowerupSaveData(PowerupManager SaveObj)
    {
        currentProgress = SaveObj.currentProgress;
        currentPowerup = SaveObj.currentPowerup != null? SaveObj.currentPowerup.Name : "null";
        extraPowerups = SaveObj.extraPowerups;
        numberOfObtainedPowerups = SaveObj.numOfPowerupsObtained;
        nextPowerupScoreDiff = SaveObj.nextPowerupScoreDiff;
        numberOfUsedPowerups = SaveObj.numOfPowerupsUsed;

    }

    public void LoadTo(PowerupManager LoadObj, GameManager gameManager)
    {
        LoadObj.currentProgress = currentProgress;
        if(currentPowerup != "null") LoadObj.currentPowerup = gameManager.Powerups.getObject(currentPowerup) as Powerup;
        else LoadObj.currentPowerup = null;
        LoadObj.extraPowerups = extraPowerups;
        LoadObj.numOfPowerupsObtained = numberOfObtainedPowerups;
        LoadObj.nextPowerupScoreDiff = nextPowerupScoreDiff;
        LoadObj.numOfPowerupsUsed = numberOfUsedPowerups;
    }
}

[System.Serializable]
public class ScoreSaveData
{
    public int score;
    public int multiplier;
    public int numberOfSnakes;
    public int scoreAtLastCrash;

    public ScoreSaveData(ScoreManager SaveObj)
    {
        score = SaveObj.Score;
        multiplier = SaveObj.Multiplier;
        numberOfSnakes = SaveObj.numberOfSnakes;
        scoreAtLastCrash = SaveObj.scoreAtLastCrash;
    }

    public void LoadTo(ScoreManager LoadObj)
    {
        LoadObj.Score = score;
        LoadObj.Multiplier = multiplier;
        LoadObj.numberOfSnakes = numberOfSnakes;
        LoadObj.scoreAtLastCrash = scoreAtLastCrash;
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

        foreach (BlockCollectionSaveData collectionData in BlockCollectionData)
        {
            BlockCollection newCollection = new BlockCollection(); 
            collectionData.LoadTo(newCollection, gameManager);
            newCollection.Build(LoadObj, gameManager.Types.getObject(collectionData.blockType) as BlockType, gameManager.playerManagers[0]);
        }
    }

    public void LoadReferences(GameManager gameManager)
    {
        foreach (BlockSlotSaveData slotData in BlockSlotData)
        {
            foreach (BlockSaveData blockData in slotData.BlockData)
            {
                Block block = blockData.location.GetBlock(gameManager.playerManagers[0]);
                if (blockData.tail) block.SetTail(blockData.tailLocation.GetBlock(gameManager.playerManagers[0]));
                if (blockData.owned) block.SetOwner();
            }
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
        foreach (BlockSaveData block in BlockData)
        {
            LoadObj.CreateBlock(gameManager.Colors.getObject(block.blockColor) as BlockColor, gameManager.Types.getObject(block.blockType) as BlockType);

        }
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
    public bool owned;

    public BlockSaveData(Block SaveObj)
    {
        blockType = SaveObj.blockType.Name;
        blockColor = SaveObj.blockColor.Name;
        location = new BlockLocationData(SaveObj);
        tail = SaveObj.Tail != null;
        owned = SaveObj.Owner != null;
        if (tail) tailLocation = new BlockLocationData(SaveObj.Tail);
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
        LoadObj.UpdateAnimations();
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
        if (SaveObj)
        {
            active = SaveObj.Active;
            x = SaveObj.x;
            y = SaveObj.y;
        }
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
    public string blockType;

    public int FillAmount;

    public BlockCollectionSaveData(BlockCollection SaveObj)
    {
        LeftCoord = SaveObj.LeftCoord;
        RightCoord = SaveObj.RightCoord;
        TopCoord = SaveObj.TopCoord;
        BottomCoord = SaveObj.BottomCoord;
        FillAmount = SaveObj.FillAmount;
        blockType = SaveObj.Blocks[0].blockType.Name;
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