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
        ClockwiseNeighbor = (EntranceSlot)slot.GetNeighbor(info.ClockwiseNeighborDirection());
        CounterClockwiseNeighbor = (EntranceSlot)slot.GetNeighbor(info.CounterClockwiseNeighborDirection());
    }
}

