using System.Collections;
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

    [SerializeField]
    public ScoreManager Score;

    public PlayerController playerController;

    public PlayGrid playGrid;
    public PlayGrid previewGrid;
    public HeightLimitIndicator HeightLimitIndicator;

    public BlockSlot startSlot;
    public BlockSlot waitSlot;

    private EntranceSlot enterSlot;
    public EntranceManager entranceManager;

    [HideInInspector]
    public bool RoundInProgress;
    [HideInInspector]
    public bool GameInProgress;

    public void OnCrash(bool resetMultiplier = true)
    {
        if(resetMultiplier) Score.ResetMultiplier();
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
            playGrid.InvokeGridAction();
            FillPreviewBar();

            DifficultyManager difficulty = GameManager.instance.difficultyManager;
            if (difficulty.HeightLimit) HeightLimitIndicator.LowerHeightLimit(SnakeHead.FindSnakeMaxY() + difficulty.HeightLimitModifier);
        }
    }

    private void EndRound()
    {
        if (SnakeHead)
        {
            SnakeHead.KillSnake(this);
        }
        playGrid.InvokeGridAction();

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
        //return (waitSlot.GetNeighbor(GameManager.Direction.DOWN).Block != null);
        return !entranceManager.CheckForValidEntrancesToGrid(this, playGrid);
    }

    private void ShufflePreviewBar()
    {
        previewGrid.InvokeGridAction();
        MakeSnake();
    }
    public void FillPreviewBar()
    {
        ShufflePreviewBar();
        while (startSlot.Blocks.Count == 0 | waitSlot.Block == null)
        {
            ShufflePreviewBar();
        }
    }

    public void MakeSnake()
    {
        if (startSlot.CheckIsClear())
        {
            Score.NumberOfSnakes += 1;
            SnakeInfo info = GameManager.instance.difficultyManager.GetSnakeInfo(Score.Score, Score.NumberOfSnakes);
            Debug.Log("Entropy: " + info.entropy + " Length: " + info.length + " Snake Number: " + Score.NumberOfSnakes + " Score: " + Score.Score);
            SnakeMaker.MakeSnake(startSlot, info);
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

            waitSlot.playGrid.transform.position = slot.transform.position;
            waitSlot.playGrid.transform.rotation = slot.transform.rotation;

            enterSlot = (EntranceSlot)slot;
        }
    }

    public void ResetGame()
    {
        Score.ResetGame();
        ResetMoveRestrictions();
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        PositionWaitSlot(entranceManager.StartingSlot);
        GameInProgress = true;
    }
    public void PrepareNewRound()
    {
        entranceManager.UpdateAnimations();
        FillPreviewBar();
        ResetMoveRestrictions();
        SnakeHead = waitSlot.Block;
    }

    public void StartNewRound(GameManager.Direction direction)
    {

        BlockSlot destination = snakeHead.Slot.GetNeighbor(direction);
        if (/*destination && SnakeHead && */GameManager.instance.BasicMove.CanMoveTo(SnakeHead, destination, this))
        {
            GameManager.instance.BasicMove.OnMove(SnakeHead, destination, this);
            RoundInProgress = true;
        }
    }
    private void EndGame()
    {
        entranceManager.UpdateAnimations();
        GameInProgress = false;
        GameManager.instance.CheckForGameOver();
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
            snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.snakeHeadType);
            snakeHead.SetOwner(this);
        }
        else
        {
            snakeHead = null;
        }
    }

    public void MoveSnake(GameManager.Direction direction)
    {

        if (GameInProgress)
        {
            if (RoundInProgress)
            {
                SnakeHead.Move(direction, this);
                MidRound();
            }
            else
            {
                BlockSlot destination = enterSlot.GetNeighbor(direction);
                if(!destination || destination.playGrid != playGrid)
                {
                    PositionWaitSlot(enterSlot.GetNextValidSlot(direction,this));
                }
                else
                {

                    StartNewRound(direction);
                }
            }
        }
    }
}
