using System;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables

    #region Block Info

    [HideInInspector]
    public BlockType blockType;
    [HideInInspector]
    public BlockColor blockColor;
    public BlockCollection BlockCollection;

    public BlockAnimationManager AnimationManager;
    public SpriteRenderer BlockSprite;
    public LineRenderer Highlight;

    private BlockSlot slot;
    public BlockSlot Slot
    {
        get { return slot; }
    }
    private PlayerManager owner;
    public PlayerManager Owner
    {
        get { return owner; }
        set { SetOwner(value); }
    }

    #endregion

    #region Coordinates

    public Vector2 Coords()
    {
        return new Vector2(Slot.x, Slot.y);
    }
    public int X
    {
        get { return Slot.x; }
    }
    public int Y
    {
        get { return Slot.y; }
    }
    public BlockSlot Neighbor(GameManager.Direction direction)
    {
        return Slot.GetNeighbor(direction);
    }

    #endregion

    #region Snake Data

    public bool isPartOfSnake()
    {
        return isPartOfSnake(this);
    }
    public static bool isPartOfSnake(Block obj)
    {
        return obj.blockType.isPartOfSnake;
    }
    public static bool isNotPartOfSnake(Block obj)
    {
        return !isPartOfSnake(obj);
    }

    private Block tail;
    public Block Tail
    {
        get { return tail; }
        set { SetTail(value); }
    }

    #endregion

    #endregion

    #region Methods to Update Block Status

    public void SetBlockType(BlockColor color, BlockType type)
    {
        blockType = type;
        blockColor = color;
        UpdateSprite();
    }
    private void AddAnimations()
    {
        foreach (BlockAnimator obj in blockType.EveryFrameAnimations)
        {
            AnimationManager.AddAnimation(new BlockAnimation(this, obj));
        }
    }
    private void UpdateSprite()
    {
            AnimationManager.AddAnimation(new BlockAnimation(this, blockType.SpriteAnimator));
    }
    private void Update()
    {
        AddAnimations();
    }


    //Tells the grid that it needs to check for fall movement and update
    private void SetGridDirty()
    {
        if (Slot && Slot.playGrid) Slot.playGrid.SetDirty();
    }

    #endregion

    #region Movement Methods

    //Base for all movement code, doesn't follow game rules.
    #region Raw Movement

    public void RawMove(GameManager.Direction neighbor)
    {
        RawMoveTo(Neighbor(neighbor));
    }
    public virtual void RawMoveTo(BlockSlot obj, BlockAnimator animation = null)
    {
        SetGridDirty();
        BlockSlot Old = Slot;
        if (Old) Old.OnUnassignment(this);
        if (obj)
        {
            if (animation == null) animation = blockType.defaultMoveAnimator;
            AnimationManager.AddAnimation(new BlockAnimation(this, animation, Old == null ? obj.transform : Old.transform, obj.transform));
            slot = obj;
            obj.OnAssignment(this);
            if (Tail != null) Tail.RawMoveTo(Old);
        }
        else
        {
            throw new System.Exception("No Slot to move to");
        }
        UpdateSprite();
    }
    public void RawBreak()
    {
        SetGridDirty();
        Slot.OnUnassignment(this);
        GameObject.Destroy(this.gameObject);
    }

    #endregion

    public void SpecialAction(PlayerManager player = null)
    {
        blockType.SpecialAction(this, player);
    }
    public void Move(GameManager.Direction neighbor, PlayerManager player = null)
    {
        MoveTo(Neighbor(neighbor), player);
    }
    public virtual void MoveTo(BlockSlot obj, PlayerManager player = null)
    {
        SetGridDirty();
        if (player && obj) obj.SetOwner(player);
        blockType.OnMove(this, obj, player);
    }
    public bool CanMoveToWithoutCrashing(BlockSlot obj, PlayerManager player = null)
    {
        return blockType.CanMoveToWithoutCrashing(this, obj, player);
    }

    public void Break(PlayerManager player = null)
    {
        SetGridDirty();
        blockType.OnBreak(this, player);
    }


    #endregion

    #region Snake Controls
    public void SetOwner(PlayerManager NewOwner)
    {
        if (Tail != null)
        {
            Tail.SetOwner(NewOwner);
        }
        owner = NewOwner;
    }
    public void ApplyRuleToSnake(Rule rule, PlayerManager player = null)
    {
        if(Tail != null)
        {
            Tail.ApplyRuleToSnake(rule);
        }
        rule.Invoke(this, player);
    }
    public void SetTail(GameManager.Direction neighbor)
    {
        SetTail(Neighbor(neighbor).Block);
    }
    public void SetTail(Block obj)
    {
        tail = obj;
    }

    public void Kill(PlayerManager player = null)
    {
        blockType.OnKill(this, player);
    }
    public void KillSnake(PlayerManager player = null)
    {

        if (Tail != null)
        {
/*            if (Tail.Slot.playGrid == Slot.playGrid) */
            Tail.KillSnake(player);
            SetTail(null);
        }
        Kill(player);
    }
    public GameManager.Direction TailDirection()
    {
        foreach (GameManager.Direction dir in Enum.GetValues(typeof(GameManager.Direction)))
        {
            if (Neighbor(dir) == tail) return dir;
        }
        return default;
    }

    public int FindSnakeMaxY()
    {
        int obj1 = (int)Coords().y;
        int obj2 = 0;
        if (tail != null)
        {
            obj2 = tail.FindSnakeMaxY();
        }
        int result = (obj1 > obj2) ? obj1 : obj2;
        return result;
    }

    #endregion
}
