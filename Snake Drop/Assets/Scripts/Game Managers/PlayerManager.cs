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
    public GameObject EntranceIndicator;

    public BlockSlot startSlot;
    public BlockSlot waitSlot;

    public EntranceSlot enterSlot;
    public EntranceManager entranceManager;

    [HideInInspector]
    public bool RoundInProgress;
    [HideInInspector]
    public bool GameInProgress;

    public void OnCrash()
    {
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

            //DifficultyManager difficulty = GameManager.instance.difficultyManager;
            //if (difficulty.HeightLimit) HeightLimitIndicator.LowerHeightLimit(SnakeHead.FindSnakeMaxY() + difficulty.HeightLimitModifier);
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
        //return (waitSlot.GetNeighbor(Directions.Direction.DOWN).Block != null);
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
        ResetMoveRestrictions();
        entranceManager.ReactivateEntrances();
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        PositionWaitSlot(entranceManager.StartingSlot);
        GameInProgress = true;
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
    }

    public void StartNewRound(Directions.Direction direction)
    {

        BlockSlot destination = snakeHead.Slot.GetNeighbor(direction);
        if (SnakeHead.CanMoveToWithoutCrashing(destination, this) && enterSlot.Active/*GameManager.instance.BasicMove.CanMoveTo(SnakeHead, destination, this)*/)
        {
            snakeHead.MoveTo(destination, this);/*GameManager.instance.BasicMove.OnMove(SnakeHead, destination, this)*/;
            GameManager.instance.plantManager.PlantNewPlants(destination.transform.position);
            RoundInProgress = true;
        }
    }
    private void EndGame()
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
        while((result.Tail != null && result.blockType != GameManager.instance.snakeHeadType))
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
            snakeHead.SetBlockType(snakeHead.blockColor, GameManager.instance.snakeHeadType);
            snakeHead.SetOwner(this);
        }
        else
        {
            snakeHead = null;
        }
    }

    public void MoveSnake(Directions.Direction direction)
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
