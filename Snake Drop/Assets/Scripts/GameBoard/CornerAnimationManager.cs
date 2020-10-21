using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerAnimationManager : MonoBehaviour, IEntranceAnimationBehavior
{
    public Sprite OpenOpen;
    public Sprite ClosedClosed;
    public Sprite OpenClosed;
    public Sprite ClosedOpen;



    public SpriteRenderer spriteRenderer;
    private EntranceSlot ClockwiseNeighbor;
    private EntranceSlot CounterClockwiseNeighbor;
    private void Awake()
    {
        GetNeighbors(GetComponent<EntranceSlot>());
    }
    public void UpdateSprite()
    {
        bool left = CounterClockwiseNeighbor.Active;
        bool right = ClockwiseNeighbor.Active;

        if (left)
        {
            if (right) SetSprite(OpenOpen);
            else SetSprite(OpenClosed);
        }
        else
        {
            if (right) SetSprite(ClosedOpen);
            else SetSprite(ClosedClosed);
        }
    }
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void GetNeighbors(EntranceSlot slot)
    {
        BlockSlot.EdgeInfo info = slot.GetEdgeInfo();
        Directions.Direction ClockwiseDir = Directions.Direction.DOWN;
        Directions.Direction CounterClockwiseDir = Directions.Direction.DOWN;

        if (info.Top)
        {
            ClockwiseDir = Directions.Direction.RIGHT;
            CounterClockwiseDir = Directions.Direction.DOWN;
        }
        if (info.Right)
        {
            ClockwiseDir = Directions.Direction.DOWN;
            CounterClockwiseDir = Directions.Direction.LEFT;
        }
        if (info.Bottom)
        {
            ClockwiseDir = Directions.Direction.LEFT;
            CounterClockwiseDir = Directions.Direction.UP;
        }
        if (info.Left)
        {
            ClockwiseDir = Directions.Direction.UP;
            CounterClockwiseDir = Directions.Direction.RIGHT;
            if (info.Top)
            {
                ClockwiseDir = Directions.Direction.RIGHT;
                CounterClockwiseDir = Directions.Direction.DOWN;
            }
        }

        ClockwiseNeighbor = (EntranceSlot)slot.GetNeighbor(ClockwiseDir);
        CounterClockwiseNeighbor = (EntranceSlot)slot.GetNeighbor(CounterClockwiseDir);
    }
}

