using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAnimationManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
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

    public void UpdateSprite()
    {
        ////BlockSlot opening = slot.getOpeningToGrid(grid);
        //if (/*opening != null && (opening.Block == null || opening.Block.isPartOfSnake())*/slot.CheckIfEntranceHasOpeningToGrid(grid)) spriteRenderer.sprite = Open;
        //else spriteRenderer.sprite = Closed;

        EntranceSlot Left = (EntranceSlot)slot.GetNeighbor(GameManager.GetCounterClockwiseNeighborDirection(slot.GetEdge(slot)));
        bool leftOpen = Left != null && Left.CheckIfEntranceHasOpeningToGrid(grid);
        EntranceSlot Right = (EntranceSlot)slot.GetNeighbor(GameManager.GetClockwiseNeighborDirection(slot.GetEdge(slot)));
        bool rightOpen = Right != null && Right.CheckIfEntranceHasOpeningToGrid(grid);
        bool thisOpen = slot.CheckIfEntranceHasOpeningToGrid(grid);

        if (!leftOpen &&  thisOpen && !rightOpen) spriteRenderer.sprite = OpenSingle;
        if (!leftOpen &&  thisOpen &&  rightOpen) spriteRenderer.sprite = OpenLeft;
        if ( leftOpen &&  thisOpen &&  rightOpen) spriteRenderer.sprite = OpenCenter;
        if ( leftOpen &&  thisOpen && !rightOpen) spriteRenderer.sprite = OpenRight;

        if ( leftOpen && !thisOpen &&  rightOpen) spriteRenderer.sprite = ClosedSingle;
        if ( leftOpen && !thisOpen && !rightOpen) spriteRenderer.sprite = ClosedLeft;
        if (!leftOpen && !thisOpen && !rightOpen) spriteRenderer.sprite = ClosedCenter;
        if (!leftOpen && !thisOpen &&  rightOpen) spriteRenderer.sprite = ClosedRight;

    }
}
