using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAnimationManager : MonoBehaviour, IEntranceAnimationBehavior
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    private EntranceSlot slot;
    public PlayGrid grid;

    public Sprite OpenSingle;
    public Sprite OpenLeft;
    public Sprite OpenCenter;
    public Sprite OpenRight;

    public Sprite ClosedSingle;
    public Sprite ClosedLeft;
    public Sprite ClosedCenter;
    public Sprite ClosedRight;


    private void Awake()
    {
        slot = gameObject.GetComponent<EntranceSlot>();
    }
    void Update()
    {
        //UpdateSprite();
    }

    public virtual void UpdateSprite()
    {
        ////BlockSlot opening = slot.getOpeningToGrid(grid);
        //if (/*opening != null && (opening.Block == null || opening.Block.isPartOfSnake())*/slot.CheckIfEntranceHasOpeningToGrid(grid)) spriteRenderer.sprite = Open;
        //else spriteRenderer.sprite = Closed;

        bool leftOpen = CheckIfSlotOpen((EntranceSlot)slot.GetNeighbor(Directions.GetCounterClockwiseNeighborDirection(slot.GetEdgeInfo().direction())));
        bool rightOpen = CheckIfSlotOpen((EntranceSlot)slot.GetNeighbor(Directions.GetClockwiseNeighborDirection(slot.GetEdgeInfo().direction())));
        bool thisOpen = CheckIfSlotOpen(slot);

        if (!leftOpen &&  thisOpen && !rightOpen) spriteRenderer.sprite = OpenSingle;
        if (!leftOpen &&  thisOpen &&  rightOpen) spriteRenderer.sprite = OpenLeft;
        if ( leftOpen &&  thisOpen &&  rightOpen) spriteRenderer.sprite = OpenCenter;
        if ( leftOpen &&  thisOpen && !rightOpen) spriteRenderer.sprite = OpenRight;

        if ( leftOpen && !thisOpen &&  rightOpen) spriteRenderer.sprite = ClosedSingle;
        if ( leftOpen && !thisOpen && !rightOpen) spriteRenderer.sprite = ClosedLeft;
        if (!leftOpen && !thisOpen && !rightOpen) spriteRenderer.sprite = ClosedCenter;
        if (!leftOpen && !thisOpen &&  rightOpen) spriteRenderer.sprite = ClosedRight;

    }

    public bool CheckIfSlotOpen(EntranceSlot slot)
    {
        return slot != null && slot.Active;
    }
}
