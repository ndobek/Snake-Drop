using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Profiling;
using System.Linq;

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
    private Animator previewGridAnimator;
    public HeightLimitIndicator HeightLimitIndicator;
    public GameObject EntranceIndicator;

    public BlockSlot startSlot;
    public BlockSlot waitSlot;

    public EntranceSlot enterSlot;
    public EntranceManager entranceManager;
    public BoardRotator boardRotator;
    public ArrowHandler arrowHandler;

    public ParticleManager particleManager;
    public MusicManager musicManager;

    [HideInInspector]
    public bool RoundInProgress;
    [HideInInspector]
    public bool GameInProgress;
    public bool GameOver;
    private bool StopMakingSnakes = false;
    private bool firstRound = false;

    public int mostRecentSnakeLength;
    public Random.State randStateForSnake;

    static readonly ProfilerMarker p_OnCrash = new ProfilerMarker("On Crash");
    static readonly ProfilerMarker p_MidRound = new ProfilerMarker("MidRound");
    static readonly ProfilerMarker p_DoGridActions = new ProfilerMarker("DoGridActions");
    static readonly ProfilerMarker p_EndRound = new ProfilerMarker("EndRound");

    public void OnCrash()
    {
        p_OnCrash.Begin();
        Rule OnCrash = GameManager.instance.GameModeManager.GameMode.OnCrash;
        musicManager.ParseCrash(snakeHead);
        if (OnCrash != null) OnCrash.Invoke(snakeHead, this);
        Score.OnCrash();
        RoundInProgress = false;
        BGManager.inst.BGBlurred = true;
        p_OnCrash.End();
    }

    public void MidRound()
    {
        p_MidRound.Begin();

        if (!RoundInProgress)
        {
            EndRound();
        }
        else
        {
            DoGridActions();

            FillPreviewBar();
        }
        musicManager.ParseBlockCollection();
        p_MidRound.End();
    }

    public void DoGridActions()
    {
        p_DoGridActions.Begin();
        playGrid.InvokeGridAction();
        entranceManager.InvokeGridAction();
        p_DoGridActions.End();
    }

    private void EndRound()
    {
        p_EndRound.Begin();
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
        p_EndRound.End();
    }

    private bool GameIsOver()
    {
        return GameManager.instance.GameModeManager.GameMode.EndGameCondition.Invoke(this);

    }

    private void ShufflePreviewBar()
    {
        previewGrid.InvokeGridAction();
        MakeSnake();
    }
    public void FillPreviewBar()
    {
        previewGrid.UpdateAllSprites();
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
            Random.state = randStateForSnake;
            Score.NumberOfSnakes += 1;
            SnakeInfo info = GameManager.instance.GameModeManager.GetSnakeInfo();
            StopMakingSnakes = info == null;
            if (!StopMakingSnakes)
            {
                Debug.Log("Entropy: " + info.entropy + " Length: " + info.length + " Snake Number: " + Score.NumberOfSnakes + " Score: " + Score.Score);
                SnakeMaker.MakeSnake(startSlot, info);
            }
            randStateForSnake = Random.state;
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
            previewGrid.UpdateAllSprites();
        }
    }

    public void PositionEntranceIndicator(Transform newTransfrom)
    {
        EntranceIndicator.transform.position = newTransfrom.position;
        EntranceIndicator.transform.rotation = newTransfrom.rotation;
    }

    public void ResetGame()
    {
        randStateForSnake = Random.state;
        Score.ResetGame();
        Powerup.ResetGame();
        ResetMoveRestrictions();
        entranceManager.ReactivateEntrances();
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        PositionWaitSlot(entranceManager.StartingSlot);
        BGManager.inst.BGBlurred = false;
        GameInProgress = true;
        RoundInProgress = false;
        GameOver = false;
        firstRound = true;
        boardRotator.rotateInstantly(Directions.Direction.UP);

        
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
        if (firstRound)
        {
            if(GameManager.instance.GameModeManager.GameMode.OnGameStart) GameManager.instance.GameModeManager.GameMode.OnGameStart.Invoke(playGrid);
            firstRound = false;
        }
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
            previewGridAnimator.Play("Down");
            snakeHead.MoveTo(destination, this);/*GameManager.instance.BasicMove.OnMove(SnakeHead, destination, this)*/;
            RoundInProgress = true;
        }
        else
        {
            musicManager.AddCrash();
            previewGridAnimator.Play("Bounce");
        }
    }
    public void EndGame()
    {
        PrepareNewRound();
        entranceManager.UpdateAnimations();
        GameInProgress = false;
        GameOver = true;
        GameManager.instance.CheckForGameOver();
    }
    public Block GetNextSnakeHead()
    {
        FillPreviewBar();
        Block result = waitSlot.Block;
        while ((result.Tail != null && result.blockType != GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType))
        {
            BlockSlot neighbor = result.Slot.GetNeighbor(Directions.Direction.UP);
            if (neighbor) result = neighbor.Block;
            else
            {
                result = null;
                break;
            }
        }
        return result;
    }
    public List<Block> GetAllSpawnedSnakeHeads()
    {
        List<Block> result = new List<Block>();
        if(RoundInProgress) result.Add(snakeHead);
        BlockSlot slotToCheck = waitSlot;
        while(slotToCheck != null)
        {
            result.AddRange(slotToCheck.Blocks.Where(s => s.blockType == GameManager.instance.GameModeManager.GameMode.TypeBank.snakeHeadType));
            slotToCheck = slotToCheck.GetNeighbor(Directions.Direction.UP);
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
            arrowHandler.OnInputGiven();
            if (RoundInProgress)
            {
                if (!snakeHead.blockType.OverrideMove(snakeHead, snakeHead.Neighbor(direction), this)) Undoer.Save();
                SnakeHead.Move(direction, this);
                MidRound();
            }
            else
            {
                BlockSlot destination = enterSlot.GetNeighbor(direction);
                if (!destination || destination.playGrid != playGrid)
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
    private void Update()
    {
        previewGridAnimator.SetBool("Up", !RoundInProgress);
    }

}
