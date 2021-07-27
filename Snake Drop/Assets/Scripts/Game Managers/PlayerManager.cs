﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    private Block snakeHead;
    public Block SnakeHead
    {
        get { return snakeHead; }
        set { ActivateSnake(value); }
    }

    public ScoreManager Score;
    public PowerupManager Powerup;
    public UndoManager Undoer;


    public PlayerController playerController;

    public PlayGrid playGrid;
    public PlayGrid previewGrid;
    [SerializeField]
    private Animator previewGridBounceAnimation;
    public HeightLimitIndicator HeightLimitIndicator;
    public GameObject EntranceIndicator;

    public BlockSlot startSlot;
    public BlockSlot waitSlot;

    public EntranceSlot enterSlot;
    public EntranceManager entranceManager;

    public ParticleManager particleManager;

    [HideInInspector]
    public bool RoundInProgress;
    [HideInInspector]
    public bool GameInProgress;
    private bool StopMakingSnakes = false;

    public int mostRecentSnakeLength;

    public void OnCrash()
    {
        Rule OnCrash = GameManager.instance.GameModeManager.GameMode.OnCrash;
        if(OnCrash != null) OnCrash.Invoke(snakeHead, this);
        RoundInProgress = false;
    }

    public void MidRound()
    {
        if (!RoundInProgress)
        {
            EndRound();
        }
        else
        {
            DoGridActions();

            FillPreviewBar();
        }

    }

    public void DoGridActions()
    {
        playGrid.InvokeGridAction();
        entranceManager.InvokeGridAction();
    }

    private void EndRound()
    {
        if (SnakeHead)
        {
            SnakeHead.KillSnake(this);
        }
        DoGridActions();

        if (GameIsOver())
        {
            EndGame(); 
        }
        else
        {
            PrepareNewRound();
        }
    }

    private bool GameIsOver()
    {
        return GameManager.instance.GameModeManager.GameMode.EndGameCondition.GameIsOver(this);

    }

    private void ShufflePreviewBar()
    {
        previewGrid.InvokeGridAction();
        MakeSnake();
    }
    public void FillPreviewBar()
    {
        ShufflePreviewBar();
        while ((startSlot.Blocks.Count == 0 | waitSlot.Block == null) && !StopMakingSnakes)
        {
            ShufflePreviewBar();
        }
    }

    public void MakeSnake()
    {
        if (startSlot.CheckIsClear())
        {
            Score.NumberOfSnakes += 1;
            SnakeInfo info = GameManager.instance.GameModeManager.GetSnakeInfo(Score.Score, Score.NumberOfSnakes);
            StopMakingSnakes = info == null;
            if (!StopMakingSnakes)
            {
                Debug.Log("Entropy: " + info.entropy + " Length: " + info.length + " Snake Number: " + Score.NumberOfSnakes + " Score: " + Score.Score);
                SnakeMaker.MakeSnake(startSlot, info);
            }
        }
    }

    public void PositionWaitSlot(BlockSlot slot)
    {
        if (slot)
        { 
            waitSlot.customDownNeighbor = slot.customDownNeighbor;
            waitSlot.customUpNeighbor = slot.customUpNeighbor;
            waitSlot.customLeftNeighbor = slot.customLeftNeighbor;
            waitSlot.customRightNeighbor = slot.customRightNeighbor;

            PositionEntranceIndicator(slot.transform);

            enterSlot = (EntranceSlot)slot;
        }
    }

    public void PositionEntranceIndicator(Transform newTransfrom)
    {
        EntranceIndicator.transform.position = newTransfrom.position;
        EntranceIndicator.transform.rotation = newTransfrom.rotation;
    }

    public void ResetGame()
    {
        GameManager.instance.plantManager.ResetGrowth();
        Score.ResetGame();
        Powerup.ResetGame();
        ResetMoveRestrictions();
        entranceManager.ReactivateEntrances();
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        PositionWaitSlot(entranceManager.StartingSlot);
        GameInProgress = true;
        RoundInProgress = false;


        if (GameManager.instance.GameModeManager.GameMode.OnGameStart) GameManager.instance.GameModeManager.GameMode.OnGameStart.Invoke(playGrid);
    }
    public void PrepareNewRound()
    {
        if (snakeHead == null) entranceManager.ReactivateEntrances();
        else
        {
            Score.ResetMultiplier();
        }
        
        entranceManager.UpdateAnimations();
        FillPreviewBar();
        ResetMoveRestrictions();
        SnakeHead = waitSlot.Block;
        mostRecentSnakeLength = SnakeHead.SnakeLength();
    }

    public void StartNewRound(Directions.Direction direction)
    {
        if (playerController.autoMover != null)
        {
            playerController.autoMover.prevMovedDirection = direction;
            playerController.autoMover.resetTime();
        }
        BlockSlot destination = snakeHead.Slot.GetNeighbor(direction);
        if (SnakeHead.CanMoveToWithoutCrashing(destination, this) && enterSlot.Active/*GameManager.instance.BasicMove.CanMoveTo(SnakeHead, destination, this)*/)
        {
            Undoer.Save();
            snakeHead.MoveTo(destination, this);/*GameManager.instance.BasicMove.OnMove(SnakeHead, destination, this)*/;
            GameManager.instance.plantManager.PlantNewPlants(destination.transform.position);
            RoundInProgress = true;
        } else
        {
            previewGridBounceAnimation.Play("Bounce");
        }
    }
    public void EndGame()
    {
        PrepareNewRound();
        entranceManager.UpdateAnimations();
        GameInProgress = false;
        GameManager.instance.CheckForGameOver();
    }
    public Block GetNextSnakeHead()
    {
        FillPreviewBar();
        Block result = waitSlot.Block;
        while((result.Tail != null && result.blockType != GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType))
        {

            result = result.Slot.GetNeighbor(Directions.Direction.UP).Block;
        }
        return result;
    }

    public void ResetMoveRestrictions()
    {
        HeightLimitIndicator.ResetHeightLimit();
        playerController.ResetGame();
    }

    private void ActivateSnake(Block newSnakeHead)
    {
        if (snakeHead) snakeHead.Kill(this);
        if (newSnakeHead)
        {
            snakeHead = newSnakeHead;
            snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType);
            snakeHead.SetOwner(this);
        }
        else
        {
            snakeHead = null;
        }
    }

    public void MoveWaitSlot(Directions.Direction direction)
    {
        PositionWaitSlot(enterSlot.GetNextValidSlot(direction, this));
    }

    public void MoveWaitSlot(bool clockwise)
    {
        PositionWaitSlot(enterSlot.GetNextValidSlot(clockwise, this));
    }

    public void MoveSnake(Directions.Direction direction)
    {

        if (GameInProgress)
        {
            if (RoundInProgress)
            {
                if(!snakeHead.blockType.OverrideMove(snakeHead, snakeHead.Neighbor(direction), this)) Undoer.Save();
                SnakeHead.Move(direction, this);
                MidRound();
            }
            else
            {
                BlockSlot destination = enterSlot.GetNeighbor(direction);
                if(!destination || destination.playGrid != playGrid)
                {
                    MoveWaitSlot(direction);
                }
                else
                {
                    StartNewRound(direction);
                }
            }
        }
    }
}
