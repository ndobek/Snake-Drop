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
    private ScoreManager scoreManager;
    public int Score
    {
        get { return scoreManager.Score; }
        set { scoreManager.Score = value; }
    }
    private int NumberOfSnakes;

    public PlayerController playerController;

    public PlayGrid playGrid;
    public PlayGrid previewGrid;
    public HeightLimitIndicator HeightLimitIndicator;

    public BlockSlot startSlot;
    public BlockSlot waitSlot;
    public BlockSlot EnterSlot
    {
        get { return waitSlot.customDownNeighbor; }
        set { SetWaitSlotNeightbor(value); }
    }

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
            playGrid.Fall();
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
        playGrid.Fall();

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
        bool result = true;
        for(int i = 0; i < playGrid.XSize; i++)
        {
            if(playGrid.GetBlock(i, playGrid.YSize - 1) == null)
            {
                result = false;
            }
        }
        return result;
    }

    private void ShufflePreviewBar()
    {
        previewGrid.Fall(GameManager.instance.BasicFall);
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
            NumberOfSnakes += 1;
            SnakeInfo info = GameManager.instance.difficultyManager.GetSnakeInfo(Score, NumberOfSnakes);
            Debug.Log("Entropy: " + info.entropy + "Length: " + info.length);
            SnakeMaker.MakeSnake(startSlot, info);
        }
    }

    public void SetWaitSlotNeightbor(BlockSlot slot)
    {
        if (slot)
        {
            waitSlot.customDownNeighbor = slot;
            waitSlot.playGrid.transform.position = new Vector3(playGrid.CoordsPosition(slot.x, 0).x, waitSlot.playGrid.transform.position.y);
        }
    }

    public void ResetGame()
    {
        Score = 0;
        NumberOfSnakes = 0;
        ResetMoveRestrictions();
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        GameInProgress = true;
    }
    public void PrepareNewRound()
    {
        FillPreviewBar();
        ResetMoveRestrictions();
        SnakeHead = waitSlot.Block;
    }

    public void StartNewRound()
    {
        BlockSlot destination = snakeHead.Slot.GetNeighbor(GameManager.Direction.DOWN);
        if (/*destination && SnakeHead && */GameManager.instance.BasicMove.CanMoveTo(SnakeHead, destination, this))
        {
            GameManager.instance.BasicMove.OnMove(SnakeHead, destination, this);
            RoundInProgress = true;
        }
    }
    private void EndGame()
    {
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
                if(direction == GameManager.Direction.LEFT || direction == GameManager.Direction.RIGHT)
                {
                    EnterSlot = EnterSlot.GetNeighbor(direction);
                }
                if(direction == GameManager.Direction.DOWN)
                {

                    StartNewRound();
                }
            }
        }
    }
}
