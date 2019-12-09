using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool GameInProgress;

    public PlayGrid playGrid;
    public PlayGrid previewGrid;
    public SnakeMaker snakeMaker;
    public BlockSlot waitSlot;
    public GameObject gameOverScreen;

    [HideInInspector]
    public int score;
    public Text ScoreText;

    [HideInInspector]
    public int HeightLimit;

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
    public static Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP: return Direction.DOWN;
            case Direction.DOWN: return Direction.UP;
            case Direction.LEFT: return Direction.RIGHT;
            case Direction.RIGHT: return Direction.LEFT;
            default: throw new System.Exception("Big OOPsy Doopsy that is not a real direction dumbass");
        }
    }

    private Direction mostRecentDirection;
    [SerializeField]
    private float autoMoveInterval;
    private float timeSinceLastAutoMove;

    [HideInInspector]
    private Block snakeHead;
    public Block SnakeHead
    {
        get { return snakeHead; }
        set { snakeHead = value; }
    }

    private List<Block> PreviewSnakes = new List<Block>();

    private void Awake()
    {
        if(!instance) instance = this;
        TouchManager.OnSwipe += MoveSnakeOnSwipe;
        TouchManager.OnHold += MoveSnakeOnHold;
        TouchManager.OnTap += MoveSnakeOnTap;
        StartGame();
    }

    public void IncreaseScore(int i)
    {
        UpdateScore(score + i);

    }
    public void UpdateScore(int i)
    {
        score = i;
        ScoreText.text = "Score: " + score.ToString();
    }

    private void MoveSnakeOnSwipe(TouchManager.TouchData Swipe)
    {
        if (snakeHead && Swipe.direction != GetOppositeDirection(mostRecentDirection))
        {
            MoveSnake(Swipe.direction);
        }
    }

    private void MoveSnakeOnHold(TouchManager.TouchData Hold)
    {
            if (timeSinceLastAutoMove > autoMoveInterval)
            {
                timeSinceLastAutoMove = 0;
                MoveSnake(mostRecentDirection);
            }
            else
            {
                timeSinceLastAutoMove += Time.deltaTime;
            }
    }
    private void MoveSnakeOnTap(TouchManager.TouchData Tap)
    {
        MoveSnake(mostRecentDirection);
    }

    public void MoveSnake(Direction direction)
    {

        if (GameInProgress && direction != Direction.UP | SnakeHead.Coords().y < HeightLimit)
        {
            mostRecentDirection = direction;
            SnakeHead.Eat(direction);
            FillPreviewBar();
            if(GameInProgress) LowerHeightLimit();
        }
    }

    public void OnBlockDeath(Block obj)
    {
        if (obj == snakeHead) OnSnakeDeath(obj);
    }
    public void OnSnakeDeath(Block obj)
    {
        playGrid.Fall();
        if (waitSlot.GetNeighbor(Direction.DOWN).Block != null)
        {
            EndGame();
        }
        else
        {
            ResetMoveRestrictions();
            ContinueGame();
        }
    }
    private void ShiftPreviewBar()
    {
        foreach(Block snake in PreviewSnakes)
        {
            snake.Move(Direction.LEFT);
        }
        MakeSnake();
    }

    private void FillPreviewBar()
    {
        MakeSnake();
        while (PreviewSnakes.Count > 0 && PreviewSnakes[0].Slot != waitSlot && PreviewSnakes[0].Neighbor(Direction.LEFT).Block == null)
        {
            ShiftPreviewBar();
        }
    }

    private void LowerHeightLimit()
    {
        int temp = SnakeHead.FindSnakeMaxY() + 2;
        if (temp < HeightLimit) HeightLimit = temp;
    }

    private void ResetMoveRestrictions()
    {
        HeightLimit = playGrid.YSize + 1;
        mostRecentDirection = Direction.DOWN;
    }

    private void MakeSnake()
    {
        if (snakeMaker.CheckIsClear()) PreviewSnakes.Add(snakeMaker.MakeSnake(25, .1f, this));
    }

    private void ActivateSnake(Block newSnakeHead)
    {
        snakeHead = newSnakeHead;
        snakeHead.ActivateSnake();
    }

    public void StartGame()
    {
        UpdateScore(0);
        gameOverScreen.SetActive(false);
        playGrid.ClearGrid();
        previewGrid.ClearGrid();
        PreviewSnakes.Clear();
        MakeSnake();
        FillPreviewBar();
        ResetMoveRestrictions();
        GameInProgress = true;
        ContinueGame();
    }
    private void ContinueGame()
    {
        snakeHead = waitSlot.Block;
        snakeHead.ActivateSnake();
        snakeHead.Move(Direction.DOWN);
        ResetMoveRestrictions();
        if (PreviewSnakes.Count > 0 && snakeHead == PreviewSnakes[0]) PreviewSnakes.RemoveAt(0);
        FillPreviewBar();
    }
    private void EndGame()
    {
        GameInProgress = false;
        gameOverScreen.SetActive(true);
    }
}
